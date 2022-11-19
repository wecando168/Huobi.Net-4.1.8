using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapReferenceData
{
    /// <summary>
    /// 【通用】获取预估结算价
    /// </summary>
    public class HuobiUsdtMarginedEliteSettlementPrice
    {
        /// <summary>
        /// 合约代码	"BTC-USDT" ...
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 本期预估结算价/预估交割价（结算类型为交割时为预估交割价；结算时为预估结算价；）
        /// </summary>
        [JsonProperty("estimated_settlement_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal EstimatedSettlementPrice { get; set; } = default(decimal);

        /// <summary>
        /// 本期结算类型	“delivery”：交割，“settlement”：结算
        /// </summary>
        [JsonProperty("settlement_type", NullValueHandling = NullValueHandling.Ignore)]
        public string SettlementType { get; set; } = string.Empty;

        /// <summary>
        /// 业务类型	futures：交割、swap：永续
        /// </summary>
        [JsonProperty("business_type", NullValueHandling = NullValueHandling.Ignore)]
        public string BusinessType { get; set; } = string.Empty;

        /// <summary>
        /// 合约类型    swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）
        /// </summary>
        [JsonProperty("contract_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 交易对	如：“BTC-USDT”
        /// </summary>
        [JsonProperty("pair", NullValueHandling = NullValueHandling.Ignore)]
        public string Pair { get; set; } = string.Empty;

        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;
    }
}