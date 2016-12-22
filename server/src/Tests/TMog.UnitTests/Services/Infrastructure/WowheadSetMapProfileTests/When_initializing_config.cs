using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TMog.UnitTests.Services.Infrastructure.WowheadSetMapProfileTests
{
    [TestClass]
    public class When_initializing_config
    {
        [TestMethod]
        public void should_be_valid()
        {
            Mapper.Initialize(config => config.AddProfiles("TMog"));
            Mapper.AssertConfigurationIsValid();
        }
    }
}
