using Test.ECommerce.Data.Models;

namespace Test.ECommerce.Service.BasketService
{
    public interface IBasketService
    {
        Basket GetProductFromBasket(Basket basket);

        void AddProductInBasket(Basket basket);

        void UpdateProductInBasket(Basket basket);
    }
}