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

        }
    }
}
