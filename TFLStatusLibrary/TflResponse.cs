
namespace TFLStatusLibrary
{
    public class TflApiResponseInformation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public LineStatus[] LineStatuses { get; set; }
    }

    public class LineStatus
    {
        public string StatusSeverityDescription { get; set; }
        public string Reason { get; set; }
    }
}
