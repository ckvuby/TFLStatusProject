using CommandLine;

namespace TFLStatus
{
    public class Options
    {
        [Option('a', "all")]
        public bool AllTubeLineStatus { get; set; }

        [Option("victoria")]
        public bool VictoriaLineStatus { get; set; }
    }
}
