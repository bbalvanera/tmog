using System.Data.Entity.ModelConfiguration;
using TMog.Entities;

namespace TMog.Data.Configuration
{
    public class WorldQuestInstanceEntityConfiguration: EntityTypeConfiguration<WorldQuestInstance>
    {
        public WorldQuestInstanceEntityConfiguration()
        {
            HasKey(e => e.WorldQuestInstanceId);

            HasMany(e => e.Rewards).WithMany().Map(config => config.MapLeftKey("WorldQuestInstanceId").MapRightKey("ItemId").ToTable("WorldQuestRewards"));
        }
    }
}
