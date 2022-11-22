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
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapTrade.Request;

namespace Huobi.Net.Clients.UsdtMargined
{
    /// <inheritdoc />
    public class HuobiClientUsdtMarginedApiTrade : IHuobiClientUsdtMarginedApiTrade
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
        

        private readonly HuobiClientUsdtMarginedApi _baseClient;

        internal HuobiClientUsdtMarginedApiTrade(HuobiClientUsdtMarginedApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapCrossTradeState>>> GetLinearSwapCrossTradeStateAsync(string? contractCode = null, string? Pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", Pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("business_type", businessType);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketSwapCrossTradeState>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossTradeStateEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapSwitchPositionMode>> LinearSwapSwitchPositionModeAsync(string marginAccount, string positionMode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"margin_account", marginAccount },
                {"position_mode", positionMode }
            };

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapSwitchPositionMode>(
                uri: _baseClient.GetUrl(LinearSwapSwitchPositionModeEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossSwitchPositionMode>> LinearSwapCrossSwitchPositionModeAsync(string marginAccount, string positionMode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"margin_account", marginAccount },
                {"position_mode", positionMode }
            };

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossSwitchPositionMode>(
                uri: _baseClient.GetUrl(LinearSwapCrossSwitchPositionModeEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapOrder>> LinearSwapOrderAsync(
            string contractCode,
            UmDirection direction,
            UmOffset? offset,
            decimal? price,
            UmLeverRate leverRate,
            long volume,
            UmOrderPriceType orderPriceType,
            decimal? tpTriggerPrice,
            decimal? tpOrderPrice,
            UmTpOrderPriceType? tpOrderPriceType,
            decimal? slTriggerPrice,
            decimal? slOrderPrice,
            UmSlOrderPriceType? slOrderPriceType,
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

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapOrder>(
                uri: _baseClient.GetUrl(LinearSwapOrderEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossOrder>> LinearSwapCrossOrderAsync(
            string contractCode,
            UmDirection direction,
            UmOffset? offset,
            decimal? price,
            UmLeverRate leverRate,
            long volume,
            UmOrderPriceType orderPriceType,
            decimal? tpTriggerPrice,
            decimal? tpOrderPrice,
            UmTpOrderPriceType? tpOrderPriceType,
            decimal? slTriggerPrice,
            decimal? slOrderPrice,
            UmSlOrderPriceType? slOrderPriceType,
            string? pair = null,
            string? contractType = null,
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
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
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

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossOrder>(
                uri: _baseClient.GetUrl(LinearSwapCrossOrderEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapBatchOrder>> LinearSwapBatchorderAsync(IEnumerable<HuobiUsdtMarginedIsolatedOrder> huobiUsdtMarginedIsolatedOrderList, CancellationToken ct = default)
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

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapBatchOrder>(
                uri: _baseClient.GetUrl(LinearSwapBatchorderEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossBatchOrder>> LinearSwapCrossBatchorderAsync(IEnumerable<HuobiUsdtMarginedCrossOrder> huobiUsdtMarginedCrossOrderList, CancellationToken ct = default)
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

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossBatchOrder>(
                uri: _baseClient.GetUrl(LinearSwapCrossBatchorderEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCancel>> LinearSwapCancelAsync(string contractCode, IEnumerable<long> ? orderIdList = null, IEnumerable<long> ? clientOrderIdList = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
            };
            if (orderIdList != null && orderIdList.Count() > 0 && orderIdList.Count() <= 10)
            {
                parameters.AddOptionalParameter("order_id", string.Join(",", orderIdList));
            }
            else if ((orderIdList == null || orderIdList.Count() == 0) && clientOrderIdList != null && clientOrderIdList.Count() > 0 && clientOrderIdList.Count() <= 10)
            {
                parameters.AddOptionalParameter("client_order_id", string.Join(",", clientOrderIdList));
            }
            else
            {
                throw new InvalidOperationException("order_id or client_order_id count must be greater than 0 and less than or equal to 10");
            }

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCancel>(
                uri: _baseClient.GetUrl(LinearSwapCancelEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossCancel>> LinearSwapCrossCancelAsync(string contractCode, IEnumerable<long>? orderIdList = null, IEnumerable<long>? clientOrderIdList = null, string ? pair = null, string ? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
            };
            if (orderIdList != null && orderIdList.Count() > 0 && orderIdList.Count() <= 10)
            {
                parameters.AddOptionalParameter("order_id", string.Join(",", orderIdList));
            }
            else if ((orderIdList == null || orderIdList.Count() == 0) && clientOrderIdList != null && clientOrderIdList.Count() > 0 && clientOrderIdList.Count() <= 10)
            {
                parameters.AddOptionalParameter("client_order_id", string.Join(",", clientOrderIdList));
            }
            else
            {
                throw new InvalidOperationException("order_id or client_order_id count must be greater than 0 and less than or equal to 10");
            }
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossCancel>(
                uri: _baseClient.GetUrl(LinearSwapCrossCancelEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCancel>> LinearSwapCancelAllAsync(string contractCode, string ? direction = null, string ? offset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
            };
            parameters.AddOptionalParameter("direction", direction);
            parameters.AddOptionalParameter("offset", offset);

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCancel>(
                uri: _baseClient.GetUrl(LinearSwapCancelAllEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossCancel>> LinearSwapCrossCancelAllAsync(string contractCode, string? pair = null, string? contractType = null, string? direction = null, string? offset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
            };
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("direction", direction);
            parameters.AddOptionalParameter("offset", offset);

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossCancel>(
                uri: _baseClient.GetUrl(LinearSwapCrossCancelAllEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapSwitchLeverRate>> LinearSwapSwitchLeverRateAsync(string contractCode, int leverRate, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
                {"lever_rate", leverRate },
            };

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapSwitchLeverRate>(
                uri: _baseClient.GetUrl(LinearSwapSwitchLeverRateEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapSwitchCrossLeverRate>> LinearSwapCrossSwitchLeverRateAsync(string contractCode, int leverRate, string? pair = null, string? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
                {"lever_rate", leverRate },
            };
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapSwitchCrossLeverRate>(
                uri: _baseClient.GetUrl(LinearSwapCrossSwitchLeverRateEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapOrderInfo>>> GetLinearSwapOrderInfoAsync(string contractCode, IEnumerable<long>? orderIdList = null, IEnumerable<long>? clientOrderIdList = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> 
            {
                {"contract_code", contractCode }
            };

            if (orderIdList != null && orderIdList.Count() > 0 && orderIdList.Count() <= 50)
            {
                parameters.AddOptionalParameter("order_id", string.Join(",", orderIdList));
            }
            else if ((orderIdList == null || orderIdList.Count() == 0) && clientOrderIdList != null && clientOrderIdList.Count() > 0 && clientOrderIdList.Count() <= 50)
            {
                parameters.AddOptionalParameter("client_order_id", string.Join(",", clientOrderIdList));
            }
            else
            {
                throw new InvalidOperationException("order_id or client_order_id count must be greater than 0 and less than or equal to 50");
            }

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketSwapOrderInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapOrderInfoEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapCrossOrderInfo>>> GetLinearSwapCrossOrderInfoAsync(string? contractCode = null, IEnumerable<long>? orderIdList = null, IEnumerable<long>? clientOrderIdList = null, string? pair = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            if(!string.IsNullOrWhiteSpace(contractCode))
            {
                parameters.AddOptionalParameter("contract_code", contractCode);
            }
            else if(string.IsNullOrWhiteSpace(contractCode) && !string.IsNullOrWhiteSpace(pair))
            {
                parameters.AddOptionalParameter("pair", pair);
            }
            else
            {
                throw new InvalidOperationException("Parameters contract_code and pair must have at least one");
            }            
            
            if (orderIdList != null && orderIdList.Count() > 0 && orderIdList.Count() <= 50)
            {
                parameters.AddOptionalParameter("order_id", string.Join(",", orderIdList));
            }
            else if ((orderIdList == null || orderIdList.Count() == 0) && clientOrderIdList != null && clientOrderIdList.Count() > 0 && clientOrderIdList.Count() <= 50)
            {
                parameters.AddOptionalParameter("client_order_id", string.Join(",", clientOrderIdList));
            }
            else
            {
                throw new InvalidOperationException("order_id or client_order_id count must be greater than 0 and less than or equal to 50");
            }

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketSwapCrossOrderInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossOrderInfoEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapOrderDetail>> GetLinearSwapOrderDetailAsync(
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

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapOrderDetail>(
                uri: _baseClient.GetUrl(GetLinearSwapOrderDetailEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossOrderDetail>> GetLinearSwapCrossOrderDetailAsync(
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

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossOrderDetail>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossOrderDetailEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapOpenOrders>> GetLinearSwapOpenordersAsync(
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

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapOpenOrders>(
                uri: _baseClient.GetUrl(GetLinearSwapOpenordersEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossOpenOrders>> GetLinearSwapCrossOpenordersAsync(
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
            else
            {
                throw new InvalidOperationException("Parameters contract_code and pair must have at least one");
            }
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            parameters.AddOptionalParameter("trade_type", tradeType);

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossOpenOrders>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossOpenordersEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapHisOrder>>> GetLinearSwapHisordersAsync(
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

            return await _baseClient.SendHuobiV3Request<IEnumerable<HuobiUsdtMarginedMarketSwapHisOrder>>(
                uri: _baseClient.GetUrl(GetLinearSwapHisordersEndpoint, ApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapCrossHisOrder>>> GetLinearSwapCrossHisordersAsync(
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

            return await _baseClient.SendHuobiV3Request<IEnumerable<HuobiUsdtMarginedMarketSwapCrossHisOrder>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossHisordersEndpoint, ApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapHisOrder>>> GetLinearSwapHisordersExactAsync(
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

            return await _baseClient.SendHuobiV3Request<IEnumerable<HuobiUsdtMarginedMarketSwapHisOrder>>(
                uri: _baseClient.GetUrl(GetLinearSwapHisordersExactEndpoint, ApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapCrossHisOrder>>> GetLinearSwapCrossHisordersExactAsync(
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

            return await _baseClient.SendHuobiV3Request<IEnumerable<HuobiUsdtMarginedMarketSwapCrossHisOrder>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossHisordersExactEndpoint, ApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapMatchResults>>> GetLinearSwapMatchresultsAsync(
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

            return await _baseClient.SendHuobiV3Request<IEnumerable<HuobiUsdtMarginedMarketSwapMatchResults>>(
                uri: _baseClient.GetUrl(GetLinearSwapMatchresultsEndpoint, ApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapCrossMatchResults>>> GetLinearSwapCrossMatchresultsAsync(            
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

            return await _baseClient.SendHuobiV3Request<IEnumerable<HuobiUsdtMarginedMarketSwapCrossMatchResults>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossMatchresultsEndpoint, ApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapMatchResults>>> GetLinearSwapMatchresultsExactAsync(
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

            return await _baseClient.SendHuobiV3Request<IEnumerable<HuobiUsdtMarginedMarketSwapMatchResults>>(
                uri: _baseClient.GetUrl(GetLinearSwapMatchresultsExactEndpoint, ApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapCrossMatchResults>>> GetLinearSwapCrossMatchresultsExactAsync(
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

            return await _baseClient.SendHuobiV3Request<IEnumerable<HuobiUsdtMarginedMarketSwapCrossMatchResults>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossMatchresultsExactEndpoint, ApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapOrder>> LinearSwapLightningClosePositionAsync(
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

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapOrder>(
                uri: _baseClient.GetUrl(LinearSwapLightningClosePositionEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossOrder>> LinearSwapCrossLightningClosePositionAsync(
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

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossOrder>(
                uri: _baseClient.GetUrl(LinearSwapCrossLightningClosePositionEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }
    }
}
