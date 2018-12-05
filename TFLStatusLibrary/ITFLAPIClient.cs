using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TFLStatusLibrary
{
    public interface ITFLAPIClient
    {
        List<LineInformation> SetupAndMakeApiCallAndReturnFormattedData();
        List<TflApiResponseInformation> MapResponseStringToObject(string responseString);
        List<LineInformation> CreateListOfFormattedLineInformation(List<TflApiResponseInformation> TflApiResponseInformation);

        LineInformation setLineInfo(LineInformation formattedLine, TflApiResponseInformation line, string statusDescription, string statusReason);

        void setHeaders(string mediatypevalue);

        Task<HttpResponseMessage> MakeTFLApiCall();
        Task<string> ConvertResponseToString(HttpResponseMessage response);
    }
}