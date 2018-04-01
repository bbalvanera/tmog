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
            Database.SetInitializer(new TMogDbInitializer());
        }

        public virtual IDbSet<Set> Sets { get; set; }

        public virtual IDbSet<Item> Items { get; set; }

        public virtual IDbSet<Source> Sources { get; set; }

        public virtual IDbSet<Zone> Zones { get; set; }

        public virtual IDbSet<Region> Regions { get; set; }

        public virtual IDbSet<Faction> Factions { get; set; }

        public virtual IDbSet<WorldQuest> WorldQuests { get; set; }

        public virtual IDbSet<WorldQuestInstance> WordQuestInstances { get; set; }

        public virtual IDbSet<WorldQuestsSubmissionLog> WorldQuestsSubmissionLogs { get; set; }

        public void DisableChangeDetection()
        {
            this.Configuration.AutoDetectChangesEnabled = false;
        }

        public void EnableChangeDetection()
        {
            this.Configuration.AutoDetectChangesEnabled = true;
        }

        public async Task<IEnumerable<T>> Execute<T>(string name, params object[] parameters)
        {
            return await Database.SqlQuery<T>(name, parameters).ToListAsync();
        }

        public void MarkEntityForDeletion<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SetEntityConfiguration());
            modelBuilder.Configurations.Add(new ItemEntityConfiguration());
            modelBuilder.Configurations.Add(new SourceEntityConfiguration());
            modelBuilder.Configurations.Add(new ZoneEntityConfiguration());
            modelBuilder.Configurations.Add(new RegionEntityConfiguration());
            modelBuilder.Configurations.Add(new FactionEntityConfiguration());
            modelBuilder.Configurations.Add(new QuestEntityConfiguration());
            modelBuilder.Configurations.Add(new WorldQuestEntityConfiguration());
            modelBuilder.Configurations.Add(new WorldQuestInstanceEntityConfiguration());
            modelBuilder.Configurations.Add(new WorldQuestSubmissionLogEntityConfiguration());
        }
    }
}
