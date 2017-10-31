using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using TMog.Data.Configuration;
using TMog.Entities;

namespace TMog.Data
{
    public class TMogDatabase : DbContext, ITMogDatabase
    {
        public TMogDatabase() : base("TMogDb")
        {
            Database.SetInitializer<TMogDatabase>(new TMogDbInitializer());
        }

        public IDbSet<Set> Sets { get; set; }

        public IDbSet<Item> Items { get; set; }

        public IDbSet<Source> Sources { get; set; }

        public IDbSet<Zone> Zones { get; set; }
        
        public IDbSet<Location> Locations { get; set; }

        public async Task<IEnumerable<T>> Execute<T>(string name, params object[] parameters)
        {
            return await Database.SqlQuery<T>(name, parameters).ToListAsync();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SetEntityConfiguration());
            modelBuilder.Configurations.Add(new ItemEntityConfiguration());
            modelBuilder.Configurations.Add(new SourceEntityConfiguration());
            modelBuilder.Configurations.Add(new ZoneEntityConfiguration());
            modelBuilder.Configurations.Add(new LocationEntityConfiguration());
        }
    }
}
