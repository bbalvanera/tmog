using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TMog.Entities;

namespace TMog.Data.Configuration
{
    internal class SetEntityConfiguration : EntityTypeConfiguration<Set>
    {
        public SetEntityConfiguration()
        {
            HasKey(e => e.SetId);

            Property(e => e.Name).IsRequired();
            Property(e => e.SetId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasMany(e => e.Items).WithMany(e => e.Sets).Map(config => config.MapLeftKey("SetId").MapRightKey("ItemId").ToTable("SetItems"));
        }
    }
}
