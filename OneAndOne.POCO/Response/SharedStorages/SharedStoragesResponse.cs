using OneAndOne.POCO.Response.DataCenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.SharedStorages
{
    public class SharedStoragesResponse
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
        private string state;
        public string State
        {
            get { return state; }
            set { state = value; }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string cloudpanel_id;
        public string CloudpanelId
        {
            get { return cloudpanel_id; }
            set { cloudpanel_id = value; }
        }
        private string size_used;
        public string SizeUsed
        {
            get { return size_used; }
            set { size_used = value; }
        }
        private string cifs_path;
        public string CifsPath
        {
            get { return cifs_path; }
            set { cifs_path = value; }
        }
        private string nfs_path;
        public string NfsPath
        {
            get { return nfs_path; }
            set { nfs_path = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string creation_date;
        public string CreationDate
        {
            get { return creation_date; }
            set { creation_date = value; }
        }
        private List<Server> servers;
        public List<Server> Servers
        {
            get { return servers; }
            set { servers = value; }
        }

        private DataCenterResponse datacenter;
        public DataCenterResponse Datacenter
        {
            get { return datacenter; }

            set { datacenter = value; }
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
        private string rights;
        public string Rights
        {
            get { return rights; }
            set { rights = value; }
        }
    }
}
