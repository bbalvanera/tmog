using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMog.WebApi.Models
{
    public class RawWorldQuest
    {
        [Required]
        [Range(typeof(long), "1483228800000", "4070908800000", ErrorMessage = "Invalid ending value")]
        public long? Ending { get; set; }

        public IEnumerable<int> Factions { get; set; }

        [Required]
        [Range(1, 100000, ErrorMessage = "Invalid Quest id")]
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<WorldQuestReward> Rewards { get; set; }

        public int Type { get; set; }

        public IEnumerable<int> Zones { get; set; }
    }
}
