using Newtonsoft.Json;

namespace OneAndOne.POCO.Requests.SshKeys
{
    public class UpdateSshKeyRequest
    {
        private string name;
        ///<summary>
        ///SSH Key name.
        ///</summary>
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string description;
        ///<summary>
        ///SSH Key description.
        ///</summary>
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
