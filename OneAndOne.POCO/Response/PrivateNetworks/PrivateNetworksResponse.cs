using OneAndOne.POCO.Response.DataCenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.PrivateNetworks
{
    public class PrivateNetworksResponse
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
        private string network_address;

        public string NetworkAddress
        {
            get { return network_address; }
            set { network_address = value; }
        }
        private string subnet_mask;

        public string SubnetMask
        {
            get { return subnet_mask; }
            set { subnet_mask = value; }
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
        private List<PrivateNetworkServerResponse> servers;

        public List<PrivateNetworkServerResponse> Servers
        {
            get { return servers; }
            set { servers = value; }
        }
        private string cloudpanel_id;

        public string CloudpanelId
        {
            get { return cloudpanel_id; }
            set { cloudpanel_id = value; }
        }

        private DataCenterResponse datacenter;
        public DataCenterResponse Datacenter
        {
            get { return datacenter; }

            set { datacenter = value; }
        }
    }

    public class PrivateNetworkServerResponse
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
}
