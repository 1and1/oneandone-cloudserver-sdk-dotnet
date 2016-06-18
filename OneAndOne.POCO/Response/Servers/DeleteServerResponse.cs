using OneAndOne.POCO.Requests.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.Servers
{
    public class DeleteServerResponse
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string cloudpanel_id;

        public string CloudPanelId
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

        public string CreationDate
        {
            get { return creation_date; }
            set { creation_date = value; }
        }
        private string first_password;

        public string FirstPassword
        {
            get { return first_password; }
            set { first_password = value; }
        }
        private Status status;

        public Status Status
        {
            get { return status; }
            set { status = value; }
        }
        private Hardware hardware;

        public Hardware Hardware
        {
            get { return hardware; }
            set { hardware = value; }
        }
        private Image image;

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }
        private Dvd dvd;

        public Dvd Dvd
        {
            get { return dvd; }
            set { dvd = value; }
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
        private List<Alert> alerts;

        public List<Alert> Alerts
        {
            get { return alerts; }
            set { alerts = value; }
        }
        private string monitoringr_policy;

        public string MonitoringrPolicy
        {
            get { return monitoringr_policy; }
            set { monitoringr_policy = value; }
        }
        private List<OneAndOne.POCO.Requests.Servers.PrivateNetworks> private_networks;

        public List<OneAndOne.POCO.Requests.Servers.PrivateNetworks> PrivateNetworks
        {
            get { return private_networks; }
            set { private_networks = value; }
        }
    }
}
