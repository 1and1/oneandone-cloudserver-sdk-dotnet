using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Users
{
    public class CreateUserRequest
    {
        private string name;
        /// <summary>
        /// Required: User's name.
        /// </summary>
        /// 
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
        private string password;
        /// <summary>
        /// Required: User's password. Pass must contain at least 8 characters using uppercase letters, numbers and other special symbols.
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "password")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string email;
        /// <summary>
        /// NOT Required: User's e-mail
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
    }
}
