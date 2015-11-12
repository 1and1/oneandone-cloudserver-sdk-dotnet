using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Respones.SharedStorages;

namespace OneAndOne.UnitTests.SharedStorages
{
    [TestClass]
    public class ServersSharedStorageTest
    {
        static OneAndOneClient client = new OneAndOneClient();
        [TestMethod]
        public void GetSharedStoragesServers()
        {
            Random random = new Random();
            var sharedStorages = client.SharedStorages.Get();
            var sharedStorage = sharedStorages[random.Next(sharedStorages.Count - 1)];
            var result = client.SharedStorages.GetSharedStorageServers(sharedStorage.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowSharedStoragesServers()
        {
            var sharedStorages = client.SharedStorages.Get();
            var sharedStorage = sharedStorages[0];
            foreach (var item in sharedStorages)
            {
                if (item.Servers != null && item.Servers.Count > 0)
                {
                    sharedStorage = item;
                    break;
                }
            }
            if (sharedStorage.Servers != null && sharedStorage.Servers.Count > 0)
            {
                var result = client.SharedStorages.ShowSharedStoragesServer(sharedStorage.Id, sharedStorage.Servers[0].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }

        [TestMethod]
        public void CreateSharedStoragesServers()
        {
            Random random = new Random();
            var servers = client.Servers.Get().Where(ser => ser.Name.Contains("ServerTest")).ToList();
            var sharedStorages = client.SharedStorages.Get();
            var sharedStorage = sharedStorages[random.Next(sharedStorages.Count - 1)];

            if (servers.Count > 0)
            {
                var serverstoAdd = new System.Collections.Generic.List<POCO.Requests.SharedStorages.Server>();
                serverstoAdd.Add(new POCO.Requests.SharedStorages.Server()
                    {
                        Id = servers[random.Next(0,servers.Count-1)].Id,
                        Rights = StorageServerRights.RW
                    });
                var result = client.SharedStorages.CreateServerSharedStorages(new POCO.Requests.SharedStorages.AttachSharedStorageServerRequest()
                {
                    Servers = serverstoAdd
                }, sharedStorage.Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }

        [TestMethod]
        public void DeleteSharedStoragesServers()
        {
            Random random = new Random();
            var sharedStorages = client.SharedStorages.Get().Where(str => str.Name.Contains("TestStorage")).ToList();
            SharedStoragesResponse sharedStorage = null;
            foreach (var item in sharedStorages)
            {
                if (item.Servers != null && item.Servers.Count > 0)
                {
                    sharedStorage = item;
                    break;
                }
            }
            if (sharedStorage != null && sharedStorage.Servers != null && sharedStorage.Servers.Count > 0)
            {
                var result = client.SharedStorages.DeleteSharedStoragesServer(sharedStorage.Id, sharedStorage.Servers[0].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }
    }
}
