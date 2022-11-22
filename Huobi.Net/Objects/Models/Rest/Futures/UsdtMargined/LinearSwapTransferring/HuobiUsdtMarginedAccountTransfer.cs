using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapTransferring
{
    /// <summary>
    /// 【通用】现货-U本位合约账户间进行资金划转
    /// </summary>
    public class HuobiUsdtMarginedAccountTransfer
    {
        /// <summary>
        /// 状态	true/false
        /// </summary>
        [JsonProperty("success", NullValueHandling = NullValueHandling.Ignore)]
        public string success { get; set; } = string.Empty;

        /// <summary>
        /// 生成的划转订单id	
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public long? Data { get; set; } = null;

        /// <summary>
        /// 响应码
        /// </summary>
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public long Code { get; set; } = default(long);

        /// <summary>
        /// 响应信息
        /// </summary>
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; } = string.Empty;
    }
}