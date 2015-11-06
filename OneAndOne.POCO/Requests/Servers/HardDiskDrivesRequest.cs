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
        public int? size { get; set; }
        /// <summary>
        /// Required: Set true if it's main
        /// </summary>
        public bool? is_main { get; set; }
    }
}
