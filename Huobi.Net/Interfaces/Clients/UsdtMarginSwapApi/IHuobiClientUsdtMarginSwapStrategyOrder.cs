using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Huobi.Net.Objects.Models;
using CryptoExchange.Net.Converters;
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapStrategy;

namespace Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// 策略接口
    /// </summary>
    public interface IHuobiClientUsdtMarginSwapStrategyOrder
    {
        /// <summary>
        /// 
        /// 【逐仓】合约计划委托下单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_trigger_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#cf8f20d352"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 BTC-USDT</param>
        /// <param name="triggerType">触发类型    ge大于等于(触发价比最新价大)；le小于(触发价比最新价小)</param>
        /// <param name="triggerPrice">触发价，精度超过最小变动单位会报错</param>
        /// <param name="volume">委托数量(张)</param>
        /// <param name="direction">买卖方向    buy:买 sell:卖</param>
        /// <param name="reduceOnly">是否为只减仓订单（该字段在双向持仓模式下无效，在单向持仓模式下不填默认为0。）	0:表示为非只减仓订单，1:表示为只减仓订单</param>
        /// <param name="orderPrice">委托价，精度超过最小变动单位会报错</param>
        /// <param name="orderPriceType">委托类型，不填默认为limit; 限价：limit ，最优5档：optimal_5，最优10档：optimal_10，最优20档：optimal_20</param>
        /// <param name="offset">开平方向    open:开 close:平 both:单向持仓</param>
        /// <param name="leverRate">开仓必须填写，平仓可以不填。杠杆倍数[开仓若有10倍多单，就不能再下20倍多单;高倍杠杆风险系数较大，请谨慎使用。</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapIsolatedTriggerOrder>> LinearSwapTriggerOrderAsync(
            string contractCode,
            string triggerType,
            decimal triggerPrice,
            long volume,
            string direction, 
            int? reduceOnly,
            decimal? orderPrice,
            string? orderPriceType,            
            string? offset,
            int ? leverRate,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【全仓】合约计划委托下单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_trigger_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#c3f89af0f9"/></para>
        /// </summary>
        /// <param name="triggerType">触发类型	ge大于等于(触发价比最新价大)；le小于(触发价比最新价小)</param>
        /// <param name="triggerPrice">触发价，精度超过最小变动单位会报错</param>
        /// <param name="volume">委托数量(张)</param>
        /// <param name="direction">买卖方向	buy:买 sell:卖</param>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="reduceOnly">是否为只减仓订单（该字段在双向持仓模式下无效，在单向持仓模式下不填默认为0。）	0:表示为非只减仓订单，1:表示为只减仓订单</param>
        /// <param name="orderPrice">委托价，精度超过最小变动单位会报错</param>
        /// <param name="orderPriceType">委托类型，不填默认为limit;	限价：limit ，最优5档：optimal_5，最优10档：optimal_10，最优20档：optimal_20</param>
        /// <param name="offset">开平方向	open:开, close:平, both:单向持仓</param>
        /// <param name="leverRate">开仓必须填写，平仓可以不填。杠杆倍数[开仓若有10倍多单，就不能再下20倍多单;高倍杠杆风险系数较大，请谨慎使用。</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTriggerOrder>> LinearSwapCrossTriggerOrderAsync(
            string triggerType,
            decimal triggerPrice,
            long volume,
            string direction,
            string? contractCode,
            string? pair,
            string? contractType,
            int? reduceOnly,
            decimal? orderPrice,
            string? orderPriceType,
            string? offset,
            int? leverRate, 
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【逐仓】合约计划委托撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_trigger_cancel"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#89cf783356"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="orderIdList">用户订单ID（多个订单ID中间以","分隔,一次最多允许撤消20个订单 ）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapIsolatedTriggerCancel>> LinearSwapTriggerCancelAsync(
            string contractCode,
            IEnumerable<long>? orderIdList,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【全仓】合约计划委托撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_trigger_cancel"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#b4e16d7b11"/></para>
        /// </summary>
        /// <param name="orderIdList">用户订单ID（多个订单ID中间以","分隔,一次最多允许撤消20个订单 ）</param>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTriggerCancel>> LinearSwapCrossTriggerCancelAsync(
            IEnumerable<long>? orderIdList,
            string? contractCode,
            string? pair,
            string? contractType,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【逐仓】合约计划委托全部撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_trigger_cancelall"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#987871ba1c"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="direction">买卖方向（不填默认全部）	"buy":买 "sell":卖</param>
        /// <param name="offset">开平方向（不填默认全部）	"open":开 "close":平</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapIsolatedTriggerCancel>> LinearSwapTriggerCancelAllAsync(
            string contractCode, 
            string? direction,
            string? offset,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【全仓】合约计划委托全部撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_trigger_cancelall"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#eafad70687"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="direction">买卖方向（不填默认全部）	"buy":买 "sell":卖</param>
        /// <param name="offset">开平方向（不填默认全部）	"open":开 "close":平</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTriggerCancel>> LinearSwapCrossTriggerCancelAllAsync(
            string? contractCode,
            string? pair,
            string? contractType,
            string? direction,
            string? offset,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【逐仓】获取计划委托当前委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_trigger_openorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#394154e49c"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="pageIndex">第几页，不填默认第一页</param>
        /// <param name="pageSize">不填默认20，不得多于50</param>
        /// <param name="tradeType">交易类型，不填默认查询全部	0:全部, 1:买入开多, 2:卖出开空, 3:买入平空, 4:卖出平多, 17：买入（单向持仓）, 18：卖出（单向持仓）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapIsolatedTriggerOpenOrders>> GetLinearSwapTriggerOpenordersAsync(
            string contractCode,
            int? pageIndex = null,
            int? pageSize = null,
            int? tradeType = null,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【全仓】获取计划委托当前委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_trigger_openorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#33b7b627de"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="pageIndex">第几页，不填默认第一页</param>
        /// <param name="pageSize">不填默认20，不得多于50</param>
        /// <param name="tradeType">交易类型，不填默认查询全部	0:全部, 1:买入开多, 2:卖出开空, 3:买入平空, 4:卖出平多, 17：买入（单向持仓）, 18：卖出（单向持仓）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTriggerOpenOrders>> GetLinearSwapCrossTriggerOpenordersAsync(
            string? contractCode = null,
            string? pair = null,
            int? pageIndex = null,
            int? pageSize = null,
            int? tradeType = null,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【逐仓】获取计划委托历史委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_trigger_hisorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#31741caa19"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码		BTC-USDT</param>
        /// <param name="tradeType">交易类型		0:全部, 1:买入开多, 2:卖出开空, 3:买入平空, 4:卖出平多, 17:买入（单向持仓）, 18:卖出（单向持仓）;后台是根据该值转换为offset和direction，然后去查询的； 其他值无法查询出结果</param>
        /// <param name="status">订单状态		多个以英文逗号隔开，计划委托单状态：0:全部（表示全部结束状态的订单）、4:已委托、5:委托失败、6:已撤单</param>
        /// <param name="createDate">日期		可随意输入正整数，如果参数超过90则默认查询90天的数据</param>
        /// <param name="pageIndex">页码，不填默认第1页	1	第几页，不填默认第一页</param>
        /// <param name="pageSize">不填默认20，不得多于50	20	不填默认20，不得多于50</param>
        /// <param name="sortBy">排序字段（降序），不填默认按照created_at降序	created_at	"created_at"：按订单创建时间进行降序，"update_time"：按订单更新时间进行降序</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapIsolatedTriggerHisorders>> GetLinearSwapTriggerHisordersAsync(
            string contractCode,
            int tradeType,
            string status,
            int createDate,
            int? pageIndex = null,
            int? pageSize = null,
            string? sortBy = null,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【全仓】获取计划委托历史委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_trigger_hisorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#28b32ee4f3"/></para>
        /// </summary>
        /// <param name="tradeType">交易类型		0:全部, 1:买入开多, 2:卖出开空, 3:买入平空, 4:卖出平多, 17:买入（单向持仓）, 18:卖出（单向持仓）;后台是根据该值转换为offset和direction，然后去查询的； 其他值无法查询出结果</param>
        /// <param name="status">订单状态		多个以英文逗号隔开，计划委托单状态：0:全部（表示全部结束状态的订单）、4:已委托、5:委托失败、6:已撤单</param>
        /// <param name="createDate">日期		可随意输入正整数，如果参数超过90则默认查询90天的数据</param>
        /// <param name="contractCode">合约代码		BTC-USDT</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="pageIndex">页码，不填默认第1页	1	第几页，不填默认第一页</param>
        /// <param name="pageSize">不填默认20，不得多于50	20	不填默认20，不得多于50</param>
        /// <param name="sortBy">排序字段（降序），不填默认按照created_at降序	created_at	"created_at"：按订单创建时间进行降序，"update_time"：按订单更新时间进行降序</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTriggerHisorders>> GetLinearSwapCrossTriggerHisordersAsync(
            int tradeType,
            string status,
            int createDate,
            string? contractCode,
            string? pair,            
            int? pageIndex = null,
            int? pageSize = null,
            string? sortBy = null,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【逐仓】对仓位设置止盈止损订单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_tpsl_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#54ae54b08d"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码		BTC-USDT</param>
        /// <param name="direction">买卖方向    buy:买入平空 sell:卖出平多</param>
        /// <param name="volume">委托数量(张)</param>
        /// <param name="tpTriggerPrice">止盈触发价格</param>
        /// <param name="tpOrderPrice">止盈委托价格（最优N档委托类型时无需填写价格）</param>
        /// <param name="tpOrderPriceType">止盈委托类型  不填默认为limit; 限价：limit ，最优5档：optimal_5，最优10档：optimal_10，最优20档：optimal_20</param>
        /// <param name="slTriggerPrice">止损触发价格</param>
        /// <param name="slOrderPrice">止损委托价格（最优N档委托类型时无需填写价格）</param>
        /// <param name="slOrderPriceType">止损委托类型  不填默认为limit; 限价：limit ，最优5档：optimal_5，最优10档：optimal_10，最优20档：optimal_20</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapIsolatedTpslOrder>> LinearSwapTpslOrderAsync(
            string contractCode,
            string direction,
            decimal volume,
            decimal? tpTriggerPrice,
            decimal? tpOrderPrice,
            string? tpOrderPriceType,
            decimal? slTriggerPrice,
            decimal? slOrderPrice,
            string? slOrderPriceType,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【全仓】对仓位设置止盈止损订单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_tpsl_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#f71977fef2"/></para>
        /// </summary>
        /// <param name="volume">委托数量(张)</param>
        /// <param name="tpTriggerPrice">止盈触发价格</param>
        /// <param name="contractCode">合约代码		BTC-USDT</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="direction">买卖方向    buy:买入平空 sell:卖出平多</param>
        /// <param name="tpOrderPrice">止盈委托价格（最优N档委托类型时无需填写价格）</param>
        /// <param name="tpOrderPriceType">止盈委托类型  不填默认为limit; 限价：limit ，最优5档：optimal_5，最优10档：optimal_10，最优20档：optimal_20</param>
        /// <param name="slTriggerPrice">止损触发价格</param>
        /// <param name="slOrderPrice">止损委托价格（最优N档委托类型时无需填写价格）</param>
        /// <param name="slOrderPriceType">止损委托类型  不填默认为limit; 限价：limit ，最优5档：optimal_5，最优10档：optimal_10，最优20档：optimal_20</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTpslOrder>> LinearSwapCrossTpslOrderAsync(
            string direction,
            decimal volume,
            string? contractCode,
            string? pair,
            string? contractType,
            decimal? tpTriggerPrice,
            decimal? tpOrderPrice,
            string? tpOrderPriceType,
            decimal? slTriggerPrice,
            decimal? slOrderPrice,
            string? slOrderPriceType,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【逐仓】止盈止损订单撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_tpsl_cancel"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#e02c036026"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	"BTC-USDT" ...</param>
        /// <param name="orderIdList">止盈止损订单ID（多个订单ID中间以","分隔,一次最多允许撤消10个订单 ）	</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapIsolatedTpslCancel>> LinearSwapTpslCancelAsync(
            string contractCode,
            IEnumerable<long>? orderIdList,             
            CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】止盈止损订单撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_tpsl_cancel"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#7b41812520"/></para>
        /// </summary>
        /// <param name="orderIdList">用户订单ID（多个订单ID中间以","分隔,一次最多允许撤消10个订单 ）</param>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTpslCancel>> LinearSwapCrossTpslCancelAsync(
            IEnumerable<long>? orderIdList,
            string? contractCode,
            string? pair,
            string? contractType,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【逐仓】止盈止损订单全部撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_tpsl_cancelall"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#719c8b6331"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	"BTC-USDT" ...</param>
        /// <param name="direction">买卖方向（不填默认全部）	"buy":买 "sell":卖</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapIsolatedTpslCancel>> LinearSwapTpslCancelAllAsync(
            string contractCode,
            string? direction,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【全仓】止盈止损订单全部撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_tpsl_cancelall"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#01ae20dbbd"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="direction">买卖方向（不填默认全部）	"buy":买 "sell":卖</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTpslCancel>> LinearSwapCrossTpslCancelAllAsync(            
            string? contractCode,
            string? pair,
            string? contractType,
            string? direction,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【逐仓】查询止盈止损订单当前委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_tpsl_openorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#6c6fee598c"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="pageIndex">第几页，不填默认第一页</param>
        /// <param name="pageSize">不填默认20，不得多于50</param>
        /// <param name="tradeType">交易类型，不填默认查询全部	0:全部,3: 买入平空,4: 卖出平多。</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapIsolatedTpslOpenOrders>> GetLinearSwapTpslOpenordersAsync(
            string contractCode,
            int? pageIndex = null,
            int? pageSize = null,
            int? tradeType = null,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【全仓】查询止盈止损订单当前委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_tpsl_openorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#9ea73b3922"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="pageIndex">第几页，不填默认第一页</param>
        /// <param name="pageSize">不填默认20，不得多于50</param>
        /// <param name="tradeType">交易类型，不填默认查询全部	0:全部,3: 买入平空,4: 卖出平多。</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTpslOpenOrders>> GetLinearSwapCrossTpslOpenordersAsync(
            string? contractCode = null,
            string? pair = null,
            int? pageIndex = null,
            int? pageSize = null,
            int? tradeType = null,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【逐仓】查询止盈止损订单历史委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_tpsl_hisorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#f75074daa4"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码		BTC-USDT</param>
        /// <param name="status">订单状态		多个以英文逗号隔开，计划委托单状态：0:全部（表示全部结束状态的订单）、4:已委托、5:委托失败、6:已撤单</param>
        ///  <param name="createDate">日期		可随意输入正整数，如果参数超过90则默认查询90天的数据</param>
        /// <param name="pageIndex">页码，不填默认第1页	1	第几页，不填默认第一页</param>
        /// <param name="pageSize">不填默认20，不得多于50	20	不填默认20，不得多于50</param>
        /// <param name="sortBy">排序字段（降序），不填默认按照created_at降序	created_at	"created_at"：按订单创建时间进行降序，"update_time"：按订单更新时间进行降序</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapIsolatedTpslHisorders>> GetLinearSwapTpslHisordersAsync(
            string contractCode,
            string status,
            long createDate,
            int? pageIndex = null,
            int? pageSize = null,
            string? sortBy = null,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【全仓】查询止盈止损订单历史委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_tpsl_hisorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#52be540948"/></para>
        /// </summary>
        /// <param name="status">订单状态		多个以英文逗号隔开，计划委托单状态：0:全部（表示全部结束状态的订单）、4:已委托、5:委托失败、6:已撤单</param>
        /// <param name="createDate">日期		可随意输入正整数，如果参数超过90则默认查询90天的数据</param>
        /// <param name="contractCode">合约代码		BTC-USDT</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="pageIndex">页码，不填默认第1页	1	第几页，不填默认第一页</param>
        /// <param name="pageSize">不填默认20，不得多于50	20	不填默认20，不得多于50</param>
        /// <param name="sortBy">排序字段（降序），不填默认按照created_at降序	created_at	"created_at"：按订单创建时间进行降序，"update_time"：按订单更新时间进行降序</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTpslHisorders>> GetLinearSwapCrossTpslHisordersAsync(
            string status,
            long createDate,
            string? contractCode,
            string? pair,
            int? pageIndex = null,
            int? pageSize = null,
            string? sortBy = null,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【逐仓】查询开仓单关联的止盈止损订单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_relation_tpsl_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#dc9f6c768f"/></para>
        /// </summary>
        /// <param name="contractCode">	合约代码	"BTC-USDT" ...</param>
        /// <param name="orderId">开仓订单id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapIsolatedRelationTpslOrder>> GetLinearSwapRelationTpslOrderAsync(
             string contractCode,
             long orderId, 
             CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【全仓】查询开仓单关联的止盈止损订单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_relation_tpsl_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#f828ac1ff9"/></para>
        /// </summary>
        /// <param name="orderId">开仓订单id</param>
        /// <param name="contractCode">合约代码	"BTC-USDT" ...</param>
        /// <param name="pair">交易对 BTC-USDT</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossRelationTpslOrder>> GetLinearSwapCrossRelationTpslOrderAsync(
            long orderId,
            string? contractCode,
            string? pair,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【逐仓】跟踪委托订单下单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_track_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#58519dfb70"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码    BTC-USDT</param>
        /// <param name="direction">买卖方向 buy:买 sell:卖</param>
        /// <param name="volume">委托数量(张)</param>
        /// <param name="callbackRate">回调幅度 如：0.01 表示1%，不可小于0.001（0.1%）</param>
        /// <param name="activePrice">激活价格</param>
        /// <param name="orderPriceType">委托类型 最优5档：optimal_5，最优10档：optimal_10，最优20档：optimal_20，理论价格：formula_price</param>
        /// <param name="reduceOnly">是否为只减仓订单（该字段在双向持仓模式下无效，在单向持仓模式下不填默认为0。）	0:表示为非只减仓订单，1:表示为只减仓订单</param>
        /// <param name="leverRate">杠杆倍数，开仓操作为必填，平仓操作为非必填</param>
        /// <param name="offset">开平方向    open:开 close:平 both:单向持仓</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapIsolatedTrackOrder>> LinearSwapTrackOrderAsync(
            string contractCode,
            string direction,
            int volume,
            decimal callbackRate,
            decimal activePrice,
            string orderPriceType,
            int? reduceOnly,
            int? leverRate,
            string? offset,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【全仓】跟踪委托订单下单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_track_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#5b04228ba8"/></para>
        /// </summary>
        /// <param name="direction">买卖方向 buy:买 sell:卖</param>
        /// <param name="volume">委托数量(张)</param>
        /// <param name="callbackRate">回调幅度 如：0.01 表示1%，不可小于0.001（0.1%）</param>
        /// <param name="activePrice">激活价格</param>
        /// <param name="orderPriceType">委托类型 最优5档：optimal_5，最优10档：optimal_10，最优20档：optimal_20，理论价格：formula_price</param>
        /// <param name="contractCode">合约代码    BTC-USDT</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="reduceOnly">是否为只减仓订单（该字段在双向持仓模式下无效，在单向持仓模式下不填默认为0。）	0:表示为非只减仓订单，1:表示为只减仓订单</param>
        /// <param name="offset">开平方向    open:开 close:平 both:单向持仓</param>
        /// <param name="leverRate">杠杆倍数，开仓操作为必填，平仓操作为非必填</param> 
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTrackOrder>> LinearSwapCrossTrackOrderAsync(
            string direction,
            int volume,
            decimal callbackRate,
            decimal activePrice,
            string orderPriceType,
            string? contractCode,
            string? pair,
            string? contractType,            
            int? reduceOnly,
            string offset,
            int? leverRate,            
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【逐仓】跟踪委托订单撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_track_cancel"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#1d86317775"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码</param>
        /// <param name="orderIdList">用户跟踪委托订单ID（多个订单ID中间以","分隔,一次最多允许撤消10个订单 ）	</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapIsolatedTrackCancel>> LinearSwapTrackCancelAsync(
            string contractCode,
            IEnumerable<long>? orderIdList,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【全仓】跟踪委托订单撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_track_cancel"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#eaf06c20d1"/></para>
        /// </summary>
        /// <param name="orderIdList">用户跟踪委托订单ID（多个订单ID中间以","分隔,一次最多允许撤消10个订单 ）	</param>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTrackCancel>> LinearSwapCrossTrackCancelAsync(
            IEnumerable<long>? orderIdList,
            string? contractCode,
            string? pair,
            string? contractType,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【逐仓】跟踪委托订单全部撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_track_cancelall"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#92f06221b5"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	BTC-USDT</param>
        /// <param name="direction">买卖方向（不填默认全部）	"buy":买 "sell":卖</param>
        /// <param name="offset">开平方向（不填默认全部）	"open":开 "close":平</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapIsolatedTrackCancel>> LinearSwapTrackCancelAllAsync(
             string contractCode,
             string? direction,
             string? offset,
             CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【全仓】跟踪委托订单全部撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_track_cancelall"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#9b43c6900c"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="direction">买卖方向（不填默认全部）	"buy":买 "sell":卖</param>
        /// <param name="offset">开平方向（不填默认全部）	"open":开 "close":平</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTrackCancel>> LinearSwapCrossTrackCancelAllAsync(
            string? contractCode,
            string? pair,
            string? contractType,
            string? direction,
            string? offset,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【逐仓】跟踪委托订单当前委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_track_openorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#20e8fe0af2"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="tradeType">交易类型（不填默认查询全部）	0:全部, 1:买入开多, 2: 卖出开空, 3:买入平空, 4:卖出平多, 17：买入（单向持仓）, 18：卖出（单向持仓）</param>
        /// <param name="pageIndex">第几页，不填默认第一页</param>
        /// <param name="pageSize">不填默认20，不得多于50</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapIsolatedTrackOpenOrders>> GetLinearSwapTrackOpenOrdersAsync(
            string contractCode,
            int? tradeType,
            int? pageIndex = null,
            int? pageSize = null, 
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【全仓】跟踪委托订单当前委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_track_openorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#c997c76ebd"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="tradeType">交易类型（不填默认查询全部）	0:全部, 1:买入开多, 2: 卖出开空, 3:买入平空, 4:卖出平多, 17：买入（单向持仓）, 18：卖出（单向持仓）</param>
        /// <param name="pageIndex">第几页，不填默认第一页</param>
        /// <param name="pageSize">不填默认20，不得多于50</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTrackOpenOrders>> GetLinearSwapCrossTrackOpenordersAsync(
            string? contractCode,
            string? pair,
            int? tradeType,
            int? pageIndex = null,
            int? pageSize = null,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 跟踪委托订单历史委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_track_hisorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#466c798453"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码		BTC-USDT</param>
        /// <param name="status">订单状态	多个以英文逗号隔开，跟踪委托订单状态：0:全部（表示全部结束状态的订单）、4:已委托、5:委托失败、6:已撤单</param>
        /// <param name="tradeType">	交易类型	0:全部, 1:买入开多, 2:卖出开空, 3:买入平空, 4:卖出平多, 17：买入（单向持仓）, 18：卖出（单向持仓）</param>
        /// <param name="createDate">日期		可随意输入正整数，如果参数超过90则默认查询90天的数据</param>
        /// <param name="pageIndex">页码，不填默认第1页	1	第几页，不填默认第一页</param>
        /// <param name="pageSize">不填默认20，不得多于50	20	不填默认20，不得多于50</param>
        /// <param name="sortBy">排序字段（降序），不填默认按照created_at降序	created_at	"created_at"：按订单创建时间进行降序，"update_time"：按订单更新时间进行降序</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapIsolatedTrackHisorders>> GetLinearSwapTrackHisordersAsync(
            string contractCode,
            string status,
            int tradeType,
            long createDate,
            int? pageIndex = null,
            int? pageSize = null,
            string? sortBy = null,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【全仓】跟踪委托订单历史委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_track_hisorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#5dc64ede61"/></para>
        /// </summary>
        /// <param name="status">订单状态	多个以英文逗号隔开，跟踪委托订单状态：0:全部（表示全部结束状态的订单）、4:已委托、5:委托失败、6:已撤单</param>
        /// <param name="tradeType">交易类型（不填默认查询全部）	0:全部, 1:买入开多, 2:卖出开空, 3:买入平空, 4:卖出平多, 17：买入（单向持仓）, 18：卖出（单向持仓）</param>
        /// <param name="createDate">日期	可随意输入正整数，如果参数超过90则默认查询90天的数据</param>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="pageIndex">页码，不填默认第1页	1	第几页，不填默认第一页</param>
        /// <param name="pageSize">不填默认20，不得多于50	20	不填默认20，不得多于50</param>
        /// <param name="sortBy">排序字段（降序），不填默认按照created_at降序	created_at	"created_at"：按订单创建时间进行降序，"update_time"：按订单更新时间进行降序</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTrackHisorders>> GetLinearSwapCrossTrackHisordersAsync(
            string status,
            int tradeType,
            long createDate,
            string? contractCode,
            string? pair,            
            int? pageIndex = null,
            int? pageSize = null,
            string? sortBy = null,
            CancellationToken ct = default
            );
    }
}
