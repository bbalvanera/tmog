using System.Data.Entity.ModelConfiguration;
using TMog.Entities;

namespace TMog.Data.Configuration
{
    public class WorldQuestSubmissionLogEntityConfiguration: EntityTypeConfiguration<WorldQuestsSubmissionLog>
    {
        public WorldQuestSubmissionLogEntityConfiguration()
        {
            HasKey(e => e.WorldQuestsSubmissionLogId);

            Property(e => e.SubmissionDate).IsRequired();
            Property(e => e.SubmitCount).IsRequired();
        }
    }
}
