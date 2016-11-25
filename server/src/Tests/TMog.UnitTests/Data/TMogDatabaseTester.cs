using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMog.Data;
using TMog.Entities;

namespace TMog.UnitTests.Data
{
    public class TMogDatabaseTester
    {
        [TestClass]
        public class When_creating_database : TMogDatabaseTesterHelper
        {
            [TestMethod]
            public void Should_create_database()
            {
                system.Sets.Select(i => i);
            }
        }

        [TestClass]
        public class When_working_with_sets : TMogDatabaseTesterHelper
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

                system.Sets.Add(set);
                var result = system.SaveChanges();

                Assert.AreNotEqual(0, result);
                Assert.AreEqual(EntityState.Unchanged, system.Entry(set).State);
            }

            [TestMethod]
            public void Should_save_items_and_set()
            {
                var set = new Set
                {
                    SetId = ReservedIds.GetNexIdFor(ReservedIds.Group.Sets),
                    Name  = "New Set 2",
                    Items = new List<Item>
                    {
                        new Item
                        {
                            ItemId   = ReservedIds.GetNexIdFor(ReservedIds.Group.Items),
                            Slot     = SlotType.Chest,
                            Quality  = QualityType.Artifact,
                            Acquired = false,
                            Hidden   = false
                        }
                    }
                };

                system.Sets.Add(set);
                var result = system.SaveChanges();

                Assert.AreEqual(EntityState.Unchanged, system.Entry(set).State);
                Assert.IsTrue(set.Items.All(i => system.Entry(i).State == EntityState.Unchanged));
            }
        }

        public class TMogDatabaseTesterHelper : SystemUnderTestHelper<TMogDatabase>
        {
            protected static readonly TMogDatabase db = new TMogDatabase();

            protected override TMogDatabase GetSystem()
            {
                return new TMogDatabase();
            }
        }

        internal static class ReservedIds
        {
            private static int current = 100000;
            private static readonly Dictionary<Group, List<int>> reservedIds = new Dictionary<Group, List<int>>();

            public enum Group
            {
                Sets,
                Items,
                Sources,
                Zones
            }

            public static int GetNexIdFor(Group group)
            {
                SafeAdd(group, ++current);
                return current;
            }

            public static List<int> GetReservedIds(Group group)
            {
                return reservedIds[group];
            }

            private static void SafeAdd(Group group, int id)
            {
                if (!reservedIds.ContainsKey(group))
                {
                    reservedIds.Add(group, new List<int>());
                }

                reservedIds[group].Add(id);
            }
        }
    }
}
