using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TFLStatusLibrary
{
    public class TflApiClient : ITFLAPIClient
    {
        private readonly IHttpClient _httpClient;
        private Uri TflApiUrl { get; set; }

        public TflApiClient(IHttpClient httpClient,Uri apiUrl)
        {
            TflApiUrl = apiUrl ?? throw new ArgumentNullException("url not valid");
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<LineInformation>> SetupAndMakeApiCallAndReturnFormattedDataAsync()
        {
            IEnumerable<LineInformation> formattedLineInfo = null;
            _httpClient.SetHeaders();
            try
            {
                var response =  await MakeTFLApiCallAsync();
                if (response.IsSuccessStatusCode) 
                {
                    var responseString = ConvertResponseToStringAsync(response).Result;
                    var tflApiResponseInformation = MapResponseStringToObject(responseString);
                    formattedLineInfo = CreateListOfFormattedLineInformation(tflApiResponseInformation);
                }
                else
                {
                    Console.WriteLine("Sorry information is not available");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e + "Sorry there was an error");
            }
            return formattedLineInfo;
        }

        public async Task<HttpResponseMessage> MakeTFLApiCallAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(TflApiUrl);
            return response;
        }

        private async Task<string> ConvertResponseToStringAsync(HttpResponseMessage response)
        {
            var jsonString = "";
            jsonString = await response.Content.ReadAsStringAsync();
            return jsonString;
        }

        private List<TflApiResponseInformation> MapResponseStringToObject(string responseString)
        {
            var tflApiResponseInformation  = JsonConvert.DeserializeObject<List<TflApiResponseInformation>>(responseString);
            return tflApiResponseInformation;
        }

        private IEnumerable<LineInformation> CreateListOfFormattedLineInformation(List<TflApiResponseInformation> tflApiResponseInformation)
        {
          
            var formattedLineInformation = tflApiResponseInformation.Select(line =>
                new LineInformation()
                {
                    LineId = line.Id,
                    LineName = line.Name,
                    LineStatus = line.LineStatuses[0].StatusSeverityDescription,
                    StatusReason = line.LineStatuses[0].Reason
                });
            
            return formattedLineInformation;
        }

    }
}
