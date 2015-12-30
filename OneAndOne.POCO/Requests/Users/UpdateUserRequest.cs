using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Users
{
    public enum UserState
    {
        ACTIVE, DISABLE
    }
    public class UpdateUserRequest
    {
        private string state;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "state")]
        public UserState State
        {
            get { return (UserState)Enum.Parse(typeof(UserState), state); }
            set
            {
                state = ((UserState)value).ToString();
            }
        }
        private string description;
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string password;
        [JsonProperty(PropertyName = "password")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string email;
        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
    }
}
