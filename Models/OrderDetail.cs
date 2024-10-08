using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhathuoc.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set;}
        public int OrderId { get; set;}
        public virtual Order? Order { get; set;}
        public int ProductId { get; set;}
        public virtual Product? Product { get; set;}
        public int Quantity { get; set;}
        public int UnitPrice { get; set;}
    }
}