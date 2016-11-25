using System.Collections.Generic;
using System.Data.Entity;
using TMog.Entities;

namespace TMog.Data
{
    public interface ITMogDatabase
    {
        IDbSet<Set> Sets { get; }

        IDbSet<Item> Items { get; }

        IDbSet<Source> Sources { get; }

        IDbSet<Zone> Zones { get; }

        IEnumerable<T> Execute<T>(string name, params object[] parameters);

        int SaveChanges();
    }
}
