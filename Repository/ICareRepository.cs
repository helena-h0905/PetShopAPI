using PetShopAPI.Models;
using PetShopAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShopAPI.Repository
{
    public interface ICareRepository
    {
        Task<List<CareViewModel>> GetCareSupplies();

        Task<CareViewModel> GetCareSupply(int? supplyId);

        Task<int> AddCareSupply(CareSupply supply);

        Task<int> DeleteCareSupply(int? supplyId);

        Task UpdateCareSupply(CareSupply supplyId);

        Task<List<CareCategoryViewModel>> GetCategories();

        Task<CareCategoryViewModel> GetCategory(int? categoryId);

        Task<int> AddCategory(CareCategory category);

        Task<int> DeleteCategory(int? categoryId);

        Task UpdateCategory(CareCategory categoryId);
    }
}