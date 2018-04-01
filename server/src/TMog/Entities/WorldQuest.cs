using System.Collections.Generic;

namespace TMog.Entities
{
    public class WorldQuest: Quest
    {
        public WorldQuestCategory Category { get; set; }

        public ICollection<WorldQuestInstance> Instances { get; set; }

        public ICollection<Faction> Factions { get; set; }
    }
}
