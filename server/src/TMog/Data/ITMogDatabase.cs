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

        IDbSet<Location> Locations { get; }

        IDbSet<Region> Regions { get; }

        Task<IEnumerable<T>> Execute<T>(string name, params object[] parameters);

        int SaveChanges();
    }
}
