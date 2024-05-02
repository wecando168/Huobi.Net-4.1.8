using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapReferenceData
{
    /// <summary>
    /// 【通用】获取合约指数信息
    /// </summary>
    public class WWTHuobiUsdtMarginedMarketSwapIndex
    {
        /// <summary>
        /// 指数价格
        /// </summary>
        [JsonProperty("index_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal IndexPrice { get; set; }

        /// <summary>
        /// 响应生成时间点，单位：毫秒	
        /// </summary>
        [JsonProperty("index_ts", NullValueHandling = NullValueHandling.Ignore)]
        public long IndexTimestamp { get; set; }

        /// <summary>
        /// 指数代码	"BTC-USDT","ETH-USDT"...
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 指数分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;
    }
}