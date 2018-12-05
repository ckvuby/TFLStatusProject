using System;
using TFLStatusLibrary;

namespace TFLStatus
{
    class Program
    {
       
        static void Main(string[] args)
        {
            TFLApiClient tflApiClient = new TFLApiClient();
            var lines = tflApiClient.SetupAndMakeApiCallAndReturnFormattedData();
            lines.ForEach(line => Console.WriteLine(line.lineId + "--" + line.lineName + "--" + line.lineStatus + "___" + line.statusDescription + "---" + line.statusReason));
            
    }
    }
}
