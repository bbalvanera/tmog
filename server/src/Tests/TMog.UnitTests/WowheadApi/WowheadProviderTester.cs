using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMog.WowheadApi;

namespace TMog.UnitTests.WowheadApi
{
    public static class WowheadProviderTester
    {
        [TestClass]
        public class When_getting_by_id : WowheadProviderTesterHelper
        {
            [TestMethod]
            public async Task should_return_null_if_invalid_id()
            {
                var result = await Subject.GetItemById(0);

                Assert.IsNull(result);    
            }

            [TestMethod]
            public async Task should_get_gear_by_id()
            {
                var itemid = 27447;

                var result = await Subject.GetItemById(itemid);

                Assert.IsNotNull(result);
                Assert.IsNull(result.BuyPrice);
                Assert.AreEqual(4, result.Class);
                Assert.AreEqual(42865, result.DisplayId);
                Assert.AreEqual(8192, result.Flags);
                Assert.AreEqual(itemid, result.Id);
                Assert.AreEqual("Bracers of Just Rewards", result.Name);
                Assert.AreEqual(3, result.Quality);
                Assert.AreEqual(70, result.RequiredLevel);
                Assert.AreEqual(29989, result.SellPrice);
                Assert.AreEqual(9, result.Slot);
                Assert.AreEqual(1, result.Sources.Count());
                Assert.AreEqual(4, result.Subclass);
                Assert.AreEqual(115, result.iLevel);

                var source = result.Sources.First();
                Assert.AreEqual(-2, source.DropLevel);
                Assert.AreEqual("Watchkeeper Gargolmar", source.Name);
                Assert.AreEqual(1, source.SubType);
                Assert.AreEqual(2, source.Type);
                Assert.AreEqual(17306, source.WowheadId);
                Assert.AreEqual(3562, source.Zone);
            }

            [TestMethod]
            public async Task should_get_non_gear_item_by_id()
            {
                var itemid = 122558;

                var result = await Subject.GetItemById(itemid);

                Assert.IsNotNull(result);
            }

            [TestMethod]
            public async Task should_return_null_if_not_found()
            {
                var itemid = 1337;

                var result = await Subject.GetItemById(itemid);

                Assert.IsNull(result);
            }
        }

        [TestClass]
        public class When_getting_by_set_id : WowheadProviderTesterHelper
        {
            [TestMethod]
            public async Task should_get_by_set_id()
            {
                var system = new WowheadProvider();

                var result = await system.GetSetById(392);

                Assert.IsNotNull(result);
                Assert.AreEqual(12, result.Items.Count());
                Assert.IsTrue(result.Items.All(item => item.Id != 0));
            }

            [TestMethod]
            public async Task should_return_null_if_not_exists()
            {
                var system = new WowheadProvider();

                var result = await system.GetSetById(5844);

                Assert.IsNull(result);
            }

        }

        public class WowheadProviderTesterHelper : SystemUnderTestHelper<WowheadProvider>
        {
            protected override WowheadProvider GetSubject()
            {
                return new WowheadProvider();
            }
        }
    }
}
