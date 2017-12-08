using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TMog.UnitTests.Services.Infrastructure.WowheadSetMapProfileTests
{
    [TestClass]
    public class When_initializing_mapper_config
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Mapper.Initialize(config => config.AddProfiles("TMog", "TMog.WebApi"));
        }

        [TestMethod]
        public void should_be_valid()
        {
            Mapper.AssertConfigurationIsValid();
        }
    }
}
