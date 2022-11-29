using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount.CommonBaseModels;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount
{
    /// <summary>
    /// 【全仓】批量获取子账户资产信息
    /// </summary>
    public class HuobiUsdtMarginedCrossSubAccountList
    {
        /// <summary>
        /// 子账户UID
        /// </summary>
        [JsonProperty("sub_uid", NullValueHandling = NullValueHandling.Ignore)]
        public string SubUid { get; set; } = string.Empty;

        /// <summary>
        /// 账户权益
        /// </summary>
        [JsonProperty("account_info_list", NullValueHandling = NullValueHandling.Ignore)]

        public IEnumerable<HuobiSubAccountCrossInfo> List { get; set; } = Array.Empty<HuobiSubAccountCrossInfo>();
    }
}