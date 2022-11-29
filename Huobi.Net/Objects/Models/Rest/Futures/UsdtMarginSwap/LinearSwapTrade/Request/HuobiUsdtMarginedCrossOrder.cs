using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapTrade.Request
{
    /// <summary>
    /// 【全仓】合约下单提交参数
    /// </summary>
    public class HuobiUsdtMarginedCrossOrder
    {
        /// <summary>
        /// 合约代码	"BTC-USDT" ...
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 委托数量(张)
        /// </summary>
        [JsonProperty("volume")]
        public long Volume { get; set; }

        /// <summary>
        /// 仓位方向	"buy":买 "sell":卖
        /// </summary>
        [JsonProperty("direction")]
        public string Direction { get; set; } = string.Empty;

        /// <summary>
        /// 杠杆倍数[“开仓”若有10倍多单，就不能再下20倍多单;高倍杠杆风险系数较大，请谨慎使用。
        /// </summary>
        [JsonProperty("lever_rate")]
        public int LeverRate { get; set; }

        /// <summary>
        /// 仓位方向	"buy":买 "sell":卖
        /// </summary>
        [JsonProperty("order_price_type")]
        public string OrderPriceType { get; set; } = string.Empty;

        /// <summary>
        /// string	交易对	BTC-USDT
        /// </summary>
        [JsonProperty("pair", NullValueHandling = NullValueHandling.Ignore)]
        public string? Pair { get; set; }

        /// <summary>
        /// 合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）
        /// </summary>
        [JsonProperty("contract_type", NullValueHandling = NullValueHandling.Ignore)]
        public string? ContractType { get; set; }

        /// <summary>
        /// 是否为只减仓订单（该字段在双向持仓模式下无效，在单向持仓模式下不填默认为0。）	0:表示为非只减仓订单，1:表示为只减仓订单
        /// </summary>
        [JsonProperty("reduce_only", NullValueHandling = NullValueHandling.Ignore)]
        public int? ReduceOnly { get; set; }

        /// <summary>
        /// 客户自己填写和维护，必须为数字	[1-9223372036854775807]
        /// </summary>
        [JsonProperty("client_order_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ClientOrderId { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }

        /// <summary>
        /// 开平方向 "open":开 "close":平 “both”:单向持仓
        /// </summary>
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public string? Offset { get; set; }

        /// <summary>
        /// 止盈触发价格
        /// </summary>
        [JsonProperty("tp_trigger_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TpTriggerPrice { get; set; }

        /// <summary>
        /// 止盈委托价格（最优N档委托类型时无需填写价格）	
        /// </summary>
        [JsonProperty("tp_order_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TpOrderPrice { get; set; }

        /// <summary>
        /// 止盈委托类型 不填默认为limit; 限价：limit ，最优5档：optimal_5，最优10档：optimal_10，最优20档：optimal_20
        /// </summary>
        [JsonProperty("tp_order_price_type", NullValueHandling = NullValueHandling.Ignore)]
        public string? TpOrderPriceType { get; set; }

        /// <summary>
        /// 止损触发价格
        /// </summary>
        [JsonProperty("sl_trigger_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SlTriggerPrice { get; set; }

        /// <summary>
        /// 止损委托价格（最优N档委托类型时无需填写价格）	
        /// </summary>
        [JsonProperty("sl_order_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SlOrderPrice { get; set; }

        /// <summary>
        /// 止损委托类型 不填默认为limit; 限价：limit ，最优5档：optimal_5，最优10档：optimal_10，最优20档：optimal_20
        /// </summary>
        [JsonProperty("sl_order_price_type", NullValueHandling = NullValueHandling.Ignore)]
        public string? SlOrderPriceType { get; set; }
    }
}