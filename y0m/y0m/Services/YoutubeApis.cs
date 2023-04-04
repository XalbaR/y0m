using System;
using System.Collections.Generic;

namespace y0m.Services
{
    public static class YouTubeApis
    {
        private const string ApiKey = "AIzaSyDHVLdFpixRJAdEdEDreffZgKORsFRIni4"; // API anahtarınızı buraya girin

        public static class Endpoints
        {
            public const string BaseUrl = "https://youtube.googleapis.com/youtube/v3";

            public const string Search = "/search";
            public const string Videos = "/videos";
        }

        public static class Params
        {
            public static readonly Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "part", "id,snippet,player,status,contentDetails" },
                { "chart", "mostPopular" },
                { "maxHeight", "50" },
                { "maxResults", "100" },
                { "regionCode", "tr" },
                { "videoCategoryId", "10" },
                { "key", ApiKey }
            };

            public static readonly Dictionary<string, string> Search = new Dictionary<string, string>
            {
                { "part", "snippet,id" },
                { "type", "video" },
                { "maxResults", "20" },
                { "order", "viewCount" },
                { "regionCode", "tr" },
                { "videoCategoryId", "10" },
                { "key", ApiKey }
            };
        }

        public static string GetListUrl()
        {
            string queryParams = BuildQueryString(Params.List);
            return $"{Endpoints.BaseUrl}{Endpoints.Videos}{queryParams}";
        }

        public static string GetSearchUrl(string query)
        {
            Dictionary<string, string> searchParams = new Dictionary<string, string>(Params.Search)
            {
                { "q", query }
            };

            string queryParams = BuildQueryString(searchParams);
            return $"{Endpoints.BaseUrl}{Endpoints.Search}{queryParams}";
        }

        private static string BuildQueryString(Dictionary<string, string> parameters)
        {
            List<string> keyValuePairs = new List<string>();

            foreach (var param in parameters)
            {
                keyValuePairs.Add($"{param.Key}={Uri.EscapeDataString(param.Value)}");
            }

            return "?" + string.Join("&", keyValuePairs);
        }
    }
}
