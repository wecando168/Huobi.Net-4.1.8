using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapMarketData
{
    /// <summary>
    /// 【通用】获取合约实时预测资金费率的K线数据
    /// </summary>
    public class HuobiUsdtMarginedMarketHistoryBasis
    {
        /// <summary>
        /// 基差=合约基准价 - 指数基准价
        /// </summary>
        [JsonProperty("basis", NullValueHandling = NullValueHandling.Ignore)]
        public string Basis { get; set; } = string.Empty;

        /// <summary>
        /// 基差率=基差/指数基准价
        /// </summary>
        [JsonProperty("basis_rate", NullValueHandling = NullValueHandling.Ignore)]
        public string BasisRate { get; set; } = string.Empty;

        /// <summary>
        /// 合约最新成交价
        /// </summary>
        [JsonProperty("contract_price", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractPrice { get; set; } = string.Empty;

        /// <summary>
        /// 唯一标识
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; } = default;

        /// <summary>
        /// 指数基准价，与基差价格类型匹配
        /// </summary>
        [JsonProperty("index_price", NullValueHandling = NullValueHandling.Ignore)]
        public string IndexPrice { get; set; } = string.Empty;
    }
}