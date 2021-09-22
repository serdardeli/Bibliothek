using Bibliothek.Core.Map;
using Bibliothek.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothek.MAP.Option
{
    public class MailingMap:CoreMap<Mailing>
    {
        public MailingMap()
        {
            ToTable("dbo.Mailings");
            Property(x => x.Email).HasMaxLength(50).IsRequired();
        }
    }
}
