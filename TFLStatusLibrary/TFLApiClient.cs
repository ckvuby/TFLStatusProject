using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TFLStatusLibrary
{
    public class TFLApiClient : ITFLAPIClient
    {
        private readonly IHttpClient _httpClient;
        private Uri TflApiUrl { get; set; }

        public TFLApiClient(IHttpClient httpClient,Uri ApiUrl)
        {
            TflApiUrl = ApiUrl ?? throw new ArgumentNullException("url not valid");
            _httpClient = httpClient;
        }

        public IEnumerable<LineInformation> SetupAndMakeApiCallAndReturnFormattedData()
        {
            IEnumerable<LineInformation> formattedLineInfo = null;
            _httpClient.SetHeaders();
            try
            {
                var response = MakeTFLApiCallAsync().Result;
                if (response.IsSuccessStatusCode) 
                {
                    var responseString = ConvertResponseToStringAsync(response).Result;
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

        public async Task<HttpResponseMessage> MakeTFLApiCallAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(TflApiUrl);
            return response;
        }

        public async Task<string> ConvertResponseToStringAsync(HttpResponseMessage response)
        {
            var jsonString = "";
            jsonString = await response.Content.ReadAsStringAsync();
            return jsonString;
        }

        public List<TflApiResponseInformation> MapResponseStringToObject(string responseString)
        {
            var TflApiResponseInformation  = JsonConvert.DeserializeObject<List<TflApiResponseInformation>>(responseString);
            return TflApiResponseInformation;
        }

        public IEnumerable<LineInformation> CreateListOfFormattedLineInformation(List<TflApiResponseInformation> TflApiResponseInformation)
        {
          
            var formattedLineInformation = TflApiResponseInformation.Select(line =>
                new LineInformation()
                {
                    lineId = line.id,
                    lineName = line.name,
                    lineStatus = line.lineStatuses[0].statusSeverityDescription,
                    statusReason = line.lineStatuses[0].reason
                });
            
            return formattedLineInformation;
        }



        

    }
}
