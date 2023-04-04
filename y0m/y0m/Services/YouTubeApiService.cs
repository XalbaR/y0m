using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using y0m.Services;

namespace y0m.Services
{
    internal class YouTubeApiService
    {
        protected HttpClient httpClient;

        public YouTubeApiService()
        {
            httpClient = new HttpClient();
        }

        protected async Task<JObject> SendRequestAsync(string requestUrl)
        {
            var response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                JObject jObject = JObject.Parse(json);
                return jObject;
            }
            else
            {
                // İstek başarısız olduğunda hata işleme kodunuzu buraya ekleyin
                string errorJson = await response.Content.ReadAsStringAsync();
                JObject errorJObject = JObject.Parse(errorJson);
                string errorMessage = errorJObject["error"]["message"].ToString();
                System.Diagnostics.Debug.WriteLine($"YouTube API error: {errorMessage}");
                return null;
            }
        }

        public async Task<JObject> GetPopularVideosAsync()
        {
            string requestUrl = YouTubeApis.GetListUrl();
            return await SendRequestAsync(requestUrl);
        }

        public async Task<JObject> SearchVideosAsync(string query)
        {
            string requestUrl = YouTubeApis.GetSearchUrl(query);
            return await SendRequestAsync(requestUrl);
        }
    }
}
