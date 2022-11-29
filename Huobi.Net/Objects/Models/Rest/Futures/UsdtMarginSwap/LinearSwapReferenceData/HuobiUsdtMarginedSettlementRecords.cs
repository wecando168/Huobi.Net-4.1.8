using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapReferenceData
{
    /// <summary>
    /// 【通用】查询平台历史结算记录
    /// </summary>
    public class HuobiUsdtMarginedSettlementRecords
    {
        /// <summary>
        /// 总页数
        /// </summary>
        [JsonProperty("total_page", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalPage { get; set; } = default;

        /// <summary>
        /// 当前页
        /// </summary>
        [JsonProperty("current_page", NullValueHandling = NullValueHandling.Ignore)]
        public int CurrentPage { get; set; } = default;

        /// <summary>
        /// 总条数
        /// </summary>
        [JsonProperty("total_size", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalSize { get; set; } = default;

        /// <summary>
        /// 结算记录列表
        /// </summary>
        [JsonProperty("settlement_record", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<UniversalSettlementRecord> SettlementRecords { get; set; } = Array.Empty<UniversalSettlementRecord>();
    }

    /// <summary>
    /// 结算记录
    /// </summary>
    public class UniversalSettlementRecord
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
        /// 本期结算时间，交割时为交割时间	
        /// </summary>
        [JsonProperty("settlement_time", NullValueHandling = NullValueHandling.Ignore)]
        public long SettlementTime { get; set; } = default(long);

        /// <summary>
        /// 分摊比例
        /// </summary>
        [JsonProperty("clawback_ratio", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ClawbackRatio { get; set; } = default;

        /// <summary>
        /// 本期结算价格，交割时为交割价格	
        /// </summary>
        [JsonProperty("settlement_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal SettlementPrice { get; set; } = default;

        /// <summary>
        /// 结算类型	settlement：结算；delivery：交割；
        /// </summary>
        [JsonProperty("settlement_type", NullValueHandling = NullValueHandling.Ignore)]
        public string SettlementType { get; set; } = string.Empty;

        /// <summary>
        /// 业务类型	futures：交割、swap：永续
        /// </summary>
        [JsonProperty("business_type", NullValueHandling = NullValueHandling.Ignore)]
        public string BusinessType { get; set; } = string.Empty;

        /// <summary>
        /// 交易对	如：“BTC-USDT”
        /// </summary>
        [JsonProperty("pair", NullValueHandling = NullValueHandling.Ignore)]
        public string Pair { get; set; } = string.Empty;
    }
}