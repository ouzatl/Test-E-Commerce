using Test.ECommerce.Data.Models;

namespace Test.ECommerce.Data.Repositories.BasketRepository
{
    public interface IBasketRepository : IBaseRepository<Basket>
    {
        Basket GetProductFromBasket(Basket basket);

        void AddProductInBasket(Basket basket);

        void UpdateProductInBasket(Basket basket);
    }
}