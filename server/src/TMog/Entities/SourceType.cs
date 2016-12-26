using System.ComponentModel.DataAnnotations;

namespace TMog.Entities
{
    public enum SourceType
    {
        Unavailable,
        [Display(Name = "Crafted")]
        Profession,
        Drop,
        [Display(Name = "PVP")]
        Pvp,
        Quest,
        Vendors,
        Achievement = 12,
        [Display(Name = "Black Market")]
        BlackMarket = 14,
        Fished = 16
    }
}
