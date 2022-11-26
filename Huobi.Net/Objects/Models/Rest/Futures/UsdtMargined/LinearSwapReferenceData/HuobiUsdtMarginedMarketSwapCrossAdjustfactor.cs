using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapReferenceData
{
    /// <summary>
    /// 【全仓】查询平台阶梯调整系数
    /// </summary>
    public class HuobiUsdtMarginedMarketSwapCrossAdjustfactor
    {
        /// <summary>
        /// 品种代码	"BTC","ETH" ...
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// 合约代码	如 永续："BTC-USDT"... ，交割："BTC-USDT-210625"...
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金模式	cross：全仓模式
        /// </summary>
        [JsonProperty("margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginMode { get; set; } = string.Empty;
        
        /// <summary>
        /// 业务类型	futures：交割、swap：永续
        /// </summary>
        [JsonProperty("business_type", NullValueHandling = NullValueHandling.Ignore)]
        public string BusinessType { get; set; } = string.Empty;

        /// <summary>
        /// 交易代码	如：“BTC-USDT”
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

        /// <summary>
        /// 全仓平台阶梯调整系数列表
        /// </summary>
        [JsonProperty("list", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<CrossAdjustfactor> CrossAdjustfactors { get; set; } = Array.Empty<CrossAdjustfactor>();
    }

    /// <summary>
    /// 全仓平台阶梯调整系数
    /// </summary>
    public class CrossAdjustfactor
    {
        /// <summary>
        /// 杠杆倍数
        /// </summary>
        [JsonProperty("lever_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal LeverRate { get; set; } = default;

        /// <summary>
        /// 全仓平台阶梯调整系数列表
        /// </summary>
        [JsonProperty("ladders", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<CrossAdjustfactorLadderDetail> CrossAdjustfactorLadderDetails { get; set; } = Array.Empty<CrossAdjustfactorLadderDetail>();
    }

    /// <summary>
    /// 全仓平台阶梯调整系明细
    /// </summary>
    public class CrossAdjustfactorLadderDetail
    {
        /// <summary>
        /// 档位 从0档开始
        /// </summary>
        [JsonProperty("ladder", NullValueHandling = NullValueHandling.Ignore)]
        public int Ladder { get; set; } = default;

        /// <summary>
        /// 净持仓量的最小值
        /// </summary>
        [JsonProperty("min_size", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MinSize { get; set; } = default;

        /// <summary>
        /// 净持仓量的最大值
        /// </summary>
        [JsonProperty("max_size", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MaxSize { get; set; } = default;

        /// <summary>
        /// 调整系数
        /// </summary>
        [JsonProperty("adjust_factor", NullValueHandling = NullValueHandling.Ignore)]
        public decimal AdjustFactor { get; set; } = default;
    }
}