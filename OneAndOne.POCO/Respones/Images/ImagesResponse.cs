using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.Images
{
    public class ImagesResponse
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
        private string os_image_type;

        public string OsImageType
        {
            get { return os_image_type; }
            set { os_image_type = value; }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private int min_hdd_size;

        public int MinHddSize
        {
            get { return min_hdd_size; }
            set { min_hdd_size = value; }
        }
        private List<object> licenses;

        public List<object> Licenses
        {
            get { return licenses; }
            set { licenses = value; }
        }
        private string cloudpanel_id;

        public string CloudpanelId
        {
            get { return cloudpanel_id; }
            set { cloudpanel_id = value; }
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
        private List<Hdd> hdds;

        public List<Hdd> Hdds
        {
            get { return hdds; }
            set { hdds = value; }
        }
        private string server_id;

        public string ServerId
        {
            get { return server_id; }
            set { server_id = value; }
        }
        private string frequency;

        public string Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }
        private int num_images;

        public int NumImages
        {
            get { return num_images; }
            set { num_images = value; }
        }
        private string creation_date;

        public string CreationDate
        {
            get { return creation_date; }
            set { creation_date = value; }
        }
    }

    public class Hdd
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
        private bool is_main;

        public bool IsMain
        {
            get { return is_main; }
            set { is_main = value; }
        }
    }

}
