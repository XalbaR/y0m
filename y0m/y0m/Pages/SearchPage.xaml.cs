using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using y0m.Models;
using y0m.Services;

namespace y0m.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        private YoutubeServices _youtubeServices;

        public SearchPage()
        {
            InitializeComponent();
            _youtubeServices = new YoutubeServices();
        }

        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(searchEntry.Text))
            {
                var videos = await _youtubeServices.SearchVideosAsync(searchEntry.Text);

                // Videoların EmbedUrl özelliklerini tekrar tanımlayın
                foreach (var video in videos)
                { 
                    int startIndex = video.VideoId.IndexOf("\"videoId\": \"") + 11;
                    int endIndex = video.VideoId.IndexOf("\n", startIndex);
                    string videoId = video.VideoId.Substring(startIndex, endIndex - startIndex).Replace("\"", "").Trim();
                    video.EmbedUrl = $"https://www.youtube.com/embed/{videoId}";
                }

                searchResultsListView.ItemsSource = videos;
            }
        }

     

        private async void OnAddToListButtonTapped(object sender, EventArgs e)
        {
            var button = sender as Button;
            var video = button?.CommandParameter as Video;
            if (video != null)
            {
                // Liste+ butonuna basıldığında yapılacak işlemleri buraya yazabilirsiniz.
                // Örnek olarak, videoyu kullanıcının favori listesine ekleyebilirsiniz.
            }
        }

        private async void OnDownloadButtonTapped(object sender, EventArgs e)
        {
            var button = sender as Button;
            var video = button?.CommandParameter as Video;
            if (video != null)
            {
                // İndirme butonuna basıldığında yapılacak işlemleri buraya yazabilirsiniz.
                // Örnek olarak, videoyu indirme işlemini başlatabilirsiniz.
            }
        }
    }
}
