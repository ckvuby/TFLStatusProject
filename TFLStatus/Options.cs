using CommandLine;

namespace TFLStatus
{
    public class Options
    {
        [Option('a', "all")]
        public bool AllTubeLineStatus { get; set; }

        [Option('v', "victoria")]
        public bool VictoriaTubeLineStatus { get; set; }
    }
}
