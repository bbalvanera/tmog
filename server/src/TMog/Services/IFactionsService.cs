using System.Collections.Generic;
using System.Threading.Tasks;
using TMog.Entities;

namespace TMog.Services
{
    public interface IFactionsService
    {
        Task Save(IEnumerable<Faction> factions);
    }
}