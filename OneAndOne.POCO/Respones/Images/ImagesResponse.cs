using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OneAndOne.POCO.Requests.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
        [JsonConverter(typeof(StringEnumConverter))]
        public OSFamliyType OsFamily
        {
            get { return (OSFamliyType)Enum.Parse(typeof(OSFamliyType), os_family); }
            set
            {
                os_family = ((OSFamliyType)value).ToString();
            }
        }
        private string os;
        [JsonConverter(typeof(StringEnumConverter))]
        public OSType Os
        {
            get
            {
                if (os == null)
                {
                    return OSType.Null;
                }
                return (OSType)Enum.Parse(typeof(OSType), os);
            }
            set
            {
                os = ((OSType)value).ToString();
            }
        }
        private string os_version;

        public string OsVersion
        {
            get { return os_version; }
            set { os_version = value; }
        }
        private int architecture;
        [JsonConverter(typeof(StringEnumConverter))]
        public ArchitectureType Architecture
        {
            get { return (ArchitectureType)Enum.Parse(typeof(ArchitectureType), architecture.ToString()); }
            set
            {
                architecture = (int)(ArchitectureType)Enum.Parse(typeof(ArchitectureType), value.ToString());
            }
        }
        private string os_image_type;

        public string OsImageType
        {
            get { return os_image_type; }
            set { os_image_type = value; }
        }
        private string type;
        [JsonConverter(typeof(StringEnumConverter))]
        public ImageType Type
        {
            get
            {
                if (type == null)
                {
                    return ImageType.NULL;
                }
                return (ImageType)Enum.Parse(typeof(ImageType), type);
            }
            set
            {
                type = ((ImageType)value).ToString();
            }
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
        [JsonConverter(typeof(StringEnumConverter))]
        public ImageFrequency Frequency
        {
            get
            {
                if (frequency == null)
                {
                    return ImageFrequency.Null;
                }
                return (ImageFrequency)Enum.Parse(typeof(ImageFrequency), frequency);
            }
            set
            {
                frequency = ((ImageFrequency)value).ToString();
            }
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
