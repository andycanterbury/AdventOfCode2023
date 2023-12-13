using Utilities;

namespace Utilities.Tests
{
    public class LCMTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var list = new List<long> { 13207, 22199, 14893, 16579, 20513, 12083 };

            var result = LeastCommonMultiple.LCM(list);

            Assert.AreEqual(10241191004509, result);
        }
    }
}