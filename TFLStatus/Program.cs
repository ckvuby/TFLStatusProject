using System;
using System.Net.Http;
using TFLStatusLibrary;

namespace TFLStatus
{
    class Program
    {
        public HttpClient httpClient;


        public void callTfl()
        {
            httpClient = new HttpClient();
            IHttpClient httpclient = new HttpClientWrapper(httpClient);
            TFLApiClient tflApiClient = new TFLApiClient(httpclient);
            var lines = tflApiClient.SetupAndMakeApiCallAndReturnFormattedData();
            //var lineEnumerator = lines.GetEnumerator();
            using (var sequenceEnum = lines.GetEnumerator())
            {
                while (sequenceEnum.MoveNext())
                {
                    Console.WriteLine(sequenceEnum.Current.lineId + "--" + sequenceEnum.Current.lineName + "--" + sequenceEnum.Current.lineStatus + "---" + sequenceEnum.Current.statusReason);
                  //  Console.WriteLine(sequenceEnum.Current.lineName);
                    //Console.WriteLine(sequenceEnum.Current.lineStatus);
                    //Console.WriteLine(sequenceEnum.Current.linestatusReason);
                }
            }
            //lineEnumerator.Current()
            //l(line => Console.WriteLine(line.lineId + "--" + line.lineName + "--" + line.lineStatus + "___" + "---" + line.statusReason));

        }
        static void Main(string[] args)
        {
            Program p = new Program();
            p.callTfl();
            
    }
    }
}
