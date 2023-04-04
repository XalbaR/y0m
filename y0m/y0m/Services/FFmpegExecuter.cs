using System;
using System.IO;
using System.Threading.Tasks;
//using Android.OS;
using Xabe.FFmpeg;

public class FFmpegExecuter
{
    public FFmpegExecuter()
    {
        string tempFolderPath = Path.Combine(Xamarin.Essentials.FileSystem.CacheDirectory, "FFmpeg");
        if (!Directory.Exists(tempFolderPath))
        {
            Directory.CreateDirectory(tempFolderPath);
        }
        FFmpeg.SetExecutablesPath(tempFolderPath);
    }

    public async Task ConvertAsync(string inputPath, string outputPath, OutputFormat format)
    {
        string fileExtension = format.ToString().ToLower();
        string output = Path.ChangeExtension(outputPath, fileExtension);

        IConversion conversion = await FFmpeg.Conversions.FromSnippet.Convert(inputPath, output);
        await conversion.Start();
    }
}

public enum OutputFormat
{
    Mp3,
    Mp4
}
