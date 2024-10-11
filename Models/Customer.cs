using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nhathuoc.Models
{
    public class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }
        public int CustomerId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }

        [Phone]
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime Dob { get; set; }
        public long Created { get; set; }
        public long Updated { get; set; }
        public virtual ICollection<Order> Orders { get; }
    }
}