using System.Net.Http;
using TFLStatusLibrary;

namespace TFLStatus
{
    public interface IConsoleApp
    {
        void ConsoleAppHandler(string[] args, HttpClient httpClient, IHttpClient httpClientWrapper);

        void ShowStatusOfAllTubeLines(Options options, HttpClient httpClient, IHttpClient httpClientWrapper);
    }
}