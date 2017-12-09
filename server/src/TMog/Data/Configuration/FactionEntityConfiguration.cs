using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TMog.Entities;

namespace TMog.Data.Configuration
{
    public class FactionEntityConfiguration: EntityTypeConfiguration<Faction>
    {
        public FactionEntityConfiguration()
        {
            HasKey(e => e.FactionId);

            Property(e => e.FactionId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(e => e.Name).IsRequired();
        }
    }
}
