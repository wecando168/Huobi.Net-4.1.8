namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapStrategy
{
    /// <summary>
    /// 【逐仓】对仓位设置止盈止损订单
    /// </summary>
    public class WWTHuobiUsdtMarginedMarketSwapIsolatedTpslOrder
    {
        /// <summary>
        /// 止盈单下单结果
        /// </summary>
        [JsonProperty("tp_order", NullValueHandling = NullValueHandling.Ignore)]
        public IsolatedTpOrder? TpOrder { get; set; }

        /// <summary>
        /// 止损单下单结果
        /// </summary>
        [JsonProperty("sl_order", NullValueHandling = NullValueHandling.Ignore)]
        public IsolatedSlOrder? SlOrder { get; set; }
    }

    /// <summary>
    /// 止盈单下单结果
    /// </summary>
    public class IsolatedTpOrder
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderId { get; set; } = default(long);

        /// <summary>
        /// 订单ID
        /// </summary>
        [JsonProperty("order_id_str", NullValueHandling = NullValueHandling.Ignore)]
        public string? OrderIdStr { get; set; } = string.Empty;
    }

    /// <summary>
    /// 止损单下单结果
    /// </summary>
    public class IsolatedSlOrder
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderId { get; set; } = default(long);

        /// <summary>
        /// 订单ID
        /// </summary>
        [JsonProperty("order_id_str", NullValueHandling = NullValueHandling.Ignore)]
        public string? OrderIdStr { get; set; } = string.Empty;
    }
}