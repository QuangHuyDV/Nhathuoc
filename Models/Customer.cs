using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhathuoc.Models
{
    public class Customer
    {
        public Customer() {
            Orders = new HashSet<Order>();
        }
        public int CustomerId { get; set; }
        public string CustomerName { get; set;}
        public string CustomerEmail { get; set;}
        public string CustomerPhone { get; set;}
        public string CustomerAddress { get; set;}
        public DateTime? Dob { get; set; }
        public long Created { get; set; }
        public long Updated { get; set; }
        public virtual ICollection<Order> Orders { get;}
    }
}