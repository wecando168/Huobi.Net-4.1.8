using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapMarketData
{
    /// <summary>
    /// 【通用】获取市场最近成交记录
    /// </summary>
    public class HuobiUsdtMarginedMarketTrade
    {
        /// <summary>
        /// 成交记录集合
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<TradeData> Data { get; set; } = Array.Empty<TradeData>();

        /// <summary>
        /// 订单唯一id（品种唯一）
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; } = default;


        /// <summary>
        /// 成交时间戳
        /// </summary>
        [JsonProperty("ts", NullValueHandling = NullValueHandling.Ignore)]
        public long Timestamp { get; set; } = default;
    }

    /// <summary>
    /// 成交记录
    /// </summary>
    public class TradeData
    {
        /// <summary>
        /// 成交量(张)。 值是买卖双边之和
        /// </summary>
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public string Amount { get; set; } = string.Empty;

        /// <summary>
        /// 成交量（币）
        /// </summary>
        [JsonProperty("quantity", NullValueHandling = NullValueHandling.Ignore)]
        public string Quantity { get; set; } = string.Empty;

        /// <summary>
        /// 成交额（计价币种）
        /// </summary>
        [JsonProperty("trade_turnover", NullValueHandling = NullValueHandling.Ignore)]
        public string TradeTurnover { get; set; } = string.Empty;

        /// <summary>
        /// 成交时间戳
        /// </summary>
        [JsonProperty("ts", NullValueHandling = NullValueHandling.Ignore)]
        public long Timestamp { get; set; } = default;

        /// <summary>
        /// 订单唯一id（品种唯一）
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; } = default;

        /// <summary>
        /// 成交价
        /// </summary>
        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public string Price { get; set; } = string.Empty;

        /// <summary>
        /// 买卖方向，即taker(主动成交)的方向
        /// </summary>
        [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
        public string Direction { get; set; } = string.Empty;

        /// <summary>
        /// 合约代码
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 业务类型
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