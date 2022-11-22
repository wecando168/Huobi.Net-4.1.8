using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapTrade.Request
{
    /// <summary>
    /// 【逐仓】切换持仓模式
    /// </summary>
    public class HuobiUsdtMarginedMarketSwapSwitchPositionMode
    {
        /// <summary>
        /// 保证金账户	比如： "BTC-USDT"，"ETH-USDT" ...
        /// </summary>
        [JsonProperty("margin_account", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginAccount { get; set; } = string.Empty;

        /// <summary>
        /// 持仓模式	single_side：单向持仓；dual_side：双向持仓
        /// </summary>
        [JsonProperty("position_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string PositionMode { get; set; } = string.Empty;
    }
}