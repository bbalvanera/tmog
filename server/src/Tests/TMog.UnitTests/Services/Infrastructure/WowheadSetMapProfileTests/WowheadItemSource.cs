﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMog.WowheadApi;

namespace TMog.UnitTests.Services.Infrastructure.WowheadSetMapProfileTests
{
    public class WowheadItemSource : IWowheadItemSource
    {
        public int? Type { get; set; }

        public int? SubType { get; set; }

        public int? WowheadId { get; set; }

        public string Name { get; set; }

        public int? Zone { get; set; }

        public int? DropLevel { get; set; }
    }
}
