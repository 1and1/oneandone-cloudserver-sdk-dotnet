using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Servers
{
    public class HardwareReqeust
    {
        /// <summary>
        /// Required: Size of the ID desired for the server
        /// </summary>
        public string fixed_instance_size_id { get; set; }
        /// <summary>
        /// Required: Total amount of processors minimum: "1",maximum: "16",multipleOf: "1",.
        /// </summary>
        public int? vcore { get; set; }
        /// <summary>
        /// Required: Number of cores per processor minimum: "1",maximum: "16",multipleOf: "1",
        /// </summary>
        public int? cores_per_processor { get; set; }
        /// <summary>
        /// Required: RAM memory size minimum: "1",maximum: "128",multipleOf: "0.5",.
        /// </summary>
        public int? ram { get; set; }
        /// <summary>
        /// Required: Hard disks
        /// </summary>
        public List<HddRequest> hdds { get; set; }
    }
}
