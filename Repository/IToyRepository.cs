using PetShopAPI.Models;
using PetShopAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopAPI.Repository
{
    public interface IToyRepository
    {
        Task<List<ToyViewModel>> GetToys();

        Task<ToyViewModel> GetToy(int? toyId);

        Task<int> AddToy(Toy toy);

        Task<int> DeleteToy(int? toyId);

        Task UpdateToy(Toy toy);
    }
}