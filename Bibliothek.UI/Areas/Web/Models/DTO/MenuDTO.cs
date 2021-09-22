using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bibliothek.UI.Areas.Web.Models.DTO
{
    public class MenuDTO
    {
        public Guid ID { get; set; }
        public Guid? ParentMenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuContent { get; set; }
    }
}