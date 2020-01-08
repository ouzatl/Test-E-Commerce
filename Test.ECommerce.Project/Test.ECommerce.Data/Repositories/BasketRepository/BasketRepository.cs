using Microsoft.EntityFrameworkCore;
using System.Linq;
using Test.ECommerce.Data.Models;

namespace Test.ECommerce.Data.Repositories.BasketRepository
{
    public class BasketRepository : BaseRepository<Basket>, IBasketRepository
    {
        private readonly DbContext _dbContext;

        public BasketRepository(TestECommerceContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Basket GetProductFromBasket(Basket basket)
        {
            var result = _dbContext.Set<Basket>().Where(x => x.IsActive == true && x.ProductCode == basket.ProductCode && x.BasketKey == basket.BasketKey).FirstOrDefault();

            return result;
        }

        public void  AddProductInBasket(Basket basket)
        {
            var result = _dbContext.Add(basket);
        }

        public void UpdateProductInBasket(Basket basket)
        {
            var result = _dbContext.Update(basket);
        }
    }
}