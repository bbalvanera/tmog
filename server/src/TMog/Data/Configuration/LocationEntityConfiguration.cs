using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TMog.Entities;

namespace TMog.Data.Configuration
{
    public class LocationEntityConfiguration : EntityTypeConfiguration<Location>
    {
        public LocationEntityConfiguration()
        {
            HasKey(e => e.LocationId);
            Property(e => e.LocationId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasRequired(e => e.Region).WithMany().Map(config => config.MapKey("RegionId"));
        }
    }
}
