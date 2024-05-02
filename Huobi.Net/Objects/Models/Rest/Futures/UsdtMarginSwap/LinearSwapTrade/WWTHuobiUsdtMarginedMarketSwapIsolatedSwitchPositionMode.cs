namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapTrade
{
    /// <summary>
    /// 【逐仓】切换持仓模式
    /// </summary>
    public class WWTHuobiUsdtMarginedMarketSwapIsolatedSwitchPositionMode
    {
        /// <summary>
        /// 保证金账户	比如： "BTC-USDT"，"ETH-USDT" ...
        /// </summary>
        [JsonProperty("margin_account", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginAccount { get; set; } = string.Empty;

        /// <summary>
        /// 持仓模式	single_side：单向持仓；dual_side：双向持仓
        /// </summary>
        [JsonProperty("position_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string PositionMode { get; set; } = string.Empty;
    }
}