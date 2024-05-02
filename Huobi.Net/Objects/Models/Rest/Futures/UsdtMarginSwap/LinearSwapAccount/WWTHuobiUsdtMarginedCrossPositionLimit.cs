using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount
{
    /// <summary>
    /// 【全仓】用户持仓量限制的查询(PrivateData)
    /// </summary>
    public class WWTHuobiUsdtMarginedCrossPositionLimit
    {
        /// <summary>
        /// 品种代码	"BTC","ETH"...
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// 合约代码	"BTC-USDT" ...
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金模式	cross：全仓模式；
        /// </summary>
        [JsonProperty("margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// 合约多仓持仓的最大值，单位为张
        /// </summary>
        [JsonProperty("buy_limit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal BuyLimit { get; set; } = default;

        /// <summary>
        /// 合约空仓持仓的最大值，单位为张
        /// </summary>
        [JsonProperty("sell_limit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal SellLimit { get; set; } = default;

        /// <summary>
        /// 业务类型	futures：交割、swap：永续
        /// </summary>
        [JsonProperty("business_type", NullValueHandling = NullValueHandling.Ignore)]
        public string BusinessType { get; set; } = string.Empty;

        /// <summary>
        /// 合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）
        /// </summary>
        [JsonProperty("contract_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 交易对	如：“BTC-USDT”
        /// </summary>
        [JsonProperty("pair", NullValueHandling = NullValueHandling.Ignore)]
        public string Pair { get; set; } = string.Empty;

        /// <summary>
        /// 用户当前品种杠杆倍数
        /// </summary>
        [JsonProperty("lever_rate", NullValueHandling = NullValueHandling.Ignore)]
        public int LeverRate { get; set; } = default;

        /// <summary>
        /// 合约多仓持仓价值上限，单位USDT
        /// </summary>
        [JsonProperty("buy_limit_value", NullValueHandling = NullValueHandling.Ignore)]
        public decimal BuyLimitValue { get; set; } = default;

        /// <summary>
        /// 合约空仓持仓价值上限，单位USDT
        /// </summary>
        [JsonProperty("sell_limit_value", NullValueHandling = NullValueHandling.Ignore)]
        public decimal SellLimitValue { get; set; } = default;

        /// <summary>
        /// 当前品种标记价格（以该价格用于计算持仓张数）	
        /// </summary>
        [JsonProperty("mark_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MarkPrice { get; set; } = default;
    }
}