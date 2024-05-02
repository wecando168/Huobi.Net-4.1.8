namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapMarketData
{
    /// <summary>
    /// 合约系统查询是否可用
    /// </summary>
    public class WWTHuobiUsdtMarginedMarketTimestamp
    {
        /// <summary>
        /// 当前系统时间戳
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string? Status { get; set; }

        /// <summary>
        /// 当前系统时间戳
        /// </summary>
        [JsonProperty("ts", NullValueHandling = NullValueHandling.Ignore)]
        public long Data { get; set; }
    }
}
