using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMog.Business;
using TMog.Entities;

namespace TMog.UnitTests.Business.SlotManagerTests
{
    [TestClass]
    public class When_calling_FromRange
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
