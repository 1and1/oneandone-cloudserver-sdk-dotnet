using OneAndOne.POCO.Requests.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.Servers
{
    public class UpdatedOperationServerResponse
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string cloudpanel_id;

        public string CloudpanelId
        {
            get { return cloudpanel_id; }
            set { cloudpanel_id = value; }
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
        private string creation_date;

        public string Creation_date
        {
            get { return creation_date; }
            set { creation_date = value; }
        }
        private string first_password;

        public string First_password
        {
            get { return first_password; }
            set { first_password = value; }
        }
        private UpdatedOperationStatus status;

        public UpdatedOperationStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        private UpdatedOperationHardware hardware;

        public UpdatedOperationHardware Hardware
        {
            get { return hardware; }
            set { hardware = value; }
        }
        private UpdatedOperationImage image;

        public UpdatedOperationImage Image
        {
            get { return image; }
            set { image = value; }
        }
        private UpdatedOperationDvd dvd;

        public UpdatedOperationDvd Dvd
        {
            get { return dvd; }
            set { dvd = value; }
        }
        private object snapshot;

        public object Snapshot
        {
            get { return snapshot; }
            set { snapshot = value; }
        }
        private List<UpdatedOperationIp> ips;

        public List<UpdatedOperationIp> Ips
        {
            get { return ips; }
            set { ips = value; }
        }
        private UpdatedOperationAlerts alerts;

        public UpdatedOperationAlerts Alerts
        {
            get { return alerts; }
            set { alerts = value; }
        }
        private UpdatedOperationMonitoringPolicy monitoring_policy;

        public UpdatedOperationMonitoringPolicy MonitoringPolicy
        {
            get { return monitoring_policy; }
            set { monitoring_policy = value; }
        }
        private List<UpdatedOperationPrivateNetwork> private_networks;

        public List<UpdatedOperationPrivateNetwork> PrivateNetworks
        {
            get { return private_networks; }
            set { private_networks = value; }
        }
    }

    public class UpdatedOperationStatus
    {
        private string state;

        public string State
        {
            get { return state; }
            set { state = value; }
        }
        private int percent;

        public int Percent
        {
            get { return percent; }
            set { percent = value; }
        }
    }

    public class UpdatedOperationHdd
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private int size;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }
        private bool is_main;

        public bool IsMain
        {
            get { return is_main; }
            set { is_main = value; }
        }
    }

    public class UpdatedOperationHardware
    {
        private int fixed_instance_size_id;

        public int Fixed_instance_size_id
        {
            get { return fixed_instance_size_id; }
            set { fixed_instance_size_id = value; }
        }
        private int vcore;

        public int Vcore
        {
            get { return vcore; }
            set { vcore = value; }
        }
        private int cores_per_processor;

        public int Cores_per_processor
        {
            get { return cores_per_processor; }
            set { cores_per_processor = value; }
        }
        private int ram;

        public int Ram
        {
            get { return ram; }
            set { ram = value; }
        }
        private List<UpdatedOperationHdd> hdds;

        public List<UpdatedOperationHdd> Hdds
        {
            get { return hdds; }
            set { hdds = value; }
        }
    }

    public class UpdatedOperationImage
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
    }

    public class UpdatedOperationDvd
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
    }

    public class UpdatedOperationFirewallPolicy
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
    }

    public class UpdatedOperationIp
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string ip;

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private object reverse_dns;

        public object ReverseDns
        {
            get { return reverse_dns; }
            set { reverse_dns = value; }
        }
        private UpdatedOperationFirewallPolicy firewall_policy;

        public UpdatedOperationFirewallPolicy FirewallPolicy
        {
            get { return firewall_policy; }
            set { firewall_policy = value; }
        }
        private List<object> load_balancers;

        public List<object> LoadBalancers
        {
            get { return load_balancers; }
            set { load_balancers = value; }
        }
    }

    public class UpdatedOperationWarning
    {
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private string description;

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

    public class UpdatedOperationCritical
    {
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private string description;

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

    public class UpdatedOperationAlerts
    {
        private List<UpdatedOperationWarning> warning;

        public List<UpdatedOperationWarning> Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private List<UpdatedOperationCritical> critical;

        public List<UpdatedOperationCritical> Critical
        {
            get { return critical; }
            set { critical = value; }
        }
    }

    public class UpdatedOperationMonitoringPolicy
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
    }

    public class UpdatedOperationPrivateNetwork
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string private_name;

        public string PrivateName
        {
            get { return private_name; }
            set { private_name = value; }
        }
    }
}
