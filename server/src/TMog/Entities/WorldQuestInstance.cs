using System;
using System.Collections.Generic;

namespace TMog.Entities
{
    public class WorldQuestInstance
    {
        public int WorldQuestInstanceId { get; set; }

        public int QuestId { get; set; }

        public DateTime ExpiresOn { get; set; }

        public ICollection<Item> Rewards { get; set; }
    }
}
