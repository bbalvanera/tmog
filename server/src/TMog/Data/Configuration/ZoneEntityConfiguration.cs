using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TMog.Entities;

namespace TMog.Data.Configuration
{
    internal class ZoneEntityConfiguration : EntityTypeConfiguration<Zone>
    {
        public ZoneEntityConfiguration()
        {
            HasKey(e => e.ZoneId);
            Property(e => e.ZoneId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasOptional(e => e.Location).WithMany().Map(config => config.MapKey("LocationId"));
        }
    }
}
