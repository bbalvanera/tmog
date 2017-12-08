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

            HasOptional(e => e.Parent).WithMany().Map(config => config.MapKey("ParentZoneId"));
            HasOptional(e => e.Region).WithMany().Map(config => config.MapKey("RegionId"));
        }
    }
}
