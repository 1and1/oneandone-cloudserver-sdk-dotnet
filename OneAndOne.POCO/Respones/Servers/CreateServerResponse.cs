using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.Servers
{
    public class CreateServerResponse
    {
        public string id { get; set; }
        public string cloudpanel_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string creation_date { get; set; }
        public string first_password { get; set; }
        public Status status { get; set; }
        public Hardware hardware { get; set; }
        public Image image { get; set; }
        public object dvd { get; set; }
        public object snapshot { get; set; }
        public object ips { get; set; }
        public List<Alert> alerts { get; set; }
        public object monitoring_policy { get; set; }
        public object private_networks { get; set; }
    }
}
