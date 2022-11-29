using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapStrategy
{
    /// <summary>
    /// 【全仓】跟踪委托订单撤单
    /// </summary>
    public class HuobiUsdtMarginedMarketSwapCrossTrackCancel
    {
        /// <summary>
        /// 撤销跟踪委托订单信息列表
        /// </summary>
        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<CrossCancelTrackOrderErrors> ErrorsList { get; set; } = Array.Empty<CrossCancelTrackOrderErrors>();

        /// <summary>
        /// 撤销成功跟踪委托订单信息列表
        /// </summary>
        [JsonProperty("successes", NullValueHandling = NullValueHandling.Ignore)]
        public string Successes { get; set; } = string.Empty;
    }

    /// <summary>
    /// 全仓撤销跟踪委托订单信息
    /// </summary>
    public class CrossCancelTrackOrderErrors
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderId { get; set; } = default(long);

        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("err_code", NullValueHandling = NullValueHandling.Ignore)]
        public int? ErrCode { get; set; } = default;

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("err_msg", NullValueHandling = NullValueHandling.Ignore)]
        public string? ErrMsg { get; set; } = string.Empty;
    }
}