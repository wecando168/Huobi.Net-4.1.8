namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount.CommonBaseModels
{
    /// <summary>
    /// 子账户合约全仓资产信息列表
    /// </summary>
    public class WWTHuobiSubAccountCrossInfoList
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
        public IEnumerable<WWTHuobiSubAccountCrossInfo> AccountInfoList { get; set; } = Array.Empty<WWTHuobiSubAccountCrossInfo>();
    }
}