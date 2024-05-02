namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapTransferring
{
    /// <summary>
    /// 【全仓】查询系统划转权限
    /// </summary>
    public class WWTHuobiUsdtMarginedSwapCrossTransferState
    {
        /// <summary>
        /// 保证金模式	cross：全仓模式；
        /// </summary>
        [JsonProperty("margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金账户	比如“BTC-USDT”
        /// </summary>
        [JsonProperty("margin_account", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginAccount { get; set; } = string.Empty;

        /// <summary>
        /// 从币币转入的权限："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("transfer_in", NullValueHandling = NullValueHandling.Ignore)]
        public int TransferIn { get; set; } = default;

        /// <summary>
        /// 转出至币币的权限："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("transfer_out", NullValueHandling = NullValueHandling.Ignore)]
        public int TransferOut { get; set; } = default;

        /// <summary>
        /// 从母账号划转到子账号的权限："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("master_transfer_sub", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MasterTransferSub { get; set; } = default;

        /// <summary>
        /// 从子账号划转到母账号的权限："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("sub_transfer_master", NullValueHandling = NullValueHandling.Ignore)]
        public decimal SubTransferMaster { get; set; } = default;

        /// <summary>
        /// 母账号划转到子账号的转入权限-跨账户："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("master_transfer_sub_inner_in", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MasterTransferSubInnerIn { get; set; } = default;

        /// <summary>
        /// 母账号划转到子账号的转出权限-跨账户："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("master_transfer_sub_inner_out", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MasterTransferSubInnerOut { get; set; } = default;

        /// <summary>
        /// 子账号划转到母账号的转出权限-跨账户："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("sub_transfer_master_inner_in", NullValueHandling = NullValueHandling.Ignore)]
        public decimal SubTransferMasterInnerOut { get; set; } = default;

        /// <summary>
        /// 同账号不同保证金账户划转的转入权限："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("transfer_inner_in	", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TransferInnerIn { get; set; } = default;

        /// <summary>
        /// 同账号不同保证金账户划转的转出权限："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("transfer_inner_out", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TransferInnerOut { get; set; } = default;
    }
}