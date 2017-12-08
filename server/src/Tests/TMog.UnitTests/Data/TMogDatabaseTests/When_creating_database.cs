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
            // Select will force a connection to database
            Subject.Sets.Select(i => i); // expect no errors
        }
    }
}
