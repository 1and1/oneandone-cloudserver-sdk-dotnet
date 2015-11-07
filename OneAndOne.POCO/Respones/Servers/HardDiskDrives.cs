using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.Servers
{
    public class Hdd
    {
        private string id;
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private int size;
        [JsonProperty(PropertyName = "size")]
        public int Size
        {
            get { return size; }
            set { size = value; }
        }
        private bool is_main;
        [JsonProperty(PropertyName = "is_main")]
        public bool IsMain
        {
            get { return is_main; }
            set { is_main = value; }
        }
    }
}
