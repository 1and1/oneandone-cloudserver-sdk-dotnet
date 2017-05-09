using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.DataCenters
{
    public class DataCenterResponse
    {
        private string id;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        private string location;

        public string Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }
        private string country_code;

        public string CountryCode
        {
            get
            {
                return country_code;
            }

            set
            {
                country_code = value;
            }
        }
    }
}
