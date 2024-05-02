namespace Huobi.Net.Enums
{
    /// <summary>
    /// ApiPath枚举
    /// </summary>
    public enum WWTApiPath
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
    public static class WWTApiPathExtensions
    {
        /// <summary>
        /// ApiPath枚举返回字符串
        /// </summary>
        /// <param name="apiPath"></param>
        /// <returns></returns>
        public static string GetString(this WWTApiPath apiPath)
        {
            switch (apiPath)
            {
                case WWTApiPath.LinearSwapApi:
                    return "linear-swap-api";
                case WWTApiPath.LinearSwapEx:
                    return "linear-swap-ex";
                case WWTApiPath.LinearSwapExMarket:
                    return "linear-swap-ex/market";
                case WWTApiPath.LinearSwapExMarketDetail:
                    return "linear-swap-ex/market/detail";
                case WWTApiPath.LinearSwapExMarketHistory:
                    return "linear-swap-ex/market/history";
                case WWTApiPath.V2LinearSwapExMarketDetail:
                    return "v2/linear-swap-ex/market/detail";

                case WWTApiPath.SwapApi:
                    return "swap-api";
                case WWTApiPath.SwapEx:
                    return "swap-ex";
                case WWTApiPath.SwapExMarket:
                    return "swap-ex/market";
                case WWTApiPath.SwapExMarketDetail:
                    return "swap-ex/market/detail";
                case WWTApiPath.SwapExMarketHistory:
                    return "swap-ex/market/history";
                case WWTApiPath.V2SwapExMarketDetail:
                    return "v2/swap-ex/market/detail";

                case WWTApiPath.Market:
                    return "market";
                case WWTApiPath.MarketDetail:
                    return "market/detail";
                case WWTApiPath.MarketHistory:
                    return "market/history";
                case WWTApiPath.IndexMarketHistory:
                    return "index/market/history";
                default:
                    return "";
            }
        }
    }
}
