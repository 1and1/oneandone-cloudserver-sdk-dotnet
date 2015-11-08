using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Servers
{
    public class UpdateHddRequest
    {
        int size;
        [JsonProperty(PropertyName = "size")]

        public int Size
        {
            get { return size; }
            set { size = value; }
        }
    }
}
