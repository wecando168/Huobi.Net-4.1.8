using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount.CommonBaseModels;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Socket.Futures.UsdtMargined.WebSocketOrderAndAccounts
{
    /// <summary>
    /// 【全仓】WebSocket资产变动信息(PrivateData)
    /// </summary>
    public class WWTHuobiUsdtMarginedAccountSebscribeCrossPositionInfo
    {
        /// <summary>
        /// 保证金模式	cross：全仓模式；
        /// </summary>
        [JsonProperty("margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金账户	比如“USDT”
        /// </summary>
        [JsonProperty("margin_account", NullValueHandling = NullValueHandling.Ignore)]

        public string MarginAccount { get; set; } = string.Empty;

        /// <summary>
        /// 保证金币种（计价币种）
        /// </summary>
        [JsonProperty("margin_asset", NullValueHandling = NullValueHandling.Ignore)]

        public string MarginAsset { get; set; } = string.Empty;

        /// <summary>
        /// 账户权益
        /// </summary>
        [JsonProperty("margin_balance", NullValueHandling = NullValueHandling.Ignore)]

        public decimal MarginBalance { get; set; } = default;

        /// <summary>
        /// 静态权益
        /// </summary>
        [JsonProperty("margin_static", NullValueHandling = NullValueHandling.Ignore)]

        public decimal MarginStatic { get; set; } = default;

        /// <summary>
        /// 持仓保证金（所有全仓仓位汇总）
        /// </summary>
        [JsonProperty("margin_position", NullValueHandling = NullValueHandling.Ignore)]

        public decimal MarginPosition { get; set; } = default;

        /// <summary>
        /// 冻结保证金
        /// </summary>
        [JsonProperty("margin_frozen", NullValueHandling = NullValueHandling.Ignore)]

        public decimal MarginFrozen { get; set; } = default;

        /// <summary>
        /// 已实现盈亏
        /// </summary>
        [JsonProperty("profit_real", NullValueHandling = NullValueHandling.Ignore)]

        public decimal ProfitReal { get; set; } = default;

        /// <summary>
        /// 未实现盈亏（所有全仓仓位汇总）
        /// </summary>
        [JsonProperty("profit_unreal", NullValueHandling = NullValueHandling.Ignore)]

        public decimal ProfitUnreal { get; set; } = default;

        /// <summary>
        /// 可划转数量
        /// </summary>
        [JsonProperty("withdraw_available", NullValueHandling = NullValueHandling.Ignore)]

        public decimal WithdrawAvailable { get; set; } = default;

        /// <summary>
        /// 保证金率
        /// </summary>
        [JsonProperty("risk_rate", NullValueHandling = NullValueHandling.Ignore)]

        public decimal RiskRate { get; set; } = default;

        /// <summary>
        /// 持仓模式	single_side：单向持仓；dual_side：双向持仓
        /// </summary>
        [JsonProperty("position_mode", NullValueHandling = NullValueHandling.Ignore)]

        public string PositionMode { get; set; } = string.Empty;

        /// <summary>
        /// 支持交割的所有合约的相关字段
        /// </summary>
        [JsonProperty("futures_contract_detail", NullValueHandling = NullValueHandling.Ignore)]

        public IEnumerable<WWTHuobiFuturesContractDetail> FuturesContractDetails { get; set; } = default!;

        /// <summary>
        /// 支持永续的所有合约的相关字段
        /// </summary>
        [JsonProperty("contract_detail", NullValueHandling = NullValueHandling.Ignore)]

        public IEnumerable<WWTHuobiContractDetail> ContractDetails { get; set; } = default!;
    }
}