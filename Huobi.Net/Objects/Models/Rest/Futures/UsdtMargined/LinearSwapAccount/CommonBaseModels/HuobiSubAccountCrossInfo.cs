using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapAccount.CommonBaseModels
{
    /// <summary>
    /// 子账户合约全仓资产信息
    /// </summary>
    public class HuobiSubAccountCrossInfo
    {
        /// <summary>
        /// 账户权益
        /// </summary>
        [JsonProperty("margin_balance", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MarginBalance { get; set; } = default;

        /// <summary>
        /// 保证金率
        /// </summary>
        [JsonProperty("risk_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal RiskRate { get; set; }

        /// <summary>
        /// 保证金币种（计价币种）
        /// </summary>
        [JsonProperty("margin_asset", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginAsset { get; set; } = string.Empty;

        /// <summary>
        /// 保证金模式	cross：全仓模式；
        /// </summary>
        [JsonProperty("margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金账户	比如“USDT”
        /// </summary>
        [JsonProperty("margin_account", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginAccount { get; set; } = string.Empty;

        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;
    }
}