using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Socket
{
    /// <summary>
    /// Huobi specified time KLine
    /// </summary>
    public class HuobiSpecifiedTimeKLine
    {
        /// <summary>
        /// KLine id
        /// </summary>
        [JsonProperty("id")]
        public long? Id { get; set; }

        /// <summary>
        /// The open price
        /// </summary>
        [JsonProperty("open")]
        public decimal? Open { get; set; }

        /// <summary>
        /// The close price
        /// </summary>
        [JsonProperty("close")]
        public decimal? Close { get; set; }

        /// <summary>
        /// The low price
        /// </summary>
        [JsonProperty("low")]
        public decimal? Low { get; set; }

        /// <summary>
        /// The high price
        /// </summary>
        [JsonProperty("high")]
        public decimal? High { get; set; }

        /// <summary>
        /// The amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// The vol
        /// </summary>
        [JsonProperty("vol")]
        public decimal? Vol { get; set; }

        /// <summary>
        /// The Count
        /// </summary>
        [JsonProperty("count")]
        public long? Count { get; set; }
    }
}
