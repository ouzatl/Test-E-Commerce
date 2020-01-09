using Test.ECommerce.Common.Enum;
using Test.ECommerce.Common.Enum.Payment;
using Test.ECommerce.Data.Contract;
using Test.ECommerce.Data.Templates;

namespace Test.ECommerce.Service.OrderService
{
    public interface IOrderService
    {
        ServiceResponse<PaymentServiceResponse, OrderContract> CreateOrder(OrderRequest orderRequest);
    }
}