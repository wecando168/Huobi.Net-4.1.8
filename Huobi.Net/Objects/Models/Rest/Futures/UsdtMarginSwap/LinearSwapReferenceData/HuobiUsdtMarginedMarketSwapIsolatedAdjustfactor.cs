using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapReferenceData
{
    /// <summary>
    /// 【逐仓】查询平台阶梯调整系数
    /// </summary>
    public class HuobiUsdtMarginedMarketSwapIsolatedAdjustfactor
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
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;

        /// <summary>
        /// 逐仓平台阶梯调整系数列表
        /// </summary>
        [JsonProperty("list", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Adjustfactor> Adjustfactors { get; set; } = Array.Empty<Adjustfactor>();
    }

    /// <summary>
    /// 逐仓平台阶梯调整系数
    /// </summary>
    public class Adjustfactor
    {
        /// <summary>
        /// 杠杆倍数
        /// </summary>
        [JsonProperty("lever_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal LeverRate { get; set; } = default;

        /// <summary>
        /// 逐仓平台阶梯调整系数列表
        /// </summary>
        [JsonProperty("ladders", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<AdjustfactorLadderDetail> AdjustfactorLadderDetails { get; set; } = Array.Empty<AdjustfactorLadderDetail>();
    }

    /// <summary>
    /// 逐仓平台阶梯调整系明细
    /// </summary>
    public class AdjustfactorLadderDetail
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