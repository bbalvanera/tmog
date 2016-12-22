using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TMog.Entities;

namespace TMog.Data.Configuration
{
    public class LocationEntityConfiguration : EntityTypeConfiguration<Location>
    {
        public LocationEntityConfiguration()
        {
            HasKey(l => l.LocationId);
            Property(l => l.LocationId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

        }
    }
}
