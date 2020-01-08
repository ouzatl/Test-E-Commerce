using System;
using System.Collections.Generic;
using System.Linq;
using Test.ECommerce.Common.NLog;
using Test.ECommerce.Data.Models;
using Test.ECommerce.Data.Repositories.ProductRepository;

namespace Test.ECommerce.Service.ProductService
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private readonly ILog _logger;

        public ProductService(IProductRepository productRepository, ILog logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public List<Product> GetAllProducts()
        {
            var result = _productRepository.GetAll().Where(x=>x.IsActive == true && x.StockCount > 0).ToList();

            return result;
        }

        public Product GetByProductCode(string productCode)
        {
            if (!string.IsNullOrEmpty(productCode))
                return _productRepository.GetById(productCode);

            return null;
        }

        public List<Product> GetProductListByCategoryCode(string categoryCode)
        {
            if (!string.IsNullOrEmpty(categoryCode))
                return _productRepository.GetProductListByCategoryCode(categoryCode);

            return null;
        }

        public void AddProduct(Product product)
        {
            product.CreateDate = DateTime.Now;
            product.ModifiedDate = DateTime.Now;
            _productRepository.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            product.ModifiedDate = DateTime.Now;
            _productRepository.Update(product);
        }

        public void DeleteProduct(string productCode)
        {
            _productRepository.Delete(productCode);
        }
    }
}