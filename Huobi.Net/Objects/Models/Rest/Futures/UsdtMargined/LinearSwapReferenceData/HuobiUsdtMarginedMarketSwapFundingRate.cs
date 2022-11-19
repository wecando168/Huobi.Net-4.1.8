using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapReferenceData
{
    /// <summary>
    /// 【通用】获取合约的资金费率
    /// </summary>
    public class HuobiUsdtMarginedMarketSwapFundingRate
    {
        /// <summary>
        /// 下一期预测资金费率（一分钟计算一次）	
        /// </summary>
        [JsonProperty("estimated_rate", NullValueHandling = NullValueHandling.Ignore)]
        public string EstimatedRate { get; set; } = string.Empty;

        /// <summary>
        /// 当期资金费率
        /// </summary>
        [JsonProperty("funding_rate", NullValueHandling = NullValueHandling.Ignore)]
        public string FundingRate { get; set; } = string.Empty;

        /// <summary>
        /// 合约代码	"BTC-USDT" ...
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 品种代码
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// 资金费币种	"USDT"...
        /// </summary>
        [JsonProperty("fee_asset", NullValueHandling = NullValueHandling.Ignore)]
        public string FeeAsset { get; set; } = string.Empty;

        /// <summary>
        /// 当期资金费率时间
        /// </summary>
        [JsonProperty("funding_time", NullValueHandling = NullValueHandling.Ignore)]
        public string FundingTime { get; set; } = string.Empty;

        /// <summary>
        /// 下一期资金费率时间
        /// </summary>
        [JsonProperty("next_funding_time", NullValueHandling = NullValueHandling.Ignore)]
        public string NextFundingTime { get; set; } = string.Empty;

        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;
    }
}