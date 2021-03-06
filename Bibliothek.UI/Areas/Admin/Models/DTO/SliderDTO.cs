using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bibliothek.UI.Areas.Admin.Models.DTO
{
    public class SliderDTO
    {
        public Guid ID { get; set; }
        public string SliderPath { get; set; }
        public string Queue { get; set; }

        public Guid CategoryID { get; set; }
        public virtual CategoryDTO Category { get; set; }

    }
}