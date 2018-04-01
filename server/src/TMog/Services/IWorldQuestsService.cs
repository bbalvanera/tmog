using System.Collections.Generic;
using System.Threading.Tasks;
using TMog.Entities;

namespace TMog.Services
{
    public interface IWorldQuestsService
    {
        Task<int> Save(IEnumerable<WorldQuest> worldQuests);

        Task<IEnumerable<WorldQuest>> GetActive();
    }
}