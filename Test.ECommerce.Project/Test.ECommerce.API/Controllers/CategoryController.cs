using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Test.ECommerce.Common.Config;
using Test.ECommerce.Common.Enum;
using Test.ECommerce.Common.Enum.Category;
using Test.ECommerce.Common.NLog;
using Test.ECommerce.Data.Contract;
using Test.ECommerce.Data.Models;
using Test.ECommerce.Service.CategoryService;

namespace Test.ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly IOptions<SiteConfig> _config;
        private readonly IMemoryCache _memoryCache;

        public CategoryController(
            ICategoryService categoryService,
            IMapper mapper,
            ILog logger,
            IOptions<SiteConfig> config,
            IMemoryCache memoryCache
            )
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
            _config = config;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("GetAllCategory")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryContract>))]
        public IActionResult GetAll()
        {
            try
            {
                var result = _mapper.Map<List<CategoryContract>>(_categoryService.GetAllCategory());
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error($"GetAllCategory :{ex}");
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddCategory")]
        [ProducesResponseType(typeof(ServiceResponse<CategoryServiceResponse, CategoryContract>), 200)]
        public IActionResult Add([FromBody] CategoryContract category)
        {
            try
            {
                var categoryData = _mapper.Map<Category>(category);

                _categoryService.AddCategory(categoryData);

                return Ok(new ServiceResponse<CategoryServiceResponse, CategoryContract>(CategoryServiceResponse.Success));
            }
            catch (Exception ex)
            {
                _logger.Error($"AddCategory :{ex} - {new Dictionary<string, CategoryContract> { { "category", category } }}");
                return BadRequest(new ServiceResponse<CategoryServiceResponse, CategoryContract>(CategoryServiceResponse.Exception));
            }
        }


        [HttpPost]
        [Route("UpdateCategory")]
        [ProducesResponseType(typeof(ServiceResponse<CategoryServiceResponse, CategoryContract>), 200)]
        public IActionResult Update([FromBody] CategoryContract category)
        {
            try
            {
                if (category != null)
                {
                    var categoryData = _mapper.Map<Category>(category);
                    _categoryService.UpdateCategory(categoryData);

                    return Ok(new ServiceResponse<CategoryServiceResponse, CategoryContract>(CategoryServiceResponse.Success));
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Error($"UpdateCategory :{ex} - {new Dictionary<string, CategoryContract> { { "category", category } }} ");
                return BadRequest(new ServiceResponse<CategoryServiceResponse, CategoryContract>(CategoryServiceResponse.Exception));
            }
        }

        [HttpPost]
        [Route("DeleteCategory")]
        [ProducesResponseType(typeof(ServiceResponse<CategoryServiceResponse, CategoryContract>), 200)]
        public IActionResult Delete([FromBody] string categoryCode)
        {
            try
            {
                if (!string.IsNullOrEmpty(categoryCode))
                {
                    _categoryService.DeleteCategory(categoryCode);

                    return Ok(new ServiceResponse<CategoryServiceResponse, CategoryContract>(CategoryServiceResponse.Success));
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Error($"DeleteCategory :{ex} - {new Dictionary<string, string> { { "categoryCode", categoryCode } }}");
                return BadRequest(new ServiceResponse<CategoryServiceResponse, CategoryContract>(CategoryServiceResponse.Exception));
            }
        }
    }
}