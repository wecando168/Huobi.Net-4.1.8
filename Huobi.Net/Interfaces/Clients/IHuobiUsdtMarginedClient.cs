using CryptoExchange.Net.Interfaces;
using Huobi.Net.Interfaces.Clients.UsdtMargined;

namespace Huobi.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Huobi Usdt Margined API. 
    /// 访问火币U本位合约API的客户端
    /// </summary>
    public interface IHuobiUsdtMarginedClient : IRestClient
    {
        /// <summary>
        /// Spot endpoints
        /// </summary>
        IHuobiClientUsdtMarginedApi UsdtMarginedApi { get; }
    }
}