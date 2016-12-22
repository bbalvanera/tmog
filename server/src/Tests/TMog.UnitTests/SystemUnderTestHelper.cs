using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TMog.UnitTests
{
    public abstract class SystemUnderTestHelper<T>
    {
        public T Subject { get; set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Subject = GetSubject();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            Subject = default(T);
        }

        protected abstract T GetSubject();
    }
}
