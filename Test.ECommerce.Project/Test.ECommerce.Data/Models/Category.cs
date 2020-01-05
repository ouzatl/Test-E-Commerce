using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.ECommerce.Data.Models
{
    public class Category
    {
        [Key]
        public string CategoryCode { get; set; }
        public string CategoryName{ get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}