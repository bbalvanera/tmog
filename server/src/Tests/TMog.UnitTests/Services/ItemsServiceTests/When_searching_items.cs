using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TMog.Data;
using TMog.WowheadApi;

namespace TMog.UnitTests.Services.ItemsServiceTests
{
    [TestClass]
    public class When_searching_items : ItemsServiceTestHelper
    {
        [TestMethod]
        public async Task should_search_by_item_id()
        {
            var subject = GetSubject(new TMogDatabase(), new WowheadProvider());

            var result = await subject.SearchItems("16861");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task should_search_in_wowhead()
        {
            var subject = GetSubject(new TMogDatabase(), new WowheadProvider());

            var result = await subject.SearchItems("138319");

            Assert.IsNotNull(result);
        }
    }
}
