namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapMarketData
{
    /// <summary>
    /// 【通用】批量获取市场最近成交记录
    /// </summary>
    public class WWTHuobiUsdtMarginedMarketHistoryTrade
    {
        /// <summary>
        /// 成交记录集合
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<HistoryTradeData> Data { get; set; } = Array.Empty<HistoryTradeData>();

        /// <summary>
        /// 订单唯一id（品种唯一）
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; } = default;

        /// <summary>
        /// 成交时间戳
        /// </summary>
        [JsonProperty("ts", NullValueHandling = NullValueHandling.Ignore)]
        public long Timestamp { get; set; } = default;
    }

    /// <summary>
    /// 成交记录
    /// </summary>
    public class HistoryTradeData
    {
        /// <summary>
        /// 成交量(张)。 值是买卖双边之和
        /// </summary>
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount { get; set; } = default;

        /// <summary>
        /// 成交量（币）
        /// </summary>
        [JsonProperty("quantity", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Quantity { get; set; } = default;

        /// <summary>
        /// 成交额（计价币种）
        /// </summary>
        [JsonProperty("trade_turnover", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TradeTurnover { get; set; } = default;

        /// <summary>
        /// 成交时间戳
        /// </summary>
        [JsonProperty("ts", NullValueHandling = NullValueHandling.Ignore)]
        public long Timestamp { get; set; } = default;

        /// <summary>
        /// 订单唯一id（品种唯一）
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; } = default;

        /// <summary>
        /// 成交价
        /// </summary>
        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Price { get; set; } = default;

        /// <summary>
        /// 买卖方向，即taker(主动成交)的方向
        /// </summary>
        [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
        public string Direction { get; set; } = string.Empty;
    }
}