using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.Servers
{
    public class Hardware
    {
        private string fixed_instance_size_id;

        public string FixedInstanceSizeId
        {
            get { return fixed_instance_size_id; }
            set { fixed_instance_size_id = value; }
        }
        private int vcore;

        public int Vcore
        {
            get { return vcore; }
            set { vcore = value; }
        }
        private int cores_per_processor;

        public int CoresPerProcessor
        {
            get { return cores_per_processor; }
            set { cores_per_processor = value; }
        }
        private int ram;

        public int Ram
        {
            get { return ram; }
            set { ram = value; }
        }
        private List<Hdd> hdds;

        public List<Hdd> Hdds
        {
            get { return hdds; }
            set { hdds = value; }
        }
    }
}
