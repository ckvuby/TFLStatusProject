
namespace TFLStatusLibrary
{
    // TODO: Split into seprate file
    // TODO: Rename as ambiguous, what about other api calls?
    public class TflApiResponseInformation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public LineStatus[] LineStatuses { get; set; }
    }

    // TODO: Consider making this a nested class of tflApiResponseInformation

    public class LineStatus
    {
        public string StatusSeverityDescription { get; set; }
        public string Reason { get; set; }
    }
}
