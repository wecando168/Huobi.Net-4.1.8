using CryptoExchange.Net;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapTrade;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapTrade.Request;
using Huobi.Net.Objects.Models.UsdtMarginSwap;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Huobi.Net.Clients.UsdtMarginSwapApi
{
    /// <inheritdoc />
    public class HuobiClientUsdtMarginSwapApiTrading : IHuobiClientUsdtMarginSwapApiTrading
    {
        //交易接口
        private const string GetLinearSwapCrossTradeStateEndpoint = "/swap_cross_trade_state";                           // 【全仓】查询系统交易权限(PublicData)
        private const string LinearSwapSwitchPositionModeEndpoint = "/swap_switch_position_mode";                        // 【逐仓】切换持仓模式(PrivateData)
        private const string LinearSwapCrossSwitchPositionModeEndpoint = "/swap_cross_switch_position_mode";             // 【全仓】切换持仓模式(PrivateData)
        private const string LinearSwapOrderEndpoint = "/swap_order";                                                    // 【逐仓】合约下单(PrivateData)
        private const string LinearSwapCrossOrderEndpoint = "/swap_cross_order";                                         // 【全仓】合约下单(PrivateData)
        private const string LinearSwapBatchorderEndpoint = "/swap_batchorder";                                          // 【逐仓】合约批量下单(PrivateData)
        private const string LinearSwapCrossBatchorderEndpoint = "/swap_cross_batchorder";                               // 【全仓】合约批量下单(PrivateData)
        private const string LinearSwapCancelEndpoint = "/swap_cancel";                                                  // 【逐仓】撤销合约订单(PrivateData)
        private const string LinearSwapCrossCancelEndpoint = "/swap_cross_cancel";                                       // 【全仓】撤销合约订单(PrivateData)       
        private const string LinearSwapCancelAllEndpoint = "/swap_cancelall";                                            // 【逐仓】撤销全部合约单(PrivateData)
        private const string LinearSwapCrossCancelAllEndpoint = "/swap_cross_cancelall";                                 // 【全仓】撤销全部合约单(PrivateData)
        private const string LinearSwapSwitchLeverRateEndpoint = "/swap_switch_lever_rate";                              // 【逐仓】切换杠杆(PrivateData)
        private const string LinearSwapCrossSwitchLeverRateEndpoint = "/swap_cross_switch_lever_rate";                   // 【全仓】切换杠杆(PrivateData)
        private const string GetLinearSwapOrderInfoEndpoint = "/swap_order_info";                                        // 【逐仓】获取用户的合约订单信息(PrivateData)
        private const string GetLinearSwapCrossOrderInfoEndpoint = "/swap_cross_order_info";                             // 【全仓】获取用户的合约订单信息(PrivateData)
        private const string GetLinearSwapOrderDetailEndpoint = "/swap_order_detail";                                    // 【逐仓】获取用户的合约订单明细信息(PrivateData)
        private const string GetLinearSwapCrossOrderDetailEndpoint = "/swap_cross_order_detail";                         // 【全仓】获取用户的合约订单明细信息(PrivateData)
        private const string GetLinearSwapOpenordersEndpoint = "/swap_openorders";                                       // 【逐仓】获取用户的合约当前未成交委托(PrivateData)
        private const string GetLinearSwapCrossOpenordersEndpoint = "/swap_cross_openorders";                            // 【全仓】获取用户的合约当前未成交委托(PrivateData)
        private const string GetLinearSwapHisordersEndpoint = "/swap_hisorders";                                         // 【逐仓】获取用户的合约历史委托(PrivateData)
        private const string GetLinearSwapCrossHisordersEndpoint = "/swap_cross_hisorders";                              // 【全仓】获取用户的合约历史委托(PrivateData)
        private const string GetLinearSwapHisordersExactEndpoint = "/swap_hisorders_exact";                              // 【逐仓】组合查询合约历史委托(PrivateData)
        private const string GetLinearSwapCrossHisordersExactEndpoint = "/swap_cross_hisorders_exact";                   // 【全仓】组合查询合约历史委托(PrivateData)
        private const string GetLinearSwapMatchresultsEndpoint = "/swap_matchresults";                                   // 【逐仓】获取用户的合约历史成交记录(PrivateData)
        private const string GetLinearSwapCrossMatchresultsEndpoint = "/swap_cross_matchresults";                        // 【全仓】获取用户的合约历史成交记录(PrivateData)
        private const string GetLinearSwapMatchresultsExactEndpoint = "/swap_matchresults_exact";                        // 【逐仓】组合查询用户历史成交记录(PrivateData)
        private const string GetLinearSwapCrossMatchresultsExactEndpoint = "/swap_cross_matchresults_exact";             // 【全仓】组合查询用户历史成交记录(PrivateData)
        private const string LinearSwapLightningClosePositionEndpoint = "/swap_lightning_close_position";                // 【逐仓】合约闪电平仓下单(PrivateData)
        private const string LinearSwapCrossLightningClosePositionEndpoint = "/swap_cross_lightning_close_position";     // 【全仓】合约闪电平仓下单(PrivateData)

        private readonly HuobiClientUsdtMarginSwapApi _baseClient;

        internal HuobiClientUsdtMarginSwapApiTrading(HuobiClientUsdtMarginSwapApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiPlacedOrderId>> PlaceIsolatedMarginOrderAsync(
            string contractCode,
            decimal quantity,
            OrderSide side,
            decimal leverageRate,
            decimal? price = null,
            Offset? offset = null,
            OrderPriceType? orderPriceType = null,
            decimal? takeProfitTriggerPrice = null,
            decimal? takeProfitOrderPrice = null,
            OrderPriceType? takeProfitOrderPriceType = null,
            decimal? stopLossTriggerPrice = null,
            decimal? stopLossOrderPrice = null,
            OrderPriceType? stopLossOrderPriceType = null,
            bool? reduceOnly = null,
            long? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "volume", quantity.ToString(CultureInfo.InvariantCulture) },
                { "direction", EnumConverter.GetString(side) },
                { "lever_rate", leverageRate }
            };
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("offset", EnumConverter.GetString(offset));
            parameters.AddOptionalParameter("order_price_type", EnumConverter.GetString(orderPriceType));
            parameters.AddOptionalParameter("tp_trigger_price", takeProfitTriggerPrice);
            parameters.AddOptionalParameter("tp_order_price", takeProfitOrderPrice);
            parameters.AddOptionalParameter("tp_order_price_type", EnumConverter.GetString(takeProfitOrderPriceType));
            parameters.AddOptionalParameter("sl_trigger_price", stopLossTriggerPrice);
            parameters.AddOptionalParameter("sl_order_price", stopLossOrderPrice);
            parameters.AddOptionalParameter("sl_order_price_type", EnumConverter.GetString(stopLossOrderPriceType));
            parameters.AddOptionalParameter("reduce_only", reduceOnly == null ? null : reduceOnly.Value ? "1" : "0");
            parameters.AddOptionalParameter("client_order_id", clientOrderId);

            return await _baseClient.SendHuobiRequest<HuobiPlacedOrderId>(_baseClient.GetUrl("/linear-swap-api/v1/swap_order"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiPlacedOrderId>> PlaceCrossMarginOrderAsync(
            decimal quantity,
            OrderSide side,
            decimal leverageRate,
            string? contractCode = null,
            string? symbol = null,
            ContractType? contractType = null,
            decimal? price = null,
            Offset? offset = null,
            OrderPriceType? orderPriceType = null,
            decimal? takeProfitTriggerPrice = null,
            decimal? takeProfitOrderPrice = null,
            OrderPriceType? takeProfitOrderPriceType = null,
            decimal? stopLossTriggerPrice = null,
            decimal? stopLossOrderPrice = null,
            OrderPriceType? stopLossOrderPriceType = null,
            bool? reduceOnly = null,
            long? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "volume", quantity.ToString(CultureInfo.InvariantCulture) },
                { "direction", EnumConverter.GetString(side) },
                { "lever_rate", leverageRate }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("offset", EnumConverter.GetString(offset));
            parameters.AddOptionalParameter("order_price_type", EnumConverter.GetString(orderPriceType));
            parameters.AddOptionalParameter("tp_trigger_price", takeProfitTriggerPrice);
            parameters.AddOptionalParameter("tp_order_price", takeProfitOrderPrice);
            parameters.AddOptionalParameter("tp_order_price_type", EnumConverter.GetString(takeProfitOrderPriceType));
            parameters.AddOptionalParameter("sl_trigger_price", stopLossTriggerPrice);
            parameters.AddOptionalParameter("sl_order_price", stopLossOrderPrice);
            parameters.AddOptionalParameter("sl_order_price_type", EnumConverter.GetString(stopLossOrderPriceType));
            parameters.AddOptionalParameter("reduce_only", reduceOnly == null ? null : reduceOnly.Value ? "1" : "0");
            parameters.AddOptionalParameter("client_order_id", clientOrderId);

            return await _baseClient.SendHuobiRequest<HuobiPlacedOrderId>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_order"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiBatchResult>> CancelIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            return await _baseClient.SendHuobiRequest<HuobiBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cancel"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiBatchResult>> CancelIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderId, IEnumerable<long> clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "order_id", string.Join(",", orderId) },
                { "client_order_id", string.Join(",", clientOrderId) }
            };
            return await _baseClient.SendHuobiRequest<HuobiBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cancel"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiBatchResult>> CancelCrossMarginOrderAsync(long? orderId = null, long? clientOrderId = null, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            return await _baseClient.SendHuobiRequest<HuobiBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_cancel"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiBatchResult>> CancelCrossMarginOrdersAsync(IEnumerable<long> orderId, IEnumerable<long> clientOrderId, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "order_id", string.Join(",", orderId) },
                { "client_order_id", string.Join(",", clientOrderId) }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            return await _baseClient.SendHuobiRequest<HuobiBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_cancel"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiBatchResult>> CancelAllIsolatedMarginOrdersAsync(string contractCode, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("direction", EnumConverter.GetString(side));
            parameters.AddOptionalParameter("offset", EnumConverter.GetString(offset));
            return await _baseClient.SendHuobiRequest<HuobiBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cancelall"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiBatchResult>> CancelAllCrossMarginOrdersAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, OrderSide? side = null, Offset? offset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("direction", EnumConverter.GetString(side));
            parameters.AddOptionalParameter("offset", EnumConverter.GetString(offset));
            return await _baseClient.SendHuobiRequest<HuobiBatchResult>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_cancelall"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiIsolatedMarginLeverageRate>> ChangeIsolatedMarginLeverageAsync(string contractCode, int leverageRate, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "lever_rate", leverageRate },
            };
            return await _baseClient.SendHuobiRequest<HuobiIsolatedMarginLeverageRate>(_baseClient.GetUrl("/linear-swap-api/v1/swap_switch_lever_rate"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiCrossMarginLeverageRate>> ChangeCrossMarginLeverageAsync(int leverageRate, string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "lever_rate", leverageRate },
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));

            return await _baseClient.SendHuobiRequest<HuobiCrossMarginLeverageRate>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_switch_lever_rate"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiIsolatedMarginOrder>>> GetIsolatedMarginOrderAsync(string contractCode, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiIsolatedMarginOrder>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_order_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiIsolatedMarginOrder>>> GetIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("order_id", string.Join(",", orderIds));
            parameters.AddOptionalParameter("client_order_id", string.Join(",", clientOrderIds));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiIsolatedMarginOrder>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_order_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiCrossMarginOrder>>> GetCrossMarginOrderAsync(string? contractCode = null, string? symbol = null, long? orderId = null, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("order_id", orderId);
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiCrossMarginOrder>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_order_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiCrossMarginOrder>>> GetCrossMarginOrdersAsync(IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, string? contractCode = null, string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("order_id", string.Join(",", orderIds));
            parameters.AddOptionalParameter("client_order_id", string.Join(",", clientOrderIds));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiCrossMarginOrder>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_order_info"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiMarginOrderDetails>> GetIsolatedMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "order_id", orderId }
            };
            return await _baseClient.SendHuobiRequest<HuobiMarginOrderDetails>(_baseClient.GetUrl("/linear-swap-api/v1/swap_order_detail"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiMarginOrderDetails>> GetCrossMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "order_id", orderId }
            };
            return await _baseClient.SendHuobiRequest<HuobiMarginOrderDetails>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_order_detail"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiIsolatedMarginOrderPage>> GetIsolatedMarginOpenOrdersAsync(string contractCode, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            parameters.AddOptionalParameter("trade_type", EnumConverter.GetString(tradeType));
            return await _baseClient.SendHuobiRequest<HuobiIsolatedMarginOrderPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_openorders"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiCrossMarginOrderPage>> GetCrossMarginOpenOrdersAsync(string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, string? sortBy = null, MarginTradeType? tradeType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            parameters.AddOptionalParameter("trade_type", EnumConverter.GetString(tradeType));
            return await _baseClient.SendHuobiRequest<HuobiCrossMarginOrderPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_openorders"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiIsolatedMarginOrderPage>> GetIsolatedMarginClosedOrdersAsync(string contractCode, MarginTradeType tradeType, bool allOrders, int daysInHistory, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "trade_type", EnumConverter.GetString(tradeType) },
                { "type", allOrders ? "1": "2" },
                { "create_date", daysInHistory },
                { "status", "0" }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            return await _baseClient.SendHuobiRequest<HuobiIsolatedMarginOrderPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_hisorders"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiCrossMarginOrderPage>> GetCrossMarginClosedOrdersAsync(MarginTradeType tradeType, bool allOrders, int daysInHistory, string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, string? sortBy = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "trade_type", EnumConverter.GetString(tradeType) },
                { "type", allOrders ? "1": "2" },
                { "create_date", daysInHistory },
                { "status", "0" }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            return await _baseClient.SendHuobiRequest<HuobiCrossMarginOrderPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_hisorders"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiIsolatedMarginUserTradePage>> GetIsolatedMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, int daysInHistory, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "trade_type", EnumConverter.GetString(tradeType) },
                { "create_date", daysInHistory },
                { "status", "0" }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiIsolatedMarginUserTradePage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_matchresults"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiCrossMarginUserTradePage>> GetCrossMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, int daysInHistory, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "trade_type", EnumConverter.GetString(tradeType) },
                { "create_date", daysInHistory },
                { "status", "0" }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiCrossMarginUserTradePage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_matchresults"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapCrossTradeState>>> GetLinearSwapCrossTradeStateAsync(string? contractCode = null, string? Pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", Pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("business_type", businessType);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketSwapCrossTradeState>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossTradeStateEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapIsolatedSwitchPositionMode>> LinearSwapSwitchPositionModeAsync(string marginAccount, string positionMode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"margin_account", marginAccount },
                {"position_mode", positionMode }
            };

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapIsolatedSwitchPositionMode>(
                uri: _baseClient.GetUrl(LinearSwapSwitchPositionModeEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapCrossSwitchPositionMode>> LinearSwapCrossSwitchPositionModeAsync(string marginAccount, string positionMode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"margin_account", marginAccount },
                {"position_mode", positionMode }
            };

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapCrossSwitchPositionMode>(
                uri: _baseClient.GetUrl(LinearSwapCrossSwitchPositionModeEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapIsolatedOrder>> LinearSwapOrderAsync(
            string contractCode,
            UmDirection direction,
            UmOffset? offset,
            decimal? price,
            UmLeverRate leverRate,
            long volume,
            UmOrderPriceType orderPriceType,
            decimal? tpTriggerPrice,
            decimal? tpOrderPrice,
            UmOrderPriceType? tpOrderPriceType,
            decimal? slTriggerPrice,
            decimal? slOrderPrice,
            UmOrderPriceType? slOrderPriceType,
            int? reduceOnly = null,
            long? clientOrderId = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
                {"volume", volume },
                {"direction", direction.ToString() },
                {"lever_rate", leverRate.GetHashCode() },
                {"order_price_type", orderPriceType.ToString() }
            };
            parameters.AddOptionalParameter("reduce_only", reduceOnly == null ? null : reduceOnly.ToString());
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            parameters.AddOptionalParameter("price", price);
            parameters.AddOptionalParameter("offset", offset.ToString());
            parameters.AddOptionalParameter("tp_trigger_price", tpTriggerPrice);
            parameters.AddOptionalParameter("tp_order_price", tpOrderPrice);
            parameters.AddOptionalParameter("tp_order_price_type", tpOrderPriceType == null ? null : tpOrderPriceType.ToString());
            parameters.AddOptionalParameter("sl_trigger_price", slTriggerPrice);
            parameters.AddOptionalParameter("sl_order_price", slOrderPrice);
            parameters.AddOptionalParameter("sl_order_price_type", slOrderPriceType == null ? null : slOrderPriceType.ToString());

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapIsolatedOrder>(
                uri: _baseClient.GetUrl(LinearSwapOrderEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapCrossOrder>> LinearSwapCrossOrderAsync(
            string contractCode,
            UmDirection direction,
            UmOffset? offset,
            decimal? price,
            UmLeverRate? leverRate,
            long volume,
            UmOrderPriceType orderPriceType,
            decimal? tpTriggerPrice,
            decimal? tpOrderPrice,
            UmOrderPriceType? tpOrderPriceType,
            decimal? slTriggerPrice,
            decimal? slOrderPrice,
            UmOrderPriceType? slOrderPriceType,
            string? pair = null,
            UmContractType? contractType = null,
            UmReduceOnly? reduceOnly = null,
            long? clientOrderId = null,
            CancellationToken ct = default
            )
        {
            var priceTypeConverter = new WWTOrderPriceTypeConverter(false);
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
                {"volume", volume },
                {"direction", EnumConverter.GetString(direction) },
                {"lever_rate", leverRate.GetHashCode() },
                {"order_price_type", EnumConverter.GetString(orderPriceType)}
            };
            if (!object.Equals(pair, null) && !object.Equals(contractType, null) && object.Equals(contractCode, null))
            {
                parameters.AddOptionalParameter("pair", pair);
                parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            }
            parameters.AddOptionalParameter("reduce_only", reduceOnly.GetHashCode());
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            parameters.AddOptionalParameter("price", price);
            parameters.AddOptionalParameter("offset", EnumConverter.GetString(offset));
            if (!object.Equals(tpTriggerPrice, null) && !object.Equals(tpOrderPrice, null) && !object.Equals(tpOrderPriceType, null))
            {
                parameters.AddOptionalParameter("tp_trigger_price", tpTriggerPrice);
                parameters.AddOptionalParameter("tp_order_price", tpOrderPrice);
                parameters.AddOptionalParameter("tp_order_price_type", EnumConverter.GetString(tpOrderPriceType));
            }
            if (!object.Equals(slTriggerPrice, null) && !object.Equals(slOrderPrice, null) && !object.Equals(slOrderPriceType, null))
            {
                parameters.AddOptionalParameter("sl_trigger_price", slTriggerPrice);
                parameters.AddOptionalParameter("sl_order_price", slOrderPrice);
                parameters.AddOptionalParameter("sl_order_price_type", EnumConverter.GetString(slOrderPriceType));
            }
            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapCrossOrder>(
                uri: _baseClient.GetUrl(LinearSwapCrossOrderEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapIsolatedBatchOrder>> LinearSwapBatchorderAsync(IEnumerable<WWTHuobiUsdtMarginedIsolatedOrder> huobiUsdtMarginedIsolatedOrderList, CancellationToken ct = default)
        {
            List<object> isolatedOrderDataList = new List<object>();
            foreach (var item in huobiUsdtMarginedIsolatedOrderList)
            {
                isolatedOrderDataList.Add(item);
            }

            var parameters = new Dictionary<string, object>
            {
                {"orders_data", isolatedOrderDataList },
            };

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapIsolatedBatchOrder>(
                uri: _baseClient.GetUrl(LinearSwapBatchorderEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapCrossBatchOrder>> LinearSwapCrossBatchorderAsync(IEnumerable<WWTHuobiUsdtMarginedCrossOrder> huobiUsdtMarginedCrossOrderList, CancellationToken ct = default)
        {
            List<object> crossOrderDataList = new List<object>();
            foreach (var item in huobiUsdtMarginedCrossOrderList)
            {
                crossOrderDataList.Add(item);
            }

            var parameters = new Dictionary<string, object>
            {
                {"orders_data", crossOrderDataList },
            };

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapCrossBatchOrder>(
                uri: _baseClient.GetUrl(LinearSwapCrossBatchorderEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapIsolatedCancel>> LinearSwapCancelAsync(string contractCode, IEnumerable<long>? orderIdList = null, IEnumerable<long>? clientOrderIdList = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
            };
            if (orderIdList != null && orderIdList.Any() && orderIdList.Count() <= 10)
            {
                parameters.AddOptionalParameter("order_id", string.Join(",", orderIdList));
            }
            else if ((orderIdList == null || orderIdList.Count() == 0) && clientOrderIdList != null && clientOrderIdList.Any() && clientOrderIdList.Count() <= 10)
            {
                parameters.AddOptionalParameter("client_order_id", string.Join(",", clientOrderIdList));
            }
            else
            {
                throw new InvalidOperationException("order_id or client_order_id count must be greater than 0 and less than or equal to 10");
            }

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapIsolatedCancel>(
                uri: _baseClient.GetUrl(LinearSwapCancelEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapCrossCancel>> LinearSwapCrossCancelAsync(string contractCode, IEnumerable<long>? orderIdList = null, IEnumerable<long>? clientOrderIdList = null, string? pair = null, string? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
            };
            if (orderIdList != null && orderIdList.Any() && orderIdList.Count() <= 10)
            {
                parameters.AddOptionalParameter("order_id", string.Join(",", orderIdList));
            }
            else if ((orderIdList == null || orderIdList.Count() == 0) && clientOrderIdList != null && clientOrderIdList.Any() && clientOrderIdList.Count() <= 10)
            {
                parameters.AddOptionalParameter("client_order_id", string.Join(",", clientOrderIdList));
            }
            else
            {
                throw new InvalidOperationException("order_id or client_order_id count must be greater than 0 and less than or equal to 10");
            }
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapCrossCancel>(
                uri: _baseClient.GetUrl(LinearSwapCrossCancelEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapIsolatedCancel>> LinearSwapCancelAllAsync(string contractCode, string? direction = null, string? offset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
            };
            parameters.AddOptionalParameter("direction", direction);
            parameters.AddOptionalParameter("offset", offset);

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapIsolatedCancel>(
                uri: _baseClient.GetUrl(LinearSwapCancelAllEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapCrossCancel>> LinearSwapCrossCancelAllAsync(string contractCode, string? pair = null, string? contractType = null, string? direction = null, string? offset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
            };
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("direction", direction);
            parameters.AddOptionalParameter("offset", offset);

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapCrossCancel>(
                uri: _baseClient.GetUrl(LinearSwapCrossCancelAllEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapIsolatedSwitchLeverRate>> LinearSwapSwitchLeverRateAsync(string contractCode, int leverRate, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
                {"lever_rate", leverRate },
            };

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapIsolatedSwitchLeverRate>(
                uri: _baseClient.GetUrl(LinearSwapSwitchLeverRateEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapCrossSwitchLeverRate>> LinearSwapCrossSwitchLeverRateAsync(string contractCode, int leverRate, string? pair = null, string? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
                {"lever_rate", leverRate },
            };
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapCrossSwitchLeverRate>(
                uri: _baseClient.GetUrl(LinearSwapCrossSwitchLeverRateEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapIsolatedOrderInfo>>> GetLinearSwapOrderInfoAsync(string contractCode, IEnumerable<long>? orderIdList = null, IEnumerable<long>? clientOrderIdList = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode }
            };

            if (orderIdList != null && orderIdList.Any() && orderIdList.Count() <= 50)
            {
                parameters.AddOptionalParameter("order_id", string.Join(",", orderIdList));
            }
            else if ((orderIdList == null || orderIdList.Count() == 0) && clientOrderIdList != null && clientOrderIdList.Any() && clientOrderIdList.Count() <= 50)
            {
                parameters.AddOptionalParameter("client_order_id", string.Join(",", clientOrderIdList));
            }
            else
            {
                throw new InvalidOperationException("order_id or client_order_id count must be greater than 0 and less than or equal to 50");
            }

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketSwapIsolatedOrderInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapOrderInfoEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapCrossOrderInfo>>> GetLinearSwapCrossOrderInfoAsync(string? contractCode = null, IEnumerable<long>? orderIdList = null, IEnumerable<long>? clientOrderIdList = null, string? pair = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            if (!string.IsNullOrWhiteSpace(contractCode))
            {
                parameters.AddOptionalParameter("contract_code", contractCode);
            }
            else if (string.IsNullOrWhiteSpace(contractCode) && !string.IsNullOrWhiteSpace(pair))
            {
                parameters.AddOptionalParameter("pair", pair);
            }
            else
            {
                throw new InvalidOperationException("Parameters contract_code and pair must have at least one");
            }

            if (orderIdList != null && orderIdList.Any() && orderIdList.Count() <= 50)
            {
                parameters.AddOptionalParameter("order_id", string.Join(",", orderIdList));
            }
            else if ((orderIdList == null || orderIdList.Count() == 0) && clientOrderIdList != null && clientOrderIdList.Any() && clientOrderIdList.Count() <= 50)
            {
                parameters.AddOptionalParameter("client_order_id", string.Join(",", clientOrderIdList));
            }
            else
            {
                throw new InvalidOperationException("order_id or client_order_id count must be greater than 0 and less than or equal to 50");
            }

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketSwapCrossOrderInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossOrderInfoEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapIsolatedOrderDetail>> GetLinearSwapOrderDetailAsync(
            long orderId,
            string contractCode,
            long? createdTimestamp = null,
            int? orderType = null,
            int? pageIndex = null,
            int? pageSize = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                { "order_id",orderId },
                { "contract_code",contractCode }
            };
            parameters.AddOptionalParameter("created_at", createdTimestamp);
            parameters.AddOptionalParameter("order_type", orderType);
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapIsolatedOrderDetail>(
                uri: _baseClient.GetUrl(GetLinearSwapOrderDetailEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapCrossOrderDetail>> GetLinearSwapCrossOrderDetailAsync(
            long orderId,
            string? contractCode = null,
            string? pair = null,
            long? createdTimestamp = null,
            int? orderType = null,
            int? pageIndex = null,
            int? pageSize = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                { "order_id",orderId },
            };
            if (!string.IsNullOrWhiteSpace(contractCode))
            {
                parameters.AddOptionalParameter("contract_code", contractCode);
            }
            else if (string.IsNullOrWhiteSpace(contractCode) && !string.IsNullOrWhiteSpace(pair))
            {
                parameters.AddOptionalParameter("pair", pair);
            }
            else
            {
                throw new InvalidOperationException("Parameters contract_code and pair must have at least one");
            }
            parameters.AddOptionalParameter("created_at", createdTimestamp);
            parameters.AddOptionalParameter("order_type", orderType);
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapCrossOrderDetail>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossOrderDetailEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapIsolatedOpenOrders>> GetLinearSwapOpenordersAsync(
            string contractCode,
            int? pageIndex = null,
            int? pageSize = null,
            string? sortBy = null,
            int? tradeType = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code",contractCode }
            };
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            parameters.AddOptionalParameter("trade_type", tradeType);

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapIsolatedOpenOrders>(
                uri: _baseClient.GetUrl(GetLinearSwapOpenordersEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapCrossOpenOrders>> GetLinearSwapCrossOpenordersAsync(
            string? contractCode = null,
            string? pair = null,
            int? pageIndex = null,
            int? pageSize = null,
            string? sortBy = null,
            int? tradeType = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object> { };
            if (!string.IsNullOrWhiteSpace(contractCode))
            {
                parameters.AddOptionalParameter("contract_code", contractCode);
            }
            else if (string.IsNullOrWhiteSpace(contractCode) && !string.IsNullOrWhiteSpace(pair))
            {
                parameters.AddOptionalParameter("pair", pair);
            }
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            parameters.AddOptionalParameter("trade_type", tradeType);

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapCrossOpenOrders>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossOpenordersEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapIsolatedHisOrder>>> GetLinearSwapHisordersAsync(
            int tradeType,
            int type,
            string status,
            string? contractCode = null,
            long? startTime = null,
            long? endTime = null,
            string? direct = null,
            long? fromId = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                { "trade_type",tradeType },
                { "type",type },
                { "status",status }
            };
            parameters.AddOptionalParameter("contract", contractCode);
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("direct", direct);
            parameters.AddOptionalParameter("from_id", fromId);

            return await _baseClient.SendHuobiV3Request<IEnumerable<WWTHuobiUsdtMarginedMarketSwapIsolatedHisOrder>>(
                uri: _baseClient.GetUrl(GetLinearSwapHisordersEndpoint, WWTApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapCrossHisOrder>>> GetLinearSwapCrossHisordersAsync(
            int tradeType,
            int type,
            string status,
            string? contractCode = null,
            string? pair = null,
            long? startTime = null,
            long? endTime = null,
            string? direct = null,
            long? fromId = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                { "trade_type",tradeType },
                { "type",type },
                { "status",status }
            };
            if (!string.IsNullOrWhiteSpace(contractCode))
            {
                parameters.AddOptionalParameter("contract", contractCode);
            }
            else if (string.IsNullOrWhiteSpace(contractCode) && !string.IsNullOrWhiteSpace(pair))
            {
                parameters.AddOptionalParameter("pair", pair);
            }
            else
            {
                throw new InvalidOperationException("Parameters contract_code and pair must have at least one");
            }
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("direct", direct);
            parameters.AddOptionalParameter("from_id", fromId);

            return await _baseClient.SendHuobiV3Request<IEnumerable<WWTHuobiUsdtMarginedMarketSwapCrossHisOrder>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossHisordersEndpoint, WWTApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapIsolatedHisOrder>>> GetLinearSwapHisordersExactAsync(
            int tradeType,
            int type,
            string status,
            string? contractCode = null,
            string? pair = null,
            long? startTime = null,
            long? endTime = null,
            string? direct = null,
            long? fromId = null,
            string? priceType = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                { "trade_type",tradeType },
                { "type",type },
                { "status",status }
            };
            if (!string.IsNullOrWhiteSpace(contractCode))
            {
                parameters.AddOptionalParameter("contract", contractCode);
            }
            else if (string.IsNullOrWhiteSpace(contractCode) && !string.IsNullOrWhiteSpace(pair))
            {
                parameters.AddOptionalParameter("pair", pair);
            }
            else
            {
                throw new InvalidOperationException("Parameters contract_code and pair must have at least one");
            }
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("direct", direct);
            parameters.AddOptionalParameter("from_id", fromId);
            parameters.AddOptionalParameter("price_type", priceType);

            return await _baseClient.SendHuobiV3Request<IEnumerable<WWTHuobiUsdtMarginedMarketSwapIsolatedHisOrder>>(
                uri: _baseClient.GetUrl(GetLinearSwapHisordersExactEndpoint, WWTApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapCrossHisOrder>>> GetLinearSwapCrossHisordersExactAsync(
            int tradeType,
            int type,
            string status,
            string? contractCode = null,
            string? pair = null,
            long? startTime = null,
            long? endTime = null,
            string? direct = null,
            long? fromId = null,
            string? priceType = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                { "trade_type",tradeType },
                { "type",type },
                { "status",status }
            };
            if (!string.IsNullOrWhiteSpace(contractCode))
            {
                parameters.AddOptionalParameter("contract", contractCode);
            }
            else if (string.IsNullOrWhiteSpace(contractCode) && !string.IsNullOrWhiteSpace(pair))
            {
                parameters.AddOptionalParameter("pair", pair);
            }
            else
            {
                throw new InvalidOperationException("Parameters contract_code and pair must have at least one");
            }
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("direct", direct);
            parameters.AddOptionalParameter("from_id", fromId);
            parameters.AddOptionalParameter("price_type", priceType);

            return await _baseClient.SendHuobiV3Request<IEnumerable<WWTHuobiUsdtMarginedMarketSwapCrossHisOrder>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossHisordersExactEndpoint, WWTApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapIsolatedMatchResults>>> GetLinearSwapMatchresultsAsync(
            int tradeType,
            string? contractCode = null,
            string? pair = null,
            long? startTime = null,
            long? endTime = null,
            string? direct = null,
            long? fromId = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                { "trade_type",tradeType }
            };
            if (!string.IsNullOrWhiteSpace(contractCode))
            {
                parameters.AddOptionalParameter("contract", contractCode);
            }
            else if (string.IsNullOrWhiteSpace(contractCode) && !string.IsNullOrWhiteSpace(pair))
            {
                parameters.AddOptionalParameter("pair", pair);
            }
            else
            {
                throw new InvalidOperationException("Parameters contract_code and pair must have at least one");
            }
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("direct", direct);
            parameters.AddOptionalParameter("from_id", fromId);

            return await _baseClient.SendHuobiV3Request<IEnumerable<WWTHuobiUsdtMarginedMarketSwapIsolatedMatchResults>>(
                uri: _baseClient.GetUrl(GetLinearSwapMatchresultsEndpoint, WWTApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapCrossMatchResults>>> GetLinearSwapCrossMatchresultsAsync(
            int tradeType,
            string? contractCode = null,
            string? pair = null,
            long? startTime = null,
            long? endTime = null,
            string? direct = null,
            long? fromId = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                { "trade_type",tradeType },
            };
            if (!string.IsNullOrWhiteSpace(contractCode))
            {
                parameters.AddOptionalParameter("contract", contractCode);
            }
            else if (string.IsNullOrWhiteSpace(contractCode) && !string.IsNullOrWhiteSpace(pair))
            {
                parameters.AddOptionalParameter("pair", pair);
            }
            else
            {
                throw new InvalidOperationException("Parameters contract_code and pair must have at least one");
            }
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("direct", direct);
            parameters.AddOptionalParameter("from_id", fromId);

            return await _baseClient.SendHuobiV3Request<IEnumerable<WWTHuobiUsdtMarginedMarketSwapCrossMatchResults>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossMatchresultsEndpoint, WWTApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapIsolatedMatchResults>>> GetLinearSwapMatchresultsExactAsync(
            string contractCode,
            int tradeType,
            long? startTime = null,
            long? endTime = null,
            string? direct = null,
            long? fromId = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract",contractCode },
                { "trade_type",tradeType }
            };
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("direct", direct);
            parameters.AddOptionalParameter("from_id", fromId);

            return await _baseClient.SendHuobiV3Request<IEnumerable<WWTHuobiUsdtMarginedMarketSwapIsolatedMatchResults>>(
                uri: _baseClient.GetUrl(GetLinearSwapMatchresultsExactEndpoint, WWTApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapCrossMatchResults>>> GetLinearSwapCrossMatchresultsExactAsync(
            int tradeType,
            string? contractCode = null,
            string? pair = null,
            long? startTime = null,
            long? endTime = null,
            string? direct = null,
            long? fromId = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                { "trade_type",tradeType },
            };
            if (!string.IsNullOrWhiteSpace(contractCode))
            {
                parameters.AddOptionalParameter("contract", contractCode);
            }
            else if (string.IsNullOrWhiteSpace(contractCode) && !string.IsNullOrWhiteSpace(pair))
            {
                parameters.AddOptionalParameter("pair", pair);
            }
            else
            {
                throw new InvalidOperationException("Parameters contract_code and pair must have at least one");
            }
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("direct", direct);
            parameters.AddOptionalParameter("from_id", fromId);

            return await _baseClient.SendHuobiV3Request<IEnumerable<WWTHuobiUsdtMarginedMarketSwapCrossMatchResults>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossMatchresultsExactEndpoint, WWTApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapIsolatedOrder>> LinearSwapLightningClosePositionAsync(
            string contractCode,
            long volume,
            string direction,
            long? clientOrderId = null,
            string? orderPriceType = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code",contractCode },
                { "volume",volume },
                { "direction",direction }
            };
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            parameters.AddOptionalParameter("order_price_type", orderPriceType);

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapIsolatedOrder>(
                uri: _baseClient.GetUrl(LinearSwapLightningClosePositionEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapCrossOrder>> LinearSwapCrossLightningClosePositionAsync(
            long volume,
            string direction,
            string? contractCode = null,
            string? pair = null,
            string? contractType = null,
            long? clientOrderId = null,
            string? orderPriceType = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                { "volume",volume },
                { "direction",direction },
            };
            if (!string.IsNullOrWhiteSpace(contractCode))
            {
                parameters.AddOptionalParameter("contract", contractCode);
            }
            else if (string.IsNullOrWhiteSpace(contractCode) && !string.IsNullOrWhiteSpace(pair) && !string.IsNullOrWhiteSpace(contractType))
            {
                parameters.AddOptionalParameter("pair", pair);
                parameters.AddOptionalParameter("contract_type", contractType);
            }
            else
            {
                throw new InvalidOperationException("Parameters contract_code and pair must have at least one");
            }
            parameters.AddOptionalParameter("client_order_id", clientOrderId);
            parameters.AddOptionalParameter("order_price_type", orderPriceType);

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapCrossOrder>(
                uri: _baseClient.GetUrl(LinearSwapCrossLightningClosePositionEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }
    }
}
