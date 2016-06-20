using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Servers
{
    public class HardwareRequest
    {
        /// <summary>
        /// Required: Size of the ID desired for the server
        /// </summary>
        /// 
        private string fixed_instance_size_id;
        [JsonProperty(PropertyName = "fixed_instance_size_id")]
        public string FixedInstanceSizeId
        {
            get { return fixed_instance_size_id; }
            set { fixed_instance_size_id = value; }
        }
        /// <summary>
        /// Required: Total amount of processors minimum: "1",maximum: "16",multipleOf: "1",.
        /// </summary>
        /// 
        private int vcore;
        [JsonProperty(PropertyName = "vcore")]
        public int Vcore
        {
            get { return vcore; }
            set { vcore = value; }
        }

        /// <summary>
        /// Required: Number of cores per processor minimum: "1",maximum: "16",multipleOf: "1",
        /// </summary>
        /// 
        private int cores_per_processor;
        [JsonProperty(PropertyName = "cores_per_processor")]
        public int CoresPerProcessor
        {
            get { return cores_per_processor; }
            set { cores_per_processor = value; }
        }

        /// <summary>
        /// Required: RAM memory size minimum: "1",maximum: "128",multipleOf: "0.5",.
        /// </summary>
        private decimal ram;
        [JsonProperty(PropertyName = "ram")]
        public decimal Ram
        {
            get { return ram; }
            set { ram = value; }
        }

        /// 
        /// <summary>
        /// Required: Hard disks
        /// </summary>
        /// 
        private List<HddRequest> hdds;
        [JsonProperty(PropertyName = "hdds")]
        public List<HddRequest> Hdds
        {
            get { return hdds; }
            set { hdds = value; }
        }

    }
}
