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

namespace Huobi.Net.Interfaces.Clients.UsdtMargined
{
    /// <summary>
    /// 策略接口
    /// </summary>
    public interface IHuobiClientUsdtMarginedApiStrategyOrder
    {
        /// <summary>
        /// 
        /// 【逐仓】合约计划委托下单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_trigger_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#cf8f20d352"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapTriggerOrder(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】合约计划委托下单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_trigger_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#c3f89af0f9"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCrossTriggerOrder(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】合约计划委托撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_trigger_cancel"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#89cf783356"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapTriggerCancel(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】合约计划委托撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_trigger_cancel"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#b4e16d7b11"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCrossTriggerCancel(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】合约计划委托全部撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_trigger_cancelall"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#987871ba1c"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapTriggerCancelAll(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】合约计划委托全部撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_trigger_cancelall"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#eafad70687"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCrossTriggerCancelAll(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】获取计划委托当前委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_trigger_openorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#394154e49c"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapTriggerOpenorders(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】获取计划委托当前委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_trigger_openorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#33b7b627de"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapCrossTriggerOpenorders(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】获取计划委托历史委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_trigger_hisorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#31741caa19"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapTriggerHisorders(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】获取计划委托历史委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_trigger_hisorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#28b32ee4f3"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapCrossTriggerHisorders(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】对仓位设置止盈止损订单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_tpsl_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#54ae54b08d"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapTpslOrder(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】止盈止损订单撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_tpsl_cancel"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#7b41812520"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCrossTpslCancel(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】止盈止损订单撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_tpsl_cancel"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#e02c036026"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapTpslCancel(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】对仓位设置止盈止损订单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_tpsl_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#f71977fef2"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCrossTpslOrder(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】止盈止损订单全部撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_tpsl_cancelall"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#719c8b6331"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapTpslCancelAll(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】止盈止损订单全部撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_tpsl_cancelall"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#01ae20dbbd"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCrossTpslCancelAll(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】止盈止损订单当前委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_tpsl_openorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#6c6fee598c"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapTpslOpenorders(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】止盈止损订单当前委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_tpsl_openorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#9ea73b3922"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapCrossTpslOpenorders(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】止盈止损订单历史委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_tpsl_hisorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#f75074daa4"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapTpslHisorders(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】止盈止损订单历史委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_tpsl_hisorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#52be540948"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapCrossTpslHisorders(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】查询开仓单关联的止盈止损订单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_relation_tpsl_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#dc9f6c768f"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapRelationTpslOrder(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】查询开仓单关联的止盈止损订单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_relation_tpsl_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#f828ac1ff9"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapCrossRelationTpslOrder(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】跟踪委托订单下单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_track_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#58519dfb70"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapTrackOrder(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】跟踪委托订单下单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_track_order"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#5b04228ba8"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCrossTrackOrder(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】跟踪委托订单撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_track_cancel"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#1d86317775"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapTrackCancel(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】跟踪委托订单撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_track_cancel"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#eaf06c20d1"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCrossTrackCancel(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】跟踪委托订单全部撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_track_cancelall"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#92f06221b5"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapTrackCancelAll(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】跟踪委托订单全部撤单(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_track_cancelall"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#9b43c6900c"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapCrossTrackCancelAll(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】跟踪委托订单当前委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_track_openorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#20e8fe0af2"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapTrackOpenOrders(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】跟踪委托订单当前委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_track_openorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#c997c76ebd"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapCrossTrackOpenorders(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 跟踪委托订单历史委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_track_hisorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#466c798453"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapTrackHisorders(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】跟踪委托订单历史委托(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_track_hisorders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#5dc64ede61"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapCrossTrackHisorders(CancellationToken ct = default);
    }
}
