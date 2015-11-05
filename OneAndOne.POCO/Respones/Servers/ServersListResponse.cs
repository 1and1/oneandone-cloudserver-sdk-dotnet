using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.Servers
{
    
    /// <summary>
    /// Server Main data 
    /// </summary>
    public class ServersListResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Status status { get; set; }
        public Image image { get; set; }
        public Hardware hardware { get; set; }
        public List<Ip> ips { get; set; }
        public List<Alert> alerts { get; set; }
    }

    public class Status
    {
        public string state { get; set; }
        public object percent { get; set; }
    }

    public class Alert
    {
        public string type { get; set; }
        public int count { get; set; }
    }



}
