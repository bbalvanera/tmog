using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TMog.Entities;

namespace TMog.Data.Configuration
{
    public class RegionEntityConfiguration : EntityTypeConfiguration<Region>
    {
        public RegionEntityConfiguration()
        {
            HasKey(e => e.RegionId);

            Property(e => e.RegionId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(e => e.Name).IsRequired();
        }
    }
}
