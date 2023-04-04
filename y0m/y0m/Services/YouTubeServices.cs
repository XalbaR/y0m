using System.Collections.Generic;
using System.Threading.Tasks;
using y0m.Models;
using Newtonsoft.Json.Linq;
using System.IO;
using YoutubeExplode.Common;
using YoutubeExplode;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;
using System;

namespace y0m.Services
{
    public class YoutubeServices
    {
        private readonly YouTubeApiService _youTubeApiService;

        public List<Video> Videos { get; private set; }

        public YoutubeServices()
        {
            _youTubeApiService = new YouTubeApiService();
        }

        public async Task<List<Video>> GetPopularVideosAsync()
        {
            var jObject = await _youTubeApiService.GetPopularVideosAsync();
            var videos = new List<Video>();

            if (jObject != null)
            {
                foreach (var item in jObject["items"])
                {
                    var video = Video.FromJObject(item); // Bu satırı değiştirin

                    if (video.Embeddable)
                    {
                        videos.Add(video);
                    }
                }
            }

            Videos = videos;
            return videos;
        }

        public async Task<List<Video>> SearchVideosAsync(string query)
        {
            var jObject = await _youTubeApiService.SearchVideosAsync(query);
            var videos = new List<Video>();

            if (jObject != null)
            {
                foreach (var item in jObject["items"])
                {
                    var video = Video.FromJObject(item); // Bu satırı değiştirin
                    videos.Add(video);
                }
            }
            Videos = videos;
            return videos;
        }

        public async Task DownloadVideoAsMp3Async(string videoId, string outputFileName)
        {
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoId);
            var audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

            if (audioStreamInfo != null)
            {
                var tempFilePath = Path.GetTempFileName();

                await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, tempFilePath);

                string downloadsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\y0m";
                Directory.CreateDirectory(downloadsFolderPath);
                string outputPath = Path.Combine(downloadsFolderPath, outputFileName);

                var ffmpeg = new FFmpegExecuter();

                await ffmpeg.ConvertAsync(tempFilePath, outputPath, OutputFormat.Mp3);
                File.Delete(tempFilePath);
            }
        }
    }
}
