using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TFLStatusLibrary
{
    public interface ITFLAPIClient
    {
        Task<IEnumerable<LineInformation>> SetupAndMakeApiCallAndReturnFormattedData();
        Task<HttpResponseMessage> MakeTFLApiCallAsync();

    }
}