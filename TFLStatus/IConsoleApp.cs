using System.Net.Http;
using TFLStatusLibrary;

namespace TFLStatus
{
    public interface IConsoleApp
    {
        void ConsoleAppHandler(string[] args);

        void ShowStatusOfAllTubeLines(string tubeline);

        //void ShowStatusOfVictoriaLine();
    }
}