using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMog.Business;
using TMog.Entities;

namespace TMog.UnitTests.Business
{
    public class SlotManagerTester
    {
        [TestClass]
        public class When_newing
        {
            [TestMethod]
            public void should_return_inactive_for_all()
            {
                var slotManager = new SlotManager();

                // when no slots are set, all slots are marked with an X.
                Assert.AreEqual("XXXXXXXXXXX", slotManager.ToString());
            }

            [TestMethod]
            public void should_new_from_existing_string()
            {
                var slotManager = new SlotManager("000000000000");

                Assert.AreEqual("000000000000", slotManager.ToString());
            }

            [TestMethod]
            public void should_report_correct_slots()
            {
                var slotManager = new SlotManager("X0X0X0000X0X");

                Assert.IsTrue(slotManager.ContainsSlot(SlotType.Head), "no head");
                Assert.IsTrue(slotManager.ContainsSlot(SlotType.Shoulder), "no shoulder");
                Assert.IsTrue(slotManager.ContainsSlot(SlotType.Chest), "no chest");
                Assert.IsTrue(slotManager.ContainsSlot(SlotType.Waist), "no waist");
                Assert.IsTrue(slotManager.ContainsSlot(SlotType.Legs), "no legs");
                Assert.IsTrue(slotManager.ContainsSlot(SlotType.Feet), "no feet");
                Assert.IsTrue(slotManager.ContainsSlot(SlotType.Hands), "no hands");
                Assert.IsFalse(slotManager.ContainsSlot(SlotType.Wrist), "yes wrist?");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void should_throw_ArgumentNullException_if_null()
            {
                var slotManager = new SlotManager(null);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void should_throw_ArgumentNullException_if_empty()
            {
                var slotManager = new SlotManager("  ");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void should_throw_ArgumentException_if_invalid_length()
            {
                var slotManager = new SlotManager("XXXX");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void should_throw_ArgumentException_if_invalid_string()
            {
                var slotManager = new SlotManager("invalid");
            }
        }

        [TestClass]
        public class When_setting_slots
        {
            private static SlotManager GetSystem(string existing = null)
            {
                return existing == null ? new SlotManager() : new SlotManager(existing);
            }

            [TestMethod]
            public void should_get_active_count_after_new()
            {
                var slotManager = GetSystem();

                Assert.AreEqual(0, slotManager.ActiveSlotCount);
            }

            [TestMethod]
            public void should_get_correct_active_count_after_setting_slot()
            {
                var slotManager = GetSystem();

                slotManager[SlotType.Chest] = true;
                slotManager[SlotType.Feet] = false;

                Assert.AreEqual(2, slotManager.ActiveSlotCount);
            }

            [TestMethod]
            public void should_get_completed_count_after_setting_slot()
            {
                var slotManager = GetSystem();

                slotManager[SlotType.Chest] = true;
                slotManager[SlotType.Feet] = false;

                Assert.AreEqual(1, slotManager.CompletedSlotCount);
            }

            [TestMethod]
            public void should_get_completed_count_after_new_from_existing()
            {
                var slotManager = GetSystem("000000001110");

                Assert.AreEqual(3, slotManager.CompletedSlotCount);
            }

            [TestMethod]
            public void should_get_active_count_after_new_from_existing()
            {
                var slotManager = GetSystem("01XXXXXXXXXX");

                Assert.AreEqual(2, slotManager.ActiveSlotCount);
            }
        }

        [TestClass]
        public class Whend_calling_FromRange
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void should_throw_ArgumentNullException_if_null()
            {
                var result = SlotManager.FromRange(null);
            }

            [TestMethod]
            public void should_convert_from_range()
            {
                var range = Enum.GetValues(typeof(SlotType)).Cast<int>();

                var result = SlotManager.FromRange(range);

                Assert.AreEqual("000000000000", result);
            }

            [TestMethod]
            public void should_convert_from_any_range()
            {
                var range = new int[] { 0, 1, 2, 3, 4 };

                var result = SlotManager.FromRange(range);

                Assert.AreEqual("00000XXXXXXX", result);
            }
        }
    }
}
