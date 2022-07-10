using Newtonsoft.Json;
using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.Images;
using OneAndOne.POCO.Response.Images;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.Images
{
    public class Images : ResourceBase
    {
        public Images(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }
        #region Basic Operations
        /// <summary>
        /// Returns a list of your images.
        /// </summary>
        /// <param name="page">Allows to use pagination. Sets the number of servers that will be shown in each page.</param>
        /// <param name="perPage">Current page to show.</param>
        /// <param name="sort">Allows to sort the result by priority:sort=name retrieves a list of elements ordered by their names.sort=-creation_date retrieves a list of elements ordered according to their creation date in descending order of priority.</param>
        /// <param name="query">Allows to search one string in the response and return the elements that contain it. In order to specify the string use parameter q:    q=My server</param>
        /// <param name="fields">Returns only the parameters requested: fields=id,name,description,hardware.ram</param>
        public List<ImagesResponse> Get(int? page = null, int? perPage = null, string sort = null, string query = null, string fields = null)
        {
            try
            {
                StringBuilder requestUrl = new StringBuilder("/images?");
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

                var result = restclient.Execute<List<ImagesResponse>>(request);
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

        ///<summary>
        //Adds a new image.
        //</summary>
        public ImagesResponse Create(CreateImageRequest image)
        {
            try
            {
                var request = new RestRequest("/images", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddBody(image);

                var result = restclient.Execute(request);
                if (result.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception(result.Content);
                }
                return JsonConvert.DeserializeObject<ImagesResponse>(result.Content);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns information about one flavour.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// 
        public ImagesResponse Show(string image_id)
        {
            try
            {
                var request = new RestRequest("/images/{image_id}", Method.GET);
                request.AddUrlSegment("image_id", image_id);

                var result = restclient.Execute<ImagesResponse>(request);
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
        /// Modifies an image.
        /// </summary>
        public ImagesResponse Update(UpdateImageRequest image, string image_id)
        {
            try
            {
                var request = new RestRequest("/images/{image_id}", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("image_id", image_id);
                request.AddBody(image);

                var result = restclient.Execute<ImagesResponse>(request);
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
        /// Removes an image.
        /// </summary>
        /// <param name="image_id">required (string ), Unique image's identifier.</param>
        /// 
        public ImagesResponse Delete(string image_id)
        {
            try
            {
                var request = new RestRequest("/images/{image_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("image_id", image_id);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<ImagesResponse>(request);
                if (result.StatusCode != HttpStatusCode.Accepted)
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
        #endregion
    }
}
