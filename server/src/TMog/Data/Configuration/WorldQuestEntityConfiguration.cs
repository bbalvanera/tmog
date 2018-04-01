using System.Data.Entity.ModelConfiguration;
using TMog.Entities;

namespace TMog.Data.Configuration
{
    public class WorldQuestEntityConfiguration: EntityTypeConfiguration<WorldQuest>
    {
        public WorldQuestEntityConfiguration()
        {
            // configure discriminator
            Map(m => m.Requires("IsWorldQuest").HasValue(true));

            HasMany(e => e.Instances);
            HasMany(e => e.Factions).WithMany().Map(config => config.MapLeftKey("QuestId").MapRightKey("FactionId"));
        }
    }
}
