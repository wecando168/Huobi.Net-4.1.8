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
    /// 【通用】批量设置子账户交易权限
    /// </summary>
    public class HuobiUsdtMarginedSubAuth
    {
        /// <summary>
        /// 子账户交易权限设置异常错误
        /// </summary>
        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<HuobiSubAuthError> Errors { get; set; } = default!;

        /// <summary>
        /// 开通合约成功的子账户uid列表
        /// </summary>
        [JsonProperty("successes", NullValueHandling = NullValueHandling.Ignore)]

        public string Successes { get; set; } = string.Empty;
    }    
}