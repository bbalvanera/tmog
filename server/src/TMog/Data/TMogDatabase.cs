using System.Collections.Generic;
using System.Data.Entity;
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

        public IDbSet<Set> Sets
        {
            get;
            set;
        }

        public IDbSet<Item> Items
        {
            get;
            set;
        }

        public IDbSet<Source> Sources
        {
            get;
            set;
        }

        public IDbSet<Zone> Zones
        {
            get;
            set;
        }

        public IDbSet<Location> Locations { get; set; }

        public IEnumerable<T> Execute<T>(string name, params object[] parameters)
        {
            return this.Database.SqlQuery<T>(name, parameters);
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
