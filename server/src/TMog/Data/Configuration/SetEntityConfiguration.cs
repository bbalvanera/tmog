using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TMog.Entities;

namespace TMog.Data.Configuration
{
    internal class SetEntityConfiguration : EntityTypeConfiguration<Set>
    {
        public SetEntityConfiguration()
        {
            HasKey(s => s.SetId);

            Property(s => s.Name).IsRequired();
            Property(s => s.SetId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasMany(s => s.Items).WithMany().Map(config => config.MapLeftKey("SetId").MapRightKey("ItemId").ToTable("SetItems"));
        }
    }
}
