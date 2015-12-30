using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Respones.Servers;

namespace OneAndOne.UnitTests
{
    [TestClass]
    public class ServerImagesTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance();
        Random random = new Random();
        [TestMethod]
        public void GetImage()
        {

            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];
            var image = client.ServerImage.Show(server.Id);

            Assert.IsNotNull(image);
        }

        [TestMethod]
        public void UpdateImage()
        {
            string randomName = "Pass!" + random.Next(1000, 3000);
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];
            if (server.Status.State != ServerState.DEPLOYING || server.Status.State != ServerState.REMOVING)
            {
                var result = client.ServerImage.Update(new POCO.Requests.Servers.UpdateServerImageRequest()
                    {
                        Id = server.Image.Id,
                        Password = randomName

                    }, server.Id);

                Assert.IsNotNull(result);
                //check if the image is deploying
                Assert.IsTrue(result.Status.Percent > 0);
                Assert.IsTrue(result.Status.State == ServerState.DEPLOYING);
            }
        }
    }
}
