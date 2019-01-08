using System.Collections.Generic;

namespace TFLStatusLibrary
{
    /// <summary>
    /// TODO: Best practice models in a specific namespace
    /// TODO: Why is Line prefix on all properties
    /// </summary>
   public class LineInformation
    {
        public string LineId { get; set; }
        public string LineName { get; set; }
        public string LineStatus { get; set;  }
        public string StatusReason { get; set; }
    }


}