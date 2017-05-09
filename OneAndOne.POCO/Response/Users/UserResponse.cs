using OneAndOne.POCO.Requests.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.Users
{
    public class UserResponse
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
        private object description;

        public object Description
        {
            get { return description; }
            set { description = value; }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private UserState state;

        public UserState State
        {
            get { return state; }
            set { state = value; }
        }
        private string creation_date;

        public string CreationDate
        {
            get { return creation_date; }
            set { creation_date = value; }
        }
        private ApiResponse api;

        public ApiResponse Api
        {
            get { return api; }
            set { api = value; }
        }
    }

    public class ApiResponse
    {
        private bool active;

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        private string key;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        private List<string> allowed_ips;

        public List<string> AllowedIps
        {
            get { return allowed_ips; }
            set { allowed_ips = value; }
        }
    }
}
