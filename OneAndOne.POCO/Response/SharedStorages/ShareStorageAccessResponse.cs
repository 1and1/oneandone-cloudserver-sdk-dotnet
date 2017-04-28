using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.SharedStorages
{
    public class SharedStorageAccessResponse
    {

        private string site_id;

        public string SiteId
        {
            get { return site_id; }
            set { site_id = value; }
        }

        private int needs_password_reset;

        public int NeedsPasswordReset
        {
            get { return needs_password_reset; }
            set { needs_password_reset = value; }
        }
        private string state;
        public string State
        {
            get { return state; }
            set { state = value; }
        }
        private string kerberos_content_file;
        public string KerberosContentFile
        {
            get { return kerberos_content_file; }
            set { kerberos_content_file = value; }
        }
        private string user_domain;
        public string UserDomain
        {
            get { return user_domain; }
            set
            {
                user_domain = value;
            }
        }
    }
}
