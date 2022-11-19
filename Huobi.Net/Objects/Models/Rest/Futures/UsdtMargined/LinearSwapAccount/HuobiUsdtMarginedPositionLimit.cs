using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapAccount
{
    /// <summary>
    /// 【逐仓】用户持仓量限制的查询(PrivateData)
    /// </summary>
    public class HuobiUsdtMarginedPositionLimit
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
        /// 合约多仓持仓的最大值，单位为张
        /// </summary>
        [JsonProperty("buy_limit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal BuyLimit { get; set; } = default(decimal);

        /// <summary>
        /// 合约空仓持仓的最大值，单位为张
        /// </summary>
        [JsonProperty("sell_limit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal SellLimit { get; set; } = default(decimal);

        /// <summary>
        /// 保证金模式	isolated：逐仓模式
        /// </summary>
        [JsonProperty("margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// 用户当前品种杠杆倍数
        /// </summary>
        [JsonProperty("lever_rate", NullValueHandling = NullValueHandling.Ignore)]
        public int LeverRate { get; set; } = default(int);

        /// <summary>
        /// 合约多仓持仓价值上限，单位USDT
        /// </summary>
        [JsonProperty("buy_limit_value", NullValueHandling = NullValueHandling.Ignore)]
        public decimal BuyLimitValue { get; set; } = default(decimal);

        /// <summary>
        /// 合约空仓持仓价值上限，单位USDT
        /// </summary>
        [JsonProperty("sell_limit_value", NullValueHandling = NullValueHandling.Ignore)]
        public decimal SellLimitValue { get; set; } = default(decimal);

        /// <summary>
        /// 当前品种标记价格（以该价格用于计算持仓张数）	
        /// </summary>
        [JsonProperty("mark_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MarkPrice { get; set; } = default(decimal);
    }
}