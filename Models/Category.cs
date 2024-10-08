using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhathuoc.Models
{
    public class Category
    {
        public Category() {
            Products = new List<Product>();
        }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public long Created { get; set; }
        public long Updated { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}