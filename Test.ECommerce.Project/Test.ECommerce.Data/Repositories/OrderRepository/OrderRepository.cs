using Microsoft.EntityFrameworkCore;
using Test.ECommerce.Data.Models;

namespace Test.ECommerce.Data.Repositories.OrderRepository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private readonly DbContext _dbContext;

        public OrderRepository(TestECommerceContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddOrder(Order order)
        {
            _dbContext.Add(order);
        }
    }
}