using PetShopAPI.Models;
using PetShopAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopAPI.Repository
{
    public class CareRepository : ICareRepository
    {
        PetShopDBContext db;
        public CareRepository(PetShopDBContext _db)
        {
            db = _db;
        }

        public async Task<List<CareViewModel>> GetCareSupplies()
        {
            if (db != null)
            {
                return await (from cs in db.CareSupplies
                              join cc in db.CareCategories on cs.CategoryId equals cc.CategoryId
                              join a in db.Animals on cs.AnimalId equals a.AnimalId
                              select new CareViewModel
                              {
                                  CareId =cs.CareSupplyId,
                                  Name = cs.Name,
                                  Description = cs.Description,
                                  AnimalId = cs.AnimalId,
                                  AnimalName = a.Name,
                                  CategoryId = cs.CategoryId,
                                  CategoryName = cc.Name,
                                  Price = cs.Price
                              }).ToListAsync();
            }

            return null;
        }

        public async Task<CareViewModel> GetCareSupply(int? supplyId)
        {
            if (db != null)
            {
                return await (from cs in db.CareSupplies
                              join cc in db.CareCategories on cs.CategoryId equals cc.CategoryId
                              join a in db.Animals on cs.AnimalId equals a.AnimalId
                              where cs.CareSupplyId == supplyId
                              select new CareViewModel
                              {
                                  CareId = cs.CareSupplyId,
                                  Name = cs.Name,
                                  Description = cs.Description,
                                  AnimalId = cs.AnimalId,
                                  AnimalName = a.Name,
                                  CategoryId = cs.CategoryId,
                                  CategoryName = cc.Name,
                                  Price = cs.Price
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> AddCareSupply(CareSupply supply)
        {
            if (db != null)
            {
                await db.CareSupplies.AddAsync(supply);
                await db.SaveChangesAsync();

                return supply.CareSupplyId;
            }

            return 0;
        }

        public async Task<int> DeleteCareSupply(int? supplyId)
        {
            int result = 0;

            if (db != null)
            {
                var supply = await db.CareSupplies.FirstOrDefaultAsync(x => x.CareSupplyId == supplyId);

                if (supply != null)
                {
                    db.CareSupplies.Remove(supply);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdateCareSupply(CareSupply supply)
        {
            if (db != null)
            {
                db.CareSupplies.Update(supply);
                await db.SaveChangesAsync();
            }
        }


        public async Task<List<CareCategoryViewModel>> GetCategories()
        {
            if (db != null)
            {
                return await (from cc in db.CareCategories
                              select new CareCategoryViewModel
                              {
                                  CategoryId = cc.CategoryId,
                                  Name = cc.Name
                              }).ToListAsync();
            }

            return null;
        }

        public async Task<CareCategoryViewModel> GetCategory(int? categoryId)
        {
            if (db != null)
            {
                return await (from cc in db.CareCategories
                              where cc.CategoryId == categoryId
                              select new CareCategoryViewModel
                              {
                                  CategoryId = cc.CategoryId,
                                  Name = cc.Name
                              }).FirstOrDefaultAsync();
            }

            return null;
        }


        public async Task<int> AddCategory(CareCategory category)
        {
            if (db != null)
            {
                await db.CareCategories.AddAsync(category);
                await db.SaveChangesAsync();

                return category.CategoryId;
            }

            return 0;
        }

        public async Task<int> DeleteCategory(int? categoryId)
        {
            int result = 0;

            if (db != null)
            {
                var category = await db.CareCategories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);

                if (category != null)
                {
                    db.CareCategories.Remove(category);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }
        public async Task UpdateCategory(CareCategory category)
        {
            if (db != null)
            {
                db.CareCategories.Update(category);
                await db.SaveChangesAsync();
            }
        }
    }
}