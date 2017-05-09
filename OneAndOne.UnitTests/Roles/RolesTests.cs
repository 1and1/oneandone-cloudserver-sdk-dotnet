using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Response.Roles;
using OneAndOne.POCO.Response.Users;

namespace OneAndOne.UnitTests.Roles
{
    [TestClass]
    public class RolesTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static RoleResponse role = null;
        static RoleResponse clone = null;
        static UserResponse user = null;


        [ClassInitialize]
        static public void TestInit(TestContext context)
        {
            var result = client.Roles.Create(".net role test");
            role = result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);

            user = client.Users.Create(new POCO.Requests.Users.CreateUserRequest()
            {
                Name = "roletestuser",
                Password = "Random123!",
                Description = "description",
            });

            //add user to role
            client.Roles.CreateRoleUsers(new System.Collections.Generic.List<string> { { user.Id } }, role.Id);
        }

        [ClassCleanup]
        static public void TestClean()
        {
            var result = client.Roles.Delete(role.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);

            if (user != null)
            {
                client.Users.Delete(user.Id);
            }

            if (clone != null)
            {
                client.Roles.Delete(clone.Id);
            }
        }

        [TestMethod]
        public void GetRoles()
        {
            var result = client.Roles.Get();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void ShowRoles()
        {
            var result = client.Roles.Show(role.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void GetRolesPermissions()
        {
            var result = client.Roles.GetPermissions(role.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetRolesUsers()
        {
            var result = client.Roles.GetRoleUsers(role.Id);

            Assert.IsTrue(result.Count > 0);

            //get single user info
            var userInfo = client.Roles.ShowRoleUser(role.Id, user.Id);

            Assert.AreEqual(userInfo.Id, user.Id);

            //remove user role
            var removed = client.Roles.DeleteRoleUser(role.Id, user.Id);
        }

        [TestMethod]
        public void CloneRoleUser()
        {
            var cloneName = "i am a clone of " + role.Name;
            clone = client.Roles.CreateRoleClone(cloneName, role.Id);
            Assert.AreEqual(clone.Name, cloneName);
        }

        [TestMethod]
        public void UpdateRoles()
        {
            var result = client.Roles.Update("updated name", "delete me", POCO.Requests.Users.UserState.ACTIVE, role.Id);

            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void UpdateRolesPermissions()
        {
            var result = client.Roles.UpdatePermissions(new Permissions
            {
                Servers = new POCO.Response.Roles.Servers
                {
                    Show = true,
                    SetName = false,
                    Shutdown = true
                }
            }, role.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Assert.IsTrue(result.Permissions.Servers.Show);
            Assert.IsFalse(result.Permissions.Servers.SetName);
            Assert.IsTrue(result.Permissions.Servers.Shutdown);
        }
    }
}
