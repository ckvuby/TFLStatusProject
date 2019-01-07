using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TFLStatusLibrary
{
    public interface ITFLAPIClient
    {
        IEnumerable<LineInformation> SetupAndMakeApiCallAndReturnFormattedData();
        Task<HttpResponseMessage> MakeTFLApiCallAsync();
    }
}