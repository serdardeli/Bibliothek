using Bibliothek.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothek.Model.Entity
{
    public class District:CoreEntity
    {
        public string DistrictName { get; set; }
        public Guid CityID { get; set; }
        public virtual City City { get; set; }
        public virtual List<Address> Addresses { get; set; }


    }
}
