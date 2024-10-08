using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhathuoc.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int OrderId { get; set;}
        public virtual Order? Order { get; set; }
        public DateTime InvoiceDate { get; set;}
        public int TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
    }
}