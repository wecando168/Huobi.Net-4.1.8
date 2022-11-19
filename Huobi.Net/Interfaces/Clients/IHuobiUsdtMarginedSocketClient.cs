using CryptoExchange.Net.Interfaces;
using Huobi.Net.Interfaces.Clients.UsdtMargined;

namespace Huobi.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Huobi usddt margined websocket API. 
    /// </summary>
    public interface IHuobiUsdtMarginedSocketClient : ISocketClient
    {
        /// <summary>
        /// Spot streams
        /// </summary>
        public IHuobiSocketClientUsdtMarginedStreams UsdtMarginedStreams { get; }
    }
}