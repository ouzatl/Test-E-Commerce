using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Test.ECommerce.Common.Config;
using Test.ECommerce.Common.Constants;
using Test.ECommerce.Common.Enum;
using Test.ECommerce.Common.Enum.Product;
using Test.ECommerce.Common.NLog;
using Test.ECommerce.Data.Contract;
using Test.ECommerce.Data.Models;
using Test.ECommerce.Service.ProductService;

namespace Test.ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly IOptions<SiteConfig> _config;
        private readonly IMemoryCache _memoryCache;

        public ProductController(
            IProductService productService,
            IMapper mapper,
            ILog logger,
            IOptions<SiteConfig> config,
            IMemoryCache memoryCache
            )
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
            _config = config;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("GetAllProduct")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductContract>))]
        public IActionResult GetAll()
        {
            try
            {
                var cacheKey = ProductConstants.GETALLPRODUCT;

                if (!_memoryCache.TryGetValue(cacheKey, out List<ProductContract> productContract))
                {

                    var result = _mapper.Map<List<ProductContract>>(_productService.GetAllProducts());
                    if (result == null)
                        return NotFound();

                    var cacheExpirationOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                        Priority = CacheItemPriority.Normal
                    };
                    _memoryCache.Set(cacheKey, productContract, cacheExpirationOptions);
                }

                return Ok(productContract);
            }
            catch (Exception ex)
            {
                _logger.Error($"GetAllProduct :{ex}");
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetByProductCode")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductContract>))]
        public IActionResult GetByProductCode(string productCode)
        {
            try
            {
                var result = _mapper.Map<ProductContract>(_productService.GetByProductCode(productCode));
                if (result != null)
                    return Ok(result);

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Error($"GetByProductCode :{ex} - {new Dictionary<string, string> { { "productCode", productCode } }}");
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetProductByCategoryCode")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductContract>))]
        public IActionResult GetByCategoryCode(string categoryCode)
        {
            try
            {
                var result = _mapper.Map<ProductContract>(_productService.GetProductListByCategoryCode(categoryCode));

                if (result != null)
                    return Ok(result);

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.Error($"GetProductByCategoryCode :{ex} - {new Dictionary<string, string> { { "categoryCode", categoryCode } }}");
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddProduct")]
        [ProducesResponseType(typeof(ServiceResponse<ProductServiceResponse, ProductContract>), 200)]
        public IActionResult Add([FromBody] ProductContract product)
        {
            try
            {
                //var validate = ProductValidate(product);
                var productData = _mapper.Map<Product>(product);

                _productService.AddProduct(productData);

                return Ok(new ServiceResponse<ProductServiceResponse, ProductContract>(ProductServiceResponse.Success));
            }
            catch (Exception ex)
            {
                _logger.Error($"AddProduct :{ex} - {new Dictionary<string, ProductContract> { { "product", product } }}");
                return BadRequest(new ServiceResponse<ProductServiceResponse, ProductContract>(ProductServiceResponse.Exception));
            }
        }

        //private ProductServiceResponse ProductValidate(ProductContract product)
        //{
        //    if (string.IsNullOrEmpty(product.ProductCode))
        //    {
        //        return ProductServiceResponse.ProductCodeNull;
        //    }

        //    if (string.IsNullOrEmpty(product.CategoryCode))
        //    {
        //        return ProductServiceResponse.CategoryCodeNull;
        //    }

        //    if (string.IsNullOrEmpty(product.ProductName))
        //    {
        //        return ProductServiceResponse.ProductNameNull;
        //    }

        //    if (string.IsNullOrEmpty(product.CategoryCode))
        //    {
        //        return ProductServiceResponse.CategoryCodeNull;
        //    }
        ////}

        [HttpPost]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(ServiceResponse<ProductServiceResponse, ProductContract>), 200)]
        public IActionResult Update([FromBody] ProductContract product)
        {
            try
            {
                if (product != null)
                {
                    var productData = _mapper.Map<Product>(product);
                    _productService.UpdateProduct(productData);

                    return Ok(new ServiceResponse<ProductServiceResponse, ProductContract>(ProductServiceResponse.Success));
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Error($"UpdateProduct :{ex} - {new Dictionary<string, ProductContract> { { "product", product } }} ");
                return BadRequest(new ServiceResponse<ProductServiceResponse, ProductContract>(ProductServiceResponse.Exception));
            }
        }

        [HttpPost]
        [Route("DeleteProduct")]
        [ProducesResponseType(typeof(ServiceResponse<ProductServiceResponse, ProductContract>), 200)]
        public IActionResult Delete([FromBody] string productCode)
        {
            try
            {
                if (!string.IsNullOrEmpty(productCode))
                {
                    _productService.DeleteProduct(productCode);

                    return Ok(new ServiceResponse<ProductServiceResponse, ProductContract>(ProductServiceResponse.Success));
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.Error($"DeleteProduct :{ex} - {new Dictionary<string, string> { { "productCode", productCode } }}");
                return BadRequest(new ServiceResponse<ProductServiceResponse, ProductContract>(ProductServiceResponse.Exception));
            }
        }
    }
}