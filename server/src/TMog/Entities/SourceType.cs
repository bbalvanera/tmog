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
        [Display(Name = "Black Market")]
        BlackMarket = 14,
        Fished = 16
    }
}
