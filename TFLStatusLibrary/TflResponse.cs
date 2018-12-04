using System;
using System.Collections.Generic;
using System.Text;

namespace TFLStatusLibrary
{
    public class TflResponse
    {
        //public List<TflLineInfo> LineInfo { get; set; }
    }

    public class TflLineInfo
    {
        public string id { get; set; }
        public string name { get; set; }

        public LineStatus[] lineStatuses { get; set; }

        public Disruptions[] disruptions { get; set; }

    }

    public class LineStatus
    {
        public string statusSeverityDescription { get; set; }

    }

    public class Disruptions
    {
        public string reason { get; set; }
        public string description { get; set; }
    }

}
