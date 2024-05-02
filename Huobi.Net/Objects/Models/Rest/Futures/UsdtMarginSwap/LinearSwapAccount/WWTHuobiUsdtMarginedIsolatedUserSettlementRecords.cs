using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount
{
    /// <summary>
    /// 【逐仓】查询用户结算记录(PrivateData)
    /// </summary>
    public class WWTHuobiUsdtMarginedIsolatedUserSettlementRecords
    {
        /// <summary>
        /// 总页数
        /// </summary>
        [JsonProperty("total_page", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalPage { get; set; } = default;

        /// <summary>
        /// 当前页
        /// </summary>
        [JsonProperty("current_page", NullValueHandling = NullValueHandling.Ignore)]
        public int CurrentPage { get; set; } = default;

        /// <summary>
        /// 总条数
        /// </summary>
        [JsonProperty("total_size", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalSize { get; set; } = default;

        /// <summary>
        /// 结算记录列表
        /// </summary>
        [JsonProperty("settlement_records", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<IsolatedSettlementRecord> SettlementRecords { get; set; } = Array.Empty<IsolatedSettlementRecord>();
    }

    /// <summary>
    /// 结算记录
    /// </summary>
    public class IsolatedSettlementRecord
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
        /// 保证金账户	比如“BTC-USDT”
        /// </summary>
        [JsonProperty("margin_account", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginAccount { get; set; } = string.Empty;

        /// <summary>
        /// 本期初始账户权益
        /// </summary>
        [JsonProperty("margin_balance_init", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MarginBalanceInit { get; set; } = default;

        /// <summary>
        /// 本期结算后账户权益
        /// </summary>
        [JsonProperty("margin_balance", NullValueHandling = NullValueHandling.Ignore)]
        public decimal MarginBalance { get; set; } = default;

        /// <summary>
        /// 本期结算已实现盈亏
        /// </summary>
        [JsonProperty("settlement_profit_real", NullValueHandling = NullValueHandling.Ignore)]
        public decimal SettlementProfitReal { get; set; } = default;

        /// <summary>
        /// 本期结算时间，交割时为交割时间	
        /// </summary>
        [JsonProperty("settlement_time", NullValueHandling = NullValueHandling.Ignore)]
        public long SettlementTime { get; set; } = default(long);

        /// <summary>
        /// 本期分摊费用
        /// </summary>
        [JsonProperty("clawback", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Clawback { get; set; } = default;

        /// <summary>
        /// 本期资金费（或本期交割费）	
        /// </summary>
        [JsonProperty("funding_fee", NullValueHandling = NullValueHandling.Ignore)]
        public decimal FundingFee { get; set; } = default;

        /// <summary>
        /// 本期平仓盈亏
        /// </summary>
        [JsonProperty("offset_profitloss", NullValueHandling = NullValueHandling.Ignore)]
        public decimal OffsetProfitloss { get; set; } = default;

        /// <summary>
        /// 本期交易手续费	
        /// </summary>
        [JsonProperty("fee", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Fee { get; set; } = default;

        /// <summary>
        /// 手续费币种
        /// </summary>
        [JsonProperty("fee_asset", NullValueHandling = NullValueHandling.Ignore)]
        public string FeeAsset { get; set; } = string.Empty;

        /// <summary>
        /// 结算记录持仓列表
        /// </summary>
        [JsonProperty("positions", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<SettlementRecordPosition> Positions { get; set; } = Array.Empty<SettlementRecordPosition>();
    }

    /// <summary>
    /// 结算记录持仓
    /// </summary>
    public class SettlementRecordPosition
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
        /// 	仓位方向	"buy":买 "sell":卖
        /// </summary>
        [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
        public string Direction { get; set; } = string.Empty;

        /// <summary>
        /// 本期结算前持仓量（张）	
        /// </summary>
        [JsonProperty("volume", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Volume { get; set; } = default;

        /// <summary>
        /// 开仓均价
        /// </summary>
        [JsonProperty("cost_open", NullValueHandling = NullValueHandling.Ignore)]
        public decimal CostOpen { get; set; } = default;

        /// <summary>
        /// 本期结算前持仓均价
        /// </summary>
        [JsonProperty("cost_hold_pre", NullValueHandling = NullValueHandling.Ignore)]
        public decimal CostHoldPre { get; set; } = default;

        /// <summary>
        /// 本期结算后持仓均价
        /// </summary>
        [JsonProperty("cost_hold", NullValueHandling = NullValueHandling.Ignore)]
        public decimal CostHold { get; set; } = default;

        /// <summary>
        /// 本期结算未实现盈亏
        /// </summary>
        [JsonProperty("settlement_profit_unreal", NullValueHandling = NullValueHandling.Ignore)]
        public decimal SettlementProfitUnreal { get; set; } = default;

        /// <summary>
        /// 本期结算价格，交割时为交割价格	
        /// </summary>
        [JsonProperty("settlement_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal SettlementPrice { get; set; } = default;

        /// <summary>
        /// 结算类型	settlement：结算；delivery：交割；
        /// </summary>
        [JsonProperty("settlement_type", NullValueHandling = NullValueHandling.Ignore)]
        public string SettlementType { get; set; } = string.Empty;
    }

}