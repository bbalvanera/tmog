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
        private readonly ITMogDatabase db;
        private readonly IWowheadProvider wowheadProvider;
        public SetsService(ITMogDatabase db, IWowheadProvider wowheadProvider)
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
            return await db.Sets.FirstOrDefaultAsync(s => s.SetId == setId);
        }

        public async Task<Set> Create(int setId)
        {
            if (setId <= 0)
            {
                throw new ArgumentException($"Invalid set id: {setId}");
            }

            var existing = db.Sets.Find(setId);
            if (existing != null)
            {
                throw new DuplicateEntityException();
            }

            return await GetSet(setId);
        }

        private async Task<Set> GetSet(int setId)
        {
            IWowheadSet wowheadSet = await wowheadProvider.GetSetById(setId);

            if (wowheadSet == null)
            {
                return null;
            }

            Set set   = new Set();
            set.SetId = setId;
            set.Name  = wowheadSet.Name;
            set.Slots = SlotManager.FromRange(wowheadSet.Items.Select(i => i.Slot).Distinct());
            set.Items = wowheadSet.Items.Select(i =>
            {
                Item item    = Map(i);
                item.Sources = GetOrCreateSourceIfNotExists(i.Sources);

                return item;
            }).ToList();

            db.Sets.Add(set);
            //db.SaveChanges();

            return set;
        }

        private Item Map(IWowheadItem wowheadItem)
        {
            return new Item
            {
                ItemId        = wowheadItem.Id,
                Name          = wowheadItem.Name,
                Slot          = (SlotType)wowheadItem.Slot,
                Quality       = (QualityType)wowheadItem.Quality.Value,
                Class         = wowheadItem.Class,
                Subclass      = wowheadItem.Subclass,
                iLevel        = wowheadItem.iLevel,
                RequiredLevel = wowheadItem.RequiredLevel,
                DisplayId     = wowheadItem.DisplayId,
                Flags         = wowheadItem.Flags,
                BuyPrice      = wowheadItem.BuyPrice,
                SellPrice     = wowheadItem.SellPrice,
                Acquired      = false
            };
        }

        private ICollection<Source> GetOrCreateSourceIfNotExists(IEnumerable<IWowheadItemSource> sources)
        {
            IList<Source> list = new List<Source>();
            if (sources.Any())
            {
                foreach (var source in sources)
                {
                    // search in memory first
                    var existing = db.Sources.Local.FirstOrDefault(s => source.Type      == (int)s.Type &&
                                                                        source.SubType   == (int?)s.SubType &&
                                                                        source.WowheadId == s.WowheadId &&
                                                                        source.DropLevel == (int?)s.DropLevel);

                    if (existing == null)
                    {
                        // search in db
                        existing = db.Sources.FirstOrDefault(s => source.Type      == (int)s.Type &&
                                                                  source.SubType   == (int?)s.SubType &&
                                                                  source.WowheadId == s.WowheadId &&
                                                                  source.DropLevel == (int?)s.DropLevel);

                        // source does not exist and needs to be created
                        if (existing == null)
                        {
                            var newSource = new Source
                            {
                                Type        = (SourceType)source.Type,
                                SubType     = (SourceSubType?)source.SubType,
                                Description = source.Name,
                                DropLevel   = (DropLevel?)source.DropLevel,
                                WowheadId   = source.WowheadId,
                                Zone        = GetOrCreateZone(source.Zone)
                            };

                            db.Sources.Add(newSource);
                            existing = newSource;
                        }
                    }

                    list.Add(existing);
                }

            }
            return list;
        }

        private Zone GetOrCreateZone(int? zoneId)
        {
            Zone result = null;
            if (zoneId.HasValue)
            {
                Zone zone = db.Zones.Local.FirstOrDefault(z => z.ZoneId == zoneId);
                if (zone == null)
                {
                    zone = db.Zones.FirstOrDefault(z => z.ZoneId == zoneId);
                    if (zone == null)
                    {
                        zone = new Zone
                        {
                            ZoneId = new int?(zoneId.Value)
                        };

                        db.Zones.Add(zone);
                    }
                }
                result = zone;
            }
            return result;
        }
    }
}
