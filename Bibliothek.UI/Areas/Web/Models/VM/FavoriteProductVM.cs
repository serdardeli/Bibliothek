using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bibliothek.UI.Areas.Web.Models.VM
{
    public class FavoriteProductVM
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }
        public decimal Price { get; set; }

    }
}