using Bibliothek.Core.Map;
using Bibliothek.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothek.MAP.Option
{
    public class DistrictMap:CoreMap<District>
    {
        public DistrictMap()
        {
            ToTable("dbo.Districts");
            Property(x => x.DistrictName).HasMaxLength(30);

            HasRequired(x => x.City)
                .WithMany(x => x.Districts)
                .HasForeignKey(x => x.CityID);

        }
    }
}
