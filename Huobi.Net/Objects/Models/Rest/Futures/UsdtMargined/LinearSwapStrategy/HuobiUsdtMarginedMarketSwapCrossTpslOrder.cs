using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapStrategy
{
    /// <summary>
    /// 【全仓】对仓位设置止盈止损订单
    /// </summary>
    public class HuobiUsdtMarginedMarketSwapCrossTpslOrder
    {
        /// <summary>
        /// 止盈单下单结果
        /// </summary>
        [JsonProperty("tp_order", NullValueHandling = NullValueHandling.Ignore)]
        public CrossTpOrder? TpOrder { get; set; }

        /// <summary>
        /// 止损单下单结果
        /// </summary>
        [JsonProperty("sl_order", NullValueHandling = NullValueHandling.Ignore)]
        public CrossSlOrder? SlOrder { get; set; }
    }

    /// <summary>
    /// 止盈单下单结果
    /// </summary>
    public class CrossTpOrder
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
    public class CrossSlOrder
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