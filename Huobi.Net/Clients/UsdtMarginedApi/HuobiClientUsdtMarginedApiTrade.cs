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
using CryptoExchange.Net.CommonObjects;
using Huobi.Net.Interfaces.Clients.UsdtMargined;

namespace Huobi.Net.Clients.UsdtMargined
{
    /// <inheritdoc />
    public class HuobiClientUsdtMarginedApiTrade : IHuobiClientUsdtMarginedApiTrade
    {
        //交易接口
        private const string GetLinearSwapCrossTradeStateEndpoint = "/linear-swap-api/v1/swap_cross_trade_state";                           // 【全仓】查询系统交易权限(PublicData)
        private const string LinearSwapSwitchPositionModeEndpoint = "/linear-swap-api/v1/swap_switch_position_mode";                        // 【逐仓】切换持仓模式(PrivateData)
        private const string LinearSwapCrossSwitchPositionModeEndpoint = "/linear-swap-api/v1/swap_cross_switch_position_mode";             // 【全仓】切换持仓模式(PrivateData)
        private const string LinearSwapOrderEndpoint = "/linear-swap-api/v1/swap_order";                                                    // 【逐仓】合约下单(PrivateData)
        private const string LinearSwapCrossOrderEndpoint = "/linear-swap-api/v1/swap_cross_order";                                         // 【全仓】合约下单(PrivateData)
        private const string LinearSwapBatchorderEndpoint = "/linear-swap-api/v1/swap_batchorder";                                          // 【逐仓】合约批量下单(PrivateData)
        private const string LinearSwapCrossBatchorderEndpoint = "/linear-swap-api/v1/swap_cross_batchorder";                               // 【全仓】合约批量下单(PrivateData)
        private const string LinearSwapCancelEndpoint = "/linear-swap-api/v1/swap_cancel";                                                  // 【逐仓】撤销合约订单(PrivateData)
        private const string LinearSwapCrossCancelEndpoint = "/linear-swap-api/v1/swap_cross_cancel";                                       // 【全仓】撤销合约订单(PrivateData)       
        private const string LinearSwapCancelAllEndpoint = "/linear-swap-api/v1/swap_cancelall";                                            // 【逐仓】撤销全部合约单(PrivateData)
        private const string LinearSwapCrossCancelAllEndpoint = "/linear-swap-api/v1/swap_cross_cancelall";                                 // 【全仓】撤销全部合约单(PrivateData)
        private const string LinearSwapSwitchLeverRateEndpoint = "/linear-swap-api/v1/swap_switch_lever_rate";                              // 【逐仓】切换杠杆(PrivateData)
        private const string LinearSwapCrossSwitchLeverRateEndpoint = "/linear-swap-api/v1/swap_cross_switch_lever_rate";                   // 【全仓】切换杠杆(PrivateData)
        private const string GetLinearSwapOrderInfoEndpoint = "/linear-swap-api/v1/swap_order_info";                                        // 【逐仓】获取用户的合约订单信息(PrivateData)
        private const string GetLinearSwapCrossOrderInfoEndpoint = "/linear-swap-api/v1/swap_cross_order_info";                             // 【全仓】获取用户的合约订单信息(PrivateData)
        private const string GetLinearSwapOrderDetailEndpoint = "/linear-swap-api/v1/swap_order_detail";                                    // 【逐仓】获取用户的合约订单明细信息(PrivateData)
        private const string GetLinearSwapCrossOrderDetailEndpoint = "/linear-swap-api/v1/swap_cross_order_detail";                         // 【全仓】获取用户的合约订单明细信息(PrivateData)
        private const string GetLinearSwapOpenordersEndpoint = "/linear-swap-api/v1/swap_openorders";                                       // 【逐仓】获取用户的合约当前未成交委托(PrivateData)
        private const string GetLinearSwapCrossOpenordersEndpoint = "/linear-swap-api/v1/swap_cross_openorders";                            // 【全仓】获取用户的合约当前未成交委托(PrivateData)
        private const string GetLinearSwapHisordersEndpoint = "/linear-swap-api/v3/swap_hisorders";                                         // 【逐仓】获取用户的合约历史委托(PrivateData)
        private const string GetLinearSwapCrossHisordersEndpoint = "/linear-swap-api/v3/swap_cross_hisorders";                              // 【全仓】获取用户的合约历史委托(PrivateData)
        private const string GetLinearSwapHisordersExactEndpoint = "/linear-swap-api/v3/swap_hisorders_exact";                              // 【逐仓】组合查询合约历史委托(PrivateData)
        private const string GetLinearSwapCrossHisordersExactEndpoint = "/linear-swap-api/v3/swap_cross_hisorders_exact";                   // 【全仓】组合查询合约历史委托(PrivateData)
        private const string GetLinearSwapMatchresultsEndpoint = "/linear-swap-api/v3/swap_matchresults";                                   // 【逐仓】获取用户的合约历史成交记录(PrivateData)
        private const string GetLinearSwapCrossMatchresultsEndpoint = "/linear-swap-api/v3/swap_cross_matchresults";                        // 【全仓】获取用户的合约历史成交记录(PrivateData)
        private const string GetLinearSwapMatchresultsExactEndpoint = "/linear-swap-api/v3/swap_matchresults_exact";                        // 【逐仓】组合查询用户历史成交记录(PrivateData)
        private const string GetLinearSwapCrossMatchresultsExactEndpoint = "/linear-swap-api/v3/swap_cross_matchresults_exact";             // 【全仓】组合查询用户历史成交记录(PrivateData)
        private const string LinearSwapLightningClosePositionEndpoint = "/linear-swap-api/v1/swap_lightning_close_position";                // 【逐仓】合约闪电平仓下单(PrivateData)
        private const string LinearSwapCrossLightningClosePositionEndpoint = "/linear-swap-api/v1/swap_cross_lightning_close_position";     // 【全仓】合约闪电平仓下单(PrivateData)
        

        private readonly HuobiClientUsdtMarginedApi _baseClient;

        internal HuobiClientUsdtMarginedApiTrade(HuobiClientUsdtMarginedApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapCrossHisorders(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapCrossHisordersExact(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapCrossMatchresults(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapCrossMatchresultsExact(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapCrossOpenorders(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapCrossOrderDetail(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapCrossOrderInfo(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapCrossTradeState(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapHisorders(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapHisordersExact(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapMatchresults(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapMatchresultsExact(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapOpenorders(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapOrderDetail(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> GetLinearSwapOrderInfo(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapBatchorder(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCancel(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCancelAll(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCrossBatchorder(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCrossCancel(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCrossCancelAll(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCrossLightningClosePosition(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCrossOrder(string contractCode, UmDirection direction, UmOffset offset, decimal price, UmLeverRate leverRate, long volume, UmOrderPriceType orderPriceType, decimal tpTriggerPrice, decimal tpOrderPrice, UmTpOrderPriceType tpOrderPriceType, decimal slTriggerPrice, decimal slOrderPrice, UmSlOrderPriceType slOrderPriceType, string? accountId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCrossSwitchLeverRate(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapCrossSwitchPositionMode(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapLightningClosePosition(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapOrder(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapSwitchLeverRate(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<WebCallResult<IEnumerable<object>>> LinearSwapSwitchPositionMode(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
