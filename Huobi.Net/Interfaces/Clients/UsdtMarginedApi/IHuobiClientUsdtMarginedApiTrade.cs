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
using CryptoExchange.Net.CommonObjects;

namespace Huobi.Net.Interfaces.Clients.UsdtMargined
{
    /// <summary>
    /// U本位合约交易接口
    /// </summary>
    public interface IHuobiClientUsdtMarginedApiTrade
    {
        /// <summary>
        /// 
        /// 【全仓】查询系统交易权限(PrivateData)
        /// <para><a href="GET /linear-swap-api/v1/swap_cross_trade_state"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#a7dff164a9"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapCrossTradeState(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】切换持仓模式(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_switch_position_mode"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#af9b5163ad"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapSwitchPositionMode(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】切换持仓模式(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_switch_position_mode"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#aa6585618a"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCrossSwitchPositionMode(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】合约下单(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#0a9b6ea149"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapOrder(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】合约下单(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#fe3db69b44"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 Contract code. Case-Insenstive.Both uppercase and lowercase are supported.e.g. "BTC-USDT"</param>
        /// <param name="direction">仓位方向 "buy":买 "sell":卖</param>
        /// <param name="offset">开平方向	"open":开 "close":平 “both”:单向持仓</param>
        /// <param name="price">价格 The price of the order, only for limit orders</param>
        /// <param name="leverRate">杠杆倍数[“开仓”若有10倍多单，就不能再下20倍多单;高倍杠杆风险系数较大，请谨慎使用。</param>
        /// <param name="volume">委托数量(张)</param>
        /// <param name="orderPriceType">订单报价类型 "limit":限价，"opponent":对手价 ，"post_only":只做maker单,post only下单只受用户持仓数量限制,"optimal_5"：最优5档，"optimal_10"：最优10档，"optimal_20"：最优20档，"ioc":IOC订单，"fok"：FOK订单, "opponent_ioc": 对手价-IOC下单，"optimal_5_ioc": 最优5档-IOC下单，"optimal_10_ioc": 最优10档-IOC下单，"optimal_20_ioc"：最优20档-IOC下单，"opponent_fok"： 对手价-FOK下单，"optimal_5_fok"：最优5档-FOK下单，"optimal_10_fok"：最优10档-FOK下单，"optimal_20_fok"：最优20档-FOK下单</param>
        /// <param name="tpTriggerPrice">止盈触发价格</param>
        /// <param name="tpOrderPrice">止盈委托价格（最优N档委托类型时无需填写价格）</param>
        /// <param name="tpOrderPriceType">止盈委托类型 不填默认为limit; 限价：limit ，最优5档：optimal_5，最优10档：optimal_10，最优20档：optimal_20</param>
        /// <param name="slTriggerPrice">止损触发价格</param>
        /// <param name="slOrderPrice">止损委托价格（最优N档委托类型时无需填写价格）</param>
        /// <param name="slOrderPriceType">止损委托类型	不填默认为limit; 限价：limit ，最优5档：optimal_5，最优10档：optimal_10，最优20档：optimal_20</param>
        /// <param name="accountId">下单账户编号 [Optional] The account id to place the order on, required for some exchanges, ignored otherwise</param>
        /// <param name="clientOrderId">用户自定义单号 [Optional] Client specified id for this order</param>
        /// <param name="ct">[Optional] Cancellation token for cancelling the request</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCrossOrder(
            string contractCode,
            UmDirection direction,
            UmOffset offset,
            decimal price,
            UmLeverRate leverRate,
            long volume,
            UmOrderPriceType orderPriceType,
            decimal tpTriggerPrice,
            decimal tpOrderPrice,
            UmTpOrderPriceType tpOrderPriceType,
            decimal slTriggerPrice,
            decimal slOrderPrice,
            UmSlOrderPriceType slOrderPriceType,
            string? accountId = null,
            string? clientOrderId = null,
            CancellationToken ct = default
            );

        /// <summary>
        /// 
        /// 【逐仓】合约批量下单(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_batchorder"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#0da150a45a"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapBatchorder(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】合约批量下单(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_batchorder"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#d6b14b4cdb"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCrossBatchorder(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】撤销合约订单(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cancel"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#7f144bc6d5"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCancel(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】撤销合约订单(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_cancel"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#b031d94ff9"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCrossCancel(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】撤销全部合约单(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cancelall"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#78ea900d9a"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCancelAll(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】撤销全部合约单(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_cancelall"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#fe509bd6b7"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCrossCancelAll(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】切换杠杆(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_switch_lever_rate"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#b006b3d147"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapSwitchLeverRate(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】切换杠杆(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_switch_lever_rate"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#477e49bfc2"/></para>
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCrossSwitchLeverRate(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】获取用户的合约订单信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_order_info"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#630c0b679f"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapOrderInfo(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】获取用户的合约订单信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_order_info"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#7fd98f1561"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapCrossOrderInfo(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】获取用户的合约订单明细信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_order_detail"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#1fbc3d0734"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapOrderDetail(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】获取用户的合约订单明细信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_order_detail"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#361d5e2049"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapCrossOrderDetail(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】获取用户的合约当前未成交委托(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_openorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#136259e73a"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapOpenorders(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】获取用户的合约当前未成交委托(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_openorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#49d96ae2a4"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapCrossOpenorders(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】获取用户的合约历史委托(PrivateData)
        /// <para><a href="POST /linear-swap-api/v3/swap_hisorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#419ab006eb"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapHisorders(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】获取用户的合约历史委托(PrivateData)
        /// <para><a href="POST /linear-swap-api/v3/swap_cross_hisorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#c0bfb4f832"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapCrossHisorders(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】组合查询合约历史委托(PrivateData)
        /// <para><a href="POST /linear-swap-api/v3/swap_hisorders_exact"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#af7b8dd177"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapHisordersExact(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】组合查询合约历史委托(PrivateData)
        /// <para><a href="POST /linear-swap-api/v3/swap_cross_hisorders_exact"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#2e2b4d2c3f"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapCrossHisordersExact(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】获取用户的合约历史成交记录(PrivateData)
        /// <para><a href="POST /linear-swap-api/v3/swap_matchresults"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#3d3462611f"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapMatchresults(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】获取用户的合约历史成交记录(PrivateData)
        /// <para><a href="POST /linear-swap-api/v3/swap_cross_matchresults"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#1988305944"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapCrossMatchresults(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】组合查询用户历史成交记录(PrivateData)
        /// <para><a href="POST /linear-swap-api/v3/swap_matchresults_exact"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#a67a844403"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapMatchresultsExact(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】组合查询用户历史成交记录(PrivateData)
        /// <para><a href="POST /linear-swap-api/v3/swap_cross_matchresults_exact"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#f2189d089d"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapCrossMatchresultsExact(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】合约闪电平仓下单(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_lightning_close_position"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#26f085577d"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapLightningClosePosition(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】合约闪电平仓下单(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_lightning_close_position"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#5fa03808b7"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCrossLightningClosePosition(CancellationToken ct = default);
    }
}
