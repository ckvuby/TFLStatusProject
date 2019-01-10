using System;
using System.Collections.Generic;
using CommandLine;

namespace TFLStatus
{
    public class Options
    {
       
        [Option('a')]
        public bool AllTubeLineStatus { get; set; }

        [Option('g', "getLine")]
        public string GetLine { get; set; }

        //[Option('g', "getLine")]
       // public IEnumerable<string> GetLine { get; set; }
        //[OptionArray('v', "values", DefaultValue = new string[] { "central", "victoria" })]

    }

   
}
