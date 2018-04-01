using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TMog.Data;
using TMog.Entities;
using TMog.Services.Exceptions;
using TMog.WowheadApi;

namespace TMog.Services
{
    public class WorldQuestsService : IWorldQuestsService
    {
        private readonly ITMogDatabase context;
        private readonly IWowheadProvider wowheadProvider;

        public WorldQuestsService(ITMogDatabase context, IWowheadProvider wowheadProvider)
        {
            this.context = context;
            this.wowheadProvider = wowheadProvider;
        }

        public async Task<IEnumerable<WorldQuest>> GetActive()
        {
            var query = await context.WorldQuests
                .Where(wq => wq.Instances.Any(i => i.ExpiresOn >= DateTime.Now))
                .Include(wq => wq.Zone)
                .Include(wq => wq.Factions)
                .Include(wq => wq.Instances.Select(i => i.Rewards))
                .ToListAsync();

            var result = query.Select(wq => new WorldQuest
            {
                QuestId   = wq.QuestId,
                Name      = wq.Name,
                Side      = wq.Side,
                Category  = wq.Category,
                Factions  = wq.Factions,
                Zone      = wq.Zone,
                Instances = wq.Instances.Where(i => i.ExpiresOn >= DateTime.Now).ToList()
            });


            return result;
        }

        public async Task<int> Save(IEnumerable<WorldQuest> worldQuests)
        {
            if (worldQuests == null || worldQuests.Count() == 0)
            {
                return 0;
            }

            using (var transaction = context.Database.BeginTransaction())
            {
                using (new ChangeDetectionSupressor(context))
                {
                    try
                    {
                        var totalProcessed = await Process(worldQuests);
                        CreateSubmissionLog(worldQuests.Count(), totalProcessed);

                        await context.SaveChangesAsync();
                        
                        transaction.Commit();

                        return totalProcessed;
                    }
                    catch (DataException ex)
                    {
                        transaction.Rollback();

                        Exception inner = ex;
                        while (inner != null)
                        {
                            if (inner.Message.Contains("Cannot insert duplicate key"))
                            {
                                throw new DuplicateEntityException("Duplicate key detected");
                            }

                            inner = inner.InnerException;
                        }

                        throw new ServiceException("Unhandled exception in FactionsService.AddAll", ex);
                    }
                    catch (Exception ex)
                    {
                        throw new ServiceException("Unhandled exception in FactionsService.AddAll", ex);
                    }
                }
            }
        }

        private async Task<int> Process(IEnumerable<WorldQuest> worldQuests)
        {
            var wqMan = new WorldQuestManager(context, wowheadProvider);
            var totalAdded = 0;

            foreach (var wq in worldQuests)
            {
                await wqMan.Update(wq);

                if (wqMan.Exists(wq))
                {
                    var instance = wq.Instances.Single();

                    if (!wqMan.ExistsInstance(instance))
                    {
                        context.WordQuestInstances.Add(instance);
                        totalAdded++;
                    }
                }
                else
                {
                    

                    context.WorldQuests.Add(wq);
                    totalAdded++;
                }
            }

            return totalAdded;
        }

        private void CreateSubmissionLog(int totalSubmitted, int totalProcessed)
        {
            var submissionLog = new WorldQuestsSubmissionLog
            {
                SubmissionDate = DateTime.Now,
                SubmitCount    = totalSubmitted,
                ProcessCount   = totalProcessed
            };

            context.WorldQuestsSubmissionLogs.Add(submissionLog);
        }
    }
}
