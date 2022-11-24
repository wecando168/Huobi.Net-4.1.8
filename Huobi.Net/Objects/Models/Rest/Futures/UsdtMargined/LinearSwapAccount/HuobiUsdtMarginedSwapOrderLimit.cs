using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapAccount.CommonBaseModels;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapAccount
{
    /// <summary>
    /// 【通用】查询用户当前的下单量限制(PrivateData)
    /// </summary>
    public class HuobiUsdtMarginedSwapOrderLimit
    {
        /// <summary>
        /// 订单报价类型	"limit":限价，"opponent":对手价，"lightning":闪电平仓，"optimal_5":最优5档，"optimal_10":最优10档，"optimal_20":最优20档，"fok":FOK订单，"ioc":IOC订单,opponent_ioc"： 对手价-IOC下单，"lightning_ioc"：闪电平仓-IOC下单，"optimal_5_ioc"：最优5档-IOC下单，"optimal_10_ioc"：最优10档-IOC下单，"optimal_20_ioc"：最优20档-IOC下单,"opponent_fok"： 对手价-FOK下单，"lightning_fok"：闪电平仓-FOK下单，"optimal_5_fok"：最优5档-FOK下单，"optimal_10_fok"：最优10档-FOK下单，"optimal_20_fok"：最优20档-FOK下单
        /// </summary>
        [JsonProperty("order_price_type", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderPriceType { get; set; } = string.Empty;

        /// <summary>
        /// 下单量限制列表
        /// </summary>
        [JsonProperty("list", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<SwapOrderLimit> SwapOrderLimitList { get; set; } = Array.Empty<SwapOrderLimit>();        
    }

    /// <summary>
    /// 下单量限制
    /// </summary>
    public class SwapOrderLimit
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
        /// 合约开仓单笔下单量最大值
        /// </summary>
        [JsonProperty("open_limit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal OpenLimit { get; set; } = default(decimal);

        /// <summary>
        /// 合约平仓单笔下单量最大值
        /// </summary>
        [JsonProperty("close_limit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal CloseLimit { get; set; } = default(decimal);

        /// <summary>
        /// 业务类型	futures：交割、swap：永续
        /// </summary>
        [JsonProperty("business_type", NullValueHandling = NullValueHandling.Ignore)]
        public string BusinessType { get; set; } = string.Empty;

        /// <summary>
        /// 合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）
        /// </summary>
        [JsonProperty("contract_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 交易对	如：“BTC-USDT”
        /// </summary>
        [JsonProperty("pair", NullValueHandling = NullValueHandling.Ignore)]
        public string Pair { get; set; } = string.Empty;

        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;
    }
}