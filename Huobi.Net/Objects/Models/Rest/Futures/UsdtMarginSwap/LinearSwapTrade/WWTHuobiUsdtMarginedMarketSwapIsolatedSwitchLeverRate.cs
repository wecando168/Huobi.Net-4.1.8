namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapTrade
{
    /// <summary>
    /// 【逐仓】切换杠杆
    /// </summary>
    public class WWTHuobiUsdtMarginedMarketSwapIsolatedSwitchLeverRate
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string? ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金模式	isolated：逐仓模式
        /// </summary>
        [JsonProperty("margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string? MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// 切换成功后的杠杆倍数
        /// </summary>
        [JsonProperty("lever_rate", NullValueHandling = NullValueHandling.Ignore)]
        public int? LeverRate { get; set; } = default;

        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;

    }
}