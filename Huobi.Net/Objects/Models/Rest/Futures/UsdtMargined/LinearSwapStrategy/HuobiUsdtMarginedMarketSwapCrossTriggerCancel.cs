using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapStrategy
{
    /// <summary>
    /// 【全仓】撤销合约计划委托订单
    /// </summary>
    public class HuobiUsdtMarginedMarketSwapCrossTriggerCancel
    {
        /// <summary>
        /// 撤销失败合约计划委托订单信息列表
        /// </summary>
        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<CrossCancelTriggerOrderErrors> ErrorsList { get; set; } = Array.Empty<CrossCancelTriggerOrderErrors>();

        /// <summary>
        /// 撤销成功合约计划委托订单信息列表
        /// </summary>
        [JsonProperty("successes", NullValueHandling = NullValueHandling.Ignore)]
        public string Successes { get; set; } = string.Empty;
    }

    /// <summary>
    /// 全仓撤销失败合约计划委托订单信息
    /// </summary>
    public class CrossCancelTriggerOrderErrors
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