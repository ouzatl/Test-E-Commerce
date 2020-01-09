using System.Collections.Generic;
using Test.ECommerce.Data.Contract;
using Test.ECommerce.Data.Models;

namespace Test.ECommerce.Service.ProductService
{
    public interface IProductService
    {
        List<ProductContract> GetAllProducts();

        ProductContract GetByProductCode(string productCode);

        List<ProductContract> GetProductListByCategoryCode(string categoryCode);

        void AddProduct(ProductContract productContract);

        void UpdateProduct(ProductContract productContract);

        void DeleteProduct(string productCode);
    }
}