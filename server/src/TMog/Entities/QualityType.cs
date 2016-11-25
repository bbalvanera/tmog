using System.ComponentModel.DataAnnotations;

namespace TMog.Entities
{
    public enum QualityType
    {
        Unknown = -1,
        Poor,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Artifact,
        Heirloom,
        [Display(Name = "WoW Token")]
        WowToken
    }
}
