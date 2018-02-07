using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;

namespace OneAndOne.UnitTests.RecoveryAppliances
{
    [TestClass]
    public class RecoveryAppliancesTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        [TestMethod]
        public void GetRecoveryAppliances()
        {
            var result = client.RecoveryAppliances.Get();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetRecoveryAppliance()
        {
            var recoveryAppliances = client.RecoveryAppliances.Get();

            var result = client.RecoveryAppliances.Show(recoveryAppliances[0].Id);

            Assert.IsNotNull(result);
        }
    }
}
