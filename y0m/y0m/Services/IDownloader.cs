using System.Threading.Tasks;

 
public interface IDownloader
{
    Task DownloadFileAsync(string url, string filename);
    Task<string> GetY0mFolderPathAsync();
}