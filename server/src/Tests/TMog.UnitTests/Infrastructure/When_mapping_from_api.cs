using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace TMog.UnitTests.Infrastructure
{
    [TestClass]
    public class When_mapping_from_api
    {
        [TestMethod]
        public void should_map_from_Model_wq_to_Entity_wq()
        {
            var theories = new List<WebApi.Models.RawWorldQuest>
            {
                new WebApi.Models.RawWorldQuest
                {
                    Id = 43964,
                    Name = "A Jarl's Feast",
                    Ending = 1512795600000,
                    Factions = new [] { 1948 },
                    Type = 2,
                    Zones = new [] { 7541 },
                    Rewards = new []
                    {
                        new WebApi.Models.WorldQuestReward
                        {
                            Id = 141923,
                            Qty = 1
                        }
                    }
                },
                new WebApi.Models.RawWorldQuest
                {
                    Id = 42969,
                    Name = "A Spy in Our Midst",
                    Ending = 1512831600000,
                    Factions = new [] { 1859, 1948 },
                    Type = 1,
                    Zones = new [] { 7542, 7541 },
                    Rewards = new []
                    {
                        new WebApi.Models.WorldQuestReward
                        {
                            Id = 139917,
                            Qty = 1
                        },
                        new WebApi.Models.WorldQuestReward
                        {
                            Id = 139917,
                            Qty = 1
                        },
                        new WebApi.Models.WorldQuestReward
                        {
                            Id = 139917,
                            Qty = 1
                        }
                    }
                }
            };

            foreach (var theory in theories)
            {
                var result = Mapper.Map<Entities.WorldQuest>(theory);
            }
        }

        [TestMethod]
        public void should_map_epoch_to_standard_datetime()
        {
            var source = new WebApi.Models.RawWorldQuest
            {
                Id = 44802,
                Name = "Ancient Guidance",
                Ending = 1512831600000
            };

            var result = Mapper.Map<Entities.WorldQuest>(source);

            Assert.AreEqual(1, result.Instances.Count);
            Assert.AreEqual(new System.DateTime(2017, 12, 9, 9, 0, 0), result.Instances.First().ExpiresOn);
        }

        [TestMethod]
        public void should_map_to_only_one_zone()
        {
            var source = new WebApi.Models.RawWorldQuest
            {
                Id = 48729,
                Ending = 1512788400000,
                Factions = new int[] { },
                Type = 2,
                Zones = new[] { 8701, 7558 }
            };

            var result = Mapper.Map<Entities.WorldQuest>(source);

            Assert.AreEqual(8701, result.Zone.ZoneId);
        }

        [TestMethod]
        public void should_map_rewards()
        {
            var source = new WebApi.Models.RawWorldQuest
            {
                Id = 44802,
                Name = "Ancient Guidance",
                Ending = 1512831600000,
                Rewards = new[]
                {
                    new WebApi.Models.WorldQuestReward
                    {
                        Id = 0,
                        Qty = 0
                    },
                    new WebApi.Models.WorldQuestReward
                    {
                        Id = 1,
                        Qty = 1
                    },
                    new WebApi.Models.WorldQuestReward
                    {
                        Id = 2,
                        Qty = 2
                    },
                    new WebApi.Models.WorldQuestReward
                    {
                        Id = 3,
                        Qty = 3
                    },
                }
            };

            var result   = Mapper.Map<Entities.WorldQuest>(source);
            var instance = result.Instances.FirstOrDefault();

            Assert.AreEqual(1, result.Instances.Count);
            Assert.AreEqual(4, instance.Rewards.Count);

            var count   = 0;
            var rewards = instance.Rewards;

            foreach (var reward in rewards)
            {
                Assert.AreEqual(count, reward.ItemId);
                count++;
            }
        }
    }
}
