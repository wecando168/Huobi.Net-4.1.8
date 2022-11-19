﻿using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapAccount.CommonBaseModels;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapAccount
{
    /// <summary>
    /// 【全仓】查询用户当前的划转限制(PrivateData)
    /// </summary>
    public class HuobiUsdtMarginedCrossTransferLimit
    {        
        /// <summary>
        /// 保证金模式	cross：全仓模式；
        /// </summary>
        [JsonProperty("margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金账户	比如“BTC-USDT”
        /// </summary>
        [JsonProperty("margin_account", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginAccount { get; set; } = string.Empty;

        /// <summary>
        /// 单笔最大转入量
        /// </summary>
        [JsonProperty("transfer_in_max_each", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TransferInMaxEach { get; set; } = default(decimal);

        /// <summary>
        /// 单笔最小转入量
        /// </summary>
        [JsonProperty("transfer_in_min_each", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TransferInMinEach { get; set; } = default(decimal);

        /// <summary>
        /// 单笔最大转出量
        /// </summary>
        [JsonProperty("transfer_out_max_each", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TransferOutMaxEach { get; set; } = default(decimal);

        /// <summary>
        /// 单笔最小转出量
        /// </summary>
        [JsonProperty("transfer_out_min_each", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TransferOutMinEach{ get; set; } = default(decimal);

        /// <summary>
        /// 单日累计最大转入量
        /// </summary>
        [JsonProperty("transfer_in_max_daily", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TransferInMaxDaily { get; set; } = default(decimal);

        /// <summary>
        /// 单日累计最大转出量
        /// </summary>
        [JsonProperty("transfer_out_max_daily", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TransferOutMaxDaily { get; set; } = default(decimal);

        /// <summary>
        /// 单日累计最大净转入量
        /// </summary>
        [JsonProperty("net_transfer_in_max_daily", NullValueHandling = NullValueHandling.Ignore)]
        public decimal NetTransferInMaxDaily { get; set; } = default(decimal);

        /// <summary>
        /// 单日累计最大净转出量
        /// </summary>
        [JsonProperty("net_transfer_out_max_daily", NullValueHandling = NullValueHandling.Ignore)]
        public decimal NetTransferOutMaxDaily { get; set; } = default(decimal);

        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;
    }    
}