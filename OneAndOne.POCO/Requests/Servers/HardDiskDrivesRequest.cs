using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Servers
{
    public class HddRequest
    {
        /// <summary>
        /// Required: Size of the hard disk minimum: "20",maximum: "2000",multipleOf: "20",
        /// </summary>
        /// 
        private int? size;
        [JsonProperty(PropertyName = "size")]
        public int? Size
        {
            get { return size; }
            set { size = value; }
        }
        /// <summary>
        /// Required: Set true if it's main
        /// </summary>
        /// 
        private bool? is_main;
        [JsonProperty(PropertyName = "is_main")]
        public bool? IsMain
        {
            get { return is_main; }
            set { is_main = value; }
        }
    }
}
