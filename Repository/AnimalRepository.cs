using PetShopAPI.Models;
using PetShopAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopAPI.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        PetShopDBContext db;
        public AnimalRepository(PetShopDBContext _db)
        {
            db = _db;
        }

        public async Task<List<AnimalViewModel>> GetAnimals()
        {
            if (db != null)
            {
                return await (from a in db.Animals
                              select new AnimalViewModel
                              {
                                  AnimalId = a.AnimalId,
                                  Name = a.Name
                              }).ToListAsync();
            }

            return null;
        }

        public async Task<AnimalViewModel> GetAnimal(int? animalId)
        {
            if (db != null)
            {
                return await (from a in db.Animals
                              where a.AnimalId == animalId
                              select new AnimalViewModel
                              {
                                  AnimalId = a.AnimalId,
                                  Name = a.Name
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> AddAnimal(Animal animal)
        {
            if (db != null)
            {
                await db.Animals.AddAsync(animal);
                await db.SaveChangesAsync();

                return animal.AnimalId;
            }

            return 0;
        }

        public async Task<int> DeleteAnimal(int? animalId)
        {
            int result = 0;

            if (db != null)
            {
                var animal = await db.Animals.FirstOrDefaultAsync(x => x.AnimalId == animalId);

                if (animal != null)
                {
                    db.Animals.Remove(animal);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdateAnimal(Animal animal)
        {
            if (db != null)
            {
                db.Animals.Update(animal);
                await db.SaveChangesAsync();
            }
        }
    }
}