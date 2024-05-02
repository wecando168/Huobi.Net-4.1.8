namespace Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// Usdt Margined API endpoints
    /// U本位合约 API 端点
    /// </summary>
    public interface IHuobiClientUsdtMarginSwapApi : IDisposable
    {
        /// <summary>
        /// The factory for creating requests. Used for unit testing
        /// </summary>
        IRequestFactory RequestFactory { get; set; }

        /// <summary>
        /// Reference Data
        /// 基础信息接口
        /// </summary>
        IWWTHuobiClientUsdtMarginSwapApiReferenceData ReferenceData { get; }

        /// <summary>
        /// Market Data
        /// 市场行情接口
        /// </summary>
        IHuobiClientUsdtMarginSwapApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Account
        /// 账户接口
        /// </summary>
        IHuobiClientUsdtMarginSwapAccount Account { get; }

        /// <summary>
        /// Trade
        /// </summary>
        IHuobiClientUsdtMarginSwapApiTrading Trading { get; }


        /// <summary>
        /// Strategy
        /// 策略接口
        /// </summary>
        IWWTHuobiClientUsdtMarginSwapApiStrategyOrder Strategy { get; }

        /// <summary>
        /// Transferring
        /// 划转接口
        /// </summary>
        IWWTHuobiClientUsdtMarginSwapApiTransferring Transferring { get; }
    }
}
