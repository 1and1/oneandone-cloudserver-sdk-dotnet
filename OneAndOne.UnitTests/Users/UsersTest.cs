using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.Users;
using OneAndOne.POCO.Response.Users;

namespace OneAndOne.UnitTests.Users
{
    [TestClass]
    public class UsersTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static UserResponse user = null;
        static String testToken;

        [ClassInitialize]
        static public void TestInit(TestContext context)
        {
            Random random = new Random();
            var ranName = "dotnetTest";
            var ranPass = "Test" + random.Next(100, 999) + "!";
            var result = client.Users.Create(new POCO.Requests.Users.CreateUserRequest()
            {
                Name = ranName,
                Password = ranPass,
                Description = "description",
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if the user is created
            var userRes = client.Users.Get(null, null, null, ranName);
            Assert.IsNotNull(userRes[0].Id);
            Assert.AreEqual(userRes[0].Name.Split('.')[1], ranName);
            user = userRes[0];

            //enable API for the user
            testToken = client.UserAPI.UpdateAPIKey(user.Id).Api.Key;
        }

        [ClassCleanup]
        static public void TestClean()
        {
            var testclient = OneAndOneClient.Instance(new Client.RESTHelpers.Configuration(testToken));
            var result = testclient.Users.Delete(user.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void GetUsers()
        {
            var result = client.Users.Get();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void ShowUsers()
        {
            Random random = new Random();

            var result = client.Users.Show(user.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void UpdateUsers()
        {
            var testclient = OneAndOneClient.Instance(new Client.RESTHelpers.Configuration(testToken));
            var result = testclient.Users.Update(new POCO.Requests.Users.UpdateUserRequest()
            {
                Description = "description",
                State = UserState.ACTIVE

            }, user.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if the user is modified
            var updatedUser = client.Users.Get(null, null, null, "dotnetTest");
            Assert.IsNotNull(updatedUser[0].Id);
            Assert.AreEqual(updatedUser[0].Description, "description");
        }
    }
}
