using Bibliothek.Model.Entity;
using Bibliothek.Service.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothek.Service.Option
{
    public class OrderService:ServiceBase<Order>
    {
        public List<Order> ListPendingOrders() =>        
            GetDefault(x => x.Confirmed == false && x.Status == Core.Enum.Status.Active).OrderByDescending(x => x.CreatedDate).ToList();
        
        public int PendingOrderCount() =>
            GetDefault(x => x.Confirmed == false && x.Status == Core.Enum.Status.Active).Count;
        
   
        public List<Order> ListOrderHistory(Guid customerId) =>        
            GetDefault(x => x.Status == Core.Enum.Status.Active && x.CustomerID == customerId).OrderByDescending(x => x.CreatedDate).ToList();

    }
}
