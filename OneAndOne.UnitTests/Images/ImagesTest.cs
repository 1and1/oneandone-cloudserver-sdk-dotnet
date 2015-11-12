using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.Images;
using OneAndOne.POCO.Respones.Images;

namespace OneAndOne.UnitTests.Images
{
    [TestClass]
    public class ImagesTest
    {
        static OneAndOneClient client = new OneAndOneClient();
        public ImagesResponse response = null;

        [TestMethod]
        public void GetImages()
        {
            //TODO: the API fails to work here
            var images = client.Images.Get();

            Assert.IsNotNull(images);
            Assert.IsTrue(images.Count > 0);
        }

        [TestMethod]
        public void ShowImage()
        {
            //var images = client.Images.Get();
            //TODO: remove hard coded id once the API is fixed
            var image = client.Images.Show("8E0DBFE99DBEECAC43753C2F3E41BE9F");

            Assert.IsNotNull(image);
            Assert.IsNotNull(image.Id);
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
            response = image;

            Assert.IsNotNull(image);
            Assert.IsNotNull(image.Id);
        }

        [TestMethod]
        public void UpdateImage()
        {
            //TODO: remove hard coded id once the API is fixed
            var random = new Random();
            //var images = client.Images.Get();
            //var image = images[random.Next(images.Count - 1)];
            var result = client.Images.Update(new UpdateImageRequest()
                {
                    Description = "updated",
                    Frequency = ImageFrequency.ONCE,
                    Name = "updaeted API"
                }, "8E0DBFE99DBEECAC43753C2F3E41BE9F");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        //35E17E647BBBC1A0C770742080643F33

        [TestMethod]
        public void DeleteImage()
        {
            //TODO: remove hard coded id once the API is fixed
            var random = new Random();
            //var images = client.Images.Get();
            //var image = images[random.Next(images.Count - 1)];
            var result = client.Images.Delete("8E0DBFE99DBEECAC43753C2F3E41BE9F");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }
    }
}
