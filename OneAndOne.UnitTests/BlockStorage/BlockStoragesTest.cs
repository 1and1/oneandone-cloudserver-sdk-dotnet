﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.BlockStorages;
using OneAndOne.POCO.Response.BlockStorages;
using OneAndOne.POCO.Response.Servers;

namespace OneAndOne.UnitTests.BlockStorage
{
    [TestClass]
    public class BlockStoragesTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static BlockStoragesResponse blockStorage;
        static ServerResponse server;

        [ClassInitialize]
        static public void ServerAndBlockStorageInit(TestContext context)
        {
            var random = new Random();
            server = Config.CreateTestServer(random.Next(100, 999) + "blockStorage test .net");
            Config.waitServerReady(server.Id);
            server = client.Servers.Show(server.Id);
            CreateBlockStorage();
        }

        [ClassCleanup]
        static public void ServerAndBlockStorageClean()
        {
            if (blockStorage != null)
            {
                Config.WaitBlockStorageReady(blockStorage.Id);
                DeleteBlockStorage();
            }

            if (server != null)
            {
                Config.waitServerReady(server.Id);
                client.Servers.Delete(server.Id, false);
            }
        }

        public static void CreateBlockStorage()
        {
            Config.waitServerReady(server.Id);
            var random = new Random();

            blockStorage = client.BlockStorages.Create(new CreateBlockStorageRequest()
            {
                Name = random.Next(100, 999) + "testBlockStorage",
                Description = ".NET SDK unit testing",
                DatacenterId = server.Datacenter.Id,
                Size = 60
            });

            Assert.IsNotNull(blockStorage);
            Assert.IsNotNull(blockStorage.Id);
            blockStorage = client.BlockStorages.Show(blockStorage.Id);
            Assert.IsNotNull(blockStorage);
            Assert.IsNotNull(blockStorage.Id);
        }

        private static void DeleteBlockStorage()
        {
            var result = client.BlockStorages.Delete(blockStorage.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            result = client.BlockStorages.Show(result.Id);
            Assert.IsTrue(result.State == "REMOVING");
        }

        [TestMethod]
        public void GetBlockStorages()
        {
            var result = client.BlockStorages.Get();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void ShowBlockStorage()
        {
            var result = client.BlockStorages.Show(blockStorage.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void UpdateBlockStorage()
        {
            Config.WaitBlockStorageReady(blockStorage.Id);
            var result = client.BlockStorages.Update(new POCO.Requests.BlockStorages.UpdateBlockStorageRequest()
            {
                Description = blockStorage.Description + " - Updated",
                Name = blockStorage.Name + " - Updated",
                Size = blockStorage.Size + 10
            }, blockStorage.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);

            var blockStorageResponse = client.BlockStorages.Show(result.Id);
            Assert.IsNotNull(blockStorageResponse.Id);
            Assert.AreEqual(result.Description, blockStorageResponse.Description);
            Assert.AreEqual(result.Size, blockStorageResponse.Size);
            Assert.AreEqual(result.Name, blockStorageResponse.Name);
            Config.WaitBlockStorageReady(result.Id);
        }

        [TestMethod]
        public void AttachServerToBlockStorage()
        {
            var serverBlockStorage = new BlockStorageServerRequest()
            {
                ServerId = server.Id
            };

            var result = client.BlockStorages.CreateServerBlockStorage(serverBlockStorage, blockStorage.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void GetBlockStorageServer()
        {
            var blockStorageServer = client.BlockStorages.GetBlockStorageServer(blockStorage.Id);

            Assert.IsNotNull(blockStorageServer);
            Assert.IsNotNull(blockStorageServer.Id);
        }

        [TestMethod]
        public void DetachBlockStorageFromServer()
        {
            var response = client.BlockStorages.DeleteBlockStorageServer(blockStorage.Id);

            Assert.IsNotNull(response);
        }
    }
}