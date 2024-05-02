namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount.CommonBaseModels
{
    /// <summary>
    /// 子账户合约逐仓资产信息
    /// </summary>
    public class WWTHuobiSubAccountIsolatedInfo
    {
        /// <summary>
        /// 品种代码	"BTC","ETH"...
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// 账户权益
        /// </summary>
        [JsonProperty("margin_balance", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MarginBalance { get; set; } = default;

        /// <summary>
        /// 预估强平价
        /// </summary>
        [JsonProperty("liquidation_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal LiquidationPrice { get; set; } = default;

        /// <summary>
        /// 保证金率
        /// </summary>
        [JsonProperty("risk_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal RiskRate { get; set; }

        /// <summary>
        /// 合约代码	"BTC-USDT" ...
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金币种（计价币种）
        /// </summary>
        [JsonProperty("margin_asset", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginAsset { get; set; } = string.Empty;

        /// <summary>
        /// 保证金模式	isolated：逐仓模式
        /// </summary>
        [JsonProperty("margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金账户	比如“BTC-USDT”
        /// </summary>
        [JsonProperty("margin_account", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginAccount { get; set; } = string.Empty;
    }
}