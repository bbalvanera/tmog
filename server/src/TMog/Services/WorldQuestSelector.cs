using System.Collections.Generic;
using System.Linq;
using TMog.Entities;

namespace TMog.Services
{
    /// <summary>
    /// Selects a set of WorldQuests that are deemed relevant base on a series of filter
    /// </summary>
    internal class WorldQuestSelector
    {
        public IEnumerable<WorldQuest> GetRelevantWorldQuests(IEnumerable<WorldQuest> activeQuests)
        {
            if (activeQuests == null || activeQuests.Count() == 0)
            {
                return Enumerable.Empty<WorldQuest>();
            }

            return activeQuests/*.Select(wq => relevantWorldQuestFilters.Any(wq))*/;
        }
    }
}
