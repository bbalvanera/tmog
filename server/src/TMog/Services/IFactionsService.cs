using System.Collections.Generic;
using System.Threading.Tasks;
using TMog.Entities;

namespace TMog.Services
{
    public interface IFactionsService
    {
        Task AddAll(IEnumerable<Faction> factions);
    }
}