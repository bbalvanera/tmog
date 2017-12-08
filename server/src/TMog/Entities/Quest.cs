using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMog.Entities
{
    public class Quest
    {
        public int QuestId { get; set; }

        public string Name { get; set; }

        public int RequiredLevel { get; set; }

        public int MyProperty { get; set; }
    }
}
