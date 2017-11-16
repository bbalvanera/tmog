using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace TMog.UnitTests.Data.TMogDatabaseTests
{
    [TestClass]
    public class When_creating_database : TMogDatabaseTestHelper
    {
        [TestMethod]
        public void Should_create_database()
        {
            // expect no errors here.
            Subject.Sets.Select(i => i);
        }
    }
}
