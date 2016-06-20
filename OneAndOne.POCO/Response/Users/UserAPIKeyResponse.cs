using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.Users
{
    public class UserAPIKeyResponse
    {
        private string key;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }
    }
}
