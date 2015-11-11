using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.Servers
{
    public class InstaceHdd
    {
        private int size;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }
        private string unit;

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        private bool is_main;

        public bool IsMain
        {
            get { return is_main; }
            set { is_main = value; }
        }
    }

    public class InstaceHardware
    {
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
        private decimal ram;

        public decimal Ram
        {
            get { return ram; }
            set { ram = value; }
        }
        private string unit;

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        private List<InstaceHdd> hdds;

        public List<InstaceHdd> Hdds
        {
            get { return hdds; }
            set { hdds = value; }
        }
    }

    public class AvailableHardwareFlavour
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private InstaceHardware hardware;

        public InstaceHardware Hardware
        {
            get { return hardware; }
            set { hardware = value; }
        }
    }
}
