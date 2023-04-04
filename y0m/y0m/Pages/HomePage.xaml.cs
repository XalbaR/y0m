using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using y0m.Services;
using y0m.Models;
using System.IO;
using Xamarin.Forms.PlatformConfiguration;

namespace y0m.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

        private async void LoadVideos()
        {
            await _youTubeService.GetPopularVideosAsync();

            VideoList.ItemsSource = _youTubeService.Videos;
        }
        public HomePage()
        {
            InitializeComponent();
            LoadVideos();
        }

        private readonly YoutubeServices _youTubeService = new YoutubeServices(); // Değiştir

        private void OnAddToListButtonTapped(object sender, EventArgs e)
        {

        }

        private async void OnDownloadButtonTapped(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Video video = button?.CommandParameter as Video;

            if (video != null)
            {
                string videoId = video.VideoId;

                string filename = $"{video.VideoId}.mp3"; // İndirilen video dosyasının adını belirleyin.

                // Downloads klasörü içinde y0m adlı klasörü oluşturun veya varsa kullanın
                string y0mFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Downloads", "y0m");
                Directory.CreateDirectory(y0mFolderPath);

                // İndirilecek dosyanın tam yolunu belirleyin
                string filePath = Path.Combine(y0mFolderPath, filename);

                await _youTubeService.DownloadVideoAsMp3Async(videoId, filePath);
            }
        }





    }

}
