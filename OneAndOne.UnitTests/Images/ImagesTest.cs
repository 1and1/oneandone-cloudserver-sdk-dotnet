using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.Images;
using OneAndOne.POCO.Response.Images;
using System.Threading;
using OneAndOne.POCO.Response.Servers;
using System.Collections.Generic;
using OneAndOne.POCO.Response.DataCenters;

namespace OneAndOne.UnitTests.Images
{
    [TestClass]
    public class ImagesTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static ImagesResponse response = null;
        static ServerResponse server = null;
        static ImagesResponse image = null;
        static DataCenterResponse dc = null;

        [ClassInitialize]
        static public void ServerInit(TestContext context)
        {
            dc = client.DataCenters.Get().FirstOrDefault();
            server = Config.CreateTestServer("image1 test .net");
            Config.waitServerReady(server.Id);
            server = client.Servers.Show(server.Id);
            CreateImage();
        }

        [ClassCleanup]
        static public void ServerClean()
        {
            if (image != null)
            {
                DeleteImage();
            }
            Config.waitServerReady(server.Id);
            client.Servers.Delete(server.Id, false);
        }



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

        public static void CreateImage()
        {
            Config.waitServerReady(server.Id);
            var random = new Random();
            image = client.Images.Create(new POCO.Requests.Images.CreateImageRequest()
            {

                ServerId = server.Id,
                Description = "describe image",
                Frequency = ImageFrequency.DAILY,
                Name = random.Next(100, 999) + "testImage",
                NumIimages = random.Next(1, 50),
                DatacetnerId = dc.Id,
                Type =ImageType.os,
                Source = ImageSource.server
            });

            response = image;
            Assert.IsNotNull(image);
            Assert.IsNotNull(image.Id);
            //check if the image is added
            image = client.Images.Show(image.Id);
            Assert.IsNotNull(image);
            Assert.IsNotNull(image.Id);
        }

        [TestMethod]
        public void UpdateImage()
        {
            Config.waitImageReady(image.Id);
            var random = new Random();
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

        static public void DeleteImage()
        {
            var result = client.Images.Delete(image.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

    }
}
