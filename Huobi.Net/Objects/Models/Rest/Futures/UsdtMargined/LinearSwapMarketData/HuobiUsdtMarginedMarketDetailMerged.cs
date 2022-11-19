using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapMarketData
{
    /// <summary>
    /// 【通用】获取聚合行情
    /// </summary>
    public class HuobiUsdtMarginedMarketDetailMerged
    {
        /// <summary>
        /// 成交量(币), 即 (成交量(张) * 单张合约面值)（最近24（当前时间-24小时）小时成交量币）。 值是买卖双边之和
        /// </summary>
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public string Amount { get; set; } = string.Empty;

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
        /// 收盘价,当K线为最晚的一根时，是最新成交价
        /// </summary>
        [JsonProperty("close", NullValueHandling = NullValueHandling.Ignore)]
        public string Close { get; set; } = string.Empty;

        /// <summary>
        /// 成交笔数（当前时间-24小时）小时成交笔数）。 值是买卖双边之和
        /// </summary>
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Count { get; set; } = default;

        /// <summary>
        /// 最高价
        /// </summary>
        [JsonProperty("high", NullValueHandling = NullValueHandling.Ignore)]
        public string High { get; set; } = string.Empty;

        /// <summary>
        /// K线ID,也就是K线时间戳
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; } = default;

        /// <summary>
        /// 最低价
        /// </summary>
        [JsonProperty("low", NullValueHandling = NullValueHandling.Ignore)]
        public string Low { get; set; } = string.Empty;

        /// <summary>
        /// 开盘价
        /// </summary>
        [JsonProperty("open", NullValueHandling = NullValueHandling.Ignore)]
        public string Open { get; set; } = string.Empty;

        /// <summary>
        /// 成交额，即 sum（每一笔成交张数 * 合约面值 * 成交价格）（当前时间-24小时）小时成交额）。 值是买卖双边之和
        /// </summary>
        [JsonProperty("trade_turnover", NullValueHandling = NullValueHandling.Ignore)]
        public string TradeTurnover { get; set; } = string.Empty;

        /// <summary>
        /// 时间戳
        /// </summary>
        [JsonProperty("ts", NullValueHandling = NullValueHandling.Ignore)]
        public long Timestamp { get; set; } = default;

        /// <summary>
        /// 成交量（张）（最近24（当前时间-24小时）小时成交量张）。 值是买卖双边之和	
        /// </summary>
        [JsonProperty("vol", NullValueHandling = NullValueHandling.Ignore)]
        public string Vol { get; set; } = string.Empty;
    }
}