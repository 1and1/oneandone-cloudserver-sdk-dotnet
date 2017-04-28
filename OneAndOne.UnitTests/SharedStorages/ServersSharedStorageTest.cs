using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Response.SharedStorages;
using OneAndOne.POCO.Response.Servers;
using System.Collections.Generic;

namespace OneAndOne.UnitTests.SharedStorages
{
    [TestClass]
    public class ServersSharedStorageTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static public SharedStoragesResponse sharedStorage = null;
        static ServerResponse server = null;

        [ClassInitialize]
        static public void TestInit(TestContext context)
        {
            var datacenters = client.DataCenters.Get();
            var dc = datacenters.FirstOrDefault();
            sharedStorage = client.SharedStorages.Create(new POCO.Requests.SharedStorages.CreateSharedStorage()
            {
                Description = "description",
                Name = "Test server shared storage .net",
                Size = 50,
                DatacenterId = dc.Id
            });

            Config.waitSharedStorageReady(sharedStorage.Id);

            server = Config.CreateTestServer("share storage test .net");
            Config.waitServerReady(server.Id);
            server = client.Servers.Show(server.Id);

            //add server to a shared storage
            var serverstoAdd = new System.Collections.Generic.List<POCO.Requests.SharedStorages.Server>();
            serverstoAdd.Add(new POCO.Requests.SharedStorages.Server()
            {
                Id = server.Id,
                Rights = StorageServerRights.RW
            });
            var addServer = client.SharedStorages.CreateServerSharedStorages(new POCO.Requests.SharedStorages.AttachSharedStorageServerRequest()
            {
                Servers = serverstoAdd
            }, sharedStorage.Id);

            Assert.IsNotNull(addServer);
            Assert.IsNotNull(addServer.Id);
            //check the server has the shared storage access
            var serverResult = client.SharedStorages.ShowSharedStoragesServer(sharedStorage.Id, server.Id);
            Assert.IsNotNull(serverResult);
        }

        [ClassCleanup]
        static public void TestClean()
        {
            if (sharedStorage != null)
            {
                Config.waitSharedStorageReady(sharedStorage.Id);
                client.SharedStorages.Delete(sharedStorage.Id);
            }

            client.Servers.Delete(server.Id, false);
        }

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
        public void RemoveSharedStoragesServers()
        {
            var result = client.SharedStorages.DeleteSharedStoragesServer(sharedStorage.Id, server.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check the server has no shared storage access
            var serverResult = client.SharedStorages.ShowSharedStoragesServer(sharedStorage.Id, server.Id);
            Assert.IsNull(serverResult);
        }
    }
}
