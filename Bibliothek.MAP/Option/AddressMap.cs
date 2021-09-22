using Bibliothek.Core.Map;
using Bibliothek.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothek.MAP.Option
{
    public class AddressMap:CoreMap<Address>
    {
        public AddressMap()
        {
            ToTable("dbo.Addresses");
            Property(x => x.AddressTitle).HasMaxLength(25).IsRequired();
            Property(x => x.AddressContent).HasMaxLength(100).IsRequired();

            HasRequired(x => x.District)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.DistrictID);
            
        }
    }
}
