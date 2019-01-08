using System.Net.Http;
using TFLStatusLibrary;

namespace TFLStatus
{
    /// <summary>
    ///  TODO: Unused
    /// </summary>
    public interface IConsoleApp
    {
        void ConsoleAppHandler(string[] args);

        void ShowStatusOfAllTubeLines();

        void ShowStatusOfVictoriaLine();
    }
}