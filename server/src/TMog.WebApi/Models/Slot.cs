﻿using System.Collections.Generic;

namespace TMog.WebApi.Models
{
    public class Slot
    {
        public string Name { get; set; }

        public bool Complete { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }
}
