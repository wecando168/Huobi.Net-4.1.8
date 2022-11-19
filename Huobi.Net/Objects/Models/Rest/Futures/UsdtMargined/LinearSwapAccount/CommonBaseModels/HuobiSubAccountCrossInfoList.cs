﻿using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapAccount.CommonBaseModels
{
    /// <summary>
    /// 子账户合约全仓资产信息列表
    /// </summary>
    public class HuobiSubAccountCrossInfoList
    {
        /// <summary>
        /// 子账户UID
        /// </summary>
        [JsonProperty("sub_uid", NullValueHandling = NullValueHandling.Ignore)]
        public long SubUid { get; set; } = default(long);

        /// <summary>
        /// 子账户资产列表
        /// </summary>
        [JsonProperty("account_info_list", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<HuobiSubAccountCrossInfo> AccountInfoList { get; set; } = Array.Empty<HuobiSubAccountCrossInfo>();
    }
}