using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.SharedStorages
{
    public class CreateSharedStorage
    {
        ///<summary>
        //Name of the shared storage
        //</summary>
        private string name;
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        ///<summary>
        //Description of the shared storage
        //</summary>
        private string description;
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        ///<summary>
        //Size of shared storage","minimum": "50","maximum": "2000","multipleOf": "50",
        //</summary>
        private int size;
        [JsonProperty(PropertyName = "size")]
        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        ///<summary>
        //datacenter_id of the shared storage
        //</summary>
        private string datacenter_id;
        [JsonProperty(PropertyName = "datacenter_id")]

        public string DatacenterId
        {
            get
            {
                return datacenter_id;
            }

            set
            {
                datacenter_id = value;
            }
        }
    }
}
