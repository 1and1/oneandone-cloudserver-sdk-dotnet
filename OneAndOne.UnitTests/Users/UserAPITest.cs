using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Collections.Generic;

namespace OneAndOne.UnitTests.Users
{
    [TestClass]
    public class UserAPITest
    {
        static OneAndOneClient client = OneAndOneClient.Instance();

        [TestMethod]
        public void ShowUsersAPIInfo()
        {
            Random random = new Random();
            var Users = client.Users.Get(null, null, null, "aliba");
            var User = Users[0];

            var result = client.UserAPI.ShowUserAPI(User.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Key);
        }


        /// <summary>
        /// Warning running this test might  stop the api from working
        /// </summary>
        [TestMethod]
        public void UpdateUsersAPI()
        {
            Random random = new Random();
            var Users = client.Users.Get(null, null, null, "aliba");
            var User = Users[0];
            var result = client.UserAPI.UpdateUserAPI(User.Id, true);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void ShowUsersAPIKey()
        {
            Random random = new Random();
            var Users = client.Users.Get(null, null, null, "aliba");
            var User = Users[0];

            var result = client.UserAPI.ShowUserAPIKey(User.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Key);
        }
        /// <summary>
        /// Warning running this test might  stop the api from working
        /// </summary>
        [TestMethod]
        public void UpdateUsersAPIKey()
        {
            Random random = new Random();
            var Users = client.Users.Get(null, null, null, "aliba");
            var User = Users[0];
            var result = client.UserAPI.UpdateAPIKey(User.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //test the new API KEY
            var test = OneAndOneClient.Instance("https://cloudpanel-api.1and1.com/v1", result.Api.Key).Users.Get(null, null, null, "aliba");
            Assert.IsNotNull(test);
        }

        [TestMethod]
        public void ShowUsersAPIIPs()
        {
            Random random = new Random();
            var Users = client.Users.Get(null, null, null, "aliba");
            var User = Users[0];

            var result = client.UserAPI.GetUserIps(User.Id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateUsersAPIIPs()
        {
            Random random = new Random();
            var Users = client.Users.Get(null, null, null, "aliba");
            var User = Users[0];
            var listOfIps = new List<string>();
            listOfIps.Add("185.13.243.86");
            var result = client.UserAPI.UpdateAPIIps(listOfIps, User.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if ip is added
            var testResult = client.Users.Get(null, null, null, "aliba")[0];
            Assert.IsTrue(testResult.Api.AllowedIps.Any(ip => ip == "185.13.243.86"));
        }

        [TestMethod]
        public void DeleteUsersAPIIPs()
        {
            Random random = new Random();
            var Users = client.Users.Get(null, null, null, "aliba");
            var User = Users[0];
            var result = client.UserAPI.DeleteUserIp(User.Id, User.Api.AllowedIps[0]);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if ip is removed
            var testResult = client.Users.Get(null, null, null, "aliba")[0];
            Assert.IsFalse(testResult.Api.AllowedIps.Any(ip => ip == "185.13.243.86"));
        }
    }
}
