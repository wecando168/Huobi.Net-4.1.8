using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapAccount.CommonBaseModels;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapAccount
{
    /// <summary>
    /// 【逐仓】批量获取子账户资产信息
    /// </summary>
    public class HuobiUsdtMarginedSubAccountInfoList
    {
        /// <summary>
        /// 总页数
        /// </summary>
        [JsonProperty("total_page", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalPage { get; set; } = default(int);

        /// <summary>
        /// 当前页
        /// </summary>
        [JsonProperty("current_page", NullValueHandling = NullValueHandling.Ignore)]
        public int CurrentPage { get; set; } = default(int);

        /// <summary>
        /// 总条数
        /// </summary>
        [JsonProperty("total_size", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalSize { get; set; } = default(int);

        /// <summary>
        /// 子账户列表
        /// </summary>
        [JsonProperty("sub_list", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<HuobiSubAccountIsolatedInfoList> SubAccountIsolatedInfoLists { get; set; } = Array.Empty<HuobiSubAccountIsolatedInfoList>();
    }
}