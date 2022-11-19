using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapAccount
{
    /// <summary>
    /// 【逐仓】查询用户可用杠杆倍数(PrivateData)
    /// </summary>
    public class HuobiUsdtMarginedAvailableLevelRate
    {
        /// <summary>
        /// 合约代码	"BTC-USDT" ...
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金模式	isolated：逐仓模式
        /// </summary>
        [JsonProperty("margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginMode { get; set; } = string.Empty;


        /// <summary>
        /// 实际可用杠杆倍数，多个以英文逗号隔开	比如："1,5,10"
        /// </summary>
        [JsonProperty("available_level_rate", NullValueHandling = NullValueHandling.Ignore)]
        public string AvailableLevelRate { get; set; } = string.Empty;
    }
}