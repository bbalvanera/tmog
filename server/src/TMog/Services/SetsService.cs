using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TMog.Business;
using TMog.Data;
using TMog.Entities;
using TMog.WowheadApi;

namespace TMog.Services
{
    public class SetsService : ISetsService
    {
        private readonly TMogDatabase tmogContext;
        private readonly IWowheadProvider wowheadProvider;

        public SetsService(TMogDatabase tmogContext, IWowheadProvider wowheadProvider)
        {
            this.tmogContext = tmogContext;
            this.wowheadProvider = wowheadProvider;
        }

        public async Task<IEnumerable<Set>> GetAll() => await tmogContext.Sets.OrderBy(s => s.Name).ToListAsync();

        public async Task<Set> GetById(int setId)
        {
            return await tmogContext.Sets
                .Include(s => s.Items.Select(i => i.Source.Zone.Parent))
                .FirstOrDefaultAsync(s => s.SetId == setId);
        }

        public async Task<Set> Create(int setId)
        {
            if (setId <= 0)
            {
                throw new ArgumentException($"Invalid set id: {setId}");
            }

            var existing = await GetById(setId);
            if (existing != null)
            {
                return existing;
            }

            return await GetSet(setId);
        }

        public async Task MarkSetSlotCompletionStatus(int setId, SlotType slot, bool completed)
        {
            var set = tmogContext.Sets.Find(setId);

            if (set == null)
            {
                throw new ArgumentException("Invalid set", nameof(setId));
            }

            SlotManager slotMan = new SlotManager(set.Slots);
            slotMan.Mark(slot, completed);
            set.Slots = slotMan.ToString();

            await tmogContext.SaveChangesAsync();
        }

        public async Task Delete(int setId)
        {
            if (await tmogContext.Sets.AnyAsync(set => set.SetId == setId))
            {
                tmogContext.Entry(new Set { SetId = setId }).State = EntityState.Deleted;
            }

            await tmogContext.SaveChangesAsync();
        }

        private async Task<Set> GetSet(int setId)
        {
            IWowheadSet wowheadSet = await wowheadProvider.GetSetById(setId);

            if (wowheadSet == null)
            {
                throw new ArgumentException("Invalid set id", "setId");
            }

            var set = Mapper.Map<Set>(wowheadSet);
            set.Items = wowheadSet.Items.Select(i =>
            {
                Item item   = GetOrCreateItem(i);
                item.Source = GetOrCreateSource(i.Sources);

                return item;
            }).ToList();

            tmogContext.Sets.Add(set);
            await tmogContext.SaveChangesAsync();

            return set;
        }

        private Item GetOrCreateItem(IWowheadItem i)
        {
            return tmogContext.Items.Find(i.Id) ?? Mapper.Map<Item>(i);
        }

        private Source GetOrCreateSource(IEnumerable<IWowheadItemSource> sources)
        {
            var source = sources.FirstOrDefault();

            if (source == null)
            {
                return null;
            }

            // search in memory first
            var existing = tmogContext.Sources.Local.FirstOrDefault(s => source.Type == (int)s.Type &&
                                                                source.SubType == (int?)s.SubType &&
                                                                source.WowheadId == s.WowheadId &&
                                                                source.DropLevel == (int?)s.DropLevel &&
                                                                source.Zone == s.ZoneId);

            if (existing == null)
            {
                // search in db
                existing = tmogContext.Sources.FirstOrDefault(s => source.Type == (int)s.Type &&
                                                          source.SubType == (int?)s.SubType &&
                                                          source.WowheadId == s.WowheadId &&
                                                          source.DropLevel == (int?)s.DropLevel &&
                                                          source.Zone == s.ZoneId);

                // source does not exist and needs to be created
                if (existing == null)
                {
                    var newSource = Mapper.Map<Source>(source);
                    newSource.Zone = GetOrCreateZone(source.Zone);

                    tmogContext.Sources.Add(newSource);
                    existing = newSource;
                }
            }

            return existing;
        }

        private Zone GetOrCreateZone(int? zoneId)
        {
            Zone result = null;
            if (zoneId.HasValue)
            {
                result = tmogContext.Zones.Find(zoneId.Value) ?? tmogContext.Zones.Add(new Zone { ZoneId = zoneId });
            }

            return result;
        }
    }
}
