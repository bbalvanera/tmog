using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TMog.UnitTests.Data.TMogDatabaseTests
{
    [TestClass]
    public class When_creating_database : TMogDatabaseTestHelper
    {
        [TestMethod]
        public void Should_create_database()
        {
            Subject.Sets.Select(i => i);
        }
    }
}
