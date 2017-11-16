using System.Data.Entity;

namespace TMog.Data.Configuration
{
    internal class TMogDbInitializer : DropCreateDatabaseIfModelChanges<TMogDatabase>
    {
    }
}
