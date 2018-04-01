using System;
using System.Collections.Generic;

namespace TMog.WebApi.Models
{
    public class WorldQuest
    {
        public int WorldQuestId { get; set; }

        public string Name { get; set; }

        public string Side { get; set; }

        public string Category { get; set; }

        public DateTime ExpiresOn { get; set; }

        public Zone Zone { get; set; }

        public IEnumerable<Faction> Factions { get; set; }

        public IEnumerable<WorldQuestReward> Rewards { get; set; }
    }
}
