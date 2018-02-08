using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using OneAndOne.POCO.Response.RecoveryAppliances;

namespace OneAndOne.Client.Endpoints.RecoveryAppliances
{
    public class RecoveryAppliances : ResourceBase
    {
        public RecoveryAppliances(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }
        /// <summary>
        /// Returns a list of all the appliances that you can use for recovering purposes..
        /// </summary>
        /// <param name="page">Allows to use pagination. Sets the number of appliances that will be shown in each page.</param>
        /// <param name="perPage">Current page to show.</param>
        /// <param name="sort">Allows to sort the result by priority:sort=name retrieves a list of elements ordered by their names.sort=-creation_date retrieves a list of elements ordered according to their creation date in descending order of priority.</param>
        /// <param name="query">Allows to search one string in the response and return the elements that contain it. In order to specify the string use parameter q:    q=myRevoceryAppliance</param>
        /// <param name="fields">Returns only the parameters requested: fields=id,name,os_family,os,os_version,os_architecture</param>
        public List<RecoveryAppliancesResponse> Get(int? page = null, int? perPage = null, string sort = null, string query = null, string fields = null)
        {
            try
            {
                string requestUrl = "/recovery_appliances?";
                if (page != null)
                {
                    requestUrl += string.Format("&page={0}", page);
                }
                if (perPage != null)
                {
                    requestUrl += string.Format("&per_page={0}", perPage);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    requestUrl += string.Format("&sort={0}", sort);
                }
                if (!string.IsNullOrEmpty(query))
                {
                    requestUrl += string.Format("&q={0}", query);
                }
                if (!string.IsNullOrEmpty(fields))
                {
                    requestUrl += string.Format("&fields={0}", fields);
                }
                var request = new RestRequest(requestUrl, Method.GET);

                var result = restclient.Execute(request);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(result.Content);
                }
                return JsonConvert.DeserializeObject<List<RecoveryAppliancesResponse>>(result.Content);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Information about specific recovery appliance
        /// </summary>
        /// <param name="id">Recovery appliance's ID</param>
        /// 
        public RecoveryApplianceResponse Show(string id)
        {
            try
            {
                var request = new RestRequest("/recovery_appliances/{id}", Method.GET);
                request.AddUrlSegment("id", id);

                var result = restclient.Execute<RecoveryApplianceResponse>(request);
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
