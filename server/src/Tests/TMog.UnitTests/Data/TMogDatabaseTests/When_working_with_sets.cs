using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMog.Entities;

namespace TMog.UnitTests.Data.TMogDatabaseTests
{
    [TestClass]
    public class When_working_with_sets : TMogDatabaseTestHelper
    {
        [ClassCleanup]
        public static void CleanUp()
        {
            foreach (var id in ReservedIds.GetReservedIds(ReservedIds.Group.Sets))
            {
                db.Entry(new Set { SetId = id }).State = EntityState.Deleted;
            }

            foreach (var id in ReservedIds.GetReservedIds(ReservedIds.Group.Items))
            {
                db.Entry(new Item { ItemId = id }).State = EntityState.Deleted;
            }

            db.SaveChanges();
        }

        [TestMethod]
        public void should_insert_new_set()
        {
            var set = new Set
            {
                SetId = ReservedIds.GetNexIdFor(ReservedIds.Group.Sets),
                Name = "New Set"
            };

            Subject.Sets.Add(set);
            var result = Subject.SaveChanges();

            Assert.AreNotEqual(0, result);
            Assert.AreEqual(EntityState.Unchanged, Subject.Entry(set).State);
        }

        [TestMethod]
        public void Should_save_items_and_set()
        {
            var set = new Set
            {
                SetId = ReservedIds.GetNexIdFor(ReservedIds.Group.Sets),
                Name = "New Set 2",
                Items = new List<Item>
                    {
                        new Item
                        {
                            ItemId   = ReservedIds.GetNexIdFor(ReservedIds.Group.Items),
                            Slot     = SlotType.Chest,
                            Quality  = QualityType.Artifact
                        }
                    }
            };

            Subject.Sets.Add(set);
            var result = Subject.SaveChanges();

            Assert.AreEqual(EntityState.Unchanged, Subject.Entry(set).State);
            Assert.IsTrue(set.Items.All(i => Subject.Entry(i).State == EntityState.Unchanged));
        }
    }
}
