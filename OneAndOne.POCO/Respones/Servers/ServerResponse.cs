using Newtonsoft.Json;
using OneAndOne.POCO.Requests.Servers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.Servers
{

    /// <summary>
    /// Server Main data 
    /// </summary>
    public class ServerResponse
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
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private Status status;

        public Status Status
        {
            get { return status; }
            set { status = value; }
        }
        private Image image;

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }
        private Hardware hardware;

        public Hardware Hardware
        {
            get { return hardware; }
            set { hardware = value; }
        }

        private Snapshots snapshot;

        public Snapshots Snapshot
        {
            get { return snapshot; }
            set { snapshot = value; }
        }
        private List<IpAddress> ips;

        public List<IpAddress> Ips
        {
            get { return ips; }
            set { ips = value; }
        }

        private List<Dvd> dvd;

        public List<Dvd> Dvd
        {
            get { return dvd; }
            set { dvd = value; }
        }
        private List<Alert> alerts;

        public List<Alert> Alerts
        {
            get { return alerts; }
            set { alerts = value; }
        }

        private string monitoring_policy_id;
        public string MonitoringPolicyId
        {
            get { return monitoring_policy_id; }
            set { monitoring_policy_id = value; }
        }

        private List<PrivateNetworks> private_networks;
        public List<PrivateNetworks> PrivateNetworks
        {
            get { return private_networks; }
            set { private_networks = value; }
        }
    }

    public class Status
    {
        private string state;
        [JsonProperty(PropertyName = "state")]
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
        POWERED_ON,
        POWERED_OFF,
        DEPLOYING,
        REMOVING,

    }

    public class Alert
    {

        private List<Warning> warning;
        [JsonProperty(PropertyName = "warning")]
        public List<Warning> Warning
        {
            get { return warning; }
            set { warning = value; }
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

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
    }

}
