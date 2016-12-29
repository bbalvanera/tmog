using TMog.Data;
using TMog.Services;
using TMog.WowheadApi;

namespace TMog.UnitTests.Services.ItemsServiceTests
{
    public class ItemsServiceTestHelper
    {
        protected ItemsService GetSubject(TMogDatabase database, IWowheadProvider wowheadProvider = null)
        {
            return new ItemsService(database, wowheadProvider);
        }
    }
}
