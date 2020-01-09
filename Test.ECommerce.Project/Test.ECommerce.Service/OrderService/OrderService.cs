using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Test.ECommerce.Common.Enum;
using Test.ECommerce.Common.Enum.Payment;
using Test.ECommerce.Common.NLog;
using Test.ECommerce.Data.Contract;
using Test.ECommerce.Data.Models;
using Test.ECommerce.Data.Repositories.OrderRepository;
using Test.ECommerce.Data.Templates;

namespace Test.ECommerce.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private readonly ILog _logger;

        public OrderService(IOrderRepository orderRepository, ILog logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public ServiceResponse<PaymentServiceResponse, OrderContract> CreateOrder(OrderRequest orderRequest)
        {
            try
            {   
                var cutomerOrder = new Order
                {
                    CustomerId = orderRequest.CustomerId,
                    CustomerAddress = orderRequest.CustomerAddress,
                    Description = orderRequest.Description,
                    CustomerName = orderRequest.CustomerName,
                    Email = orderRequest.CustomerEmail,
                    BasketJson = JsonConvert.SerializeObject(orderRequest.OrderItems),
                    CreateDate = DateTime.Now,
                    TotalPrice = CalculateTotalPrice(orderRequest.OrderItems),
                };

                _orderRepository.Add(cutomerOrder);
            }
            catch (System.Exception  ex)
            {
                _logger.Error($"CreateOrder :{ex}");
                return new ServiceResponse<PaymentServiceResponse, OrderContract>(PaymentServiceResponse.Exception);
            }

            return new ServiceResponse<PaymentServiceResponse, OrderContract>(PaymentServiceResponse.Success);
        }

        private double CalculateTotalPrice(List<OrderItem> orderItems)
        {
            var totalPrice = default(double);

            foreach (var item in orderItems)
            {
                totalPrice =+ item.Quantity * item.Product.ProductPrice;
            }

            return totalPrice;
        }
    }
}