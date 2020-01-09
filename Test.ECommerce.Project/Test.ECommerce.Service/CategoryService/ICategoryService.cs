using System.Collections.Generic;
using Test.ECommerce.Data.Contract;

namespace Test.ECommerce.Service.CategoryService
{
    public interface ICategoryService
    {
        List<CategoryContract> GetAllCategory();

        void AddCategory(CategoryContract categoryContract);

        void UpdateCategory(CategoryContract categoryContract);

        void DeleteCategory(string categoryCode);
    }
}