using Bibliothek.Core.Map;
using Bibliothek.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothek.MAP.Option
{
    public class CityMap:CoreMap<City>
    {
        public CityMap()
        {
            ToTable("dbo.Cities");
            Property(x => x.CityName).HasMaxLength(30).IsRequired();

            HasMany(x => x.Districts)
                .WithRequired(x => x.City)
                .HasForeignKey(x => x.CityID);
        }
    }
}
