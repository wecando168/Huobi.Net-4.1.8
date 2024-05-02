namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapMarketData
{
    /// <summary>
    /// 【通用】获取合约的溢价指数K线
    /// </summary>
    public class WWTHuobiUsdtMarginedMarketPremiumIndexKline
    {
        /// <summary>
        /// k线id
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; } = default;

        /// <summary>
        /// 开盘值（溢价指数）
        /// </summary>
        [JsonProperty("open", NullValueHandling = NullValueHandling.Ignore)]
        public string Open { get; set; } = string.Empty;

        /// <summary>
        /// 收盘值（溢价指数）
        /// </summary>
        [JsonProperty("close", NullValueHandling = NullValueHandling.Ignore)]
        public string Close { get; set; } = string.Empty;

        /// <summary>
        /// 最低值（溢价指数）
        /// </summary>
        [JsonProperty("low", NullValueHandling = NullValueHandling.Ignore)]
        public string Low { get; set; } = string.Empty;

        /// <summary>
        /// 最高值（溢价指数）
        /// </summary>
        [JsonProperty("high", NullValueHandling = NullValueHandling.Ignore)]
        public string High { get; set; } = string.Empty;

        /// <summary>
        /// 成交量(币), 数值为0
        /// </summary>
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public string Amount { get; set; } = string.Empty;

        /// <summary>
        /// 成交量(张)，数值为0
        /// </summary>
        [JsonProperty("vol", NullValueHandling = NullValueHandling.Ignore)]
        public string Vol { get; set; } = string.Empty;

        /// <summary>
        /// 成交额, 数值为0
        /// </summary>
        [JsonProperty("trade_turnover", NullValueHandling = NullValueHandling.Ignore)]
        public string TradeTurnover { get; set; } = string.Empty;

        /// <summary>
        /// 成交笔数，数值为0
        /// </summary>
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public string Count { get; set; } = string.Empty;
    }
}