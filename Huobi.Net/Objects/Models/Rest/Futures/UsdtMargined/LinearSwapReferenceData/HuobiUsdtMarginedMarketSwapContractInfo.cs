using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapReferenceData
{
    /// <summary>
    /// 【通用】获取合约信息
    /// </summary>
    public class HuobiUsdtMarginedMarketSwapContractInfo
    {
        /// <summary>
        /// 账户类型	1:非统一账户（全仓逐仓账户）
        /// </summary>
        [JsonProperty("account_type", NullValueHandling = NullValueHandling.Ignore)]
        public int AccountType { get; set; } = default;

        /// <summary>
        /// 品种代码
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// 合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 合约面值，即1张合约对应多少标的币种（如BTC-USDT合约则面值单位就是BTC）	0.1，0.01...
        /// </summary>
        [JsonProperty("contract_size", NullValueHandling = NullValueHandling.Ignore)]
        public decimal ContractSize { get; set; } = default;

        /// <summary>
        /// 合约价格最小变动精度	0.001, 0.01...
        /// </summary>
        [JsonProperty("price_tick", NullValueHandling = NullValueHandling.Ignore)]
        public decimal PriceTick { get; set; } = default;

        /// <summary>
        /// 合约交割日期,永续无需交割时该字段为空字符串	如"20180720"
        /// </summary>
        [JsonProperty("delivery_date", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryDate { get; set; } = string.Empty;

        /// <summary>
        /// 交割时间（合约无需交割时，该字段值为空字符串），单位：毫秒	
        /// </summary>
        [JsonProperty("delivery_time", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryTime { get; set; } = string.Empty;

        /// <summary>
        /// 合约上市日期	如"20180706"
        /// </summary>
        [JsonProperty("create_date", NullValueHandling = NullValueHandling.Ignore)]
        public string CreateDate { get; set; } = string.Empty;

        /// <summary>
        /// 合约状态	合约状态: 0:已下市、1:上市、2:待上市、3:停牌，4:待开盘、5:结算中、6:交割中、7:结算完成、8:交割完成
        /// </summary>
        [JsonProperty("contract_status", NullValueHandling = NullValueHandling.Ignore)]
        public int ContractStatus { get; set; } = default;

        /// <summary>
        /// settlement_date	true	string	合约下次结算时间	时间戳，如"1490759594752"
        /// </summary>
        [JsonProperty("settlement_date", NullValueHandling = NullValueHandling.Ignore)]
        public string SettlementDate { get; set; } = string.Empty;

        /// <summary>
        /// 合约支持的保证金模式	cross：全仓模式；isolated：逐仓模式；all：全逐仓都支持
        /// </summary>
        [JsonProperty("support_margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string SupportMarginMode { get; set; } = string.Empty;

        /// <summary>
        /// 业务类型	futures：交割、swap：永续
        /// </summary>
        [JsonProperty("business_type", NullValueHandling = NullValueHandling.Ignore)]
        public string BusinessType { get; set; } = string.Empty;

        /// <summary>
        /// 交易代码	如：“BTC-USDT”
        /// </summary>
        [JsonProperty("pair", NullValueHandling = NullValueHandling.Ignore)]
        public string Pair { get; set; } = string.Empty;

        /// <summary>
        /// 合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）
        /// </summary>
        [JsonProperty("contract_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;
    }
}