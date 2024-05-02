using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount.CommonBaseModels;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount
{
    /// <summary>
    /// 【全仓】批量获取子账户资产信息
    /// </summary>
    public class WWTHuobiUsdtMarginedCrossSubAccountList
    {
        /// <summary>
        /// 子账户UID
        /// </summary>
        [JsonProperty("sub_uid", NullValueHandling = NullValueHandling.Ignore)]
        public string SubUid { get; set; } = string.Empty;

        /// <summary>
        /// 账户权益
        /// </summary>
        [JsonProperty("account_info_list", NullValueHandling = NullValueHandling.Ignore)]

        public IEnumerable<WWTHuobiSubAccountCrossInfo> List { get; set; } = Array.Empty<WWTHuobiSubAccountCrossInfo>();
    }
}