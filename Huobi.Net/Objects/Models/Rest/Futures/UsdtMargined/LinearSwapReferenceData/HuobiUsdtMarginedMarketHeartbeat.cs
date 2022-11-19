using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapReferenceData
{
    /// <summary>
    /// 合约系统查询是否可用
    /// </summary>
    public class HuobiUsdtMarginedMarketHeartbeat
    {
        /// <summary>
        /// 交割合约 1: 可用 0: 不可用(即停服维护)
        /// </summary>
        [JsonProperty("heartbeat", NullValueHandling = NullValueHandling.Ignore)]
        public int Heartbeat { get; set; }

        /// <summary>
        /// null: 正常. 交割合约预计恢复时间， 单位:毫秒
        /// </summary>
        [JsonProperty("estimated_recovery_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? EstimatedRecoveryTime { get; set; }

        /// <summary>
        /// 币本位永续 1: 可用 0: 不可用(即停服维护)
        /// </summary>
        [JsonProperty("swap_heartbeat", NullValueHandling = NullValueHandling.Ignore)]
        public int SwapHeartbeat { get; set; }

        /// <summary>
        /// null: 正常. 币本位永续合约预计恢复时间，单位：毫秒.
        /// </summary>
        [JsonProperty("swap_estimated_recovery_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? SwapEstimatedRecoveryTime { get; set; }

        /// <summary>
        /// U本位合约 1: 可用 0: 不可用(即停服维护)
        /// </summary>
        [JsonProperty("linear_swap_heartbeat", NullValueHandling = NullValueHandling.Ignore)]
        public int LinearSwapHeartbeat { get; set; }

        /// <summary>
        /// null: 正常. U本位合约预计恢复时间，单位：毫秒.
        /// </summary>
        [JsonProperty("linear_swap_estimated_recovery_time", NullValueHandling = NullValueHandling.Ignore)]
        public long? LinearSwapEstimatedRecoveryTime { get; set; }
    }

}
