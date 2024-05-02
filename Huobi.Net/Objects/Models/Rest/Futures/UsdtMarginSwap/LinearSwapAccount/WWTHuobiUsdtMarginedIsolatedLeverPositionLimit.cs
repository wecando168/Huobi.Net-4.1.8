namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount
{
    /// <summary>
    /// 【逐仓】查询用户所有杠杆持仓量限制(PrivateData)
    /// </summary>
    public class WWTHuobiUsdtMarginedIsolatedLeverPositionLimit
    {
        /// <summary>
        /// 品种代码	"BTC","ETH"...
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// 合约代码	"BTC-USDT" ...
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金模式	isolated：逐仓模式
        /// </summary>
        [JsonProperty("margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// 杠杆持仓量限制列表
        /// </summary>
        [JsonProperty("list", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<LeverPositionLimitDetail> LeverPositionLimitDetailList { get; set; } = Array.Empty<LeverPositionLimitDetail>();
    }

    /// <summary>
    /// 杠杆倍数限制明细
    /// </summary>
    public class LeverPositionLimitDetail
    {
        /// <summary>
        /// 品种杠杆倍数
        /// </summary>
        [JsonProperty("lever_rate", NullValueHandling = NullValueHandling.Ignore)]
        public int LeverRate { get; set; } = default;

        /// <summary>
        /// 合约多仓持仓价值上限，单位USDT
        /// </summary>
        [JsonProperty("buy_limit_value", NullValueHandling = NullValueHandling.Ignore)]
        public decimal BuyLimitValue { get; set; } = default;

        /// <summary>
        /// 合约空仓持仓价值上限，单位USDT
        /// </summary>
        [JsonProperty("sell_limit_value", NullValueHandling = NullValueHandling.Ignore)]
        public decimal SellLimitValue { get; set; } = default;
    }
}