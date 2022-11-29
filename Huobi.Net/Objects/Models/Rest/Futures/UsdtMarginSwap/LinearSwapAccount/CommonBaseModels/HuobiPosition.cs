using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount.CommonBaseModels
{
    /// <summary>
    /// 持仓信息
    /// </summary>
    public class HuobiPosition
    {
        /// <summary>
        /// 品种代码	"BTC","ETH"...
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// 	合约代码	"BTC-USDT" ...
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 持仓量（张）
        /// </summary>
        [JsonProperty("volume", NullValueHandling = NullValueHandling.Ignore)]

        public decimal Volume { get; set; } = default;

        /// <summary>
        /// 可平仓数量（张）
        /// </summary>
        [JsonProperty("available", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Available { get; set; } = default;

        /// <summary>
        /// 冻结数量（张）
        /// </summary>
        [JsonProperty("frozen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Frozen { get; set; } = default;

        /// <summary>
        /// 开仓均价
        /// </summary>
        [JsonProperty("cost_open", NullValueHandling = NullValueHandling.Ignore)]
        public decimal CostOpen { get; set; } = default;

        /// <summary>
        /// 持仓均价
        /// </summary>
        [JsonProperty("cost_hold", NullValueHandling = NullValueHandling.Ignore)]
        public decimal CostHold { get; set; } = default;

        /// <summary>
        /// 未实现盈亏
        /// </summary>
        [JsonProperty("profit_unreal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ProfitUnreal { get; set; } = default;

        /// <summary>
        /// 收益率
        /// </summary>
        [JsonProperty("profit_rate", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ProfitRate { get; set; } = default;

        /// <summary>
        /// 杠杠倍数
        /// </summary>
        [JsonProperty("lever_rate", NullValueHandling = NullValueHandling.Ignore)]
        public int LeverRate { get; set; } = default;

        /// <summary>
        /// 持仓保证金
        /// </summary>
        [JsonProperty("position_margin", NullValueHandling = NullValueHandling.Ignore)]
        public decimal PositionMargin { get; set; } = default;

        /// <summary>
        /// 仓位方向	"buy":买，即多仓 "sell":卖，即空仓
        /// </summary>
        [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
        public string Direction { get; set; } = string.Empty;

        /// <summary>
        /// 收益
        /// </summary>
        [JsonProperty("profit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Profit { get; set; } = default;

        /// <summary>
        /// 最新价
        /// </summary>
        [JsonProperty("last_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal LastPrice { get; set; } = default;

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

        /// <summary>
        /// 持仓模式	single_side：单向持仓；dual_side：双向持仓
        /// </summary>
        [JsonProperty("position_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string PositionMode { get; set; } = string.Empty;
    }
}