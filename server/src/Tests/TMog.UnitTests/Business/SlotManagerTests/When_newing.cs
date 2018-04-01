using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMog.Common;
using TMog.Entities;

namespace TMog.UnitTests.Business.SlotManagerTests
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
            var slotManager = new SlotManager("XXXXXXXXXXXXX");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void should_throw_ArgumentException_if_invalid_string()
        {
            var slotManager = new SlotManager("invalid");
        }
    }
}
