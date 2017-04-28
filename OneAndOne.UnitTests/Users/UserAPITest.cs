using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Collections.Generic;
using OneAndOne.POCO.Response.Users;

namespace OneAndOne.UnitTests.Users
{
    [TestClass]
    public class UserAPITest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static UserResponse user = null;
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

            var enabled = client.UserAPI.UpdateUserAPI(user.Id, true);

            Assert.IsNotNull(enabled);
        }

        [ClassCleanup]
        static public void TestClean()
        {
            DeleteUsersAPIIPs();
            client.Users.Delete(user.Id);
        }
        [TestMethod]
        public void ShowUsersAPIInfo()
        {
            var result = client.UserAPI.ShowUserAPI(user.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Key);
        }
           

        [TestMethod]
        public void ShowUsersAPIKey()
        {
            var result = client.UserAPI.ShowUserAPIKey(user.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Key);
        }
        /// <summary>
        /// Warning running this test might  stop the api from working
        /// </summary>
        [TestMethod]
        public void UpdateUsersAPIKey()
        {
            var result = client.UserAPI.UpdateAPIKey(user.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //test the new API KEY
            var test = OneAndOneClient.Instance(new Client.RESTHelpers.Configuration { ApiUrl = "https://cloudpanel-api.1and1.com/v1", ApiKey = result.Api.Key }).Users.Get(null, null, null, "dotnetTest");
            Assert.IsNotNull(test);
        }

        [TestMethod]
        public void ShowUsersAPIIPs()
        {
            var result = client.UserAPI.GetUserIps(user.Id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateUsersAPIIPs()
        {
            var listOfIps = new List<string>();
            listOfIps.Add("185.13.243.86");
            var result = client.UserAPI.UpdateAPIIps(listOfIps, user.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if ip is added
            var testResult = client.Users.Show(user.Id);
            Assert.IsTrue(testResult.Api.AllowedIps.Any(ip => ip == "185.13.243.86"));
        }

        static public void DeleteUsersAPIIPs()
        {
            var result = client.UserAPI.DeleteUserIp(user.Id, user.Api.AllowedIps[0]);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if ip is removed
            var testResult = client.Users.Show(user.Id);
            Assert.IsFalse(testResult.Api.AllowedIps.Any(ip => ip == "185.13.243.86"));
        }
    }
}
