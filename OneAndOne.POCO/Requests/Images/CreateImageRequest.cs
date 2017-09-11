using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Images
{
    public class CreateImageRequest
    {
        private string server_id;
        ///<summary>
        ///Server ID.
        ///</summary>
        [JsonProperty(PropertyName = "server_id")]
        public string ServerId
        {
            get { return server_id; }
            set { server_id = value; }
        }

        private string name;
        ///<summary>
        ///Image name.
        ///</summary>
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string description;
        ///<summary>
        ///Image description
        ///</summary>
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string frequency;
        [JsonProperty(PropertyName = "frequency")]
        ///<summary>
        ///Creation policy frequency
        ///</summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ImageFrequency Frequency
        {
            get { return (ImageFrequency)Enum.Parse(typeof(ImageFrequency), frequency); }
            set
            {
                frequency = ((ImageFrequency)value).ToString();
            }
        }

        private int num_images;
        ///<summary>
        ///Maximum number of images
        ///</summary>
        [JsonProperty(PropertyName = "num_images")]
        public int NumIimages
        {
            get { return num_images; }
            set { num_images = value; }
        }

        ///<summary>
        ///ID of the datacenter where the shared storage will be created
        ///</summary>
        private string datacetner_id;

        public string DatacetnerId
        {
            get { return datacetner_id; }

            set { datacetner_id = value; }
        }

        ///<summary>
        ///Source of the new image: server (from an existing server), image (from an imported image) or iso (from an imported iso)
        ///</summary>
        private string source;

        [JsonConverter(typeof(StringEnumConverter))]
        public ImageSource Source
        {
            get { return (ImageSource)Enum.Parse(typeof(ImageSource), source); }
            set { source = ((ImageSource)value).ToString(); }
        }

        ///<summary>
        ///URL where the image can be downloaded. It is required when the source is image or iso.
        ///</summary>
        private String url;
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        ///<summary>
        ///ID of the Operative System you want to import. You can get a list of the available ones with the method /iamges/os.
        ///</summary>
        private String os_id;

        public string OsId
        {
            get { return os_id; }
            set { os_id = value; }
        }

        ///<summary>
        ///Type of the ISO you want to import: os (Operative System) or app (Application).  It is required when the source is iso.
        ///</summary>
        private string type;
        [JsonConverter(typeof(StringEnumConverter))]
        public ImageType Type
        {
            get
            {
                if (type == null) return ImageType.Null;
                return (ImageType)Enum.Parse(typeof(ImageType), type);
            }
            set { type = ((ImageType)value).ToString(); }
        }
    }

    public enum ImageFrequency
    {
        ONCE,
        DAILY,
        WEEKLY,
        Null
    }

    public enum ImageType
    {
        os,
        app,
        Null
    }

    public enum ImageSource
    {
        server,
        image,
        iso,
        Null
    }
}
