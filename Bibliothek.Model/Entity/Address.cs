using Bibliothek.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothek.Model.Entity
{
    public class Address:CoreEntity
    {
        public string AddressTitle { get; set; }
        public string AddressContent { get; set; }
        public Guid DistrictID { get; set; }
        public virtual District District { get; set; }
        public Guid CustomerID { get; set; }
        //public virtual Customer Customer { get; set; }


    }
}
