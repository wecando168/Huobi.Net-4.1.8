using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapTrade.Request;

namespace Huobi.Net.Objects.Models.Socket.Futures.UsdtMargined.WebSocketOrderAndAccounts
{
    /// <summary>
    /// 【逐仓】获取用户的合约订单明细信息
    /// </summary>
    public class WWTHuobiUsdtMarginedMarketSubscribeOrderData
    {
        /// <summary>
        /// string	操作名称，推送固定值为 notify;
        /// </summary>
        [JsonProperty("op", NullValueHandling = NullValueHandling.Ignore)]
        public string? Op { get; set; } = string.Empty;

        /// <summary>
        /// 推送的主题
        /// </summary>
        [JsonProperty("topic", NullValueHandling = NullValueHandling.Ignore)]
        public string? Topic { get; set; } = string.Empty;

        /// <summary>
        /// 服务端应答时间戳
        /// </summary>
        [JsonProperty("ts", NullValueHandling = NullValueHandling.Ignore)]
        public long? Timestamp { get; set; } = null;

        /// <summary>
        /// 用户uid
        /// </summary>
        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public string? Uid { get; set; } = string.Empty;

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
        /// 委托数量
        /// </summary>
        [JsonProperty("volume", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Volume { get; set; } = default;

        /// <summary>
        /// 委托价格
        /// </summary>
        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; } = default;

        /// <summary>
        /// 订单报价类型	"limit":限价，"opponent":对手价，"post_only":只做maker单,post only下单只受用户持仓数量限制，"lightning":闪电平仓，"optimal_5":最优5档，"optimal_10":最优10档，"optimal_20":最优20档，"fok":FOK订单，"ioc":IOC订单, "opponent_ioc": 对手价-IOC下单，"lightning_ioc": 闪电平仓-IOC下单，"optimal_5_ioc": 最优5档-IOC下单，"optimal_10_ioc": 最优10档-IOC下单，"optimal_20_ioc"：最优20档-IOC下单，"opponent_fok"： 对手价-FOK下单，"lightning_fok"：闪电平仓-FOK下单，"optimal_5_fok"：最优5档-FOK下单，"optimal_10_fok"：最优10档-FOK下单，"optimal_20_fok"：最优20档-FOK下单
        /// </summary>
        [JsonProperty("order_price_type", NullValueHandling = NullValueHandling.Ignore)]
        public string? OrderPriceType { get; set; } = string.Empty;

        /// <summary>
        /// 买卖方向	"buy":买 "sell":卖
        /// </summary>
        [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
        public string? Direction { get; set; } = string.Empty;

        /// <summary>
        /// 开平方向	"open":开 "close":平 "both":单向持仓
        /// </summary>
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public string? Offset { get; set; } = string.Empty;

        /// <summary>
        /// 订单状态(1准备提交 2准备提交 3已提交 4部分成交 5部分成交已撤单 6全部成交 7已撤单 11撤单中)
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public int? Status { get; set; } = default;

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
        /// String类型订单ID
        /// </summary>
        [JsonProperty("order_id_str", NullValueHandling = NullValueHandling.Ignore)]
        public string? OrderIdStr { get; set; } = string.Empty;

        /// <summary>
        /// 客户订单ID
        /// </summary>
        [JsonProperty("client_order_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? ClientOrderId { get; set; } = null;

        /// <summary>
        /// 订单来源（system:系统、web:用户网页、api:用户API、m:用户M站、risk:风控系统、settlement:交割结算、ios：ios客户端、android：安卓客户端、windows：windows客户端、mac：mac客户端、trigger：计划委托触发、tpsl:止盈止损触发 ）
        /// </summary>
        [JsonProperty("order_source", NullValueHandling = NullValueHandling.Ignore)]
        public string? OrderSource { get; set; } = string.Empty;

        /// <summary>
        /// 订单类型	1:报单 、 2:撤单 、 3:强平、4:交割
        /// </summary>
        [JsonProperty("order_type", NullValueHandling = NullValueHandling.Ignore)]
        public int? OrderType { get; set; } = default;

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
        /// 结算类型 0:非强平类型，1：多空轧差， 2:部分接管，3：全部接管
        /// </summary>
        [JsonProperty("liquidation_type", NullValueHandling = NullValueHandling.Ignore)]
        public string? LiquidationType { get; set; } = string.Empty;

        /// <summary>
        /// 成交均价
        /// </summary>
        [JsonProperty("trade_avg_price", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TradeAvgPrice { get; set; } = default;

        /// <summary>
        /// 保证金币种（计价币种）	
        /// </summary>
        [JsonProperty("margin_asset", NullValueHandling = NullValueHandling.Ignore)]
        public string? MarginAsset { get; set; } = string.Empty;

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
        /// 撤单时间
        /// </summary>
        [JsonProperty("canceled_at", NullValueHandling = NullValueHandling.Ignore)]
        public long? CanceledTimestamp { get; set; } = null;

        /// <summary>
        /// 手续费币种	（"USDT"...）
        /// </summary>
        [JsonProperty("fee_asset", NullValueHandling = NullValueHandling.Ignore)]
        public string? FeeAsset { get; set; } = string.Empty;

        /// <summary>
        /// 保证金模式   isolated：逐仓模式
        /// </summary>
        [JsonProperty("margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string? MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金账户   比如“BTC-USDT”
        /// </summary>
        [JsonProperty("margin_account", NullValueHandling = NullValueHandling.Ignore)]
        public string? MarginAccount { get; set; } = string.Empty;

        /// <summary>
        /// 是否设置止盈止损	1：是；0：否
        /// </summary>
        [JsonProperty("is_tpsl", NullValueHandling = NullValueHandling.Ignore)]
        public int? IsTpsl { get; set; } = default;

        /// <summary>
        /// 真实收益（使用开仓均价计算，包含仓位跨结算的已实现盈亏。）	
        /// </summary>
        [JsonProperty("real_profit", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? RealProfit { get; set; } = default;

        /// <summary>
        /// 是否为只减仓订单	0:表示为非只减仓订单，1:表示为只减仓订单
        /// </summary>
        [JsonProperty("reduce_only", NullValueHandling = NullValueHandling.Ignore)]
        public int? ReduceOnly { get; set; } = default;

        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;

        /// <summary>
        /// 逐仓合约成交明细列表
        /// </summary>
        [JsonProperty("trades", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<HuobiUsdtMarginedWSTrade> HuobiUsdtMarginedIsolatedWSTrades { get; set; } = Array.Empty<HuobiUsdtMarginedWSTrade>();
    }

    /// <summary>
    /// 【通用】U本位合约WebSocket推送成交明细
    /// </summary>
    public class HuobiUsdtMarginedWSTrade : WWTHuobiUsdtMarginedTrade
    {
        /// <summary>
        /// 手续费币种	“USDT”
        /// </summary>
        [JsonProperty("fee_asset", NullValueHandling = NullValueHandling.Ignore)]
        public string FeeAsset { get; set; } = string.Empty;
    }
}