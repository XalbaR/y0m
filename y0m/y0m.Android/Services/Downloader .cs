using System;
using System.Net;
using System.Threading.Tasks;
using y0m.Droid.Services;
using Xamarin.Forms;

using System.IO;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Dependency(typeof(Downloader))]
namespace y0m.Droid.Services
{
    public class Downloader : IDownloader
    {
        public async Task DownloadFileAsync(string url, string filename)
        {
            WebClient webClient = new WebClient();
            var path = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath, filename);

            webClient.DownloadProgressChanged += (s, e) =>
            {
                // İndirme işlemi sırasında istediğiniz bir şey yapabilirsiniz.
            };

            webClient.DownloadFileCompleted += (s, e) =>
            {
                // İndirme işlemi tamamlandığında istediğiniz bir şey yapabilirsiniz.
            };

            await webClient.DownloadFileTaskAsync(new Uri(url), path);
        }

        public async Task<string> GetY0mFolderPathAsync()
        {
            string y0mFolderPath = Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath, "y0m");
            Directory.CreateDirectory(y0mFolderPath);
            return y0mFolderPath;
        }
    }
}
