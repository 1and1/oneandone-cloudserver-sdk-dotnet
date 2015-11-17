using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.MonitoringCenter
{

    public enum PeriodType
    {
        LAST_HOUR, LAST_24H, LAST_7D, LAST_30D, LAST_365D, CUSTOM
    }
    public class MonitoringCenterResponse
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
        private Status status;

        public Status Status
        {
            get { return status; }
            set { status = value; }
        }
        private Alerts alerts;

        public Alerts Alerts
        {
            get { return alerts; }
            set { alerts = value; }
        }
        private Agent agent;

        public Agent Agent
        {
            get { return agent; }
            set { agent = value; }
        }
    }
    public class Cpu
    {
        private string state;

        public string State
        {
            get { return state; }
            set { state = value; }
        }
    }
    public class Ram
    {
        private string state;

        public string State
        {
            get { return state; }
            set { state = value; }
        }
    }
    public class Disk
    {
        private string state;

        public string State
        {
            get { return state; }
            set { state = value; }
        }
    }
    public class Transfer
    {
        private string state;

        public string State
        {
            get { return state; }
            set { state = value; }
        }
    }
    public class InternalPing
    {
        private string state;

        public string State
        {
            get { return state; }
            set { state = value; }
        }
    }
    public class Status
    {
        private string state;

        public string State
        {
            get { return state; }
            set { state = value; }
        }
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
        private Disk disk;

        public Disk Disk
        {
            get { return disk; }
            set { disk = value; }
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
    public class Resources
    {
        private int critical;

        public int Critical
        {
            get { return critical; }
            set { critical = value; }
        }
        private int warning;

        public int Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private int ok;

        public int Ok
        {
            get { return ok; }
            set { ok = value; }
        }
    }
    public class Ports
    {
        private int critical;

        public int Critical
        {
            get { return critical; }
            set { critical = value; }
        }
        private int warning;

        public int Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private int ok;

        public int Ok
        {
            get { return ok; }
            set { ok = value; }
        }
    }
    public class Process
    {
        private int critical;

        public int Critical
        {
            get { return critical; }
            set { critical = value; }
        }
        private int warning;

        public int Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private int ok;

        public int Ok
        {
            get { return ok; }
            set { ok = value; }
        }
    }
    public class Alerts
    {
        private Resources resources;

        public Resources Resources
        {
            get { return resources; }
            set { resources = value; }
        }
        private Ports ports;

        public Ports Ports
        {
            get { return ports; }
            set { ports = value; }
        }
        private Process process;

        public Process Process
        {
            get { return process; }
            set { process = value; }
        }
    }
    public class Agent
    {
        private bool agent_installed;

        public bool AgentInstalled
        {
            get { return agent_installed; }
            set { agent_installed = value; }
        }
        private bool monitoring_needs_agent;

        public bool MonitoringNeedsAgent
        {
            get { return monitoring_needs_agent; }
            set { monitoring_needs_agent = value; }
        }
        private bool missing_agent_alert;

        public bool MissingAgentAlert
        {
            get { return missing_agent_alert; }
            set { missing_agent_alert = value; }
        }
    }

}
