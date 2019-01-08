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

        // TODO: Inconsistent naming
        // TODO: should be readonly
        private Uri TflApiUrl { get; set; }

        public TflApiClient(IHttpClient httpClient,Uri apiUrl)
        {
            // TODO: Untested code
            TflApiUrl = apiUrl ?? throw new ArgumentNullException(nameof(apiUrl));
            _httpClient = httpClient;
        }

        // TODO: SRP broken, code does too much
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
                    var tflApiResponseInformation = MapResponseStringToObject(responseString);
                    formattedLineInfo = CreateListOfFormattedLineInformation(tflApiResponseInformation);
                }
                else
                {
                    // TODO: Invalid in a service
                    Console.WriteLine("Sorry information is not available");
                }
            }
            catch (Exception e)
            {
                // TODO: Invalid in a service as the exception is hidden
                Console.WriteLine(e + "Sorry there was an error");
            }

            // TODO: Can return nulls, and no tests
            return formattedLineInfo;
        }

        // TODO: Ambiguous name
        public async Task<HttpResponseMessage> MakeTFLApiCallAsync()
        {
            // TODO: Does not setup headers, is that important?

            HttpResponseMessage response = await _httpClient.GetAsync(TflApiUrl);
            return response;
        }

        // TODO: Rename as this isnt what it does
        private async Task<string> ConvertResponseToStringAsync(HttpResponseMessage response)
        {
            var jsonString = "";
            jsonString = await response.Content.ReadAsStringAsync();
            return jsonString;
        }

        //TODO: Rename as it doesnt map to object, it maps to list of tflapiresponse items
        // TODO: SRP broken
        // TODO: Why map twice?
        private List<TflApiResponseInformation> MapResponseStringToObject(string responseString)
        {
            var tflApiResponseInformation  = JsonConvert.DeserializeObject<List<TflApiResponseInformation>>(responseString);
            return tflApiResponseInformation;
        }

        // TODO: Information isnt formatted, its mapped
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
