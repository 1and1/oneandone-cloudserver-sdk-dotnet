using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneAndOne.POCO.Converters;
using OneAndOne.POCO.Response.Servers;

namespace OneAndOne.POCO.Response.ServerAppliances
{
    public class ServerAppliancesResponse
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

        private List<string> available_datacenters;
        [JsonConverter(typeof(SingleValueArrayConverter<string>))]
        [JsonProperty(PropertyName = "available_datacenters")]
        public List<string> AvailableDatacenters
        {
            get { return available_datacenters; }
            set { available_datacenters = value; }
        }

        private string os;

        public string Os
        {
            get { return os; }
            set { os = value; }
        }
        private string os_version;

        public string OsVersion
        {
            get { return os_version; }
            set { os_version = value; }
        }

        private List<string> server_type_compatibility;
        [JsonProperty(PropertyName = "server_type_compatibility")]
        public List<ServerTypeCompatibility> ServerTypeCompatibility
        {
            get
            {
                return server_type_compatibility?.ConvertAll(x =>
                    (ServerTypeCompatibility) Enum.Parse(typeof(ServerTypeCompatibility), x));
            }
            set
            {
                server_type_compatibility = ((List<ServerTypeCompatibility>)value).Select(x => ((ServerTypeCompatibility)x).ToString()).ToList();
            }
        }

        private int os_architecture;

        public int OsArchitecture
        {
            get { return os_architecture; }
            set { os_architecture = value; }
        }
        private string os_image_type;
        [JsonConverter(typeof(StringEnumConverter))]
        public OSImageType OsImageType
        {
            get { return (OSImageType)Enum.Parse(typeof(OSImageType), os_image_type); }
            set
            {
                os_image_type = ((OSImageType)value).ToString();
            }
        }

        private string os_family;
        [JsonConverter(typeof(StringEnumConverter))]
        public OSFamliyType OsFamily
        {
            get { return (OSFamliyType)Enum.Parse(typeof(OSFamliyType), os_family); }
            set
            {
                os_family = ((OSFamliyType)value).ToString();
            }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private int min_hdd_size;

        public int MinHddSize
        {
            get { return min_hdd_size; }
            set { min_hdd_size = value; }
        }
        //private List<License> licenses;

        //public List<License> Licenses
        //{
        //    get { return licenses; }
        //    set { licenses = value; }
        //}
        private bool automatic_installation;

        public bool AutomaticInstallation
        {
            get { return automatic_installation; }
            set { automatic_installation = value; }
        }
    }

    public class License
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
