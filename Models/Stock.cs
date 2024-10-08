using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhathuoc.Models
{
    public class Stock
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product{ get; set; }
        public int SupplierId { get; set; }
        public virtual Supplier? Supplier{ get; set;}
        public int QuantityReceived { get; set; }
        public DateTime ReceivedDate { get; set; }
        public long Created { get; set; }
        public long Updated { get; set; }
    }
}