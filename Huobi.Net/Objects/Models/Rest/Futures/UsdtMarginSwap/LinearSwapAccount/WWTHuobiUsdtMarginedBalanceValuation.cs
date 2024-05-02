using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount
{
    /// <summary>
    /// 【通用】获取账户合约总资产估值
    /// </summary>
    public class WWTHuobiUsdtMarginedBalanceValuation
    {
        /// <summary>
        /// 资产估值币种，即按该币种为单位进行估值	"BTC", "USD", "USDT", "CNY", "EUR", "GBP", "VND", "HKD", "TWD", "MYR", "SGD", "KRW", "RUB", "TRY"
        /// </summary>
        [JsonProperty("valuation_asset", NullValueHandling = NullValueHandling.Ignore)]
        public string ValuationAsset { get; set; } = string.Empty;

        /// <summary>
        /// 资产估值
        /// </summary>
        [JsonProperty("balance", NullValueHandling = NullValueHandling.Ignore)]

        public string Balance { get; set; } = string.Empty;
    }
}