using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TMog.Data;
using TMog.Entities;
using TMog.Properties;

namespace TMog.Services
{
    public class FactionsService : IFactionsService
    {
        private readonly ITMogDatabase tmogContext;

        public FactionsService(ITMogDatabase tmogContext)
        {
            this.tmogContext = tmogContext;
        }

        public async Task AddAll(IEnumerable<Faction> factions)
        {
            if (factions == null || factions.Count() == 0)
            {
                return;
            }

            using (var transaction = tmogContext.Database.BeginTransaction())
            {
                try
                {
                    tmogContext.Database.ExecuteSqlCommand(Resources.ClearFactions);
                    tmogContext.DisableChangeDetection();

                    var count = 0;
                    var commitCount = 1000;
                    foreach (var faction in factions)
                    {
                        tmogContext.Factions.Add(faction);
                        count++;

                        if (count % commitCount == 0)
                        {
                            await tmogContext.SaveChangesAsync();
                        }
                    }

                    await tmogContext.SaveChangesAsync();

                    transaction.Commit();
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
                finally
                {
                    tmogContext.EnableChangeDetection();
                }
            }
        }
    }
}
