using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Test.ECommerce.Common.NLog;
using Test.ECommerce.Data.Contract;
using Test.ECommerce.Data.Models;
using Test.ECommerce.Data.Repositories.ProductRepository;

namespace Test.ECommerce.Service.ProductService
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private readonly ILog _logger;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ILog logger, IMapper mapper)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public List<ProductContract> GetAllProducts()
        {
            var result = _mapper.Map<List<ProductContract>>(_productRepository.GetAll().Where(x => x.IsActive == true && x.StockCount > 0).ToList());

            return result;
        }

        public ProductContract GetByProductCode(string productCode)
        {
            if (!string.IsNullOrEmpty(productCode))
                return _mapper.Map<ProductContract>(_productRepository.GetById(productCode));

            return null;
        }

        public List<ProductContract> GetProductListByCategoryCode(string categoryCode)
        {
            if (!string.IsNullOrEmpty(categoryCode))
                return _mapper.Map<List<ProductContract>>(_productRepository.GetProductListByCategoryCode(categoryCode));

            return null;
        }

        public void AddProduct(ProductContract productContract)
        {
            productContract.CreateDate = DateTime.Now;
            productContract.ModifiedDate = DateTime.Now;
            var product = _mapper.Map<Product>(productContract);
            _productRepository.Add(product);
        }

        public void UpdateProduct(ProductContract productContract)
        {
            productContract.ModifiedDate = DateTime.Now;
            var product = _mapper.Map<Product>(productContract);
            _productRepository.Update(product);
        }

        public void DeleteProduct(string productCode)
        {
            _productRepository.Delete(productCode);
        }
    }
}