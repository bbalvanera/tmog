using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TMog.Business;
using TMog.Data;
using TMog.Entities;
using TMog.WowheadApi;

namespace TMog.Services
{
    public class SetsService : ISetsService
    {
        private readonly TMogDatabase db;
        private readonly IWowheadProvider wowheadProvider;

        public SetsService(TMogDatabase db, IWowheadProvider wowheadProvider)
        {
            this.db = db;
            this.wowheadProvider = wowheadProvider;
        }

        public async Task<IEnumerable<Set>> GetAll()
        {
            return await db.Sets.OrderBy(s => s.Name).ToListAsync();
        }

        public async Task<Set> GetById(int setId)
        {
            return await db.Sets
                .Include(s => s.Items.Select(i => i.Source.Zone.Location))
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
            var set = db.Sets.Find(setId);

            if (set == null)
            {
                throw new ArgumentException("Invalid set", nameof(setId));
            }

            SlotManager slotMan = new SlotManager(set.Slots);
            slotMan.Mark(slot, completed);
            set.Slots = slotMan.ToString();

            await db.SaveChangesAsync();
        }

        public async Task Delete(int setId)
        {
            if (await db.Sets.AnyAsync(set => set.SetId == setId))
            {
                db.Entry(new Set { SetId = setId }).State = EntityState.Deleted;
            }

            await db.SaveChangesAsync();
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

            db.Sets.Add(set);
            await db.SaveChangesAsync();

            return set;
        }

        private Item GetOrCreateItem(IWowheadItem i)
        {
            return db.Items.Find(i.Id) ?? Mapper.Map<Item>(i);
        }

        private Source GetOrCreateSource(IEnumerable<IWowheadItemSource> sources)
        {
            var source = sources.FirstOrDefault();

            if (source == null)
            {
                return null;
            }

            // search in memory first
            var existing = db.Sources.Local.FirstOrDefault(s => source.Type == (int)s.Type &&
                                                                source.SubType == (int?)s.SubType &&
                                                                source.WowheadId == s.WowheadId &&
                                                                source.DropLevel == (int?)s.DropLevel &&
                                                                source.Zone == s.ZoneId);

            if (existing == null)
            {
                // search in db
                existing = db.Sources.FirstOrDefault(s => source.Type == (int)s.Type &&
                                                          source.SubType == (int?)s.SubType &&
                                                          source.WowheadId == s.WowheadId &&
                                                          source.DropLevel == (int?)s.DropLevel &&
                                                          source.Zone == s.ZoneId);

                // source does not exist and needs to be created
                if (existing == null)
                {
                    var newSource = Mapper.Map<Source>(source);
                    newSource.Zone = GetOrCreateZone(source.Zone);

                    db.Sources.Add(newSource);
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
                result = db.Zones.Find(zoneId.Value) ?? db.Zones.Add(new Zone { ZoneId = zoneId });
            }

            return result;
        }
    }
}
