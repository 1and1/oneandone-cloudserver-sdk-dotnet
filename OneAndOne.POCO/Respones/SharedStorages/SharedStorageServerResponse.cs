using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.SharedStorages
{
    public class SharedStorageServerResponse
    {
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string rights;
        [JsonConverter(typeof(StringEnumConverter))]
        public StorageServerRights Rights
        {
            get { return (StorageServerRights)Enum.Parse(typeof(StorageServerRights), rights); }
            set
            {
                rights = ((StorageServerRights)value).ToString();
            }
        }
    }
    public enum StorageServerRights
    {
        R,
        RW
    }
}
