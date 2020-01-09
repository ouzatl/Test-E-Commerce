using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Test.ECommerce.Common.NLog;
using Test.ECommerce.Data.Contract;
using Test.ECommerce.Data.Models;
using Test.ECommerce.Data.Repositories.CategoryRepository;

namespace Test.ECommerce.Service.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private readonly ILog _logger;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, ILog logger, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public List<CategoryContract> GetAllCategory()
        {
            var result = _mapper.Map<List<CategoryContract>>(_categoryRepository.GetAll().ToList());

            return result;
        }

        public void AddCategory(CategoryContract categoryContract)
        {
            var category = _mapper.Map<Category>(categoryContract);
            _categoryRepository.Add(category);
        }

        public void UpdateCategory(CategoryContract categoryContract)
        {
            var category = _mapper.Map<Category>(categoryContract);
            _categoryRepository.Update(category);
        }

        public void DeleteCategory(string categoryCode)
        {
            _categoryRepository.Delete(categoryCode);
        }
    }
}