using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;

namespace OneAndOne.UnitTests.SharedStorages
{
    [TestClass]
    public class SharedStoragesTest
    {
        static OneAndOneClient client = new OneAndOneClient();
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
        }

        [TestMethod]
        public void UpdateSharedStorages()
        {
            Random random = new Random();
            var sharedStorages = client.SharedStorages.Get().Where(str => str.Name.Contains("TestStorage")).ToList();
            var sharedStorage = sharedStorages[random.Next(sharedStorages.Count - 1)];
            var result = client.SharedStorages.Update(new POCO.Requests.SharedStorages.UpdateSharedStorageRequest()
            {
                Description = "description",
                Name = "TestStorageupdated" + random.Next(100, 999),
                Size = sharedStorage.Size + 50
            }, sharedStorage.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
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
        }
    }
}
