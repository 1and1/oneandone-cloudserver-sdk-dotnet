using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.DVDS
{
    public class DVDResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public string os_family { get; set; }
        public string os { get; set; }
        public string os_version { get; set; }
        public int architecture { get; set; }
        public string type { get; set; }
    }
}
