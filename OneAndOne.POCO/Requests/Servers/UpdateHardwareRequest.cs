using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Servers
{
    public class UpdateHardwareRequest
    {

        private int vcore;
        [JsonProperty(PropertyName = "vcore")]
        /// <summary>
        /// Required: Total amount of processors minimum: "1",maximum: "16",multipleOf: "1",.
        /// </summary>
        /// 
        public int Vcore
        {
            get { return vcore; }
            set { vcore = value; }
        }


        private int cores_per_processor;
        [JsonProperty(PropertyName = "cores_per_processor")]
        /// <summary>
        /// Required: Number of cores per processor minimum: "1",maximum: "16",multipleOf: "1",
        /// </summary>
        /// 
        public int CoresPerProcessor
        {
            get { return cores_per_processor; }
            set { cores_per_processor = value; }
        }


        private int ram;
        [JsonProperty(PropertyName = "ram")]
        /// <summary>
        /// Required: RAM memory size minimum: "1",maximum: "128",multipleOf: "0.5",.
        /// </summary>
        public int Ram
        {
            get { return ram; }
            set { ram = value; }
        }
    }
}
