using System.Collections.Generic;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMog.Entities;
using TMog.WowheadApi;

namespace TMog.UnitTests.Services.Infrastructure.WowheadSetMapProfileTests
{
    [TestClass]
    public class When_mapping_from_wowheadItem
    {
        [TestInitialize]
        public virtual void TestInitialize()
        {
            Mapper.Initialize(config => config.AddProfiles("TMog"));
        }

        [TestMethod]
        public void should_map_correctly()
        {
            var wowheadItem = new WowheadItem
            {
                Id = 1,
                Name = "Name",
                Slot = 1,
                Quality = 2,
                Sources = new List<IWowheadItemSource>
                {
                    new WowheadItemSource
                    {
                        Name = "wowheaditem one",
                        Zone = 2
                    },
                    new WowheadItemSource
                    {
                        Name = "wowheaditem two",
                        Zone = 1
                    }
                }
            };

            var result = Mapper.Map<IWowheadItem, Item>(wowheadItem);

            Assert.AreEqual(wowheadItem.Id, result.ItemId);
            Assert.AreEqual(wowheadItem.Name, result.Name);
            Assert.AreEqual("Head", result.Slot.ToString());
            Assert.AreEqual("Uncommon", result.Quality.ToString());
        }

        [TestMethod]
        public void should_map_even_when_quality_is_null()
        {
            var wowheadItem = new WowheadItem
            {
                Id = 1,
                Name = "Name",
                Slot = 1,
                Quality = null
            };

            var result = Mapper.Map<IWowheadItem, Entities.Item>(wowheadItem);

            Assert.AreEqual(wowheadItem.Id, result.ItemId);
            Assert.AreEqual(wowheadItem.Name, result.Name);
            Assert.AreEqual("Head", result.Slot.ToString());
            Assert.AreEqual("Unknown", result.Quality.ToString());
        }
    }
}
