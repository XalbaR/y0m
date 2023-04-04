using Newtonsoft.Json.Linq;

namespace y0m.Models
{
    public class Video
    {
        public string VideoId { get; set; }
        public string ThumbnailUrl { get; set; }
        public string EmbedUrl { get; set; }
        public string Title { get; set; }
        public string EmbedHtml { get; set; }
        public string PrivacyStatus { get; set; }
        public bool Embeddable { get; set; }
        public string ChannelId { get; set; }
        public string ChannelTitle { get; set; }
        public string PublishedAt { get; set; }



        public static Video FromJObject(JToken item)
        {
            string videoId = item["id"]?.ToString();
            string title = item["snippet"]?["title"]?.ToString();
            string description = item["snippet"]?["description"]?.ToString();
            string thumbnailUrl = item["snippet"]?["thumbnails"]?["default"]?["url"]?.ToString();
            string channelId = item["snippet"]?["channelId"]?.ToString();
            string channelTitle = item["snippet"]?["channelTitle"]?.ToString();
            string publishedAt = item["snippet"]?["publishedAt"]?.ToString();
            string embedUrl = $"https://www.youtube.com/embed/{videoId}";
            string embedHtml = item["player"]?["embedHtml"]?.ToString();

            string privacyStatus = item["status"]?["privacyStatus"]?.ToString() ?? string.Empty;
            bool embeddable = item["status"]?["embeddable"]?.ToObject<bool>() ?? false;

            return new Video
            {
                VideoId = videoId,
                Title = title,
                ThumbnailUrl = thumbnailUrl,
                EmbedUrl = embedUrl,
                EmbedHtml = embedHtml,
                PrivacyStatus = privacyStatus,
                Embeddable = embeddable,
                ChannelId = channelId,
                ChannelTitle = channelTitle,
                PublishedAt = publishedAt
            };
        }

    }
}
