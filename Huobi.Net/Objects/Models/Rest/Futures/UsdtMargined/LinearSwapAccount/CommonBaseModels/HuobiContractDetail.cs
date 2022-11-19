using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapAccount.CommonBaseModels
{
    /// <summary>
    /// 支持永续的所有合约的相关字段
    /// </summary>
    public class HuobiContractDetail
    {
        /// <summary>
        /// 品种代码	"BTC","ETH"...
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// 合约代码	永续："BTC-USDT" ...
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;


        /// <summary>
        /// 持仓保证金（当前持有仓位所占用的保证金）
        /// </summary>
        [JsonProperty("margin_position", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MarginPosition { get; set; } = default;

        /// <summary>
        /// 冻结保证金
        /// </summary>
        [JsonProperty("margin_frozen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MarginFrozen { get; set; } = default;

        /// <summary>
        /// 可用保证金
        /// </summary>
        [JsonProperty("margin_available", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MarginAvailable { get; set; } = default;

        /// <summary>
        /// 未实现盈亏
        /// </summary>
        [JsonProperty("profit_unreal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ProfitUnreal { get; set; } = default;

        /// <summary>
        /// 预估强平价
        /// </summary>
        [JsonProperty("liquidation_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal LiquidationPrice { get; set; } = default;

        /// <summary>
        /// 杠杠倍数
        /// </summary>
        [JsonProperty("lever_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal LeverRate { get; set; } = default;

        /// <summary>
        /// 调整系数
        /// </summary>
        [JsonProperty("adjust_factor", NullValueHandling = NullValueHandling.Ignore)]
        public decimal AdjustFactor { get; set; } = default;

        /// <summary>
        /// 合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）
        /// </summary>
        [JsonProperty("contract_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 如：“BTC-USDT”
        /// </summary>
        [JsonProperty("pair", NullValueHandling = NullValueHandling.Ignore)]
        public string Pair { get; set; } = string.Empty;

        /// <summary>
        /// 业务类型	futures：交割、swap：永续
        /// </summary>
        [JsonProperty("business_type", NullValueHandling = NullValueHandling.Ignore)]
        public string BusinessType { get; set; } = string.Empty;

        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;
    }
}