using System.ComponentModel.DataAnnotations;

namespace TMog.Entities
{
    public enum SourceSubType
    {
        [Display(Name = "none")]
        None,
        [Display(Name = "npc")]
        Npc,
        [Display(Name = "object")]
        Object,
        [Display(Name = "item")]
        Item,
        [Display(Name = "tbd")]
        TBD,
        [Display(Name = "quest")]
        Quest,
        [Display(Name = "spell")]
        Spell,
        [Display(Name = "achievement")]
        Achievement = 10
    }
}
