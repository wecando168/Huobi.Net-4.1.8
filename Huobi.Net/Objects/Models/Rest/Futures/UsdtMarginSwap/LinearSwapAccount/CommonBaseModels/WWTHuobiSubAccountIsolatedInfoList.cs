namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount.CommonBaseModels
{
    /// <summary>
    /// 子账户合约逐仓资产信息列表
    /// </summary>
    public class WWTHuobiSubAccountIsolatedInfoList
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
        public IEnumerable<WWTHuobiSubAccountIsolatedInfo> AccountInfoList { get; set; } = Array.Empty<WWTHuobiSubAccountIsolatedInfo>();
    }
}