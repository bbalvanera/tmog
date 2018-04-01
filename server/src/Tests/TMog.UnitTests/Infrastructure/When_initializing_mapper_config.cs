using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMog.UnitTests.Infrastructure
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
