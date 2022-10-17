using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XmAssignment.Common.Enums;
using XmAssignment.Common.Models;
using XmAssignment.Common.Utils;

namespace XmAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class SourceController : ControllerBase
    {
        /// <summary>
        /// Get All Available API Source BTC To USD.
        /// </summary>
        [HttpGet(Name = "GetAllSource")]
        public IEnumerable<Source> GetAllAvailbleSource()
        {
            var sources = new List<Source>();
            sources.Add(new Source { Name = "Bitfinex", PriceSource = PriceSource.Bitfinex, GetUrl = Constants.BitfinexAPIUrlGetBTCUSDPair });
            sources.Add(new Source { Name = "Bitstamp", PriceSource = PriceSource.Bitstamp, GetUrl = Constants.BitstampAPIUrlGetBTCUSDPair });
            return sources;
        }
    }
}
