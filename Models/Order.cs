using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhathuoc.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
            Invoices = new HashSet<Invoice>();
        }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public string Status { get; set; }
        public long Created { get; set; }
        public long Updated { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set;}
        public virtual ICollection<Invoice> Invoices { get;}
    }
}