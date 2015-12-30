using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.Images;
using OneAndOne.POCO.Respones.Images;
using System.Threading;
using OneAndOne.POCO.Respones.Servers;

namespace OneAndOne.UnitTests.Images
{
    [TestClass]
    public class ImagesTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance();
        public ImagesResponse response = null;

        [TestMethod]
        public void GetImages()
        {
            var images = client.Images.Get();

            Assert.IsNotNull(images);
            Assert.IsTrue(images.Count > 0);
        }

        [TestMethod]
        public void ShowImage()
        {
            var images = client.Images.Get();
            var image = client.Images.Show(images[0].Id);

            Assert.IsNotNull(image);
            Assert.IsNotNull(image.Id);
        }

        [TestMethod]
        public void CreateImage()
        {
            var random = new Random();
            var servers = client.Servers.Get();
            var server = servers[0];
            while (server.Status.State != ServerState.POWERED_OFF)
            {
                server = servers[random.Next(0, servers.Count - 1)];
                break;
            }
            var image = client.Images.Create(new POCO.Requests.Images.CreateImageRequest()
                {

                    ServerId = server.Id,
                    Description = "describe image",
                    Frequency = ImageFrequency.DAILY,
                    Name = random.Next(100, 999) + "testImage",
                    NumIimages = random.Next(1, 50)


                });
            response = image;

            Assert.IsNotNull(image);
            Assert.IsNotNull(image.Id);
            //check if the image is added
            var resultImage = client.Images.Show(image.Id);
            Assert.IsNotNull(resultImage);
            Assert.IsNotNull(resultImage.Id);
        }

        [TestMethod]
        public void UpdateImage()
        {
            var random = new Random();
            var images = client.Images.Get().Where(img => img.Name.Contains("testImage")).ToList();
            var image = images[random.Next(images.Count - 1)];
            while (image.State != "POWERED_ON" && image.State != "ACTIVE")
            {
                Thread.Sleep(1000);
                image = client.Images.Show(image.Id);
            }
            var result = client.Images.Update(new UpdateImageRequest()
                {
                    Description = "updated",
                    Frequency = ImageFrequency.ONCE,
                    Name = "updaeted API"
                }, image.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if the image is updated
            var resultImage = client.Images.Show(image.Id);
            Assert.IsNotNull(resultImage);
            Assert.AreEqual(resultImage.Description, "updated");
            Assert.AreEqual(resultImage.Name, "updaeted API");
            Assert.AreEqual(resultImage.Frequency, ImageFrequency.ONCE);
        }

        [TestMethod]
        public void DeleteImage()
        {
            var images = client.Images.Get().Where(img => img.Name.Contains("testImage") || img.Name.Contains("updaeted API")).ToList();
            var image = images[0];
            foreach (var item in images)
            {
                if (item.State == "POWERED_ON")
                    image = item;
            }
            var result = client.Images.Delete(image.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void DeleteAllTestImages()
        {
            var random = new Random();
            var images = client.Images.Get().Where(img => img.Name.Contains("testImage") || img.Name.Contains("updaeted API")).ToList();
            foreach (var item in images)
            {
                if (item.State == "POWERED_ON" || item.State == "ACTIVE")
                {
                    var result = client.Images.Delete(item.Id);
                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result.Id);
                }
            }
        }
    }
}
