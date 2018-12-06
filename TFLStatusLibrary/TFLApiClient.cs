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
            catch (HttpRequestException e)
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
                if (line.disruptions.Length == 0)
                {
                    formattedLine = setLineInfo(formattedLine, line, "", "");
                }
                else
                {
                    formattedLine = setLineInfo(formattedLine, line, line.disruptions[0].description, line.disruptions[0].reason);           
                }
                formattedLineInformation.Add(formattedLine);
            }
            return formattedLineInformation;
        }

        public LineInformation setLineInfo(LineInformation formattedLine, TflApiResponseInformation line, string statusDescription, string statusReason)
        {
            
            formattedLine.lineId = line.id;
            formattedLine.lineName = line.name;
            formattedLine.lineStatus = line.lineStatuses[0].statusSeverityDescription;
            formattedLine.statusDescription = statusDescription;
            formattedLine.statusReason = statusReason;
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
