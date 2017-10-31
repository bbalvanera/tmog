using System.ComponentModel.DataAnnotations;

namespace TMog.Entities
{
    public enum DropLevel
    {
        Unknown,
        [Display(Name = "(N)")]
        DungeonNormal = -1,
        [Display(Name = "(H)")]
        DungeonHeroic = -2,
        [Display(Name = "(10N)")]
        Raid10Normal = 1,
        [Display(Name = "(25N)")]
        Raid25Normal,
        [Display(Name = "(10H)")]
        Raid10Heroic,
        [Display(Name = "(25H)")]
        Raid25Heroic
    }
}
