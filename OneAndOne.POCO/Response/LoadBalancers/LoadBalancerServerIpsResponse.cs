using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.LoadBalancers
{
    public class LoadBalancerServerIpsResponse
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string ip;

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }
        private string server_name;

        public string ServerName
        {
            get { return server_name; }
            set { server_name = value; }
        }
    }
}
