using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMog.Data;
using TMog.Entities;
using TMog.Services.Exceptions;
using TMog.WowheadApi;

namespace TMog.Services
{
    /// <summary>
    /// Provides methods to work with WorldQuests
    /// </summary>
    internal class WorldQuestManager
    {
        private readonly ITMogDatabase context;
        private readonly IWowheadProvider wowheadProvider;

        public WorldQuestManager(ITMogDatabase context, IWowheadProvider wowheadProvider)
        {
            this.context = context;
            this.wowheadProvider = wowheadProvider;
        }

        /// <summary>
        /// Update a WorldQuest by filling missing information. If WQ exists, no data is updated.
        /// </summary>
        /// <remarks>
        /// This method will update a WorldQuest by setting properties to existing object from database to avoid inserting duplicate data.
        /// If the given WorldQuest already exists, no data is updated.
        /// </remarks>
        /// <param name="wq"></param>
        /// <returns></returns>
        public async Task Update(WorldQuest wq)
        {
            if (wq == null)
            {
                return;
            }

            if (!Exists(wq))
            {
                FillZone(wq);
                FillFactions(wq);
            }

            if (!ExistsInstance(wq.Instances.Single()))
            {
                await FillInstance(wq);
            }
        }

        public bool ExistsInstance(WorldQuestInstance wqi)
        {
            return context.WordQuestInstances.Where(wq => wq.QuestId == wqi.QuestId && wq.ExpiresOn == wqi.ExpiresOn).Any();
        }

        public bool Exists(WorldQuest wq)
        {
            return context.WorldQuests.Find(wq.QuestId) != null;
        }

        private void FillZone(WorldQuest wq)
        {
            if (wq.Zone == null || wq.Zone.ZoneId == null)
            {
                return;
            }

            var zoneId       = wq.Zone.ZoneId.Value;
            var existingZone = GetZone(zoneId);

            wq.Zone = existingZone ?? throw new EntityNotFoundException($"Could not find Zone with id: {zoneId}.")
            {
                EntityName   = "Zone",
                ItemNotFound = $"ZoneId={zoneId}"
            };
        }

        private void FillFactions(WorldQuest wq)
        {
            if (wq.Factions == null || wq.Factions.Count == 0)
            {
                return;
            }

            var newFactions = new List<Faction>();
            foreach (var faction in wq.Factions)
            {
                var newFaction = GetFaction(faction.FactionId);

                if (newFaction == null)
                {
                    throw new EntityNotFoundException($"Could not find Faction with id: {faction.FactionId}.")
                    {
                        EntityName   = "Faction",
                        ItemNotFound = $"FactionId={faction.FactionId}"
                    };
                }

                newFactions.Add(newFaction);
            }

            wq.Factions = newFactions;
        }

        private async Task FillInstance(WorldQuest wq)
        {
            var newRewards = new List<Item>();
            var instances = wq.Instances;

            foreach (var instance in instances)
            {
                if (instance.Rewards == null || instance.Rewards.Count == 0)
                {
                    return;
                }

                foreach (var item in instance.Rewards)
                {
                    var newItem = await GetOrCreateItem(item.ItemId);

                    if (newItem == null)
                    {
                        throw new EntityNotFoundException($"Could not find Faction with id: {item.ItemId}.")
                        {
                            EntityName   = "Reward",
                            ItemNotFound = $"RewardId={item.ItemId}"
                        };
                    }

                    newRewards.Add(newItem);
                }

                instance.Rewards = newRewards;
            }
        }

        private Faction GetFaction(int factionId)
        {
            var existing = context.Factions.Local.FirstOrDefault(z => z.FactionId == factionId);

            if (existing == null)
            {
                existing = context.Factions.Find(factionId);
            }

            return existing;
        }

        private Zone GetZone(int zoneId)
        {
            var existing = context.Zones.Local.FirstOrDefault(z => z.ZoneId == zoneId);

            if (existing == null)
            {
                existing = context.Zones.Find(zoneId);
            }

            return existing;
        }

        private async Task<Item> GetOrCreateItem(int itemId)
        {
            var existing = context.Items.Local.FirstOrDefault(i => i.ItemId == itemId);

            if (existing == null)
            {
                existing = context.Items.Find(itemId);

                if (existing == null)
                {
                    var wowheadItem = await wowheadProvider.GetItemById(itemId);
                    if (wowheadItem != null)
                    {
                        existing = Mapper.Map<Item>(wowheadItem);
                    }
                }
            }

            return existing;
        }
    }
}
