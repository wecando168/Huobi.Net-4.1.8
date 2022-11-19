using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapReferenceData
{
    /// <summary>
    /// 【通用】获取合约的历史资金费率
    /// </summary>
    public class HuobiUsdtMarginedMarketSwapHistoricalFundingRate
    {
        /// <summary>
        /// 总页数	
        /// </summary>
        [JsonProperty("total_page", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalPage { get; set; } = default;

        /// <summary>
        /// 当前页	
        /// </summary>
        [JsonProperty("current_page", NullValueHandling = NullValueHandling.Ignore)]
        public int CurrentPage { get; set; } = default;

        /// <summary>
        /// 总条数	
        /// </summary>
        [JsonProperty("total_size", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalSize { get; set; } = default;

        /// <summary>
        /// 当前合约代码的历史资金费率	
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<HistoricalFundingRateData> Data { get; set; } = Array.Empty<HistoricalFundingRateData>();
    }

    /// <summary>
    /// 当前合约代码的历史资金费率
    /// </summary>
    public class HistoricalFundingRateData
    {
        /// <summary>
        /// 平均溢价指数
        /// </summary>
        [JsonProperty("avg_premium_index", NullValueHandling = NullValueHandling.Ignore)]
        public string AvgPremiumIndex { get; set; } = string.Empty;

        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;

        /// <summary>
        /// 当期资金费率
        /// </summary>
        [JsonProperty("funding_rate", NullValueHandling = NullValueHandling.Ignore)]
        public string FundingRate { get; set; } = string.Empty;

        /// <summary>
        /// 实际资金费率
        /// </summary>
        [JsonProperty("realized_rate", NullValueHandling = NullValueHandling.Ignore)]
        public string RealizedRate { get; set; } = string.Empty;

        /// <summary>
        /// 资金费率时间
        /// </summary>
        [JsonProperty("funding_time", NullValueHandling = NullValueHandling.Ignore)]
        public string FundingTime { get; set; } = string.Empty;

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
    }

}