using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.DVDs
{
    public class DVDs : ResourceBase
    {
        public DVDs(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }
        /// <summary>
        /// Returns a list of all the operative systems and tools that you can load into your virtual DVD unit.
        /// </summary>
        /// <param name="page">Allows to use pagination. Sets the number of servers that will be shown in each page.</param>
        /// <param name="perPage">Current page to show.</param>
        /// <param name="sort">Allows to sort the result by priority:sort=name retrieves a list of elements ordered by their names.sort=-creation_date retrieves a list of elements ordered according to their creation date in descending order of priority.</param>
        /// <param name="query">Allows to search one string in the response and return the elements that contain it. In order to specify the string use parameter q:    q=My server</param>
        /// <param name="fields">Returns only the parameters requested: fields=id,name,description,hardware.ram</param>

        public List<OneAndOne.POCO.Response.DVDS.DVDResponse> Get(int? page = null, int? perPage = null, string sort = null, string query = null, string fields = null)
        {
            try
            {
                StringBuilder requestUrl = new StringBuilder("/dvd_isos?");
                if (page != null)
                {
                    requestUrl.AppendFormat("&page={0}", page);
                }
                if (perPage != null)
                {
                    requestUrl.AppendFormat("&per_page={0}", perPage);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    requestUrl.AppendFormat("&sort={0}", sort);
                }
                if (!string.IsNullOrEmpty(query))
                {
                    requestUrl.AppendFormat("&q={0}", query);
                }
                if (!string.IsNullOrEmpty(fields))
                {
                    requestUrl.AppendFormat("&fields={0}", fields);
                }
                var request = new RestRequest(requestUrl.ToString(), Method.GET);
                var result = restclient.Execute<List<OneAndOne.POCO.Response.DVDS.DVDResponse>>(request);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(result.Content);
                }
                return result.Data;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Information about specific ISO image
        /// </summary>
        /// <param name="id">Iso's ID</param>
        /// 
        public OneAndOne.POCO.Response.DVDS.DVDResponse Show(string id)
        {
            try
            {
                var request = new RestRequest("/dvd_isos/{id}", Method.GET);
                request.AddUrlSegment("id", id);

                var result = restclient.Execute<OneAndOne.POCO.Response.DVDS.DVDResponse>(request);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(result.Content);
                }
                return result.Data;
            }
            catch
            {
                throw;
            }
        }
    }
}
