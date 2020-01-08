using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Test.ECommerce.API.Helpers;
using Test.ECommerce.Common.Config;
using Test.ECommerce.Common.Enum;
using Test.ECommerce.Common.Enum.Basket;
using Test.ECommerce.Common.NLog;
using Test.ECommerce.Data.Contract;
using Test.ECommerce.Data.Models;
using Test.ECommerce.Data.Templates;
using Test.ECommerce.Service.BasketService;
using Test.ECommerce.Service.ProductService;

namespace Test.ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly IOptions<SiteConfig> _config;
        private readonly IMemoryCache _memoryCache;

        public BasketController(
            IBasketService basketService,
            IProductService productService,
            IMapper mapper,
            ILog logger,
            IOptions<SiteConfig> config,
            IMemoryCache memoryCache
            )
        {
            _basketService = basketService;
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
            _config = config;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("GetSession")]
        public IActionResult GetSession()
        {
            var cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");

            return Ok(cart);
        }

        [HttpPost]
        [Route("AddProductInBasket")]
        [ProducesResponseType(typeof(ServiceResponse<BasketServiceResponse, BasketContract>), 200)]
        public IActionResult AddProductInBasket([FromBody] string productCode, int? customerId)
        {
            try
            {
                var product = _productService.GetByProductCode(productCode);
                List<OrderItem> cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");

                if (cart == null)
                {
                    List<OrderItem> orderItem = new List<OrderItem>();
                    orderItem.Add(new OrderItem { Product = product, Quantity = 1 });
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", orderItem);
                }
                else
                {
                    int index = isExist(productCode);
                    if (index != -1)
                    {
                        if (cart[index].Quantity >= product.StockCount )
                            return Ok(new ServiceResponse<BasketServiceResponse, BasketContract>(BasketServiceResponse.MaxStock));

                        cart[index].Quantity++;
                    }
                    else
                    {
                        cart.Add(new OrderItem { Product = product, Quantity = 1 });
                    }
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                }

                return Ok(new ServiceResponse<BasketServiceResponse, BasketContract>(BasketServiceResponse.Success));
            }
            catch (Exception ex)
            {
                _logger.Error($"DeleteProductInBasket :{ex} - {new Dictionary<string, string> { { "productCode", productCode }, { "customerId", customerId.ToString() } }}");
                return BadRequest(new ServiceResponse<BasketServiceResponse, BasketContract>(BasketServiceResponse.Exception));
            }
        }

        [HttpPost]
        [Route("UpdateProductInBasket")]
        [ProducesResponseType(typeof(ServiceResponse<BasketServiceResponse, BasketContract>), 200)]
        public IActionResult UpdateProductInBasket([FromBody] string productCode, int? customerId, int quantity)
        {
            try
            {
                if (SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart") != null)
                {
                    var getProduct = _productService.GetByProductCode(productCode);
                    List<OrderItem> cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
                    int index = isExist(productCode);
                    if (index != -1)
                    {
                        if (cart[index].Quantity >= getProduct.StockCount)
                            return Ok(new ServiceResponse<BasketServiceResponse, BasketContract>(BasketServiceResponse.MaxStock));

                        cart[index].Quantity = quantity;
                    }
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

                    return Ok(new ServiceResponse<BasketServiceResponse, BasketContract>(BasketServiceResponse.Success));
                }

                return Ok(new ServiceResponse<BasketServiceResponse, BasketContract>(BasketServiceResponse.NotFound));
            }
            catch (Exception ex)
            {
                _logger.Error($"DeleteProductInBasket :{ex} - {new Dictionary<string, string> { { "productCode", productCode }, { "customerId", customerId.ToString() } }}");
                return BadRequest(new ServiceResponse<BasketServiceResponse, BasketContract>(BasketServiceResponse.Exception));
            }
        }

        [HttpPost]
        [Route("DeleteProductInBasket")]
        [ProducesResponseType(typeof(ServiceResponse<BasketServiceResponse, BasketContract>), 200)]
        public IActionResult DeleteProductInBasket(string productCode)
        {
            try
            {
                List<OrderItem> cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
                int index = isExist(productCode);
                cart.RemoveAt(index);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

                return Ok(new ServiceResponse<BasketServiceResponse, BasketContract>(BasketServiceResponse.Success));
            }
            catch (Exception ex)
            {
                _logger.Error($"DeleteProductInBasket :{ex} - {new Dictionary<string, string> { { "productCode", productCode } }}");
                return BadRequest(new ServiceResponse<BasketServiceResponse, ProductContract>(BasketServiceResponse.Exception));
            }
        }

        [HttpPost]
        [Route("DeleteAllBasket")]
        [ProducesResponseType(typeof(ServiceResponse<BasketServiceResponse, BasketContract>), 200)]
        public IActionResult DeleteAllBasket()
        {
            try
            {
                List<OrderItem> cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
                cart.Clear();

                return Ok(new ServiceResponse<BasketServiceResponse, BasketContract>(BasketServiceResponse.Success));
            }
            catch (Exception ex)
            {
                _logger.Error($"DeleteAllBasket :{ex}");
                return BadRequest(new ServiceResponse<BasketServiceResponse, BasketContract>(BasketServiceResponse.Exception));
            }
        }

        private int isExist(string productCode)
        {
            List<OrderItem> cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductCode.Equals(productCode))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}