using System.Collections.Generic;
using Test.ECommerce.Data.Models;

namespace Test.ECommerce.Data.Repositories.ProductRepository
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        List<Product> GetProductListByCategoryCode(string categoryCode);
    }
}