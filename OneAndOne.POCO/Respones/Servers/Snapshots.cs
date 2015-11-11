using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.Servers
{
    public class Snapshots
    {

        private string id;
        [JsonProperty(PropertyName = "id")]

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string creation_date;
        [JsonProperty(PropertyName = "creation_date")]

        public string CreationDate
        {
            get { return creation_date; }
            set { creation_date = value; }
        }

        private string deletion_date;
        [JsonProperty(PropertyName = "deletion_date")]

        public string DeletionDate
        {
            get { return deletion_date; }
            set { deletion_date = value; }
        }

    }

}
