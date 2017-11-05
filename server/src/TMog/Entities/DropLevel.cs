using System.ComponentModel.DataAnnotations;

namespace TMog.Entities
{
    public enum DropLevel
    {
        Unknown,
        [Display(Name = "Normal")]
        DungeonNormal = -1,
        [Display(Name = "Heroic")]
        DungeonHeroic = -2,
        [Display(Name = "10 Normal")]
        Raid10Normal = 1,
        [Display(Name = "25 Normal")]
        Raid25Normal,
        [Display(Name = "10 Heroic")]
        Raid10Heroic,
        [Display(Name = "25 Heroic")]
        Raid25Heroic
    }
}
