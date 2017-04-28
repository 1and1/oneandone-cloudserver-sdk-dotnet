using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Threading;
using OneAndOne.POCO.Response.SharedStorages;

namespace OneAndOne.UnitTests.SharedStorages
{
    [TestClass]
    public class SharedStoragesTest
    {
        static public SharedStoragesResponse sharedStorage = null;
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);

        [ClassInitialize]
        static public void TestInit(TestContext context)
        {
            var datacenters = client.DataCenters.Get();
            var dc = datacenters.FirstOrDefault();
            sharedStorage = client.SharedStorages.Create(new POCO.Requests.SharedStorages.CreateSharedStorage()
            {
                Description = "description",
                Name = "TestStorage .net",
                Size = 50,
                DatacenterId = dc.Id
            });

            Assert.IsNotNull(sharedStorage);
            Assert.IsNotNull(sharedStorage.Id);
            //check the storage is created
            var storageresult = client.SharedStorages.Show(sharedStorage.Id);
            Assert.IsNotNull(sharedStorage.Id);
        }

        [ClassCleanup]
        static public void TestClean()
        {
            if (sharedStorage != null)
            {
                Config.waitSharedStorageReady(sharedStorage.Id);
                DeleteSharedStorages();
            }
        }


        [TestMethod]
        public void GetSharedStorages()
        {
            var result = client.SharedStorages.Get();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void ShowSharedStorages()
        {
            Random random = new Random();

            var sharedStorages = client.SharedStorages.Get();
            var sharedStorage = sharedStorages[random.Next(sharedStorages.Count - 1)];

            var result = client.SharedStorages.Show(sharedStorage.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void UpdateSharedStorages()
        {
            Random random = new Random();
            Config.waitSharedStorageReady(sharedStorage.Id);
            var result = client.SharedStorages.Update(new POCO.Requests.SharedStorages.UpdateSharedStorageRequest()
            {
                Description = "description",
                Name = "TestStorageupdated" + random.Next(100, 999),
                Size = sharedStorage.Size + 50
            }, sharedStorage.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            var storageresult = client.SharedStorages.Show(result.Id);
            //check the storage is updated with new values
            Assert.IsNotNull(result.Id);
            Assert.AreEqual(result.Description, storageresult.Description);
            Assert.AreEqual(result.Size, storageresult.Size);
            Assert.AreEqual(result.Name, storageresult.Name);
            Config.waitSharedStorageReady(result.Id);

        }

        static public void DeleteSharedStorages()
        {
            var result = client.SharedStorages.Delete(sharedStorage.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check the storage is being removed
            var storageresult = client.SharedStorages.Show(result.Id);
            Assert.IsTrue(storageresult.State == "REMOVING");
        }

        [TestMethod]
        public void ShowSharedStorageAccess()
        {
            var result = client.SharedStorages.ShowSharedStorageAccess();

            Assert.IsNotNull(result);
        }

        [Ignore()]
        [TestMethod]
        public void UpdateSharedStorageAccess()
        {
            Config.waitSharedStorageReady(sharedStorage.Id);
            var result = client.SharedStorages.UpdateSharedStorageAccess("Asdasdfgagsw32!!");
            Assert.IsNotNull(result);
        }
    }
}
