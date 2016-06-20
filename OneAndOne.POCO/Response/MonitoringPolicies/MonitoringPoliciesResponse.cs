using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.MonitoringPolicies
{
    public class MonitoringPoliciesResponse
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
        private int @default;

        public int Default
        {
            get { return @default; }
            set { @default = value; }
        }
        private string state;

        public string State
        {
            get { return state; }
            set { state = value; }
        }
        private string creation_date;

        public string CreationDate
        {
            get { return creation_date; }
            set { creation_date = value; }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private bool agent;

        public bool Agent
        {
            get { return agent; }
            set { agent = value; }
        }
        private List<Server> servers;

        public List<Server> Servers
        {
            get { return servers; }
            set { servers = value; }
        }
        private Thresholds thresholds;

        public Thresholds Thresholds
        {
            get { return thresholds; }
            set { thresholds = value; }
        }
        private List<Ports> ports;

        public List<Ports> Ports
        {
            get { return ports; }
            set { ports = value; }
        }
        private List<Processes> processes;

        public List<Processes> Processes
        {
            get { return processes; }
            set { processes = value; }
        }
        private string cloudpanel_id;

        public string CloudpanelId
        {
            get { return cloudpanel_id; }
            set { cloudpanel_id = value; }
        }
    }

   

    public class Processes
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string process;

        public string Process
        {
            get { return process; }
            set { process = value; }
        }

        private string alert_if;

        [JsonConverter(typeof(StringEnumConverter))]
        public ProcessAlertType AlertIf
        {
            get { return (ProcessAlertType)Enum.Parse(typeof(ProcessAlertType), alert_if); }
            set
            {
                alert_if = ((ProcessAlertType)value).ToString();
            }
        }
        private bool email_notification;

        public bool EmailNotification
        {
            get { return email_notification; }
            set { email_notification = value; }
        }


    }
    public class Ports
    {

        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string protocol;
        [JsonConverter(typeof(StringEnumConverter))]
        public ProtocolType Protocol
        {
            get { return (ProtocolType)Enum.Parse(typeof(ProtocolType), protocol); }
            set
            {
                protocol = ((ProtocolType)value).ToString();
            }
        }

        private int port;

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private string alert_if;

        [JsonConverter(typeof(StringEnumConverter))]
        public AlertIfType AlertIf
        {
            get { return (AlertIfType)Enum.Parse(typeof(AlertIfType), alert_if); }
            set
            {
                alert_if = ((AlertIfType)value).ToString();
            }

        }

        private bool email_notification;

        public bool EmailNotification
        {
            get { return email_notification; }
            set { email_notification = value; }
        }

    }
    public class Server
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

    public class Warning
    {
        private int value;

        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private bool alert;

        public bool Alert
        {
            get { return alert; }
            set { alert = value; }
        }
    }

    public class Critical
    {
        private int value;

        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private bool alert;

        public bool Alert
        {
            get { return alert; }
            set { alert = value; }
        }
    }

    public class Cpu
    {
        private Warning warning;

        public Warning Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private Critical critical;

        public Critical Critical
        {
            get { return critical; }
            set { critical = value; }
        }
    }

    public class Ram
    {
        private Warning warning;

        public Warning Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private Critical critical;

        public Critical Critical
        {
            get { return critical; }
            set { critical = value; }
        }
    }
    public class Transfer
    {
        private Warning warning;

        public Warning Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private Critical critical;

        public Critical Critical
        {
            get { return critical; }
            set { critical = value; }
        }
    }
    public class InternalPing
    {
        private Warning warning;

        public Warning Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private Critical critical;

        public Critical Critical
        {
            get { return critical; }
            set { critical = value; }
        }
    }

    public class Thresholds
    {
        private Cpu cpu;

        public Cpu Cpu
        {
            get { return cpu; }
            set { cpu = value; }
        }
        private Ram ram;

        public Ram Ram
        {
            get { return ram; }
            set { ram = value; }
        }
        private Transfer transfer;

        public Transfer Transfer
        {
            get { return transfer; }
            set { transfer = value; }
        }
        private InternalPing internal_ping;

        public InternalPing InternalPing
        {
            get { return internal_ping; }
            set { internal_ping = value; }
        }
    }

}
