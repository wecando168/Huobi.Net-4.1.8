using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapMarketData
{
    /// <summary>
    /// 【通用】获取合约市场最优挂单
    /// </summary>
    public class HuobiUsdtMarginedMarketBbo
    {
        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;

        /// <summary>
        /// 业务类型	futures：交割、swap：永续
        /// </summary>
        [JsonProperty("business_type", NullValueHandling = NullValueHandling.Ignore)]
        public string BusinessType { get; set; } = string.Empty;

        /// <summary>
        /// 合约代码 或 合约标识	永续："BTC-USDT" ... ，交割：“BTC-USDT-210625”... 或 BTC-USDT-CW（当周合约标识）、BTC-USDT-NW（次周合约标识）、BTC-USDT-CQ（当季合约标识）、BTC-USDT-NQ（次季合约标识）
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// [买1价,买1量(张)]
        /// </summary>
        [JsonProperty("bid", NullValueHandling = NullValueHandling.Ignore)]
        public HuobiBidAskEntry? BestBid { get; set; } = default;

        /// <summary>
        /// [卖1价,卖1量(张)]	
        /// </summary>
        [JsonProperty("ask", NullValueHandling = NullValueHandling.Ignore)]
        public HuobiBidAskEntry? BestAsk { get; set; } = default;

        /// <summary>
        /// 撮合ID，唯一标识
        /// </summary>
        [JsonProperty("mrid", NullValueHandling = NullValueHandling.Ignore)]
        public long Mrid { get; set; } = default;

        /// <summary>
        /// 系统检测orderbook时间点，单位：毫秒	
        /// </summary>
        [JsonProperty("ts", NullValueHandling = NullValueHandling.Ignore)]
        public long Timestamp { get; set; } = default;
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public class HuobiBidAskEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// The price for this entry
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// The quantity for this entry
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }
    }
}