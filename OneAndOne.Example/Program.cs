using OneAndOne.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Example
{
    public class Program
    {
        static OneAndOneClient client = new OneAndOneClient();
        static void Main(string[] args)
        {
            GetServers();

        }

        static void GetServers()
        {
            var serversResult = client.Servers.GetServers(2, 3);
        }
    }
}
