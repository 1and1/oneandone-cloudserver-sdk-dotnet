﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Respones.MonitoringCenter;
using OneAndOne.POCO;

namespace OneAndOne.UnitTests.Logs
{
    [TestClass]
    public class LogsTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance();
        [TestMethod]
        public void GetLogs()
        {
            var result = client.Logs.Get(PeriodType.LAST_24H);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowLog()
        {
            Random random = new Random();
            var logs = client.Logs.Get(PeriodType.LAST_24H);
            var log = logs[random.Next(logs.Count - 1)];

            var result = client.Logs.Show(log.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }
    }
}
