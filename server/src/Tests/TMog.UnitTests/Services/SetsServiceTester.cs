using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMog.Data;
using TMog.Services;
using TMog.WowheadApi;

namespace TMog.UnitTests.Services
{
    public class SetsServiceTester : SystemUnderTestHelper<SetsService>
    {
        protected override SetsService GetSystem()
        {
            return new SetsService(new TMogDatabase(), new WowheadProvider());
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
        public class When_creating_set : SetsServiceTester
        {
            [TestMethod]
            public async Task should_create_correctly()
            {
                await system.Create(1119);
            }
        }
    }
}
