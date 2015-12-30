using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO;

namespace OneAndOne.UnitTests.Usages
{
    [TestClass]
    public class UsagesTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance();
        [TestMethod]
        public void GetUsages()
        {
            var result = client.Usages.Get(PeriodType.LAST_24H);

            Assert.IsNotNull(result);
        }
    }
}
