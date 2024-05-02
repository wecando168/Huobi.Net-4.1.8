using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapReferenceData
{
    /// <summary>
    /// 【逐仓】获取平台阶梯保证金
    /// </summary>
    public class WWTHuobiUsdtMarginedMarketIsolatedLadderMargin
    {
        /// <summary>
        /// 保证金账户	比如“USDT”
        /// </summary>
        [JsonProperty("margin_account", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginAccount { get; set; } = string.Empty;

        /// <summary>
        /// 品种代码
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// 合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金模式	cross：全仓模式
        /// </summary>
        [JsonProperty("margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// 阶梯保证金数据列表
        /// </summary>
        [JsonProperty("list", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<LadderMarginList> LadderMarginLists { get; set; } = Array.Empty<LadderMarginList>();
    }

    /// <summary>
    /// 阶梯保证金数据列表
    /// </summary>
    public class LadderMarginList
    {
        /// <summary>
        /// 杠杆倍数
        /// </summary>
        [JsonProperty("lever_rate", NullValueHandling = NullValueHandling.Ignore)]
        public int LeverRate { get; set; } = default;

        /// <summary>
        /// 该合约对应杠杆倍数下的阶梯保证金数据
        /// </summary>
        [JsonProperty("ladders", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<LadderMarginDetail> LadderMarginDetails { get; set; } = Array.Empty<LadderMarginDetail>();
    }

    /// <summary>
    /// 阶梯保证金数据
    /// </summary>
    public class LadderMarginDetail
    {
        /// <summary>
        /// 最小账户权益（该阶梯权益范围起点，包含该值）
        /// </summary>
        [JsonProperty("min_margin_balance", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MinMarginBalance { get; set; } = default;

        /// <summary>
        /// 最大账户权益（该阶梯权益范围终点，不包含该值，该值属于下一阶梯的权益范围起点）	
        /// </summary>
        [JsonProperty("max_margin_balance", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MaxMarginBalance { get; set; } = default;

        /// <summary>
        /// 最小可用保证金（范围内包含该值）	
        /// </summary>
        [JsonProperty("min_margin_available", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MinMarginAvailable { get; set; } = default;

        /// <summary>
        /// 最大可用保证金（范围内不包含该值，该值属于下一阶梯的最小可用保证金）
        /// </summary>
        [JsonProperty("max_margin_available", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MaxMarginAvailable { get; set; } = default;
    }
}