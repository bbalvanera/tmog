using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TMog.Entities;

namespace TMog.Data.Configuration
{
    internal class ZoneEntityConfiguration : EntityTypeConfiguration<Zone>
    {
        public ZoneEntityConfiguration()
        {
            HasKey(z => z.ZoneId);
            Property(z => z.ZoneId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasOptional(z => z.Location).WithMany().Map(m => m.MapKey("LocationId"));
        }
    }
}
