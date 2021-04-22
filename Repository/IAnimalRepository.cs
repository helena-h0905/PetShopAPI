using PetShopAPI.Models;
using PetShopAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopAPI.Repository
{
    public interface IAnimalRepository
    {
        Task<List<AnimalViewModel>> GetAnimals();

        Task<AnimalViewModel> GetAnimal(int? animalId);

        Task<int> AddAnimal(Animal animal);

        Task<int> DeleteAnimal(int? animalId);

        Task UpdateAnimal(Animal animal);
    }
}