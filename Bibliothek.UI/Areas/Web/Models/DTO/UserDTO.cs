using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bibliothek.UI.Areas.Web.Models.DTO
{
    public class UserDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string AddressTitle { get; set; }
        public string AddressContent { get; set; }
        public string DistrictName { get; set; }
        public string CityName { get; set; }
    }
}