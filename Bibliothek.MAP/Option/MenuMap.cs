using Bibliothek.Core.Map;
using Bibliothek.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothek.MAP.Option
{
    public class MenuMap:CoreMap<Menu>
    {
        public MenuMap()
        {
            ToTable("dbo.Menus");
            Property(x => x.MenuName).IsRequired();
            Property(x => x.ParentMenuID).IsOptional();
            Property(x => x.MenuContent).IsOptional();
        }
    }
}
