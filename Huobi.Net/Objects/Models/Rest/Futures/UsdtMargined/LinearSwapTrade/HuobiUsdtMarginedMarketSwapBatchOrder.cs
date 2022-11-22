using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapTrade.Request
{
    /// <summary>
    /// 【逐仓】合约批量下单
    /// </summary>
    public class HuobiUsdtMarginedMarketSwapBatchOrder
    {
        /// <summary>
        /// 失败订单信息列表
        /// </summary>
        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<IsolatedBatchOrderErrors> ErrorsList { get; set; } = Array.Empty<IsolatedBatchOrderErrors>();

        /// <summary>
        /// 成功订单信息列表
        /// </summary>
        [JsonProperty("success", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<IsolatedBatchOrderSuccess> SuccessList { get; set; } = Array.Empty<IsolatedBatchOrderSuccess>();
    }

    /// <summary>
    /// 逐仓失败订单信息
    /// </summary>
    public class IsolatedBatchOrderErrors
    {
        /// <summary>
        /// 订单索引
        /// </summary>
        [JsonProperty("index", NullValueHandling = NullValueHandling.Ignore)]
        public int? Index { get; set; } = default(int);

        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("err_code", NullValueHandling = NullValueHandling.Ignore)]
        public int? ErrCode { get; set; } = default(int);

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("err_msg", NullValueHandling = NullValueHandling.Ignore)]
        public string? ErrMsg { get; set; } = string.Empty;
    }

    /// <summary>
    /// 逐仓成功订单信息
    /// </summary>
    public class IsolatedBatchOrderSuccess
    {
        /// <summary>
        /// 订单索引
        /// </summary>
        [JsonProperty("index", NullValueHandling = NullValueHandling.Ignore)]
        public int? Index { get; set; } = default(int);

        /// <summary>
        /// 订单ID
        /// </summary>
        [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderId { get; set; } = default(long);

        /// <summary>
        /// 用户下单时填写的客户端订单ID，没填则不返回	
        /// </summary>
        [JsonProperty("client_order_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ClientOrderId { get; set; } = default(long);

        /// <summary>
        /// String类型订单ID
        /// </summary>
        [JsonProperty("order_id_str", NullValueHandling = NullValueHandling.Ignore)]
        public string? OrderIdStr { get; set; } = string.Empty;
    }
}