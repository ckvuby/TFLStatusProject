using CommandLine;

namespace TFLStatus
{
    public class Options
    {
        [Option('a', "all")]
        public bool AllTubeLineStatus { get; set; }
    }
}
