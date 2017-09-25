using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Servers
{
    public class UpdateStatusRequest
    {
        
        private string action;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "action")]
        public ServerAction Action
        {
            get { return (ServerAction)Enum.Parse(typeof(ServerAction), action); }
            set
            {
                action = ((ServerAction)value).ToString();
            }
        }
        private string method;
        [JsonProperty(PropertyName = "method")]
        public ServerActionMethod Method
        {
            get { return (ServerActionMethod)Enum.Parse(typeof(ServerActionMethod), method); }
            set
            {
                method = ((ServerActionMethod)value).ToString();
            }
        }
        private bool recovery_mode;
        [JsonProperty(PropertyName = "recovery_mode")]
        public bool RecoveryMode
        {
            get { return recovery_mode; }
            set { recovery_mode = value; }
        }
        private string recovery_image_id;
        [JsonProperty(PropertyName = "recovery_image_id")]
        public string RecoveryImageId
        {
            get { return recovery_image_id; }
            set { recovery_image_id = value; }
        }

    }
}
