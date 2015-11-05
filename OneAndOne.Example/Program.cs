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
        static void Main(string[] args)
        {
            GetServers();

        }

        static void GetServers()
        {
            Servers server = new Servers();
            var serversResult = server.GetServers();
        }
    }
}
