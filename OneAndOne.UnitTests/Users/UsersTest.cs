using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.Users;

namespace OneAndOne.UnitTests.Users
{
    [TestClass]
    public class UsersTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance();
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

            var Users = client.Users.Get(null, null, null, "03d60140.aliba");
            var User = Users[0];

            var result = client.Users.Show(User.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void CreateUsers()
        {
            Random random = new Random();
            var ranName = "dotnetTest" + random.Next(100, 999);
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
            var user = client.Users.Get(null, null, null, ranName);
            Assert.IsNotNull(user[0].Id);
            Assert.AreEqual(user[0].Name.Split('.')[1], ranName);
        }

        [TestMethod]
        public void UpdateUsers()
        {
            Random random = new Random();
            var Users = client.Users.Get(null, null, null, "aliba");
            var User = Users[0];
            var result = client.Users.Update(new POCO.Requests.Users.UpdateUserRequest()
            {
                Description = "description",
                State = UserState.ACTIVE

            }, User.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if the user is created
            var user = client.Users.Get(null, null, null, "aliba");
            Assert.IsNotNull(user[0].Id);
            Assert.AreEqual(user[0].Description, "description");
        }

        [TestMethod]
        public void DeleteUser()
        {
            Random random = new Random();
            var Users = client.Users.Get().Where(str => str.Name.Contains("dotnetTest")).ToList();
            var User = Users[random.Next(Users.Count - 1)];
            var result = client.Users.Delete(User.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void DeleteAllTestUsers()
        {
            Random random = new Random();
            var Users = client.Users.Get().Where(str => str.Name.Contains("dotnetTest")).ToList();
            foreach (var item in Users)
            {
                var result = client.Users.Delete(item.Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }

        }
    }
}
