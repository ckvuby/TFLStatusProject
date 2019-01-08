using CommandLine;

namespace TFLStatus
{

    // TODO: At least one of these is required, so should use grouping or a sensible default
    public class Options
    {
        
        // TODO: Helptext should be supplied
        [Option('a', "all")]
        public bool AllTubeLineStatus { get; set; }

        // TODO: Helptext should be supplied
        [Option('v', "victoria")]
        public bool VictoriaTubeLineStatus { get; set; }
    }
}
