using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Images
{
    public class UpdateImageRequest
    {
        private string name;
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string description;
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
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
    }
}
