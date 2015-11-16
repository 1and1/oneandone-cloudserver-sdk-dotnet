﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.Servers;

namespace OneAndOne.UnitTests.PublicIPs
{
    [TestClass]
    public class PublicIPsTest
    {
        static OneAndOneClient client = new OneAndOneClient();
        [TestMethod]
        public void GetPublicIPs()
        {
            var result = client.PublicIPs.Get();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void ShowPublicIP()
        {
            Random random = new Random();

            var publicIps = client.PublicIPs.Get();
            var publicIp = publicIps[random.Next(publicIps.Count - 1)];

            var result = client.PublicIPs.Show(publicIp.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void CreatePublicIP()
        {
            Random random = new Random();
            var result = client.PublicIPs.Create(new POCO.Requests.PublicIPs.CreatePublicIPRequest()
                {
                    ReverseDns = random.Next(10, 99) + "netTest.net",
                    Type = IPType.IPV4
                });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void UpdatePublicIP()
        {
            Random random = new Random();
            var publicIps = client.PublicIPs.Get().Where(str => str.ReverseDns != null && str.ReverseDns.Contains("netTest")).ToList();
            var publicIp = publicIps[random.Next(publicIps.Count - 1)];
            var result = client.PublicIPs.Update(random.Next(10, 99) + "updateTest.net", publicIp.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void DeletePublicIP()
        {
            Random random = new Random();
            var publicIps = client.PublicIPs.Get().Where(str => str.ReverseDns != null && (str.ReverseDns.Contains("netTest") || str.ReverseDns.Contains("updateTest"))).ToList();
            var publicIp = publicIps[random.Next(publicIps.Count - 1)];
            var result = client.PublicIPs.Delete(publicIp.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void DeleteAllTESTPublicIP()
        {
            Random random = new Random();
            var publicIps = client.PublicIPs.Get().Where(str => str.ReverseDns != null && (str.ReverseDns.Contains("netTest") || str.ReverseDns.Contains("updateTest"))).ToList();
            foreach (var item in publicIps)
            {
                var result = client.PublicIPs.Delete(item.Id);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }
    }
}
