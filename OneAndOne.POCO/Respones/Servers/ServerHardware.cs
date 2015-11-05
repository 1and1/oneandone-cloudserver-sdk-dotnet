using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.Servers
{
    public class Hardware
    {
        public string fixed_instance_size_id { get; set; }
        public int vcore { get; set; }
        public int cores_per_processor { get; set; }
        public int ram { get; set; }
        public List<Hdd> hdds { get; set; }
    }
}
