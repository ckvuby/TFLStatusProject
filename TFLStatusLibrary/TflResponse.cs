
namespace TFLStatusLibrary
{
    public class TflApiResponseInformation
    {
        public string id { get; set; }
        public string name { get; set; }
        public LineStatus[] lineStatuses { get; set; }
    }

    public class LineStatus
    {
        public string statusSeverityDescription { get; set; }
        public string reason { get; set; }
    }
}
