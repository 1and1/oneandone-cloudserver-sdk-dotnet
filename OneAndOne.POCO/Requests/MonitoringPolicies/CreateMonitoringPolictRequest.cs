using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.MonitoringPolicies
{
    public class CreateMonitoringPolictRequest
    {

        private string name;
        /// <summary>
        /// Required: Monitoring policy name.
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string description;
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string email;
        /// <summary>
        /// Required: User's email
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private bool agent;
        /// <summary>
        /// Required: Set true for using agent
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "agent")]
        public bool Agent
        {
            get { return agent; }
            set { agent = value; }
        }
        private Thresholds thresholds;
        [JsonProperty(PropertyName = "thresholds")]
        public Thresholds Thresholds
        {
            get { return thresholds; }
            set { thresholds = value; }
        }
        private List<Ports> ports;
        /// <summary>
        /// Required: Array of ports that will be monitoring
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "ports")]
        public List<Ports> Ports
        {
            get { return ports; }
            set { ports = value; }
        }
        private List<Processes> processes;
        /// <summary>
        /// Required: Array of processes that will be monitoring
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "processes")]
        public List<Processes> Processes
        {
            get { return processes; }
            set { processes = value; }
        }
    }
   

    public class Processes
    {
        private string process;
        /// <summary>
        /// Required:Name of the process", "maxLength": 50
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "process")]
        public string Process
        {
            get { return process; }
            set { process = value; }
        }

        private string alert_if;
        /// <summary>
        /// Required: "Case of alert", "enum": ["RUNNING", "NOT_RUNNING"]
        /// </summary>
        /// 
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "alert_if")]
        public ProcessAlertType AlertIf
        {
            get { return (ProcessAlertType)Enum.Parse(typeof(ProcessAlertType), alert_if); }
            set
            {
                alert_if = ((ProcessAlertType)value).ToString();
            }
        }
        private bool email_notification;
        [JsonProperty(PropertyName = "email_notification")]
        public bool EmailNotification
        {
            get { return email_notification; }
            set { email_notification = value; }
        }


    }
    public class Ports
    {
        private string protocol;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "protocol")]
        public ProtocolType Protocol
        {
            get { return (ProtocolType)Enum.Parse(typeof(ProtocolType), protocol); }
            set
            {
                protocol = ((ProtocolType)value).ToString();
            }
        }

        private int port;
        [JsonProperty(PropertyName = "port")]
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private string alert_if;
        /// <summary>
        /// Required:"Case of alert","enum": ["RESPONDING", "NOT_RESPONDING"]
        /// </summary>
        /// 
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "alert_if")]
        public AlertIfType AlertIf
        {
            get { return (AlertIfType)Enum.Parse(typeof(AlertIfType), alert_if); }
            set
            {
                alert_if = ((AlertIfType)value).ToString();
            }

        }

        private bool email_notification;
        /// <summary>
        /// Required: Set true for sending e-mail notifications  
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "email_notification")]
        public bool EmailNotification
        {
            get { return email_notification; }
            set { email_notification = value; }
        }

    }
    public class Server
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

    public class DiskCritical
    {
        private int value;
        /// <summary>
        /// Required: Advise when this value is exceeded (%)  "maximum": 100
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "value")]
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private bool alert;
        /// <summary>
        /// Required: Enable alert
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "alert")]
        public bool Alert
        {
            get { return alert; }
            set { alert = value; }
        }
    }
    public class DiskWarning
    {
        private int value;
        /// <summary>
        /// Required: Advise when this value is exceeded (%)"minimum": 1,"maximum": 95,
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "value")]
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private bool alert;
        /// <summary>
        /// Required: Enable alert
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "alert")]
        public bool Alert
        {
            get { return alert; }
            set { alert = value; }
        }
    }
    public class Critical
    {
        private int value;
        /// <summary>
        /// Required: Advise when this value is exceeded (%) "maximum": 100
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "value")]
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private bool alert;
        /// <summary>
        /// Required: Enable alert
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "alert")]
        public bool Alert
        {
            get { return alert; }
            set { alert = value; }
        }
    }
    public class Warning
    {
        private int value;
        /// <summary>
        /// Required: Advise when this value is exceeded (%)"minimum": 1,"maximum": 95,
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "value")]
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private bool alert;
        /// <summary>
        /// Required: Enable alert
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "alert")]
        public bool Alert
        {
            get { return alert; }
            set { alert = value; }
        }
    }

    public class InternalPingWarning
    {
        private int value;
        /// <summary>
        /// Required: AAdvise when this value is exceeded (ms) minimum": 1,
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "value")]
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private bool alert;
        /// <summary>
        /// Required: Enable alert
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "alert")]
        public bool Alert
        {
            get { return alert; }
            set { alert = value; }
        }
    }

    public class InternalPingCritical
    {
        private int value;
        /// <summary>
        /// Required: Advise when this value is exceeded (ms) "maximum": 100,
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "value")]
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private bool alert;
        /// <summary>
        /// Required: Enable alert
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "alert")]
        public bool Alert
        {
            get { return alert; }
            set { alert = value; }
        }
    }

    public class TransferCritical
    {
        private int value;
        /// <summary>
        /// Required: Advise when this value is exceeded (kbps) "minimum": 1,
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "value")]
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private bool alert;
        /// <summary>
        /// Required: Enable alert
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "alert")]
        public bool Alert
        {
            get { return alert; }
            set { alert = value; }
        }
    }



    public class Cpu
    {
        private Warning warning;
        /// <summary>
        /// Required: Set limits for warning
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "warning")]
        public Warning Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private Critical critical;
        /// <summary>
        /// Required: Set limits for critical case
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "critical")]
        public Critical Critical
        {
            get { return critical; }
            set { critical = value; }
        }
    }

    public class Ram
    {
        private Warning warning;
        /// <summary>
        /// Required: Set limits for warning
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "warning")]
        public Warning Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private Critical critical;
        /// <summary>
        /// Required: Set limits for critical case
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "critical")]
        public Critical Critical
        {
            get { return critical; }
            set { critical = value; }
        }
    }

    public class Disk
    {
        private DiskWarning warning;
        /// <summary>
        /// Required: Set limits for warning
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "warning")]
        public DiskWarning Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private DiskCritical critical;
        /// <summary>
        /// Required: Set limits for critical case
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "critical")]
        public DiskCritical Critical
        {
            get { return critical; }
            set { critical = value; }
        }
    }
    public class Transfer
    {
        private Warning warning;
        /// <summary>
        /// Required: Set limits for warning
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "warning")]
        public Warning Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private TransferCritical critical;
        /// <summary>
        /// Required: Set limits for critical case
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "critical")]
        public TransferCritical Critical
        {
            get { return critical; }
            set { critical = value; }
        }
    }
    public class InternalPing
    {
        private InternalPingWarning warning;
        /// <summary>
        /// Required: Set limits for warning
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "warning")]
        public InternalPingWarning Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private InternalPingCritical critical;
        /// <summary>
        /// Required: Set limits for critical case
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "critical")]
        public InternalPingCritical Critical
        {
            get { return critical; }
            set { critical = value; }
        }
    }

    public class Thresholds
    {
        private Disk disk;
        /// <summary>
        /// Required: Consumption limits of Disk
        /// </summary>
        ///
        [JsonProperty(PropertyName = "disk")]
        public Disk Disk
        {
            get { return disk; }
            set { disk = value; }
        }

        private Cpu cpu;
        /// <summary>
        /// Required: Consumption limits of CPU
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "cpu")]
        public Cpu Cpu
        {
            get { return cpu; }
            set { cpu = value; }
        }
        private Ram ram;
        /// <summary>
        /// Required: Consumption limits of Ram
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "ram")]
        public Ram Ram
        {
            get { return ram; }
            set { ram = value; }
        }
        private Transfer transfer;
        /// <summary>
        /// Required: Consumption limits of Transfer
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "transfer")]
        public Transfer Transfer
        {
            get { return transfer; }
            set { transfer = value; }
        }
        private InternalPing internal_ping;
        /// <summary>
        /// Required: Response limits of internal ping
        /// </summary>
        ///
        [JsonProperty(PropertyName = "internal_ping")]
        public InternalPing InternalPing
        {
            get { return internal_ping; }
            set { internal_ping = value; }
        }
    }
}
