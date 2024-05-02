namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapMarketData
{
    /// <summary>
    /// 【通用】获取合约行情深度数据
    /// </summary>
    public class WWTHuobiUsdtMarginedMarketDepth
    {
        /// <summary>
        /// 卖盘,[price(挂单价), vol(此价格挂单张数)], 按price升序	
        /// </summary>
        [JsonProperty("asks", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<HuobiOrderBookEntry> Asks { get; set; } = Array.Empty<HuobiOrderBookEntry>();

        /// <summary>
        /// 买盘,[price(挂单价), vol(此价格挂单张数)], 按price降序	
        /// </summary>
        [JsonProperty("bids", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<HuobiOrderBookEntry> Bids { get; set; } = Array.Empty<HuobiOrderBookEntry>();

        /// <summary>
        /// 数据所属的 channel，格式： market.period	
        /// </summary>
        [JsonProperty("ch", NullValueHandling = NullValueHandling.Ignore)]
        public string Channel { get; set; } = string.Empty;

        /// <summary>
        /// 消息id
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; } = default;

        /// <summary>
        /// 订单ID
        /// </summary>
        [JsonProperty("mrid", NullValueHandling = NullValueHandling.Ignore)]
        public long Mrid { get; set; } = default;

        /// <summary>
        /// 响应生成时间点，单位：毫秒
        /// </summary>
        [JsonProperty("ts", NullValueHandling = NullValueHandling.Ignore)]
        public long Timestamp { get; set; } = default;

        /// <summary>
        /// 版本
        /// </summary>
        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public int Version { get; set; } = default;
    }
}