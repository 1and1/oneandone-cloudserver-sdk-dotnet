using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;

namespace OneAndOne.UnitTests.DVDISO
{
    [TestClass]
    public class DVDIsosTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        [TestMethod]
        public void GetDVDISOs()
        {
            var result = client.DVDs.Get();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetDVDISO()
        {
            var dvds = client.DVDs.Get();

            var result = client.DVDs.Show(dvds[0].Id);

            Assert.IsNotNull(result);
        }
    }
}
