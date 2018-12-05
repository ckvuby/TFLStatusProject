using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TFLStatusLibrary
{
    public class TFLApiClient
    {
        HttpClient client = new HttpClient();
        private string apiRequestUrl = "https://api.tfl.gov.uk/line/mode/tube/status?detail=true";

        public List<LineInfo> ApiCall()
        {
            List<LineInfo> formattedLineInfo = null;
            setHeaders();
            try
            {
                var response = makeTFLApiCall().Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = GetJsonResponseAsString(response).Result;
                    var lineInfo = GetLineInformation(responseJson);
                    formattedLineInfo = FormatLineInformation(lineInfo);
                    Console.WriteLine(formattedLineInfo);

                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Sorry there was an error");
            }
            return formattedLineInfo;
        }

        public List<TflLineInfo> GetLineInformation(string responseJson)
        {
            var lineInformation = JsonConvert.DeserializeObject<List<TflLineInfo>>(responseJson);
            return lineInformation;
        }

        public List<LineInfo> FormatLineInformation(List<TflLineInfo> lineInfo)
        {
            var formattedLineInformation = new List<LineInfo>();

            foreach (TflLineInfo line in lineInfo)
            {
                var formattedLine = new LineInfo();

                if (line.disruptions.Length == 0)
                {
                    formattedLine.lineId = line.id;
                    formattedLine.lineName = line.name;
                    formattedLine.lineStatus = line.lineStatuses[0].statusSeverityDescription;
                    formattedLine.statusDescription = "";
                    formattedLine.statusReason = "";
                }
                else
                {
                    formattedLine.lineId = line.id;
                    formattedLine.lineName = line.name;
                    formattedLine.lineStatus = line.lineStatuses[0].statusSeverityDescription;
                    formattedLine.statusDescription = line.disruptions[0].description;
                    formattedLine.statusReason = line.disruptions[0].reason;
                }
                formattedLineInformation.Add(formattedLine);
            }
            return formattedLineInformation;
        }

        public void setHeaders()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> makeTFLApiCall()
        {
            HttpResponseMessage response = await client.GetAsync(apiRequestUrl);
            return response;
        }

        public async Task<string> GetJsonResponseAsString(HttpResponseMessage response)
        {
            var json = "";
            json = await response.Content.ReadAsStringAsync();
            return json;
                
        }




    }
}
