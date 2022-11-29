using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapStrategy
{
    /// <summary>
    /// 【全仓】查询开仓单关联的止盈止损订单详情
    /// </summary>
    public class HuobiUsdtMarginedMarketSwapCrossRelationTpslOrder
    {
        /// <summary>
        /// 合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）
        /// </summary>
        [JsonProperty("contract_type", NullValueHandling = NullValueHandling.Ignore)]
        public string? ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 业务类型	futures：交割、swap：永续
        /// </summary>
        [JsonProperty("business_type", NullValueHandling = NullValueHandling.Ignore)]
        public string? BusinessType { get; set; } = string.Empty;

        /// <summary>
        /// 交易对	如：“BTC-USDT”
        /// </summary>
        [JsonProperty("pair", NullValueHandling = NullValueHandling.Ignore)]
        public string? Pair { get; set; } = string.Empty;

        /// <summary>
        /// 品种代码
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string? Symbol { get; set; } = string.Empty;

        /// <summary>
        /// 合约代码
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string? ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金模式   cross：全仓模式
        /// </summary>
        [JsonProperty("margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string? MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金账户   比如“BTC-USDT”
        /// </summary>
        [JsonProperty("margin_account", NullValueHandling = NullValueHandling.Ignore)]
        public string? MarginAccount { get; set; } = string.Empty;

        /// <summary>
        /// 委托数量
        /// </summary>
        [JsonProperty("volume", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Volume { get; set; } = default;

        /// <summary>
        /// 委托价
        /// </summary>
        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }

        /// <summary>
        /// 订单报价类型	"limit":限价，"opponent":对手价，"post_only":只做maker单,post only下单只受用户持仓数量限制，"lightning":闪电平仓，"optimal_5":最优5档，"optimal_10":最优10档，"optimal_20":最优20档，"fok":FOK订单，"ioc":IOC订单, "opponent_ioc": 对手价-IOC下单，"lightning_ioc": 闪电平仓-IOC下单，"optimal_5_ioc": 最优5档-IOC下单，"optimal_10_ioc": 最优10档-IOC下单，"optimal_20_ioc"：最优20档-IOC下单，"opponent_fok"： 对手价-FOK下单，"lightning_fok"：闪电平仓-FOK下单，"optimal_5_fok"：最优5档-FOK下单，"optimal_10_fok"：最优10档-FOK下单，"optimal_20_fok"：最优20档-FOK下单
        /// </summary>
        [JsonProperty("order_price_type", NullValueHandling = NullValueHandling.Ignore)]
        public string? OrderPriceType { get; set; } = string.Empty;

        /// <summary>
        /// 买卖方向 "buy":买 "sell":卖
        /// </summary>
        [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
        public string? Direction { get; set; } = string.Empty;

        /// <summary>
        /// 开平方向	"open":开 "close":平 "both":单向持仓
        /// </summary>
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public string? Offset { get; set; } = string.Empty;

        /// <summary>
        /// 杠杆倍数
        /// </summary>
        [JsonProperty("lever_rate", NullValueHandling = NullValueHandling.Ignore)]
        public int? LeverRate { get; set; } = default;

        /// <summary>
        /// 订单ID
        /// </summary>
        [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderId { get; set; } = default(long);

        /// <summary>
        /// 字符串类型的订单ID
        /// </summary>
        [JsonProperty("order_id_str", NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderIdStr { get; set; } = default(long);

        /// <summary>
        /// 客户订单ID
        /// </summary>
        [JsonProperty("client_order_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ClientOrderId { get; set; } = null;

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public long? CreatedTimestamp { get; set; } = default(long);

        /// <summary>
        /// 成交数量
        /// </summary>
        [JsonProperty("trade_volume", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TradeVolume { get; set; } = default;

        /// <summary>
        /// 成交总金额 ，即sum（每一笔成交张数* 合约面值 * 成交价格）	
        /// </summary>
        [JsonProperty("trade_turnover", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TradeTurnover { get; set; } = default;

        /// <summary>
        /// 手续费
        /// </summary>
        [JsonProperty("fee", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Fee { get; set; } = default;

        /// <summary>
        /// 成交均价
        /// </summary>
        [JsonProperty("trade_avg_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TradeAvgPrice { get; set; } = default;

        /// <summary>
        /// 冻结保证金
        /// </summary>
        [JsonProperty("margin_frozen", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? MarginFrozen { get; set; } = default;

        /// <summary>
        /// 平仓盈亏（使用持仓均价计算，不包含仓位跨结算的已实现盈亏。）	
        /// </summary>
        [JsonProperty("profit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Profit { get; set; } = default;

        /// <summary>
        /// 订单状态(1准备提交 2准备提交 3已提交 4部分成交 5部分成交已撤单 6全部成交 7已撤单 11撤单中)
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public int? Status { get; set; } = default;

        /// <summary>
        /// 订单类型	1:报单 、 2:撤单 、 3:强平、4:交割
        /// </summary>
        [JsonProperty("order_type", NullValueHandling = NullValueHandling.Ignore)]
        public int? OrderType { get; set; } = default;

        /// <summary>
        /// 订单来源（system:系统、web:用户网页、api:用户API、m:用户M站、risk:风控系统、settlement:交割结算、ios：ios客户端、android：安卓客户端、windows：windows客户端、mac：mac客户端、trigger：计划委托触发、tpsl:止盈止损触发 ）
        /// </summary>
        [JsonProperty("order_source", NullValueHandling = NullValueHandling.Ignore)]
        public string? OrderSource { get; set; } = string.Empty;

        /// <summary>
        /// 手续费币种
        /// </summary>
        [JsonProperty("fee_asset", NullValueHandling = NullValueHandling.Ignore)]
        public string FeeAsset { get; set; } = string.Empty;

        /// <summary>
        /// 下order单时间	
        /// </summary>
        [JsonProperty("canceled_at", NullValueHandling = NullValueHandling.Ignore)]
        public long? CanceledTimestamp { get; set; }

        /// <summary>
        /// 【全仓】关联的止盈止损单信息集合
        /// </summary>
        [JsonProperty("tpsl_order_type", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<HuobiUsdtMarginedCrossTpslOrderInfo> HuobiUsdtMarginedCrossTpslOrderInfos { get; set; } = Array.Empty<HuobiUsdtMarginedCrossTpslOrderInfo>();
    }

    /// <summary>
    /// 【全仓】关联的止盈止损单信息
    /// </summary>
    public class HuobiUsdtMarginedCrossTpslOrderInfo
    {        
        /// <summary>
        /// 委托数量
        /// </summary>
        [JsonProperty("volume", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Volume { get; set; } = default;

        /// <summary>
        /// 买卖方向 "buy":买 "sell":卖
        /// </summary>
        [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
        public string? Direction { get; set; } = string.Empty;

        /// <summary>
        /// 止盈止损类型 “tp”:止盈单；"sl"：止损单
        /// </summary>
        [JsonProperty("tpsl_order_type", NullValueHandling = NullValueHandling.Ignore)]
        public string? TpslOrderType { get; set; } = string.Empty;

        /// <summary>
        /// 订单ID
        /// </summary>
        [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderId { get; set; } = default(long);

        /// <summary>
        /// 字符串类型的订单ID
        /// </summary>
        [JsonProperty("order_id_str", NullValueHandling = NullValueHandling.Ignore)]
        public long? OrderIdStr { get; set; } = default(long);

        /// <summary>
        /// 触发类型	ge大于等于；le小于等于
        /// </summary>
        [JsonProperty("trigger_type", NullValueHandling = NullValueHandling.Ignore)]
        public string? TriggerType { get; set; } = string.Empty;

        /// <summary>
        /// 触发价
        /// </summary>
        [JsonProperty("trigger_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal TriggerPrice { get; set; }

        /// <summary>
        /// 委托价
        /// </summary>
        [JsonProperty("order_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OrderPrice { get; set; }

        /// <summary>
        /// 订单创建时间	
        /// </summary>
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public long CreatedTimestamp { get; set; }

        /// <summary>
        /// 订单报价类型 限价：limit ，最优5档：optimal_5，最优10档：optimal_10，最优20档：optimal_20
        /// </summary>
        [JsonProperty("order_price_type", NullValueHandling = NullValueHandling.Ignore)]
        public string? OrderPriceType { get; set; } = string.Empty;

        /// <summary>
        /// 关联的止盈止损单id（用户同时设置止盈止损单时，该字段才有值，否则数值为-1）
        /// </summary>
        [JsonProperty("relation_tpsl_order_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? RelationTpslOrderId { get; set; } = string.Empty;

        /// <summary>
        /// 1:准备提交、2:已提交、3:报单中、8：撤单未找到、9：撤单中、10：失败
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public int? Status { get; set; } = default;

        /// <summary>
        /// 下order单时间	
        /// </summary>
        [JsonProperty("canceled_at", NullValueHandling = NullValueHandling.Ignore)]
        public long? CanceledTimestamp { get; set; }

        /// <summary>
        /// 被触发时下order单失败错误码
        /// </summary>
        [JsonProperty("fail_code", NullValueHandling = NullValueHandling.Ignore)]
        public int? FailCode { get; set; }

        /// <summary>
        /// 被触发时下order单失败原因	
        /// </summary>
        [JsonProperty("fail_reason", NullValueHandling = NullValueHandling.Ignore)]
        public string? FailReason { get; set; }

        /// <summary>
        /// 被触发时的价格
        /// </summary>
        [JsonProperty("triggered_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TriggeredPrice { get; set; } = default;

        /// <summary>
        /// 该字段为关联限价单的关联字段，未触发前数值为-1
        /// </summary>
        [JsonProperty("relation_order_id", NullValueHandling = NullValueHandling.Ignore)]
        public string? RelationOrderId { get; set; } = string.Empty;

        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;
    }
}