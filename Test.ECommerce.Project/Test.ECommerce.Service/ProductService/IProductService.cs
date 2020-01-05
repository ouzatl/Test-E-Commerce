using System.Collections.Generic;
using Test.ECommerce.Data.Models;

namespace Test.ECommerce.Service.ProductService
{
    public interface IProductService
    {
        List<Product> GetAllProducts();

        Product GetByProductCode(string productCode);

        List<Product> GetProductListByCategoryCode(string categoryCode);

        void AddProduct(Product product);

        void UpdateProduct(Product product);

        void DeleteProduct(string product);
    }
}