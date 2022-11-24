using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapTrade
{
    /// <summary>
    /// 【全仓】撤销合约订单
    /// </summary>
    public class HuobiUsdtMarginedMarketSwapCrossCancel
    {
        /// <summary>
        /// 撤销失败订单信息列表
        /// </summary>
        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<CrossCancelOrderErrors> ErrorsList { get; set; } = Array.Empty<CrossCancelOrderErrors>();

        /// <summary>
        /// 撤销成功订单信息列表
        /// </summary>
        [JsonProperty("successes", NullValueHandling = NullValueHandling.Ignore)]
        public string Successes { get; set; } = string.Empty;
    }

    /// <summary>
    /// 全仓撤销失败订单信息
    /// </summary>
    public class CrossCancelOrderErrors
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
        public int? ErrCode { get; set; } = default(int);

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("err_msg", NullValueHandling = NullValueHandling.Ignore)]
        public string? ErrMsg { get; set; } = string.Empty;
    }
}