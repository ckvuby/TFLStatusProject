using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TFLStatusLibrary
{
    /// <summary>
    /// TODO: Naming, why is this all caps, and why is Client included in the name
    /// </summary>
    public interface ITFLAPIClient
    {
        /// <summary>
        /// TODO: SRP broken
        /// </summary>
        /// <returns></returns>
        IEnumerable<LineInformation> SetupAndMakeApiCallAndReturnFormattedData();

        /// <summary>
        /// TODO: Unclear name and returns raw HttpResponse rather than LineInformation
        /// </summary>
        /// <returns></returns>
        Task<HttpResponseMessage> MakeTFLApiCallAsync();

    }
}