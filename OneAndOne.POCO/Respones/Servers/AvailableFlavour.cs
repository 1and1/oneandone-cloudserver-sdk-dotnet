using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.Servers
{
    public class InstaceHdd
    {
        public int size { get; set; }
        public string unit { get; set; }
        public bool is_main { get; set; }
    }

    public class InstaceHardware
    {
        public int vcore { get; set; }
        public int cores_per_processor { get; set; }
        public int ram { get; set; }
        public string unit { get; set; }
        public List<InstaceHdd> hdds { get; set; }
    }

    public class AvailableHardwareFlavour
    {
        public string name { get; set; }
        public string id { get; set; }
        public InstaceHardware hardware { get; set; }
    }
}
