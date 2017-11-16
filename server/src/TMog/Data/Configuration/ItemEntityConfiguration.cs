using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TMog.Entities;

namespace TMog.Data.Configuration
{
    internal class ItemEntityConfiguration : EntityTypeConfiguration<Item>
    {
        public ItemEntityConfiguration()
        {
            HasKey(e => e.ItemId);
            Property(e => e.ItemId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasOptional(e => e.Source).WithMany().Map(config => config.MapKey("SourceId"));
        }
    }
}
