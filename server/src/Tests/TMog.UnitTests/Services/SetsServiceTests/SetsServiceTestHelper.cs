﻿using TMog.Data;
using TMog.Services;
using TMog.WowheadApi;

namespace TMog.UnitTests.Services.SetsServiceTests
{
    public class SetsServiceTestHelper
    {

        protected SetsService GetSubject(TMogDatabase database, IWowheadProvider wowheadProvider = null)
        {
            return new SetsService(database, wowheadProvider);
        }
    }
}
