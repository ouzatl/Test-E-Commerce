using Microsoft.EntityFrameworkCore;
using Test.ECommerce.Data.Models;

namespace Test.ECommerce.Data
{
    public class TestECommerceContext : DbContext
    {
        public TestECommerceContext(DbContextOptions<TestECommerceContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}