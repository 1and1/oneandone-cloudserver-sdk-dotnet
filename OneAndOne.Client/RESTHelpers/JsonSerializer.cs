using Newtonsoft.Json;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.RESTHelpers
{
    public class CustomSerializer : ISerializer
    {
        private string _contentType = "application/json";

        public string Serialize(object obj)
        {
            string retVal = JsonConvert.SerializeObject(obj);
            return retVal;
        }

        public string RootElement { get; set; }

        public string Namespace { get; set; }

        public string DateFormat { get; set; }

        public string ContentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }
    }
}
