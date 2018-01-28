using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.SshKeys;
using OneAndOne.POCO.Response.SshKeys;

namespace OneAndOne.UnitTests.SshKeys
{
    [TestClass]
    public class SshKeysTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static SshKeyResponse sshKey;

        [ClassInitialize]
        public static void ServerInit(TestContext context)
        {
            CreateSshKey();
        }

        [ClassCleanup]
        public static void ServerClean()
        {
            if (sshKey != null)
            {
                DeleteSshKey();
            }
        }

        public static void CreateSshKey()
        {
            var random = new Random();

            sshKey = client.SshKeys.Create(new CreateSshKeyRequest()
            {
                Name = random.Next(100, 999) + "testSshKey",
                Description = "SshKey Unit Test Description",
                PublicKey = "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABBAACAQC3Sn8qRLZ/7MfWbvIxq2z8bRY9SKweeGZB/sVlVDxI5Oq7ERHy0EdDfP13WA675IutilgOsUlm0ccQZouG7T7ORO7id8dzN1CPr8OwMXI+ie6Y6svWzZo5Gu2UORgc+8d7AXein5IFNKIHGsL2hXh8mfSQuFLwYHSgg+hNQWbr0IXfoJTg/7xHWiDS6vL5bQRbYIz698k3HSo4sFzROo4Z/FjrKvpC/fkvhatugXZK2i/k3bhiG+hCrJZ69TCCUT7IITM2Qmx4934H9f3XKG4yCfeI/Wam8AomN6XBDY7lub5bzgq31h61NmD7WRNnJyYaWhDZ+AtRcnoPwuU8PSnlz3OikOW5kQ+xDvW2epfJ2oUfc7fxp3+ib6xnH9eXnV6flVb3UsgSxpnLJWdcEN7TmUmRmwl5AAkQx+lO9i9FuGb+cMwVtzD7G6VkvJDWKXmZLf5+m6OlqusuUBSD82jlAGYMjCLmerLyd4XOpgq7SZca/5rRZnazIlW0LgYsXtoB83AYIqIXNVAhIx7V3mJNdcuTbOgPZvIaFqwm+InZwX7ipre5KobkqcP9isVUBtEhauepdzH7FUtZNiA9h4Z7iLnE25Y/I5U/IRvlWeWpOdM34bNIrVxKM81T62njGYrpIShqehXqavkthBObfVxb0HXOpTNucJE2DIkwOVr29Q== your@email.com"
            });

            Assert.IsNotNull(sshKey);
            Assert.IsNotNull(sshKey.Id);
            sshKey = client.SshKeys.Show(sshKey.Id);
            Assert.IsNotNull(sshKey);
            Assert.IsNotNull(sshKey.Id);
        }

        public static void DeleteSshKey()
        {
            var result = client.SshKeys.Delete(sshKey.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void GetSshKeys()
        {
            var sshKeys = client.SshKeys.Get();

            Assert.IsNotNull(sshKeys);
            Assert.IsTrue(sshKeys.Count > 0);
        }

        [TestMethod]
        public void ShowSshKey()
        {
            var sshKeys = client.SshKeys.Get();
            var sshKey = client.SshKeys.Show(sshKeys.FirstOrDefault()?.Id);

            Assert.IsNotNull(sshKey);
            Assert.IsNotNull(sshKey.Id);
        }

        [TestMethod]
        public void UpdateSshKey()
        {
            var result = client.SshKeys.Update(new UpdateSshKeyRequest()
            {
                Name = "Updated Name",
                Description = "Updated Description"
            }, sshKey.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            var updatedSshKey = client.SshKeys.Show(sshKey.Id);
            Assert.IsNotNull(updatedSshKey);
            Assert.AreEqual(updatedSshKey.Name, "Updated Name");
            Assert.AreEqual(updatedSshKey.Description, "Updated Description");
        }
    }
}
