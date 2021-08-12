using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class HttpHelper
    {
        private static readonly HttpClient HttpClient;

        static HttpHelper()
        {
            HttpClient = new HttpClient();
        }

        /// <summary>
        /// Get 一个请求
        /// </summary>
        /// <param name="requestUri">请求URL</param>
        /// <returns></returns>
        public static string Get(string requestUri)
        {
            var response = HttpClient.GetAsync(requestUri);
            return response.Result.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// Get 一个请求
        /// </summary>
        /// <param name="requestUri">请求URL</param>
        /// <param name="urlDictionary">请求参数</param>
        /// <returns></returns>
        public static string Get(string requestUri, Dictionary<string, string> urlDictionary)
        {
            var paramter = urlDictionary.Aggregate(string.Empty, (current, item) => current + (item.Key + "=" + item.Value + "&"));
            var response = HttpClient.GetAsync(requestUri + "?" + paramter.TrimEnd('&'));
            return response.Result.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// 异步 Get
        /// </summary>
        /// <param name="requestUri">请求URL</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string requestUri)
        {
            var response = await HttpClient.GetAsync(requestUri);
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 异步 Get
        /// </summary>
        /// <param name="requestUri">请求URL</param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(string requestUri)
        {
            var response = await HttpClient.GetAsync(requestUri);
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }


        /// <summary>
        /// Get 一个请求
        /// </summary>
        /// <param name="requestUri">请求URL</param>
        /// <param name="urlDictionary">请求参数</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string requestUri, Dictionary<string, string> urlDictionary)
        {
            var paramter = urlDictionary.Aggregate(string.Empty, (current, item) => current + (item.Key + "=" + item.Value + "&"));
            var response = await HttpClient.GetAsync(requestUri + "?" + paramter.TrimEnd('&'));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }

        /// <summary>
        /// Post 一个请求
        /// </summary>
        /// <param name="requestUri">请求URL</param>
        /// <param name="urlDictionary">请求参数</param>
        /// <returns></returns>
        public static string Post(string requestUri, Dictionary<string, string> urlDictionary)
        {
            var paramter = new FormUrlEncodedContent(urlDictionary);
            var response = HttpClient.PostAsync(requestUri, paramter);
            return response.Result.Content.ReadAsStringAsync().Result;
        }

        /// <summary>
        /// 异步Post 一个请求
        /// </summary>
        /// <param name="requestUri">请求URL</param>
        /// <param name="urlDictionary">请求参数</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string requestUri, Dictionary<string, string> urlDictionary)
        {
            var paramter = new FormUrlEncodedContent(urlDictionary);
            var response = await HttpClient.PostAsync(requestUri, paramter);
            return await response.Content.ReadAsStringAsync();
        }


        /// <summary>
        /// 异步 Post
        /// </summary>
        /// <param name="requestUri">请求URL</param>
        /// <returns></returns>
        public static async Task<T> SetHeaderGetAsync<T>(string requestUri)
        {
            HttpClient.DefaultRequestHeaders.Add("x-ContentType-foo", "text/html;charset=UTF-8");
            var response = await HttpClient.GetAsync(requestUri);
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public static string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "application/json;charset=UTF-8";
            //SetHeaderValue(request.Headers, "Accept-Encoding", "gzip");  //启用gzip
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
    }
}
