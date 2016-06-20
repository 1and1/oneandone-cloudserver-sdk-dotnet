using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.Servers
{
    public class Dvd
    {
        private string id;
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        [JsonProperty(PropertyName = "name")]

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
