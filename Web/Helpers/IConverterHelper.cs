using RowingApp.Common.Responses;
using RowingApp.Web.Data.Entities;
using RowingApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RowingApp.Web.Helpers
{
    public interface IConverterHelper
    {
        CategoryEntity ToCategoryEntity(CategoryViewModel model, string path, bool isNew);

        CategoryViewModel ToCategoryViewModel(CategoryEntity categoryEntity);

        Task<ProductEntity> ToProductAsync(ProductViewModel model, bool isNew);

        ProductViewModel ToProductViewModel(ProductEntity product);

        CountryEntity ToCountryEntity(CountryViewModel model, string path, bool isNew);

        CountryViewModel ToCountryViewModel(CountryEntity countryEntity);

        TeamEntity ToTeamEntity(TeamViewModel model, string path,bool isNew);

        TeamViewModel ToTeamViewModel(TeamEntity product);

        ProductResponse ToProductResponse(ProductEntity productEntity);

        List<ProductResponse> ToProductResponse(List<ProductEntity> productEntities);
    }
}