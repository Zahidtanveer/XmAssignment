using XmAssignment.Common.Entities;
using XmAssignment.Common.Enums;
using XmAssignment.Common.ResponseModels;
using XmAssignment.Common.Utils;

namespace XmAssignment.API.Services
{
    public sealed class APICallFetchData
    {
        static public async Task<BtcPrice> GetBtcPrice(PriceSource priceSource)
        {
            var btcPrice = new BtcPrice();
            if (priceSource == PriceSource.Bitfinex)
            {
                var bitfinexResponse = await FetchBtcUSDPriceFromAPI<BitfinexResponseModel>(Constants.BitfinexAPIUrlGetBTCUSDPair);
                btcPrice.Price = bitfinexResponse.last_price;
                btcPrice.Soruce = priceSource;
                btcPrice.Timestamp = DateConverter.UnixTimestampToDateTime(bitfinexResponse.timestamp);
            }
            else if (priceSource == PriceSource.Bitstamp)
            {
                var bitstampResponse = await FetchBtcUSDPriceFromAPI<BitstampResponseModel>(Constants.BitstampAPIUrlGetBTCUSDPair);
                btcPrice.Price = bitstampResponse.last;
                btcPrice.Soruce = priceSource;
                btcPrice.Timestamp = DateConverter.UnixTimestampToDateTime(bitstampResponse.timestamp);
            }
            return btcPrice;
        }

        static private async Task<T> FetchBtcUSDPriceFromAPI<T>(string url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Headers =
                {
                  { "accept", "application/json" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var bodyJson = response.Content.ReadAsStringAsync();
                return await response.Content.ReadFromJsonAsync<T>();
            }
        }
    }
}
