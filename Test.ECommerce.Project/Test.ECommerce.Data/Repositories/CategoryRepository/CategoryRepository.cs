using Microsoft.EntityFrameworkCore;
using Test.ECommerce.Data.Models;

namespace Test.ECommerce.Data.Repositories.CategoryRepository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly DbContext _dbContext;

        public CategoryRepository(TestECommerceContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}