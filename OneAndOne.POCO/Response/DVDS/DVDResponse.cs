using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.DVDS
{
    public class DVDResponse
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
        private string os_family;

        public string OsFamily
        {
            get { return os_family; }
            set { os_family = value; }
        }
        private string os;

        public string Os
        {
            get { return os; }
            set { os = value; }
        }
        private string os_version;

        public string OsVersion
        {
            get { return os_version; }
            set { os_version = value; }
        }
        private int architecture;

        public int Architecture
        {
            get { return architecture; }
            set { architecture = value; }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
