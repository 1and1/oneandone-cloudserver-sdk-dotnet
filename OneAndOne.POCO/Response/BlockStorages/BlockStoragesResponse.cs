using OneAndOne.POCO.Response.DataCenters;

namespace OneAndOne.POCO.Response.BlockStorages
{
    public class BlockStoragesResponse
    {
        private string id;
        /// <summary>
        /// Block storage ID
        /// </summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private int size;
        /// <summary>
        /// Block storage total size. min: 20, max: 500, multipleOf: 10
        /// </summary>
        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        private string state;
        /// <summary>
        /// Block storage state
        /// </summary>
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        private string description;
        /// <summary>
        /// Block storage description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private DataCenterResponse datacenter;
        /// <summary>
        /// Location where the resource is available
        /// </summary>
        public DataCenterResponse Datacenter
        {
            get { return datacenter; }
            set { datacenter = value; }
        }

        private string name;
        /// <summary>
        /// Block storage name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string creation_date;
        /// <summary>
        /// Date when Block storage was created
        /// </summary>
        public string CreationDate
        {
            get { return creation_date; }
            set { creation_date = value; }
        }

        private Server server;
        /// <summary>
        /// Server allowed to access to the block storage
        /// </summary>
        public Server Server
        {
            get { return server; }
            set { server = value; }
        }
    }

    public class Server
    {
        private string id;
        /// <summary>
        /// Server ID
        /// </summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        /// <summary>
        /// Server name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
