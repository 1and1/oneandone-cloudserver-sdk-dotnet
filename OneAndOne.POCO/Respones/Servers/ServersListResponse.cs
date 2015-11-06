using System;
using System.Collections.Generic;
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
        private Image imarge;

        public Image Imarge
        {
            get { return imarge; }
            set { imarge = value; }
        }
        private Hardware hardware;

        public Hardware Hardware
        {
            get { return hardware; }
            set { hardware = value; }
        }
        private List<IpAddress> ips;

        public List<IpAddress> Ips
        {
            get { return ips; }
            set { ips = value; }
        }
        private List<Alert> alerts;

        public List<Alert> Alerts
        {
            get { return alerts; }
            set { alerts = value; }
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
        private object percent;

        public object Percent
        {
            get { return percent; }
            set { percent = value; }
        }
    }

    public class Alert
    {
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
    }

}
