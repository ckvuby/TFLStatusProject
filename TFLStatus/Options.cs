using CommandLine;

namespace TFLStatus
{
    public class Options
    {
        [Option('a', "all")]
        public bool AllTubeLineStatus { get; set; }

        [Option('l', "getLine")]
        public string TubeLine { get; set; }
    }
}
