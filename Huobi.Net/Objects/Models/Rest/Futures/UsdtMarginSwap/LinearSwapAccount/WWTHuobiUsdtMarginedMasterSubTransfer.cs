namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount
{
    /// <summary>
    /// 【通用】母子账户划转(PrivateData)
    /// </summary>
    public class WWTHuobiUsdtMarginedMasterSubTransfer
    {
        /// <summary>
        /// 划转订单ID
        /// </summary>
        [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderId { get; set; } = string.Empty;
    }
}