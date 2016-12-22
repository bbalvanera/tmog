using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
            return await db.Sets
                .Include(s => s.Items.Select(i => i.Sources.Select(src => src.Zone)))
                .FirstOrDefaultAsync(s => s.SetId == setId);
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
                return existing;
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

            var set = Mapper.Map<Set>(wowheadSet);
            set.Items = wowheadSet.Items.Select(i =>
            {
                Item item    = Mapper.Map<Item>(i);
                item.Sources = GetOrCreateSourceIfNotExists(i.Sources);

                return item;
            }).ToList();

            db.Sets.Add(set);
            db.SaveChanges();

            return set;
        }

        private List<Source> GetOrCreateSourceIfNotExists(IEnumerable<IWowheadItemSource> sources)
        {
            var list = new List<Source>();
            if (sources.Any())
            {
                foreach (var source in sources)
                {
                    // search in memory first
                    var existing = db.Sources.Local.FirstOrDefault(s => source.Type == (int)s.Type &&
                                                                        source.SubType == (int?)s.SubType &&
                                                                        source.WowheadId == s.WowheadId &&
                                                                        source.DropLevel == (int?)s.DropLevel);

                    if (existing == null)
                    {
                        // search in db
                        existing = db.Sources.FirstOrDefault(s => source.Type == (int)s.Type &&
                                                                  source.SubType == (int?)s.SubType &&
                                                                  source.WowheadId == s.WowheadId &&
                                                                  source.DropLevel == (int?)s.DropLevel);

                        // source does not exist and needs to be created
                        if (existing == null)
                        {
                            var newSource  = Mapper.Map<Source>(source);
                            newSource.Zone = GetOrCreateZone(source.Zone);

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
                result = db.Zones.Find(zoneId.Value) ?? db.Zones.Add(new Zone { ZoneId = zoneId });
            }
            return result;
        }
    }
}
