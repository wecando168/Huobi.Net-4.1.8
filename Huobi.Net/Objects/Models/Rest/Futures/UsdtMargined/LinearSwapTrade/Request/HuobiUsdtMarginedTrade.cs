using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapTrade.Request
{
    /// <summary>
    /// 【通用】U本位合约成交明细
    /// </summary>
    public class HuobiUsdtMarginedTrade
    {
        /// <summary>
        /// 全局唯一的交易标识
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string? Id { get; set; } = string.Empty;

        /// <summary>
        /// 与linear-swap-api/v1/swap_cross_matchresults返回结果中的match_id一样，是撮合结果id， 非唯一，可重复，注意：一个撮合结果代表一个taker单和N个maker单的成交记录的集合，如果一个taker单吃了N个maker单，那这N笔trade都是一样的撮合结果id
        /// </summary>
        [JsonProperty("trade_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? TradeId { get; set; } = default(long);

        /// <summary>
        /// 成交价格
        /// </summary>
        [JsonProperty("trade_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TradePrice { get; set; } = default;

        /// <summary>
        /// 成交量（张）
        /// </summary>
        [JsonProperty("trade_vlume", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TradeVolume { get; set; } = default;

        /// <summary>
        /// 成交金额（成交数量* 合约面值 * 成交价格）	
        /// </summary>
        [JsonProperty("trade_turnover", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TradeTurnover { get; set; } = default;

        /// <summary>
        /// 成交手续费
        /// </summary>
        [JsonProperty("trade_fee", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TradeFee { get; set; } = default;

        /// <summary>
        /// taker或maker
        /// </summary>
        [JsonProperty("role", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Role { get; set; } = default;

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public long? CreatedTimestamp { get; set; } = default(long);

        /// <summary>
        /// 该笔成交的平仓盈亏（使用持仓均价计算，不包含仓位跨结算的已实现盈亏。）	
        /// </summary>
        [JsonProperty("profit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Profit { get; set; } = default;

        /// <summary>
        /// 该笔成交的真实收益（使用开仓均价计算，包含仓位跨结算的已实现盈亏。）	
        /// </summary>
        [JsonProperty("real_profit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? RealProfit { get; set; } = default;
    }
}