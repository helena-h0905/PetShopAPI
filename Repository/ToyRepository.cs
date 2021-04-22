using PetShopAPI.Models;
using PetShopAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopAPI.Repository
{
    public class ToyRepository : IToyRepository
    {
        PetShopDBContext db;
        public ToyRepository(PetShopDBContext _db)
        {
            db = _db;
        }

        public async Task<List<ToyViewModel>> GetToys()
        {
            if (db != null)
            {
                return await (from t in db.Toys join a in db.Animals on t.AnimalId equals a.AnimalId
                              select new ToyViewModel
                              {
                                  ToyId = t.ToyId,
                                  Name = t.Name,
                                  Description = t.Description,
                                  AnimalId = t.AnimalId,
                                  AnimalName = a.Name,
                                  Price = t.Price
                              }).ToListAsync();
            }

            return null;
        }

        public async Task<ToyViewModel> GetToy(int? toyId)
        {
            if (db != null)
            {
                return await (from t in db.Toys
                              join a in db.Animals on t.AnimalId equals a.AnimalId
                              where t.ToyId == toyId
                              select new ToyViewModel
                              {
                                  ToyId = t.ToyId,
                                  Name = t.Name,
                                  Description = t.Description,
                                  AnimalId = t.AnimalId,
                                  AnimalName = a.Name,
                                  Price = t.Price
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> AddToy(Toy toy)
        {
            if (db != null)
            {
                await db.Toys.AddAsync(toy);
                await db.SaveChangesAsync();

                return toy.ToyId;
            }

            return 0;
        }

        public async Task<int> DeleteToy(int? toyId)
        {
            int result = 0;

            if (db != null)
            {
                var toy = await db.Toys.FirstOrDefaultAsync(x => x.ToyId == toyId);

                if (toy != null)
                {
                    db.Toys.Remove(toy);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdateToy(Toy toy)
        {
            if (db != null)
            {
                db.Toys.Update(toy);
                await db.SaveChangesAsync();
            }
        }
    }
}