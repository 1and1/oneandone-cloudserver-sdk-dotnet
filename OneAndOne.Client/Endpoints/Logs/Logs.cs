﻿using OneAndOne.POCO;
using OneAndOne.POCO.Respones.Logs;
using OneAndOne.POCO.Respones.MonitoringCenter;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.Logs
{
    public class Logs : ResourceBase
    {
        public Logs(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }
        ///<summary>
        ///Returns a list with logs.
        ///</summary>
        ///<param name="page">Allows to use pagination. Sets the number of servers that will be shown in each page.</param>
        ///<param name="perPage">Current page to show.</param>
        ///<param name="sort">Allows to sort the result by priority:sort=name retrieves a list of elements ordered by their names.sort=-creation_date retrieves a list of elements ordered according to their creation date in descending order of priority.</param>
        ///<param name="query">Allows to search one string in the response and return the elements that contain it. In order to specify the string use parameter q:    q=My server</param>
        ///<param name="fields">Returns only the parameters requested: fields=id,name,description,hardware.ram</param>
        ///<param name="period">required (one of LAST_HOUR,LAST_24H,LAST_7D,LAST_30D,LAST_365D,CUSTOM ),Time range whose logs will be shown.</param>
        ///<param name="start_date">(date) The first date in a custom range. Required only if selected period is CUSTOM.</param>
        ///<param name="end_date">(date) The second date in a custom range. Required only if selected period is CUSTOM.</param>
        public List<LogsResponse> Get(PeriodType period, int? page = null, int? perPage = null, string sort = null, string query = null, string fields = null, DateTime? start_date = null, DateTime? end_date = null)
        {
            try
            {
                string requestUrl = "/logs?";
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
                requestUrl += string.Format("&period={0}", period);
                if (period == PeriodType.CUSTOM)
                {
                    requestUrl += string.Format("&start_date={0}", start_date.Value.ToString("s") + "Z");
                    requestUrl += string.Format("&end_date={0}", end_date.Value.ToString("s") + "Z");
                }
                var request = new RestRequest(requestUrl, Method.GET);

                var result = restclient.Execute<List<LogsResponse>>(request);
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
        /// Returns information about a log
        /// </summary>
        /// <param name="log_id">Unique monitoring policy's identifier.</param>
        /// 
        public LogsResponse Show(string log_id)
        {
            try
            {
                var request = new RestRequest("/logs/{log_id}", Method.GET);
                request.AddUrlSegment("log_id", log_id);

                var result = restclient.Execute<LogsResponse>(request);
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
