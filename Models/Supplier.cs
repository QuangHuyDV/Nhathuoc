using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhathuoc.Models
{
    public class Supplier
    {
        public Supplier()
        {
            Stocks = new HashSet<Stock>();
        }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long Created { get; set; }
        public long Updated { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}