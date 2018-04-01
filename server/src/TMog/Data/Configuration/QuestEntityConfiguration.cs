using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TMog.Entities;

namespace TMog.Data.Configuration
{
    public class QuestEntityConfiguration: EntityTypeConfiguration<Quest>
    {
        public QuestEntityConfiguration()
        {
            HasKey(e => e.QuestId);

            Property(e => e.QuestId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(e => e.Name).IsRequired();

            HasOptional(e => e.Zone).WithMany().Map(m => m.MapKey("ZoneId"));

            // configure discriminator
            Map(m => m.Requires("IsWorldQuest").HasValue(false));
        }
    }
}
