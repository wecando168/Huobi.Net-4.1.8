using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount
{
    /// <summary>
    /// 【通用】获取用户的API指标禁用信息
    /// </summary>
    public class HuobiUsdtMarginedApiTradingStatus
    {
        /// <summary>
        /// 是否被禁用	1：被禁用中，0：没有被禁用
        /// </summary>
        [JsonProperty("is_disable", NullValueHandling = NullValueHandling.Ignore)]
        public int IsDisable { get; set; } = default;

        /// <summary>
        /// 触发禁用的订单价格类型，多个订单价格类型以英文逗号分割，例如：“limit,post_only,FOK,IOC”	
        /// </summary>
        [JsonProperty("order_price_types", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderPriceTypes { get; set; } = string.Empty;

        /// <summary>
        /// 触发禁用的原因，表示当前的禁用是由哪个指标触发	"COR":撤单率（Cancel Order Ratio），“TDN”：总禁用次数（Total Disable Number）
        /// </summary>
        [JsonProperty("disable_reason", NullValueHandling = NullValueHandling.Ignore)]
        public string DisableReason { get; set; } = string.Empty;

        /// <summary>
        /// 禁用时间间隔，单位：毫秒
        /// </summary>
        [JsonProperty("disable_interval", NullValueHandling = NullValueHandling.Ignore)]
        public long DisableInterval { get; set; } = default(long);

        /// <summary>
        /// 计划恢复时间，单位：毫秒
        /// </summary>
        [JsonProperty("recovery_time", NullValueHandling = NullValueHandling.Ignore)]
        public long RecoveryTime { get; set; } = default(long);

        /// <summary>
        /// 撤单率（Cancel Order Ratio）
        /// </summary>
        [JsonProperty("COR", NullValueHandling = NullValueHandling.Ignore)]
        public CancelOrderRatio cancelOrderRatio { get; set; }

        /// <summary>
        /// 总禁用次数（Total Disable Number）
        /// </summary>
        [JsonProperty("TDN", NullValueHandling = NullValueHandling.Ignore)]
        public TotalDisableNumber totalDisableNumber { get; set; } = default!;
    }

    /// <summary>
    /// 表示撤单率的指标（Cancel Order Ratio）
    /// </summary>
    public class CancelOrderRatio
    {
        /// <summary>
        /// 委托单笔数的阈值
        /// </summary>
        [JsonProperty("orders_threshold", NullValueHandling = NullValueHandling.Ignore)]
        public long OrdersThreshold { get; set; } = default(long);

        /// <summary>
        /// 用户委托单笔数的实际值
        /// </summary>
        [JsonProperty("orders", NullValueHandling = NullValueHandling.Ignore)]
        public long Orders { get; set; } = default(long);

        /// <summary>
        /// 用户委托单中的无效撤单笔数
        /// </summary>
        [JsonProperty("invalid_cancel_orders", NullValueHandling = NullValueHandling.Ignore)]
        public long InvalidCancelOrders { get; set; } = default(long);

        /// <summary>
        /// 撤单率的阈值
        /// </summary>
        [JsonProperty("cancel_ratio_threshold", NullValueHandling = NullValueHandling.Ignore)]
        public decimal CancelRatioThreshold { get; set; } = default;

        /// <summary>
        /// 用户撤单率的实际值
        /// </summary>
        [JsonProperty("cancel_ratio", NullValueHandling = NullValueHandling.Ignore)]
        public long CancelRatio { get; set; } = default(long);

        /// <summary>
        /// 	用户是否触发该指标	1：已经触发，0：没有触发
        /// </summary>
        [JsonProperty("is_trigger", NullValueHandling = NullValueHandling.Ignore)]
        public int IsTrigger { get; set; } = default;

        /// <summary>
        /// 该指标是否开启
        /// </summary>
        [JsonProperty("is_active", NullValueHandling = NullValueHandling.Ignore)]
        public int IsActive { get; set; } = default;
    }

    /// <summary>
    /// 表示总禁用次数的指标（Total Disable Number）
    /// </summary>
    public class TotalDisableNumber
    {
        /// <summary>
        /// 总禁用次数的阈值
        /// </summary>
        [JsonProperty("disables_threshold", NullValueHandling = NullValueHandling.Ignore)]
        public long DisablesThreshold { get; set; } = default(long);

        /// <summary>
        /// 总禁用次数的实际值
        /// </summary>
        [JsonProperty("disables", NullValueHandling = NullValueHandling.Ignore)]
        public long Disables { get; set; } = default(long);

        /// <summary>
        /// 用户是否触发该指标	1：已经触发，0：没有触发
        /// </summary>
        [JsonProperty("is_trigger", NullValueHandling = NullValueHandling.Ignore)]
        public int IsTrigger { get; set; } = default;

        /// <summary>
        /// 该指标是否开启
        /// </summary>
        [JsonProperty("is_active", NullValueHandling = NullValueHandling.Ignore)]
        public int IsActive { get; set; } = default;
    }

}