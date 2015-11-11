using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.Images;

namespace OneAndOne.UnitTests.Images
{
    [TestClass]
    public class ImagesTest
    {
        static OneAndOneClient client = new OneAndOneClient();

        [TestMethod]
        public void GetImages()
        {
            var images = client.Images.Get();

            Assert.IsNotNull(images);
            Assert.IsTrue(images.Count > 0);
        }

        [TestMethod]
        public void CreateImage()
        {
            var random = new Random();
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];
            var image = client.Images.Create(new POCO.Requests.Images.CreateImageRequest()
                {

                    ServerId = server.Id,
                    Description = "describe image",
                    Frequency = ImageFrequency.DAILY,
                    Name = random.Next(100, 999) + "testImage",
                    NumIimages = random.Next(1, 50)


                });

            Assert.IsNotNull(image);
            Assert.IsNotNull(image.Id);
        }
    }
}
