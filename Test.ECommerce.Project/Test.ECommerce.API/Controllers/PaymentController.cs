using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Test.ECommerce.API.Helpers;
using Test.ECommerce.Common.Enum;
using Test.ECommerce.Common.Enum.Payment;
using Test.ECommerce.Common.NLog;
using Test.ECommerce.Data.Contract;
using Test.ECommerce.Data.Templates;
using Test.ECommerce.Service.OrderService;

namespace Test.ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly IOrderService _orderService;

        public PaymentController(
            IOrderService orderService,
            IMapper mapper,
            ILog logger)
        {
            _mapper = mapper;
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        [Route("CheckOut")]
        [ProducesResponseType(typeof(ServiceResponse<PaymentServiceResponse, OrderContract>), 200)]
        public IActionResult CheckOut(OrderRequest orderRequest)
        {
            try
            {
                if (orderRequest != null)
                {
                    var result = _orderService.CreateOrder(orderRequest);

                    if (result.Status != PaymentServiceResponse.Success)
                    {
                        return BadRequest(new ServiceResponse<PaymentServiceResponse, OrderItem>(result.Status));
                    }

                    return Ok(new ServiceResponse<PaymentServiceResponse, OrderItem>(PaymentServiceResponse.Success));
                }

                return Ok(new ServiceResponse<PaymentServiceResponse, OrderItem>(PaymentServiceResponse.Exception));
            }
            catch (Exception ex)
            {
                _logger.Error($"CheckOut :{ex}");
                return BadRequest(new ServiceResponse<PaymentServiceResponse, OrderContract>(PaymentServiceResponse.Exception));
            }
        }
    }
}