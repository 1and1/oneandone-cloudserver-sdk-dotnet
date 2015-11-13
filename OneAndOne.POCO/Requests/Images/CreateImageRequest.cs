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
        //<summary>
        //Server ID.
        //</summary>
        private string server_id;
        [JsonProperty(PropertyName = "server_id")]
        public string ServerId
        {
            get { return server_id; }
            set { server_id = value; }
        }
        //<summary>
        //Image name.
        //</summary>
        private string name;
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        //<summary>
        //Image description
        //</summary>
        private string description;
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        //<summary>
        //Creation policy frequency
        //</summary>
        private string frequency;
        [JsonProperty(PropertyName = "frequency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ImageFrequency Frequency
        {
            get { return (ImageFrequency)Enum.Parse(typeof(ImageFrequency), frequency); }
            set
            {
                frequency = ((ImageFrequency)value).ToString();
            }
        }
        //<summary>
        //Maximum number of images
        //</summary>
        private int num_images;
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
    }
}
