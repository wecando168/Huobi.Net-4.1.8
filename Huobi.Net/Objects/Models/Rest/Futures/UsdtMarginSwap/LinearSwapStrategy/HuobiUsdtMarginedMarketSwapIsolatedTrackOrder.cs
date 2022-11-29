using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapStrategy
{
    /// <summary>
    /// 【逐仓】跟踪委托订单下单
    /// </summary>
    public class HuobiUsdtMarginedMarketSwapIsolatedTrackOrder
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderId { get; set; } = default(long);

        /// <summary>
        /// 字符串类型的跟踪委托订单ID
        /// </summary>
        [JsonProperty("order_id_str", NullValueHandling = NullValueHandling.Ignore)]
        public string? OrderIdStr { get; set; } = string.Empty;
    }
}