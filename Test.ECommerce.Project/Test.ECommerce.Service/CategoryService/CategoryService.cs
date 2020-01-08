using System.Collections.Generic;
using System.Linq;
using Test.ECommerce.Common.NLog;
using Test.ECommerce.Data.Models;
using Test.ECommerce.Data.Repositories.CategoryRepository;

namespace Test.ECommerce.Service.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private readonly ILog _logger;

        public CategoryService(ICategoryRepository categoryRepository, ILog logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public List<Category> GetAllCategory()
        {
            var result = _categoryRepository.GetAll().ToList();

            return result;
        }

        public Category GetByCategoryCode(string categoryCode)
        {
            if (!string.IsNullOrEmpty(categoryCode))
                return _categoryRepository.GetById(categoryCode);

            return null;
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.Add(category);
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.Update(category);
        }

        public void DeleteCategory(string categoryCode)
        {
            _categoryRepository.Delete(categoryCode);
        }
    }
}