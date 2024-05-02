using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount
{
    /// <summary>
    /// 【逐仓】获取用户的合约账户信息(PrivateData)
    /// </summary>
    public class WWTHuobiUsdtMarginedIsolatedAccountInfo
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
        /// 持仓保证金（当前持有仓位所占用的保证金）
        /// </summary>
        [JsonProperty("margin_position", NullValueHandling = NullValueHandling.Ignore)]

        public decimal MarginPosition { get; set; } = default;

        /// <summary>
        /// 冻结保证金
        /// </summary>
        [JsonProperty("margin_frozen", NullValueHandling = NullValueHandling.Ignore)]

        public decimal MarginFrozen { get; set; } = default;

        /// <summary>
        /// 可用保证金
        /// </summary>
        [JsonProperty("margin_available", NullValueHandling = NullValueHandling.Ignore)]

        public decimal MarginAvailable { get; set; } = default;

        /// <summary>
        /// 已实现盈亏
        /// </summary>
        [JsonProperty("profit_real", NullValueHandling = NullValueHandling.Ignore)]

        public decimal ProfitReal { get; set; } = default;

        /// <summary>
        /// 未实现盈亏
        /// </summary>
        [JsonProperty("profit_unreal", NullValueHandling = NullValueHandling.Ignore)]

        public decimal ProfitUnreal { get; set; } = default;

        /// <summary>
        /// 保证金率
        /// </summary>
        [JsonProperty("risk_rate", NullValueHandling = NullValueHandling.Ignore)]

        public decimal RiskRate { get; set; } = default;

        /// <summary>
        ///可划转数量
        /// </summary>
        [JsonProperty("withdraw_available", NullValueHandling = NullValueHandling.Ignore)]

        public decimal WithdrawAvailable { get; set; } = default;

        /// <summary>
        /// 预估强平价
        /// </summary>
        [JsonProperty("liquidation_price", NullValueHandling = NullValueHandling.Ignore)]

        public decimal LiquidationPrice { get; set; } = default;

        /// <summary>
        /// 杠杠倍数
        /// </summary>
        [JsonProperty("lever_rate", NullValueHandling = NullValueHandling.Ignore)]

        public decimal LeverRate { get; set; } = default;

        /// <summary>
        /// 调整系数
        /// </summary>
        [JsonProperty("adjust_factor", NullValueHandling = NullValueHandling.Ignore)]

        public decimal AdjustFactor { get; set; } = default;

        /// <summary>
        /// 静态权益
        /// </summary>
        [JsonProperty("margin_static", NullValueHandling = NullValueHandling.Ignore)]

        public decimal MarginStatic { get; set; } = default;

        /// <summary>
        /// 合约代码	"BTC-USDT"... ,如果缺省，默认返回所有合约
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

        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]

        public string TradePartition { get; set; } = string.Empty;

        /// <summary>
        /// 持仓模式	single_side：单向持仓；dual_side：双向持仓
        /// </summary>
        [JsonProperty("position_mode", NullValueHandling = NullValueHandling.Ignore)]

        public string PositionMode { get; set; } = string.Empty;

        /// <summary>
        /// 持仓列表
        /// </summary>
        [JsonProperty("positionList", NullValueHandling = NullValueHandling.Ignore)]

        public object PositionList { get; set; } = default!;
    }
}