namespace Huobi.Net.Enums
{
    /// <summary>
    /// ApiPath枚举
    /// </summary>
    public enum ApiPath
    {
        /// <summary>
        /// U本位合约：linear-swap-api
        /// </summary>
        LinearSwapApi,

        /// <summary>
        /// U本位合约：linear-swap-ex
        /// </summary>
        LinearSwapEx,

        /// <summary>
        /// U本位合约：linear-swap-ex/market
        /// </summary>
        LinearSwapExMarket,

        /// <summary>
        /// U本位合约：linear-swap-ex/market/detail
        /// </summary>
        LinearSwapExMarketDetail,

        /// <summary>
        /// U本位合约：linear-swap-ex/market/history
        /// </summary>
        LinearSwapExMarketHistory,

        /// <summary>
        /// U本位合约：v2/linear-swap-ex/market/detail
        /// </summary>
        V2LinearSwapExMarketDetail,

        /// <summary>
        /// 币本位永续合约：swap-api
        /// </summary>
        SwapApi,

        /// <summary>
        /// 币本位永续合约：swap-ex
        /// </summary>
        SwapEx,

        /// <summary>
        /// 币本位永续合约：swap-ex/market
        /// </summary>
        SwapExMarket,

        /// <summary>
        /// 币本位永续合约：swap-ex/market/detail
        /// </summary>
        SwapExMarketDetail,

        /// <summary>
        /// 币本位永续合约：v2/swap-ex/market/detail
        /// </summary>
        V2SwapExMarketDetail,

        /// <summary>
        /// 币本位永续合约：swap-ex/market/history
        /// </summary>
        SwapExMarketHistory,

        /// <summary>
        /// 币本位交割合约：api
        /// </summary>
        Api,

        /// <summary>
        /// 合约通用：market
        /// </summary>
        Market,

        /// <summary>
        /// 合约通用：market/detail
        /// </summary>
        MarketDetail,

        /// <summary>
        /// 合约通用：market/history
        /// </summary>
        MarketHistory,

        /// <summary>
        /// 合约通用：index/market/history
        /// </summary>
        IndexMarketHistory,
    }

    /// <summary>
    /// ApiPath枚举扩展
    /// </summary>
    public static class ApiPathExtensions
    {
        /// <summary>
        /// ApiPath枚举返回字符串
        /// </summary>
        /// <param name="apiPath"></param>
        /// <returns></returns>
        public static string GetString(this ApiPath apiPath)
        {
            switch (apiPath)
            {
                case ApiPath.LinearSwapApi:
                    return "linear-swap-api";
                case ApiPath.LinearSwapEx:
                    return "linear-swap-ex";
                case ApiPath.LinearSwapExMarket:
                    return "linear-swap-ex/market";
                case ApiPath.LinearSwapExMarketDetail:
                    return "linear-swap-ex/market/detail";
                case ApiPath.LinearSwapExMarketHistory:
                    return "linear-swap-ex/market/history";
                case ApiPath.V2LinearSwapExMarketDetail:
                    return "v2/linear-swap-ex/market/detail";

                case ApiPath.SwapApi:
                    return "swap-api";
                case ApiPath.SwapEx:
                    return "swap-ex";
                case ApiPath.SwapExMarket:
                    return "swap-ex/market";
                case ApiPath.SwapExMarketDetail:
                    return "swap-ex/market/detail";
                case ApiPath.SwapExMarketHistory:
                    return "swap-ex/market/history";
                case ApiPath.V2SwapExMarketDetail:
                    return "v2/swap-ex/market/detail";

                case ApiPath.Market:
                    return "market";
                case ApiPath.MarketDetail:
                    return "market/detail";
                case ApiPath.MarketHistory:
                    return "market/history";
                case ApiPath.IndexMarketHistory:
                    return "index/market/history";
                default:
                    return "";
            }
        }
    }
}
