using System.ComponentModel.DataAnnotations;

namespace TMog.WebApi.Models
{
    public class WorldQuestReward
    {
        [Required]
        public int? Id { get; set; }

        public string Name { get; set; }

        public int? Quality { get; set; }

        public int Qty { get; set; }
    }
}
