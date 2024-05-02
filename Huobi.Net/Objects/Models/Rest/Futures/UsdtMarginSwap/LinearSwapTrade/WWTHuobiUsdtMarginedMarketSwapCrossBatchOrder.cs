namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapTrade
{
    /// <summary>
    /// 【逐仓】合约批量下单
    /// </summary>
    public class WWTHuobiUsdtMarginedMarketSwapCrossBatchOrder
    {
        /// <summary>
        /// 失败订单信息列表
        /// </summary>
        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<CrossBatchOrderErrors> ErrorsList { get; set; } = Array.Empty<CrossBatchOrderErrors>();

        /// <summary>
        /// 成功订单信息列表
        /// </summary>
        [JsonProperty("success", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<CrossBatchOrderSuccess> SuccessList { get; set; } = Array.Empty<CrossBatchOrderSuccess>();
    }

    /// <summary>
    /// 全仓失败订单信息
    /// </summary>
    public class CrossBatchOrderErrors
    {
        /// <summary>
        /// 订单索引
        /// </summary>
        [JsonProperty("index", NullValueHandling = NullValueHandling.Ignore)]
        public int? Index { get; set; } = default;

        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("err_code", NullValueHandling = NullValueHandling.Ignore)]
        public int? ErrCode { get; set; } = default;

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("err_msg", NullValueHandling = NullValueHandling.Ignore)]
        public string? ErrMsg { get; set; } = string.Empty;
    }

    /// <summary>
    /// 全仓成功订单信息
    /// </summary>
    public class CrossBatchOrderSuccess
    {
        /// <summary>
        /// 订单索引
        /// </summary>
        [JsonProperty("index", NullValueHandling = NullValueHandling.Ignore)]
        public int? Index { get; set; } = default;

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