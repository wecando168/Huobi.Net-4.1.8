using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Contract code data
    /// </summary>
    public class HuobiContractCodeData
    {
        /// <summary>
        /// 开盘价
        /// </summary>
        [JsonProperty("open", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OpenPrice { get; set; }

        /// <summary>
        /// 收盘价,当K线为最晚的一根时，是最新成交价
        /// </summary>
        [JsonProperty("close", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ClosePrice { get; set; }
        
        /// <summary>
        /// 最高价
        /// </summary>
        [JsonProperty("high", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? HighPrice { get; set; }

        /// <summary>
        /// 最低价
        /// </summary>
        [JsonProperty("low", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? LowPrice { get; set; }

        /// <summary>
        /// 成交量(币), 即 sum(每一笔成交量(张) * 单张合约面值/该笔成交价)。 值是买卖双边之和
        /// </summary>
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Amount { get; set; }

        /// <summary>
        /// 成交量张数。 值是买卖双边之和
        /// </summary>
        [JsonProperty("vol", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Vol { get; set; }

        /// <summary>
        /// 成交额, 即sum（每一笔成交张数 * 合约面值 * 成交价格）。 值是买卖双边之和
        /// </summary>
        [JsonProperty("trade_turnover", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TradeTurnover { get; set; }

        /// <summary>
        /// 成交笔数。 值是买卖双边之和
        /// </summary>
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Count { get; set; }
    }

    /// <summary>
    /// Contract code depth data
    /// </summary>
    public class HuobiContractCodedDepthData
    {
        /// <summary>
        /// The current best bid for the symbol
        /// </summary>
        [JsonProperty("bids")]
        public IEnumerable<HuobiOrderBookEntry>? Bids { get; set; }

        /// <summary>
        /// The current best ask for the symbol
        /// </summary>
        [JsonProperty("asks")]
        public IEnumerable<HuobiOrderBookEntry>? Asks { get; set; }
    }

    /// <summary>
    /// Huobi contract code kline tick
    /// </summary>
    public class HuobiContractCodeKlineTick : HuobiContractCodeData
    {
        /// <summary>
        /// K线ID,也就是K线时间戳，K线起始时间戳
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        /// <summary>
        /// 合约订单编号
        /// </summary>
        [JsonProperty("mrid", NullValueHandling = NullValueHandling.Ignore)]
        public long? MarginedId { get; set; }
    }

    /// <summary>
    /// Huobi contract code depth tick
    /// </summary>
    public class HuobiContractCodeDepthTick : HuobiContractCodedDepthData
    {
        /// <summary>
        /// K线ID,也就是K线时间戳，K线起始时间戳
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        /// <summary>
        /// 合约订单编号
        /// </summary>
        [JsonProperty("mrid", NullValueHandling = NullValueHandling.Ignore)]
        public long? MarginedId { get; set; }
    }

    /// <summary>
    /// 火币合约WebSocket K线
    /// </summary>
    public class HuobiContractCodeKline
    {
        /// <summary>
        /// 请求参数
        /// </summary>
        [JsonProperty("ch", NullValueHandling = NullValueHandling.Ignore)]
        public string Channel { get; set; } = string.Empty;

        /// <summary>
        /// 响应生成时间点，单位：毫秒
        /// </summary>
        [JsonProperty("ts", NullValueHandling = NullValueHandling.Ignore)]
        public long? Timestamp { get; set; } = default;

        /// <summary>
        /// K线ID,也就是K线时间戳，K线起始时间戳
        /// </summary>
        [JsonProperty("tick", NullValueHandling = NullValueHandling.Ignore)]
        public HuobiContractCodeKlineTick? Tick { get; set; } = default;
    }
}
