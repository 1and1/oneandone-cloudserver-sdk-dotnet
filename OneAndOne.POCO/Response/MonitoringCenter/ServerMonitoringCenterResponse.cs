using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.MonitoringCenter
{
    public class ServerMonitoringCenterResponse
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
        private ServerStatus status;

        public ServerStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        private AlertsCount alerts;

        public AlertsCount Alerts
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
        private ServerCpu cpu;

        public ServerCpu Cpu
        {
            get { return cpu; }
            set { cpu = value; }
        }
        private ServerRam ram;

        public ServerRam Ram
        {
            get { return ram; }
            set { ram = value; }
        }
        private ServerTransfer transfer;

        public ServerTransfer Transfer
        {
            get { return transfer; }
            set { transfer = value; }
        }
        private ServerInternalPing internal_ping;

        public ServerInternalPing InternalPing
        {
            get { return internal_ping; }
            set { internal_ping = value; }
        }
    }

    public class ServerStatus
    {
        private string state;

        public string State
        {
            get { return state; }
            set { state = value; }
        }
    }
    public class AlertsCount
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
    public class InfoData
    {
        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        private double? used_percent;

        public double? UsedPercent
        {
            get { return used_percent; }
            set { used_percent = value; }
        }
    }
    public class Unit
    {
        private string used_percent;

        public string UsedPercent
        {
            get { return used_percent; }
            set { used_percent = value; }
        }
    }
    public class ServerCpu
    {
        private int warning;

        public int Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private int critical;

        public int Critical
        {
            get { return critical; }
            set { critical = value; }
        }
        private List<InfoData> data;

        public List<InfoData> Data
        {
            get { return data; }
            set { data = value; }
        }
        private Unit unit;

        public Unit Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
    public class ServerRam
    {
        private int warning;

        public int Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private int critical;

        public int Critical
        {
            get { return critical; }
            set { critical = value; }
        }
        private List<InfoData> data;

        public List<InfoData> Data
        {
            get { return data; }
            set { data = value; }
        }
        private Unit unit;

        public Unit Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
    public class TransferData
    {
        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        private int downstream;

        public int Downstream
        {
            get { return downstream; }
            set { downstream = value; }
        }
        private int upstream;

        public int Upstream
        {
            get { return upstream; }
            set { upstream = value; }
        }
    }
    public class TransferUnit
    {
        private string downstream;

        public string Downstream
        {
            get { return downstream; }
            set { downstream = value; }
        }
        private string upstream;

        public string Upstream
        {
            get { return upstream; }
            set { upstream = value; }
        }
    }
    public class ServerTransfer
    {
        private int warning;

        public int Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private int critical;

        public int Critical
        {
            get { return critical; }
            set { critical = value; }
        }
        private List<TransferData> data;

        public List<TransferData> Data
        {
            get { return data; }
            set { data = value; }
        }
        private TransferUnit unit;

        public TransferUnit Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
    public class InternalPingData
    {
        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        private int pl;

        public int Pl
        {
            get { return pl; }
            set { pl = value; }
        }
        private int rta;

        public int Rta
        {
            get { return rta; }
            set { rta = value; }
        }
    }
    public class InternalPingUnit
    {
        private string pl;

        public string Pl
        {
            get { return pl; }
            set { pl = value; }
        }
        private string rta;

        public string Rta
        {
            get { return rta; }
            set { rta = value; }
        }
    }
    public class ServerInternalPing
    {
        private int warning;

        public int Warning
        {
            get { return warning; }
            set { warning = value; }
        }
        private int critical;

        public int Critical
        {
            get { return critical; }
            set { critical = value; }
        }
        private List<InternalPingData> data;

        public List<InternalPingData> Data
        {
            get { return data; }
            set { data = value; }
        }
        private InternalPingUnit unit;

        public InternalPingUnit Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
