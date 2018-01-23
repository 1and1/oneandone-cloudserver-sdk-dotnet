using Newtonsoft.Json;

namespace OneAndOne.POCO.Requests.SshKeys
{
    public class CreateSshKeyRequest
    {
        ///<summary>
        ///Ssh key name.
        ///</summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        ///<summary>
        ///Ssh key description.
        ///</summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        ///<summary>
        ///Public key to import. If not given, new SSH key pair will be
        ///created and the private key is returned in the response.
        ///</summary>
        [JsonProperty(PropertyName = "public_key")]
        public string PublicKey { get; set; }
    }
}
