using System.Data.Entity;
using TMog.Properties;

namespace TMog.Data.Configuration
{
    internal class TMogDbInitializer : DropCreateDatabaseIfModelChanges<TMogDatabase>
    {
        protected override void Seed(TMogDatabase context)
        {
            var allItems = Resources.dbo_AllItems;
            context.Database.ExecuteSqlCommand(allItems);
            base.Seed(context);
        }
    }
}
