using Bibliothek.Core.Enum;
using Bibliothek.Model.Entity;
using Bibliothek.Service.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothek.Service.Option
{
    public class AddressService:ServiceBase<Address>
    {
        public List<Address> GetByCustomerId( Guid customerId) =>
            table.Where(x => x.Status == Status.Active && x.CustomerID == customerId).ToList();

    }
}
