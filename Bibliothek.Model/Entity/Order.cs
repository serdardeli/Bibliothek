using Bibliothek.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothek.Model.Entity
{
    public class Order:CoreEntity
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }

        public int OrderNo { get; set; }
        public Guid CustomerID { get; set; }
        public string Phone { get; set; }
        public Guid AddressID { get; set; }
        //public virtual Address Address { get; set; }
        public virtual Customer Customer { get; set; }
        public bool Confirmed { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
