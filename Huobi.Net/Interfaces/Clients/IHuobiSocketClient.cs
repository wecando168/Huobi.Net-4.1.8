using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;

namespace Huobi.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Huobi websocket API. 
    /// </summary>
    public interface IHuobiSocketClient : ISocketClient
    {
        /// <summary>
        /// Spot streams
        /// </summary>
        public IHuobiSocketClientSpotApi SpotApi { get; }

        /// <summary>
        /// Usdt margin swap streams
        /// </summary>
        public IHuobiSocketClientUsdtMarginSwapApi UsdtMarginSwapApi { get; }
    }
}