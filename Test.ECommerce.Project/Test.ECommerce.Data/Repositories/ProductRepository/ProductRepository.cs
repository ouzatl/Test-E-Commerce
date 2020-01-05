using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Test.ECommerce.Data.Models;

namespace Test.ECommerce.Data.Repositories.ProductRepository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly DbContext _dbContext;

        public ProductRepository(TestECommerceContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetProductListByCategoryCode(string categoryCode)
        {
            var productlist = _dbContext.Set<Product>().Where(x => x.CategoryCode == categoryCode && x.IsActive == true).ToList();
            return productlist;
        }
    }
}