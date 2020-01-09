using Test.ECommerce.Data.Contract;

namespace Test.ECommerce.Data.Templates
{
    public class OrderItem
    {
        public ProductContract Product { get; set; }

        public int Quantity { get; set; }

        public double? Price { get; set; }
    }
}