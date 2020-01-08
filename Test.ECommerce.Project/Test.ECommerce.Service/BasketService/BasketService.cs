using Test.ECommerce.Common.NLog;
using Test.ECommerce.Data.Models;
using Test.ECommerce.Data.Repositories.BasketRepository;

namespace Test.ECommerce.Service.BasketService
{
    public class BasketService : IBasketService
    {
        private IBasketRepository _basketRepository;
        private readonly ILog _logger;

        public BasketService(IBasketRepository basketRepository, ILog logger)
        {
            _basketRepository = basketRepository;
            _logger = logger;
        }

        public Basket GetProductFromBasket(Basket basket)
        {
            var result = _basketRepository.GetProductFromBasket(basket);

            return result;
        }

        public void AddProductInBasket(Basket basket)
        {
            _basketRepository.AddProductInBasket(basket);
        }

        public void UpdateProductInBasket(Basket basket)
        {
            _basketRepository.UpdateProductInBasket(basket);
        }
    }
}
