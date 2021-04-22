using Microsoft.AspNetCore.Mvc;
using PetShopAPI.Models;
using PetShopAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopAPI.Repository
{
    public interface IFoodRepository
    {
        Task<List<FoodViewModel>> GetAllFood();

        Task<FoodViewModel> GetFood(int? foodId);

        Task<int> AddFood(Food food);

        Task<int> DeleteFood(int? foodId);

        Task UpdateFood(Food food);

        Task<List<FoodKindViewModel>> GetFoodKinds();

        Task<FoodKindViewModel> GetFoodKind(int? foodKindId);

        Task<int> AddFoodKind(FoodKind foodKind);

        Task<int> DeleteFoodKind(int? foodKindId);

        Task UpdateFoodKind(FoodKind foodKind);

        Task<ActionResult<IEnumerable<Food>>> GetFoodForAnimal(int? animalId);

    }
}