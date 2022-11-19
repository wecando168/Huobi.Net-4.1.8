using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapReferenceData
{
    /// <summary>
    /// 【通用】获取合约当前总持仓量
    /// </summary>
    public class HuobiUsdtMarginedMarketSwapOpenInterest
    {
        /// <summary>
        /// 总持仓额（单位为合约的计价币种，如USDT）
        /// </summary>
        [JsonProperty("volume", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Volume { get; set; } = default;

        /// <summary>
        /// 持仓量(币)，单边数量
        /// </summary>
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount { get; set; } = default;

        /// <summary>
        /// 品种代码	"BTC", "ETH" ...
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// 总持仓额（单位为合约的计价币种，如USDT）
        /// </summary>
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Value { get; set; } = default;

        /// <summary>
        /// 合约代码	永续："BTC-USDT"... ，交割："BTC-USDT-210625"...
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 最近24小时成交量（币）（当前时间-24小时）,值是买卖双边之和	
        /// </summary>
        [JsonProperty("trade_amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TradeAmount { get; set; } = default;

        /// <summary>
        /// 最近24小时成交量（张）（当前时间-24小时）,值是买卖双边之和
        /// </summary>
        [JsonProperty("trade_volume", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TradeVolume { get; set; } = default;

        /// <summary>
        /// 最近24小时成交额 （当前时间-24小时）,值是买卖双边之和
        /// </summary>
        [JsonProperty("trade_turnover", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TradeTurnover { get; set; } = default;

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
        /// 合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）
        /// </summary>
        [JsonProperty("contract_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;
    }
}