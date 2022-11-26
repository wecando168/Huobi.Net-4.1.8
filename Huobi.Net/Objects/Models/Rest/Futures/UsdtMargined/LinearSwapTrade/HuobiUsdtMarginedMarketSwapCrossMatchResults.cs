using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapTrade
{
    /// <summary>
    /// 【全仓】获取历史成交记录(新)
    /// </summary>
    public class HuobiUsdtMarginedMarketSwapCrossMatchResults
    {
        /// <summary>
        /// 查询id，可作为下一次查询请求的from_id字段
        /// </summary>
        [JsonProperty("query_id", NullValueHandling = NullValueHandling.Ignore)]
        public long QueryId { get; set; } = default(long);

        /// <summary>
        /// 合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）
        /// </summary>
        [JsonProperty("contract_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 交易对	如：“BTC-USDT”
        /// </summary>
        [JsonProperty("pair", NullValueHandling = NullValueHandling.Ignore)]
        public string Pair { get; set; } = string.Empty;

        /// <summary>
        /// 业务类型	futures：交割、swap：永续
        /// </summary>
        [JsonProperty("business_type", NullValueHandling = NullValueHandling.Ignore)]
        public string BusinessType { get; set; } = string.Empty;

        /// <summary>
        /// 撮合结果id，不唯一，可能重复
        /// </summary>
        [JsonProperty("match_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? MatchId { get; set; } = default(long);

        /// <summary>
        /// 订单ID
        /// </summary>
        [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderId { get; set; } = default(long);

        /// <summary>
        /// 品种代码
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string? Symbol { get; set; } = string.Empty;

        /// <summary>
        /// 合约代码
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string? ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 买卖方向	"buy":买 "sell":卖
        /// </summary>
        [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
        public string? Direction { get; set; } = string.Empty;

        /// <summary>
        /// 开平方向	"open":开 "close":平 "both":单向持仓
        /// </summary>
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public string? Offset { get; set; } = string.Empty;

        /// <summary>
        /// 成交数量
        /// </summary>
        [JsonProperty("trade_volume", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TradeVolume { get; set; } = default;

        /// <summary>
        /// 成交价格
        /// </summary>
        [JsonProperty("trade_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TradePrice { get; set; } = default;

        /// <summary>
        /// 成交总金额 ，即sum（每一笔成交张数* 合约面值 * 成交价格）	
        /// </summary>
        [JsonProperty("trade_turnover", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TradeTurnover { get; set; } = default;

        /// <summary>
        /// 成交手续费
        /// </summary>
        [JsonProperty("trade_fee", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TradeFee { get; set; } = default;

        /// <summary>
        /// 本期平仓盈亏
        /// </summary>
        [JsonProperty("offset_profitloss", NullValueHandling = NullValueHandling.Ignore)]
        public decimal OffsetProfitloss { get; set; } = default;

        /// <summary>
        /// 成交时间戳
        /// </summary>
        [JsonProperty("create_date", NullValueHandling = NullValueHandling.Ignore)]
        public string CreateTimestamp { get; set; } = string.Empty;

        /// <summary>
        /// taker或maker
        /// </summary>
        [JsonProperty("role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// 订单来源（system:系统、web:用户网页、api:用户API、m:用户M站、risk:风控系统、settlement:交割结算、ios：ios客户端、android：安卓客户端、windows：windows客户端、mac：mac客户端、trigger：计划委托触发、tpsl:止盈止损触发 ）
        /// </summary>
        [JsonProperty("order_source", NullValueHandling = NullValueHandling.Ignore)]
        public string? OrderSource { get; set; } = string.Empty;

        /// <summary>
        /// String类型订单ID
        /// </summary>
        [JsonProperty("order_id_str", NullValueHandling = NullValueHandling.Ignore)]
        public string? OrderIdStr { get; set; } = string.Empty;

        /// <summary>
        /// 唯一成交id,由于match_id并不是unique的，具体使用方式是用match_id和id作为联合主键，拼接成unique的成交ID。
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string? Id { get; set; } = string.Empty;

        /// <summary>
        /// 手续费币种	（"USDT"...）
        /// </summary>
        [JsonProperty("fee_asset", NullValueHandling = NullValueHandling.Ignore)]
        public string? FeeAsset { get; set; } = string.Empty;

        /// <summary>
        /// 保证金模式   isolated：逐仓模式
        /// </summary>
        [JsonProperty("margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string? MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金账户   比如“BTC-USDT”
        /// </summary>
        [JsonProperty("margin_account", NullValueHandling = NullValueHandling.Ignore)]
        public string? MarginAccount { get; set; } = string.Empty;

        /// <summary>
        /// 真实收益（使用开仓均价计算，包含仓位跨结算的已实现盈亏。）	
        /// </summary>
        [JsonProperty("real_profit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? RealProfit { get; set; } = default;

        /// <summary>
        /// 是否为只减仓订单	0:表示为非只减仓订单，1:表示为只减仓订单
        /// </summary>
        [JsonProperty("reduce_only", NullValueHandling = NullValueHandling.Ignore)]
        public int? ReduceOnly { get; set; } = default;

        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;
    }
}