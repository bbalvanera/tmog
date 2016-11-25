using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TMog.UnitTests
{
    public abstract class SystemUnderTestHelper<T>
    {
        protected T system;

        [TestInitialize]
        public virtual void TestInitialize()
        {
            system = GetSystem();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            system = default(T);
        }

        protected abstract T GetSystem();
    }
}
