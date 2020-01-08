using System.Collections.Generic;
using Test.ECommerce.Data.Models;

namespace Test.ECommerce.Service.CategoryService
{
    public interface ICategoryService
    {
        List<Category> GetAllCategory();

        Category GetByCategoryCode(string categoryCode);

        void AddCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteCategory(string categoryCode);
    }
}