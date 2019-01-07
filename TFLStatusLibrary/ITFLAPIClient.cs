using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TFLStatusLibrary
{
    public interface ITFLAPIClient
    {
        IEnumerable<LineInformation> SetupAndMakeApiCallAndReturnFormattedData();
        List<TflApiResponseInformation> MapResponseStringToObject(string responseString);
        IEnumerable<LineInformation> CreateListOfFormattedLineInformation(List<TflApiResponseInformation> TflApiResponseInformation);
        Task<HttpResponseMessage> MakeTFLApiCallAsync();
        Task<string> ConvertResponseToStringAsync(HttpResponseMessage response);
    }
}