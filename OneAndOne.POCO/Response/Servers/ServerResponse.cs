using Newtonsoft.Json;
using OneAndOne.POCO.Converters;
using OneAndOne.POCO.Requests.Servers;
using OneAndOne.POCO.Response.DataCenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

namespace OneAndOne.POCO.Response.Servers
{
    /// <summary>
    /// Server Main data 
    /// </summary>
    public class ServerResponse
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

        private string server_type;
        [JsonProperty(PropertyName = "server_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ServerType State
        {
            get { return (ServerType)Enum.Parse(typeof(ServerType), server_type); }
            set
            {
                server_type = ((ServerType)value).ToString();
            }
        }

        private string cloudpanel_id;
        [JsonProperty(PropertyName = "cloudpanel_id")]

        public string CloudPanelId
        {
            get { return cloudpanel_id; }
            set { cloudpanel_id = value; }
        }
        private string description;
        [JsonProperty(PropertyName = "description")]

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string creation_date;
        [JsonProperty(PropertyName = "creation_date")]

        public DateTime CreationDate
        {
            get { return DateTimeOffset.Parse(creation_date).DateTime; }
            set { creation_date = value.ToString(); }
        }

        private string first_password;
        [JsonProperty(PropertyName = "first_password")]

        public string FirstPassword
        {
            get { return first_password; }
            set { first_password = value; }
        }

        private string ipv6_range;
        [JsonProperty(PropertyName = "ipv6_range")]

        public string Ipv6Range
        {
            get { return ipv6_range; }
            set { ipv6_range = value; }
        }

        private string hostname;
        [JsonProperty(PropertyName = "hostname")]

        public string Hostname
        {
            get { return hostname; }
            set { hostname = value; }
        }

        private Status status;
        [JsonProperty(PropertyName = "status")]

        public Status Status
        {
            get { return status; }
            set { status = value; }
        }

        private ServerHardware hardware;
        [JsonProperty(PropertyName = "hardware")]

        public ServerHardware Hardware
        {
            get { return hardware; }
            set { hardware = value; }
        }

        private Image image;
        [JsonProperty(PropertyName = "image")]

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        private Dvd dvd;
        [JsonProperty(PropertyName = "dvd")]

        public Dvd DVD
        {
            get { return dvd; }
            set { dvd = value; }
        }

        private Snapshots snapshot;
        [JsonProperty(PropertyName = "snapshot")]

        public Snapshots Snapshot
        {
            get { return snapshot; }
            set { snapshot = value; }
        }

        private List<IpAddress> ips;
        [JsonProperty(PropertyName = "ips")]
        [JsonConverter(typeof(SingleValueArrayConverter<IpAddress>))]
        public List<IpAddress> Ips
        {
            get { return ips; }
            set { ips = value; }
        }
        private List<Alert> alerts;
        [JsonConverter(typeof(SingleValueArrayConverter<Alert>))]
        [JsonProperty(PropertyName = "alerts")]
        public List<Alert> Alerts
        {
            get { return alerts; }
            set { alerts = value; }
        }

        private MonitoringPolicy monitoring_policy_id;
        [JsonProperty(PropertyName = "monitoring_policy_id")]
        public MonitoringPolicy MonitoringPolicyId
        {
            get { return monitoring_policy_id; }
            set { monitoring_policy_id = value; }
        }

        private MonitoringPolicy monitoring_policy;
        [JsonProperty(PropertyName = "monitoring_policy")]
        public MonitoringPolicy MonitoringPolicy
        {
            get { return monitoring_policy; }
            set { monitoring_policy = value; }
        }

        private List<OneAndOne.POCO.Requests.Servers.PrivateNetworks> private_networks;
        [JsonProperty(PropertyName = "private_networks")]
        [JsonConverter(typeof(SingleValueArrayConverter<OneAndOne.POCO.Requests.Servers.PrivateNetworks>))]
        public List<OneAndOne.POCO.Requests.Servers.PrivateNetworks> PrivateNetworks
        {
            get { return private_networks; }
            set { private_networks = value; }
        }

        private DataCenterResponse datacenter;
        public DataCenterResponse Datacenter
        {
            get { return datacenter; }

            set { datacenter = value; }
        }
    }

    public class Status
    {
        private string state;
        [JsonProperty(PropertyName = "state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ServerState State
        {
            get { return (ServerState)Enum.Parse(typeof(ServerState), state); }
            set
            {
                state = ((ServerState)value).ToString();
            }
        }
        private int percent;
        [JsonProperty(PropertyName = "percent")]
        public int Percent
        {
            get { return percent; }
            set { percent = value; }
        }
    }
    public enum ServerState
    {
        POWERING_ON,
        POWERED_ON,
        POWERED_OFF,
        POWERING_OFF,
        DEPLOYING,
        REBOOTING,
        REMOVING,
        CONFIGURING,
        ON_RECOVERY

    }

    [JsonArrayAttribute]
    public class Alert
    {

        private List<Warning> warning;
        [JsonProperty(PropertyName = "warning")]
        [JsonConverter(typeof(SingleValueArrayConverter<Warning>))]
        public List<Warning> Warning
        {
            get { return warning; }
            set { warning = value; }
        }

        private List<Critical> critical;
        [JsonProperty(PropertyName = "critical")]
        [JsonConverter(typeof(SingleValueArrayConverter<Critical>))]
        private List<Critical> Critical
        {
            get { return critical; }
            set { critical = value; }
        }
    }

    public class Critical
    {
        private string type;
        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private string description;
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string date;
        [JsonProperty(PropertyName = "date")]
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
    }

    public class Warning
    {
        private string type;
        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private string description;
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string date;
        [JsonProperty(PropertyName = "date")]
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
    }

    public class MonitoringPolicy
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
