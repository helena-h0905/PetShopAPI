using PetShopAPI.Models;
using PetShopAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PetShopAPI.Repository
{
    public class FoodRepository : IFoodRepository
    {
        PetShopDBContext db;
        public FoodRepository(PetShopDBContext _db)
        {
            db = _db;
        }

        public async Task<List<FoodViewModel>> GetAllFood()
        {
            if (db != null)
            {
                return await (from f in db.Foods join fk in db.FoodKinds on f.FoodKindId equals fk.FoodKindId join a in db.Animals on  f.AnimalId equals a.AnimalId
                              select new FoodViewModel
                              {
                                  FoodId = f.FoodId,
                                  Name = f.Name,
                                  Description = f.Description,
                                  AnimalId = f.AnimalId,
                                  AnimalName = a.Name,
                                  NumberOfPackagesInStorage = f.NumberOfPackagesInStorage,
                                  PackageWeightInKg = f.PackageWeightInKg,
                                  EarliestExpirationDate = f.EarliestExpirationDate,
                                  FoodKindId = f.FoodKindId,
                                  FoodKindName = fk.Name,
                                  Price = f.Price
                              }).ToListAsync();
            }

            return null;
        }

        public async Task<FoodViewModel> GetFood(int? foodId)
        {
            if (db != null)
            {
                return await (from f in db.Foods
                              join fk in db.FoodKinds on f.FoodKindId equals fk.FoodKindId
                              join a in db.Animals on f.AnimalId equals a.AnimalId
                              where f.FoodId == foodId
                              select new FoodViewModel
                              {
                                  FoodId = f.FoodId,
                                  Name = f.Name,
                                  Description = f.Description,
                                  AnimalId = f.AnimalId,
                                  AnimalName = a.Name,
                                  NumberOfPackagesInStorage = f.NumberOfPackagesInStorage,
                                  PackageWeightInKg = f.PackageWeightInKg,
                                  EarliestExpirationDate = f.EarliestExpirationDate,
                                  FoodKindId = f.FoodKindId,
                                  FoodKindName = fk.Name,
                                  Price = f.Price
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> AddFood(Food food)
        {
            if (db != null)
            {
                await db.Foods.AddAsync(food);
                await db.SaveChangesAsync();

                return food.FoodId;
            }

            return 0;
        }

        public async Task<int> DeleteFood(int? foodId)
        {
            int result = 0;

            if (db != null)
            {
                var food = await db.Foods.FirstOrDefaultAsync(x => x.FoodId == foodId);

                if (food != null)
                {
                    db.Foods.Remove(food);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdateFood(Food food)
        {
            if (db != null)
            {
                db.Foods.Update(food);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<FoodKindViewModel>> GetFoodKinds()
        {
            if (db != null)
            {
                return await (from fk in db.FoodKinds
                              select new FoodKindViewModel
                              {
                                  FoodKindId = fk.FoodKindId,
                                  Name = fk.Name
                              }).ToListAsync();
            }

            return null;
        }

        public async Task<FoodKindViewModel> GetFoodKind(int? kindId)
        {
            if (db != null)
            {
                return await (from fk in db.FoodKinds
                              where fk.FoodKindId == kindId
                              select new FoodKindViewModel
                              {
                                  FoodKindId = fk.FoodKindId,
                                  Name = fk.Name
                              }).FirstOrDefaultAsync();
            }

            return null;
        }


        public async Task<ActionResult<IEnumerable<Food>>> GetFoodForAnimal(int? animalId)
        {
            string StoredProc = "exec GetFoodForAnimal " +
                    "@AnimalId = " + animalId;

            var list = db.Foods.FromSqlRaw(StoredProc).ToListAsync();

            return await list;

        }

        public async Task<int> AddFoodKind(FoodKind foodKind)
        {
            if (db != null)
            {
                await db.FoodKinds.AddAsync(foodKind);
                await db.SaveChangesAsync();

                return foodKind.FoodKindId;
            }

            return 0;
        }

        public async Task<int> DeleteFoodKind(int? foodKindId)
        {
            int result = 0;

            if (db != null)
            {
                var foodKind = await db.FoodKinds.FirstOrDefaultAsync(x => x.FoodKindId == foodKindId);

                if (foodKind != null)
                {
                    db.FoodKinds.Remove(foodKind);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }
        public async Task UpdateFoodKind(FoodKind foodKind)
        {
            if (db != null)
            {
                db.FoodKinds.Update(foodKind);
                await db.SaveChangesAsync();
            }
        }
    }
}
