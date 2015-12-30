using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Threading;

namespace OneAndOne.UnitTests.SharedStorages
{
    [TestClass]
    public class SharedStoragesTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance();
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
        public void CreateSharedStorages()
        {
            Random random = new Random();
            var result = client.SharedStorages.Create(new POCO.Requests.SharedStorages.CreateSharedStorage()
                {
                    Description = "description",
                    Name = "TestStorage" + random.Next(100, 999),
                    Size = 50
                });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check the storage is created
            var storageresult = client.SharedStorages.Show(result.Id);
            Assert.IsNotNull(result.Id);

        }

        [TestMethod]
        public void UpdateSharedStorages()
        {
            Random random = new Random();
            var sharedStorages = client.SharedStorages.Get().Where(str => str.Name.Contains("TestStorage")).ToList();
            var sharedStorage = sharedStorages[0];
            foreach (var item in sharedStorages)
            {
                if (item.State == "CONFIGURING")
                    continue;
            }
            while (sharedStorage.State == "CONFIGURING")
            {
                Thread.Sleep(2000);
                sharedStorage = client.SharedStorages.Show(sharedStorage.Id);
            }
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

        }

        [TestMethod]
        public void DeleteSharedStorages()
        {
            Random random = new Random();
            var sharedStorages = client.SharedStorages.Get().Where(str => str.Name.Contains("TestStorage")).ToList();
            var sharedStorage = sharedStorages[random.Next(sharedStorages.Count - 1)];
            var result = client.SharedStorages.Delete(sharedStorage.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check the storage is being removed
            var storageresult = client.SharedStorages.Show(result.Id);
            Assert.IsTrue(storageresult.State == "REMOVING");
        }

        [TestMethod]
        public void DeleteAllTestSharedStorages()
        {
            Random random = new Random();
            var sharedStorages = client.SharedStorages.Get().Where(str => str.Name.Contains("TestStorage")).ToList();
            foreach (var item in sharedStorages)
            {
                var result = client.SharedStorages.Delete(item.Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }

        }

        [TestMethod]
        public void ShowSharedStorageAccess()
        {
            var result = client.SharedStorages.ShowSharedStorageAccess();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateSharedStorageAccess()
        {
            var result = client.SharedStorages.UpdateSharedStorageAccess("test123!");
            Assert.IsNotNull(result);
        }
    }
}
