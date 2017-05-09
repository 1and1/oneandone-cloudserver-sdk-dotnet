using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Response.Servers;
using System.Linq;
using System.Collections.Generic;

namespace OneAndOne.UnitTests
{
    [TestClass]
    public class ServerImagesTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static ServerResponse server = null;
        Random random = new Random();

        [ClassInitialize]
        static public void ServerInit(TestContext context)
        {
            server = Config.CreateTestServer("Server image test .net");
            Config.waitServerReady(server.Id);
            server = client.Servers.Show(server.Id);
        }

        [ClassCleanup]
        static public void ServerClean()
        {
            Config.waitServerReady(server.Id);
            client.Servers.Delete(server.Id, false);
        }


        [TestMethod]
        public void GetImage()
        {

            var image = client.ServerImage.Show(server.Id);

            Assert.IsNotNull(image);
        }

        [TestMethod]
        public void UpdateImage()
        {
            Config.waitServerReady(server.Id);
            string randomName = "Pass!" + random.Next(1000, 3000);
            var result = client.ServerImage.Update(new POCO.Requests.Servers.UpdateServerImageRequest()
            {
                Id = server.Image.Id,
                Password = randomName

            }, server.Id);

            Assert.IsNotNull(result);
        }
    }
}
