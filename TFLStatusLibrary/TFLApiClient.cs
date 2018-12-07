using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TFLStatusLibrary
{
    public class TFLApiClient : ITFLAPIClient
    {
        private readonly IHttpClient _httpClient;

        public TFLApiClient(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private string apiRequestUrl = "https://api.tfl.gov.uk/line/mode/tube/status?detail=true";

        public List<LineInformation> SetupAndMakeApiCallAndReturnFormattedData()
        {
            List<LineInformation> formattedLineInfo = null;
            _httpClient.SetHeaders();
            try
            {
                var response = MakeTFLApiCall().Result;
                if (response.IsSuccessStatusCode) 
                {
                    var responseString = ConvertResponseToString(response).Result;
                    var TflApiResponseInformation = MapResponseStringToObject(responseString);
                    formattedLineInfo = CreateListOfFormattedLineInformation(TflApiResponseInformation);
                }
                else
                {
                    Console.WriteLine("Sorry information is not available");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Sorry there was an error");
            }
            return formattedLineInfo;
        }

        public List<TflApiResponseInformation> MapResponseStringToObject(string responseString)
        {
            var TflApiResponseInformation  = JsonConvert.DeserializeObject<List<TflApiResponseInformation>>(responseString);
            return TflApiResponseInformation;
        }

        public List<LineInformation> CreateListOfFormattedLineInformation(List<TflApiResponseInformation> TflApiResponseInformation)
        {
            var formattedLineInformation = new List<LineInformation>();
            foreach (TflApiResponseInformation line in TflApiResponseInformation)
            {
                var formattedLine = new LineInformation();
                formattedLine = setLineInfo(formattedLine, line);
                formattedLineInformation.Add(formattedLine);
            }
            return formattedLineInformation;
        }

        public LineInformation setLineInfo(LineInformation formattedLine, TflApiResponseInformation line)
        {
            
            formattedLine.lineId = line.id;
            formattedLine.lineName = line.name;
            formattedLine.lineStatus = line.lineStatuses[0].statusSeverityDescription;
            formattedLine.statusReason = line.lineStatuses[0].reason;
            return formattedLine;
        }

        public async Task<HttpResponseMessage> MakeTFLApiCall()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiRequestUrl);
            return response;
        }

        public async Task<string> ConvertResponseToString(HttpResponseMessage response)
        {
            var jsonString = "";
            jsonString = await response.Content.ReadAsStringAsync();
            return jsonString;
        }

    }
}
