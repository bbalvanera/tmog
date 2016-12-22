using System.Collections.Generic;
using System.Linq;
using TMog.Entities;

namespace TMog.Models
{
    public class SetInfo
    {
        public int SetId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public ICollection<Item> Items
        {
            get;
            set;
        }

        public int TotalItems
        {
            get
            {
                return Items?.GroupBy(i => i.Slot).Count() ?? 0;
            }
        }

        //public int TotalAcquired
        //{
        //    get
        //    {
        //        return Items?.GroupBy(i => i.Acquired).Count() ?? 0;
        //    }
        //}

        //public int TotalMissing
        //{
        //    get
        //    {
        //        var acquiredItems = Items?.Where(i => i.Acquired);

        //        return Items?.Where(i => !acquiredItems.Any(ac => ac.Slot == i.Slot))
        //                     .GroupBy(i => i.Slot)
        //                     .Count() ?? 0;
        //    }
        //}

        public SetInfo(Set set)
        {
            SetId = set.SetId;
            Name  = set.Name;
            Items = set.Items;
        }
    }
}
