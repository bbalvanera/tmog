using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMog.Business;
using TMog.Entities;

namespace TMog.UnitTests.Business.SlotManagerTests
{
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
}
