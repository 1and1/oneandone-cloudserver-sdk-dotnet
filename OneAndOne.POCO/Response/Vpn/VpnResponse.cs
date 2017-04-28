using OneAndOne.POCO.Response.DataCenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.Vpn
{
    public class VpnResponse
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

        private object description;

        public object Description
        {
            get { return description; }

            set { description = value; }
        }

        private string state;

        public string State
        {
            get
            { return state; }
            set { state = value; }
        }

        private DataCenterResponse datacenter;

        public DataCenterResponse Datacenter
        {
            get { return datacenter; }

            set { datacenter = value; }
        }

        private string type;

        public string Type
        {
            get { return type; }

            set { type = value; }
        }

        private List<string> ips;

        public List<string> Ips
        {
            get { return ips; }
            set { ips = value; }
        }

        private string cloudpanel_id;

        public string CloudpanelId
        {
            get { return cloudpanel_id; }

            set { cloudpanel_id = value; }
        }

        private string creation_date;

        public string CreationDate
        {
            get { return creation_date; }

            set { creation_date = value; }
        }
    }

    public class Config
    {
        private string config_zip_file;

        /// <summary>
        /// A string with base64 format with the configuration for OpenVPN. It is a zip file.
        /// </summary>
        /// 
        public string ConfigZipFile
        {
            get
            {
                return config_zip_file;
            }

            set
            {
                config_zip_file = value;
            }
        }
    }
}
