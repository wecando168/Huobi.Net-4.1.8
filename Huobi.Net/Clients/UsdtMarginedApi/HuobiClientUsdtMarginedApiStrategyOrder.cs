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
using Huobi.Net.Interfaces.Clients.UsdtMargined;

namespace Huobi.Net.Clients.UsdtMargined
{
    /// <inheritdoc />
    public class HuobiClientUsdtMarginedApiStrategyOrder : IHuobiClientUsdtMarginedApiStrategyOrder
    {        
        //策略接口
        private const string LinearSwapTriggerOrderEndpoint = "/linear-swap-api/v1/swap_trigger_order";                                     // 【逐仓】合约计划委托下单(PrivateData)
        private const string LinearSwapCrossTriggerOrderEndpoint = "/linear-swap-api/v1/swap_cross_trigger_order";                          // 【全仓】合约计划委托下单(PrivateData)
        private const string LinearSwapTriggerCancelEndpoint = "/linear-swap-api/v1/swap_trigger_cancel";                                   // 【逐仓】合约计划委托撤单(PrivateData)
        private const string LinearSwapCrossTriggerCancelEndpoint = "/linear-swap-api/v1/swap_cross_trigger_cancel";                        // 【全仓】合约计划委托撤单(PrivateData)
        private const string LinearSwapTriggerCancelAllEndpoint = "/linear-swap-api/v1/swap_trigger_cancelall";                             // 【逐仓】合约计划委托全部撤单(PrivateData)
        private const string LinearSwapCrossTriggerCancelAllEndpoint = "/linear-swap-api/v1/swap_cross_trigger_cancelall";                  // 【全仓】合约计划委托全部撤单(PrivateData)
        private const string GetLinearSwapTriggerOpenordersEndpoint = "/linear-swap-api/v1/swap_trigger_openorders";                        // 【逐仓】获取计划委托当前委托(PrivateData)
        private const string GetLinearSwapCrossTriggerOpenordersEndpoint = "/linear-swap-api/v1/swap_cross_trigger_openorders";             // 【全仓】获取计划委托当前委托(PrivateData)
        private const string GetLinearSwapTriggerHisordersEndpoint = "/linear-swap-api/v1/swap_trigger_hisorders";                          // 【逐仓】获取计划委托历史委托(PrivateData)
        private const string GetLinearSwapCrossTriggerHisordersEndpoint = "/linear-swap-api/v1/swap_cross_trigger_hisorders";               // 【全仓】获取计划委托历史委托(PrivateData)
        private const string LinearSwapTpslOrderEndpoint = "/linear-swap-api/v1/swap_tpsl_order";                                           // 【逐仓】对仓位设置止盈止损订单(PrivateData)
        private const string LinearSwapCrossTpslOrderEndpoint = "/linear-swap-api/v1/swap_cross_tpsl_order";                                // 【全仓】对仓位设置止盈止损订单(PrivateData)
        private const string LinearSwapTpslCancelEndpoint = "/linear-swap-api/v1/swap_tpsl_cancel";                                         // 【逐仓】止盈止损订单撤单(PrivateData)
        private const string LinearSwapCrossTpslCancelEndpoint = "/linear-swap-api/v1/swap_cross_tpsl_cancel";                              // 【全仓】止盈止损订单撤单(PrivateData)
        private const string LinearSwapTpslCancelAllEndpoint = "/linear-swap-api/v1/swap_tpsl_cancelall";                                   // 【逐仓】止盈止损订单全部撤单(PrivateData)
        private const string LinearSwapCrossTpslCancelallEndpoint = "/linear-swap-api/v1/swap_cross_tpsl_cancelall";                        // 【全仓】止盈止损订单全部撤单(PrivateData)
        private const string GetLinearSwapTpslOpenordersEndpoint = "/linear-swap-api/v1/swap_tpsl_openorders";                              // 【逐仓】止盈止损订单当前委托(PrivateData)
        private const string GetLinearSwapCrossTpslOpenordersEndpoint = "/linear-swap-api/v1/swap_cross_tpsl_openorders";                   // 【全仓】止盈止损订单当前委托(PrivateData)
        private const string GetLinearSwapTpslHisordersEndpoint = "/linear-swap-api/v1/swap_tpsl_hisorders";                                // 【逐仓】止盈止损订单历史委托(PrivateData)
        private const string GetLinearSwapCrossTpslHisordersEndpoint = "/linear-swap-api/v1/swap_cross_tpsl_hisorders";                     // 【全仓】止盈止损订单历史委托(PrivateData)
        private const string GetLinearSwapRelationTpslOrderEndpoint = "/linear-swap-api/v1/swap_relation_tpsl_order";                       // 【逐仓】查询开仓单关联的止盈止损订单(PrivateData)        
        private const string GetLinearSwapCrossRelationTpslOrderEndpoint = "/linear-swap-api/v1/swap_cross_relation_tpsl_order";            // 【全仓】查询开仓单关联的止盈止损订单(PrivateData)
        private const string LinearSwapTrackOrderEndpoint = "/linear-swap-api/v1/swap_track_order";                                         // 【逐仓】跟踪委托订单下单(PrivateData)
        private const string LinearSwapCrossTrackOrderEndpoint = "/linear-swap-api/v1/swap_cross_track_order";                              // 【全仓】跟踪委托订单下单(PrivateData)
        private const string LinearSwapTrackCancelEndpoint = "/linear-swap-api/v1/swap_track_cancel";                                       // 【逐仓】跟踪委托订单撤单(PrivateData)
        private const string LinearSwapCrossTrackCancelEndpoint = "/linear-swap-api/v1/swap_cross_track_cancel";                            // 【全仓】跟踪委托订单撤单(PrivateData)
        private const string LinearSwapTrackCancelAllEndpoint = "/linear-swap-api/v1/swap_track_cancelall";                                 // 【逐仓】跟踪委托订单全部撤单(PrivateData)
        private const string LinearSwapCrossTrackCancelAllEndpoint = "/linear-swap-api/v1/swap_cross_track_cancelall";                      // 【全仓】跟踪委托订单全部撤单(PrivateData)
        private const string GetLinearSwapTrackOpenOrdersEndpoint = "/linear-swap-api/v1/swap_track_openorders";                            // 【逐仓】跟踪委托订单当前委托(PrivateData)
        private const string GetLinearSwapCrossTrackOpenordersEndpoint = "/linear-swap-api/v1/swap_cross_track_openorders";                 // 【全仓】跟踪委托订单当前委托(PrivateData)
        private const string GetLinearSwapTrackHisordersEndpoint = "/linear-swap-api/v1/swap_track_hisorders";                              // 【逐仓】跟踪委托订单历史委托(PrivateData)        
        private const string GetLinearSwapCrossTrackHisordersEndpoint = "/linear-swap-api/v1/swap_cross_track_hisorders";                   // 【全仓】跟踪委托订单历史委托(PrivateData)

        private readonly HuobiClientUsdtMarginedApi _baseClient;

        internal HuobiClientUsdtMarginedApiStrategyOrder(HuobiClientUsdtMarginedApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapCrossRelationTpslOrder(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapCrossTpslHisorders(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapCrossTpslOpenorders(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapCrossTrackHisorders(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapCrossTrackOpenorders(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapCrossTriggerHisorders(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapCrossTriggerOpenorders(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapRelationTpslOrder(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapTpslHisorders(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapTpslOpenorders(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapTrackHisorders(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapTrackOpenOrders(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapTriggerHisorders(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapTriggerOpenorders(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCrossTpslCancel(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCrossTpslCancelAll(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCrossTpslOrder(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCrossTrackCancel(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCrossTrackCancelAll(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCrossTrackOrder(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCrossTriggerCancel(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCrossTriggerCancelAll(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCrossTriggerOrder(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapTpslCancel(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapTpslCancelAll(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapTpslOrder(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapTrackCancel(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapTrackCancelAll(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapTrackOrder(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapTriggerCancel(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapTriggerCancelAll(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapTriggerOrder(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
