using Bibliothek.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothek.Model.Entity
{
    public class City:CoreEntity
    {
        public string CityName { get; set; }
        //public int DistrictID { get; set; }
        public virtual List<District> Districts { get; set; }
    }
}
