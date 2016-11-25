using System.Data.Entity.ModelConfiguration;
using TMog.Entities;

namespace TMog.Data.Configuration
{
    internal class SourceEntityConfiguration : EntityTypeConfiguration<Source>
    {
        public SourceEntityConfiguration()
        {
            HasKey(s => s.SourceId);
            HasMany(s => s.Items);
        }
    }
}
