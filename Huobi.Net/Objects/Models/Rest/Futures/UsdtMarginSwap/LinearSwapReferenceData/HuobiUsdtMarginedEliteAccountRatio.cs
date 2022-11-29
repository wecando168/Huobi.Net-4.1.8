using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapReferenceData
{
    /// <summary>
    /// 【通用】精英账户多空持仓对比-账户数
    /// </summary>
    public class HuobiUsdtMarginedEliteAccountRatio
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
        /// 业务类型	futures：交割、swap：永续
        /// </summary>
        [JsonProperty("business_type", NullValueHandling = NullValueHandling.Ignore)]
        public string BusinessType { get; set; } = string.Empty;

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

        /// <summary>
        /// 多空持仓对比列表
        /// </summary>
        [JsonProperty("list", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<AccountRatioDetail> AccountRatioDetails { get; set; } = Array.Empty<AccountRatioDetail>();
    }

    /// <summary>
    /// 多空账户数对比数据
    /// </summary>
    public class AccountRatioDetail
    {
        /// <summary>
        /// 净多仓的账户比例
        /// </summary>
        [JsonProperty("buy_ratio", NullValueHandling = NullValueHandling.Ignore)]
        public decimal BuyRatio { get; set; } = default;

        /// <summary>
        /// 净空仓的账户比例
        /// </summary>
        [JsonProperty("sell_ratio", NullValueHandling = NullValueHandling.Ignore)]
        public decimal SellRatio { get; set; } = default;

        /// <summary>
        /// 锁仓的账户比例
        /// </summary>
        [JsonProperty("locked_ratio", NullValueHandling = NullValueHandling.Ignore)]
        public decimal LockedRatio { get; set; } = default;

        /// <summary>
        /// 响应生成时间点，单位：毫秒	
        /// </summary>
        [JsonProperty("settlement_time", NullValueHandling = NullValueHandling.Ignore)]
        public long Timestamp { get; set; } = default(long);
    }
}