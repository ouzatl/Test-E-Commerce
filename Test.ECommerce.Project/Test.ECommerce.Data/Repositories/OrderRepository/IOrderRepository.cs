using Test.ECommerce.Data.Models;

namespace Test.ECommerce.Data.Repositories.OrderRepository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        void AddOrder(Order order);
    }
}