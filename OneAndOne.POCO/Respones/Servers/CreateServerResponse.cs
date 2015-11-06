using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.Servers
{
    public class CreateServerResponse
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
        private object dvd;

        public object Dvd
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
        private object ips;

        public object Ips
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
        private object monitoringr_policy;

        public object MonitoringrPolicy
        {
            get { return monitoringr_policy; }
            set { monitoringr_policy = value; }
        }
        private object private_networks;

        public object PrivateNetworks
        {
            get { return private_networks; }
            set { private_networks = value; }
        }
    }
}
