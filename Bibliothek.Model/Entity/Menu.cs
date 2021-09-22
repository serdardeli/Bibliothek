using Bibliothek.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothek.Model.Entity
{
    public class Menu:CoreEntity
    {
        public Guid? ParentMenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuContent { get; set; }

    }
}
