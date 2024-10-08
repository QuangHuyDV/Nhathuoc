using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Nhathuoc.Models
{
    public class Product
    {
        public Product()
        {
            OrderDetail = new HashSet<OrderDetail>();
            Stocks = new HashSet<Stock>();
        }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public int Price { get; set; }
        public string Unit { get; set; }
        public int QuantityInStock { get; set; }
        public string ExpiryDate { get; set; }
        public string Mannufacurer { get; set; }
        public long Created { get; set; }
        public long Updated { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}