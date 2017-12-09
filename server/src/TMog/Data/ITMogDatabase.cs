using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using TMog.Entities;

namespace TMog.Data
{
    public interface ITMogDatabase
    {
        IDbSet<Set> Sets { get; }

        IDbSet<Item> Items { get; }

        IDbSet<Source> Sources { get; }

        IDbSet<Zone> Zones { get; }

        IDbSet<Region> Regions { get; }

        IDbSet<Faction> Factions { get; }

        Task<IEnumerable<T>> Execute<T>(string name, params object[] parameters);

        void MarkEntityForDeletion<TEntity>(TEntity entity) where TEntity: class;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        void DisableChangeDetection();

        void EnableChangeDetection();

        Database Database { get; }
    }
}
