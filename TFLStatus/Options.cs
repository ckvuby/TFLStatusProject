using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace TFLStatus
{
    class Options
    {
        [Option('a', "all")]
        public bool AllTubeLineStatus { get; set; }

        [Option('v', "victoria")]
        public bool ShowVictoriaLineStatus { get; set; }
    }
}
