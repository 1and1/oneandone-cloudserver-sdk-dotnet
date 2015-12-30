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
    }

    public enum ImageFrequency
    {
        ONCE,
        DAILY,
        WEEKLY,
        Null
    }
}
