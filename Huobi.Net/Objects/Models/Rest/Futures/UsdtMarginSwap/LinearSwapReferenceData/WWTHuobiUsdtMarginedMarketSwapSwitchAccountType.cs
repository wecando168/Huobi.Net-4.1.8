using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapReferenceData
{
    /// <summary>
    /// 账户类型更改接口
    /// </summary>
    public class WWTHuobiUsdtMarginedMarketSwapSwitchAccountType
    {
        /// <summary>
        /// 账户类型	1:非统一账户（全仓逐仓账户）
        /// </summary>
        [JsonProperty("account_type", NullValueHandling = NullValueHandling.Ignore)]
        public int AccountType { get; set; }
    }
}