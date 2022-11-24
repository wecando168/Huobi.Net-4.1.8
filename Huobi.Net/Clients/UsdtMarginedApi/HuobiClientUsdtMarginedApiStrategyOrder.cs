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
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapStrategy;

namespace Huobi.Net.Clients.UsdtMargined
{
    /// <inheritdoc />
    public class HuobiClientUsdtMarginedApiStrategyOrder : IHuobiClientUsdtMarginedApiStrategyOrder
    {        
        //策略接口
        private const string LinearSwapTriggerOrderEndpoint = "/swap_trigger_order";                                     // 【逐仓】合约计划委托下单(PrivateData)
        private const string LinearSwapCrossTriggerOrderEndpoint = "/swap_cross_trigger_order";                          // 【全仓】合约计划委托下单(PrivateData)
        private const string LinearSwapTriggerCancelEndpoint = "/swap_trigger_cancel";                                   // 【逐仓】合约计划委托撤单(PrivateData)
        private const string LinearSwapCrossTriggerCancelEndpoint = "/swap_cross_trigger_cancel";                        // 【全仓】合约计划委托撤单(PrivateData)
        private const string LinearSwapTriggerCancelAllEndpoint = "/swap_trigger_cancelall";                             // 【逐仓】合约计划委托全部撤单(PrivateData)
        private const string LinearSwapCrossTriggerCancelAllEndpoint = "/swap_cross_trigger_cancelall";                  // 【全仓】合约计划委托全部撤单(PrivateData)
        private const string GetLinearSwapTriggerOpenordersEndpoint = "/swap_trigger_openorders";                        // 【逐仓】获取计划委托当前委托(PrivateData)
        private const string GetLinearSwapCrossTriggerOpenordersEndpoint = "/swap_cross_trigger_openorders";             // 【全仓】获取计划委托当前委托(PrivateData)
        private const string GetLinearSwapTriggerHisordersEndpoint = "/swap_trigger_hisorders";                          // 【逐仓】获取计划委托历史委托(PrivateData)
        private const string GetLinearSwapCrossTriggerHisordersEndpoint = "/swap_cross_trigger_hisorders";               // 【全仓】获取计划委托历史委托(PrivateData)
        private const string LinearSwapTpslOrderEndpoint = "/swap_tpsl_order";                                           // 【逐仓】对仓位设置止盈止损订单(PrivateData)
        private const string LinearSwapCrossTpslOrderEndpoint = "/swap_cross_tpsl_order";                                // 【全仓】对仓位设置止盈止损订单(PrivateData)
        private const string LinearSwapTpslCancelEndpoint = "/swap_tpsl_cancel";                                         // 【逐仓】止盈止损订单撤单(PrivateData)
        private const string LinearSwapCrossTpslCancelEndpoint = "/swap_cross_tpsl_cancel";                              // 【全仓】止盈止损订单撤单(PrivateData)
        private const string LinearSwapTpslCancelAllEndpoint = "/swap_tpsl_cancelall";                                   // 【逐仓】止盈止损订单全部撤单(PrivateData)
        private const string LinearSwapCrossTpslCancelAllEndpoint = "/swap_cross_tpsl_cancelall";                        // 【全仓】止盈止损订单全部撤单(PrivateData)
        private const string GetLinearSwapTpslOpenordersEndpoint = "/swap_tpsl_openorders";                              // 【逐仓】止盈止损订单当前委托(PrivateData)
        private const string GetLinearSwapCrossTpslOpenordersEndpoint = "/swap_cross_tpsl_openorders";                   // 【全仓】止盈止损订单当前委托(PrivateData)
        private const string GetLinearSwapTpslHisordersEndpoint = "/swap_tpsl_hisorders";                                // 【逐仓】止盈止损订单历史委托(PrivateData)
        private const string GetLinearSwapCrossTpslHisordersEndpoint = "/swap_cross_tpsl_hisorders";                     // 【全仓】止盈止损订单历史委托(PrivateData)
        private const string GetLinearSwapRelationTpslOrderEndpoint = "/swap_relation_tpsl_order";                       // 【逐仓】查询开仓单关联的止盈止损订单(PrivateData)        
        private const string GetLinearSwapCrossRelationTpslOrderEndpoint = "/swap_cross_relation_tpsl_order";            // 【全仓】查询开仓单关联的止盈止损订单(PrivateData)
        private const string LinearSwapTrackOrderEndpoint = "/swap_track_order";                                         // 【逐仓】跟踪委托订单下单(PrivateData)
        private const string LinearSwapCrossTrackOrderEndpoint = "/swap_cross_track_order";                              // 【全仓】跟踪委托订单下单(PrivateData)
        private const string LinearSwapTrackCancelEndpoint = "/swap_track_cancel";                                       // 【逐仓】跟踪委托订单撤单(PrivateData)
        private const string LinearSwapCrossTrackCancelEndpoint = "/swap_cross_track_cancel";                            // 【全仓】跟踪委托订单撤单(PrivateData)
        private const string LinearSwapTrackCancelAllEndpoint = "/swap_track_cancelall";                                 // 【逐仓】跟踪委托订单全部撤单(PrivateData)
        private const string LinearSwapCrossTrackCancelAllEndpoint = "/swap_cross_track_cancelall";                      // 【全仓】跟踪委托订单全部撤单(PrivateData)
        private const string GetLinearSwapTrackOpenOrdersEndpoint = "/swap_track_openorders";                            // 【逐仓】跟踪委托订单当前委托(PrivateData)
        private const string GetLinearSwapCrossTrackOpenordersEndpoint = "/swap_cross_track_openorders";                 // 【全仓】跟踪委托订单当前委托(PrivateData)
        private const string GetLinearSwapTrackHisordersEndpoint = "/swap_track_hisorders";                              // 【逐仓】跟踪委托订单历史委托(PrivateData)        
        private const string GetLinearSwapCrossTrackHisordersEndpoint = "/swap_cross_track_hisorders";                   // 【全仓】跟踪委托订单历史委托(PrivateData)

        private readonly HuobiClientUsdtMarginedApi _baseClient;

        internal HuobiClientUsdtMarginedApiStrategyOrder(HuobiClientUsdtMarginedApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapTriggerOrder>> LinearSwapTriggerOrderAsync(
            string contractCode,
            string triggerType,
            decimal triggerPrice,
            long volume,
            string direction,
            int? reduceOnly,
            decimal? orderPrice,
            string? orderPriceType,
            string? offset,
            int? leverRate, 
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
                {"trigger_type", triggerType },
                {"trigger_price", triggerPrice },
                {"volume", volume },
                {"direction", direction}
            };
            parameters.AddOptionalParameter("reduce_only", reduceOnly == null ? null : reduceOnly.ToString());
            parameters.AddOptionalParameter("order_price", orderPrice);
            parameters.AddOptionalParameter("order_price_type", orderPriceType);
            parameters.AddOptionalParameter("offset", offset);
            parameters.AddOptionalParameter("lever_rate", leverRate);

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapTriggerOrder>(
                uri: _baseClient.GetUrl(LinearSwapTriggerOrderEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTriggerOrder>> LinearSwapCrossTriggerOrderAsync(
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
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"trigger_type", triggerType },
                {"trigger_price", triggerPrice },
                {"volume", volume },
                {"direction", direction }
            };
            if(string.IsNullOrWhiteSpace(contractCode) && (string.IsNullOrWhiteSpace(pair) || string.IsNullOrWhiteSpace(contractType)))
            {
                throw new ArgumentException($"At least one of \"contract code\" and \"pair + contract type\" is not null");
            }
            else
            {
                if(!string.IsNullOrWhiteSpace(contractCode))
                {
                    parameters.AddOptionalParameter("contract_code", contractCode);
                }
                else
                {
                    parameters.AddOptionalParameter("pair", pair);
                    parameters.AddOptionalParameter("contract_type", contractType);
                }
            }
            parameters.AddOptionalParameter("reduce_only", reduceOnly == null ? null : reduceOnly.ToString());
            parameters.AddOptionalParameter("order_price", orderPrice);
            parameters.AddOptionalParameter("order_price_type", orderPriceType);
            parameters.AddOptionalParameter("offset", offset);
            parameters.AddOptionalParameter("lever_rate", leverRate);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossTriggerOrder>(
                uri: _baseClient.GetUrl(LinearSwapCrossTriggerOrderEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapTriggerCancel>> LinearSwapTriggerCancelAsync(
            string contractCode,
            IEnumerable<long>? orderIdList,
            CancellationToken ct = default 
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode }
            };
            if (orderIdList != null && orderIdList.Count() > 0 && orderIdList.Count() <= 20)
            {
                parameters.AddOptionalParameter("order_id", string.Join(",", orderIdList));
            }
            else
            {
                throw new InvalidOperationException("order_id count must be greater than 0 and less than or equal to 20");
            }
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapTriggerCancel>(
                uri: _baseClient.GetUrl(LinearSwapTriggerCancelEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTriggerCancel>> LinearSwapCrossTriggerCancelAsync(
            IEnumerable<long>? orderIdList,
            string? contractCode,
            string? pair,
            string? contractType,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object> { };
            if (orderIdList != null && orderIdList.Count() > 0 && orderIdList.Count() <= 10)
            {
                parameters.AddOptionalParameter("order_id", string.Join(",", orderIdList));
            }
            else
            {
                throw new InvalidOperationException("order_id count must be greater than 0 and less than or equal to 10");
            }
            if (string.IsNullOrWhiteSpace(contractCode) && (string.IsNullOrWhiteSpace(pair) || string.IsNullOrWhiteSpace(contractType)))
            {
                throw new ArgumentException($"At least one of \"contract code\" and \"pair + contract type\" is not null");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(contractCode))
                {
                    parameters.AddOptionalParameter("contract_code", contractCode);
                }
                else
                {
                    parameters.AddOptionalParameter("pair", pair);
                    parameters.AddOptionalParameter("contract_type", contractType);
                }
            }
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossTriggerCancel>(
                uri: _baseClient.GetUrl(LinearSwapCrossTriggerCancelEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapTriggerCancel>> LinearSwapTriggerCancelAllAsync(
            string contractCode,
            string? direction,
            string? offset,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
            };
            parameters.AddOptionalParameter("direction", direction);
            parameters.AddOptionalParameter("offset", offset);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapTriggerCancel>(
                uri: _baseClient.GetUrl(LinearSwapTriggerCancelAllEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTriggerCancel>> LinearSwapCrossTriggerCancelAllAsync(
            string? contractCode,
            string? pair,
            string? contractType,
            string? direction,
            string? offset,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object> { };
            if (string.IsNullOrWhiteSpace(contractCode) && (string.IsNullOrWhiteSpace(pair) || string.IsNullOrWhiteSpace(contractType)))
            {
                throw new ArgumentException($"At least one of \"contract code\" and \"pair + contract type\" is not null");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(contractCode))
                {
                    parameters.AddOptionalParameter("contract_code", contractCode);
                }
                else
                {
                    parameters.AddOptionalParameter("pair", pair);
                    parameters.AddOptionalParameter("contract_type", contractType);
                }
            }
            parameters.AddOptionalParameter("direction", direction);
            parameters.AddOptionalParameter("offset", offset);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossTriggerCancel>(
                uri: _baseClient.GetUrl(LinearSwapCrossTriggerCancelAllEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapTriggerOpenOrders>> GetLinearSwapTriggerOpenordersAsync(
            string contractCode,
            int? pageIndex = null,
            int? pageSize = null,
            int? tradeType = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
            };
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("trade_type", tradeType);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapTriggerOpenOrders>(
                uri: _baseClient.GetUrl(GetLinearSwapTriggerOpenordersEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTriggerOpenOrders>> GetLinearSwapCrossTriggerOpenordersAsync(
            string? contractCode,
            string? pair,
            int? pageIndex = null,
            int? pageSize = null,
            int? tradeType = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object> { };
            if (string.IsNullOrWhiteSpace(contractCode) && string.IsNullOrWhiteSpace(pair))
            {
                throw new ArgumentException($"At least one of \"contract code\" and \"pair\" is not null");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(contractCode))
                {
                    parameters.AddOptionalParameter("contract_code", contractCode);
                }
                else
                {
                    parameters.AddOptionalParameter("pair", pair);
                }
            }
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("trade_type", tradeType);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossTriggerOpenOrders>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossTriggerOpenordersEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapTriggerHisorders>> GetLinearSwapTriggerHisordersAsync(
            string contractCode,
            int tradeType,
            string status,
            int createDate,
            int? pageIndex = null,
            int? pageSize = null,
            string? sortBy = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
                {"trade_type", tradeType },
                {"status", status },
                {"create_date", createDate },
            };
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapTriggerHisorders>(
                uri: _baseClient.GetUrl(GetLinearSwapTriggerHisordersEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTriggerHisorders>> GetLinearSwapCrossTriggerHisordersAsync(
            int tradeType,
            string status,
            int createDate,
            string? contractCode,
            string? pair,
            int? pageIndex = null,
            int? pageSize = null,
            string? sortBy = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"trade_type", tradeType },
                {"status", status },
                {"create_date", createDate },
            };
            if (string.IsNullOrWhiteSpace(contractCode) && string.IsNullOrWhiteSpace(pair))
            {
                throw new ArgumentException($"At least one of \"contract code\" and \"pair\" is not null");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(contractCode))
                {
                    parameters.AddOptionalParameter("contract_code", contractCode);
                }
                else
                {
                    parameters.AddOptionalParameter("pair", pair);
                }
            }
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossTriggerHisorders>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossTriggerHisordersEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapTpslOrder>> LinearSwapTpslOrderAsync(
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
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
                {"direction", direction },
                {"volume", volume },
            };
            parameters.AddOptionalParameter("tp_trigger_price", tpTriggerPrice);
            parameters.AddOptionalParameter("tp_order_price", tpOrderPrice);
            parameters.AddOptionalParameter("tp_order_price_type", tpOrderPriceType);
            parameters.AddOptionalParameter("sl_trigger_price", slTriggerPrice);
            parameters.AddOptionalParameter("sl_order_price", slOrderPrice);
            parameters.AddOptionalParameter("sl_order_price_type", slOrderPriceType);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapTpslOrder>(
                uri: _baseClient.GetUrl(LinearSwapTpslOrderEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTpslOrder>> LinearSwapCrossTpslOrderAsync(
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
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"direction", direction },
                {"volume", volume },
            };
            if (string.IsNullOrWhiteSpace(contractCode) && (string.IsNullOrWhiteSpace(pair) || string.IsNullOrWhiteSpace(contractType)))
            {
                throw new ArgumentException($"At least one of \"contract code\" and \"pair + contract type\" is not null");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(contractCode))
                {
                    parameters.AddOptionalParameter("contract_code", contractCode);
                }
                else
                {
                    parameters.AddOptionalParameter("pair", pair);
                    parameters.AddOptionalParameter("contract_type", contractType);
                }
            }
            parameters.AddOptionalParameter("tp_trigger_price", tpTriggerPrice);
            parameters.AddOptionalParameter("tp_order_price", tpOrderPrice);
            parameters.AddOptionalParameter("tp_order_price_type", tpOrderPriceType);
            parameters.AddOptionalParameter("sl_trigger_price", slTriggerPrice);
            parameters.AddOptionalParameter("sl_order_price", slOrderPrice);
            parameters.AddOptionalParameter("sl_order_price_type", slOrderPriceType);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossTpslOrder>(
                uri: _baseClient.GetUrl(LinearSwapCrossTpslOrderEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapTriggerCancel>> LinearSwapTpslCancelAsync(
            string contractCode,
            IEnumerable<long>? orderIdList,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode }
            };
            if (orderIdList != null && orderIdList.Count() > 0 && orderIdList.Count() <= 10)
            {
                parameters.AddOptionalParameter("order_id", string.Join(",", orderIdList));
            }
            else
            {
                throw new InvalidOperationException("order_id count must be greater than 0 and less than or equal to 10");
            }
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapTriggerCancel>(
                uri: _baseClient.GetUrl(LinearSwapTpslCancelEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTriggerCancel>> LinearSwapCrossTpslCancelAsync(
            IEnumerable<long>? orderIdList,
            string? contractCode,
            string? pair,
            string? contractType,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object> { };
            if (orderIdList != null && orderIdList.Count() > 0 && orderIdList.Count() <= 10)
            {
                parameters.AddOptionalParameter("order_id", string.Join(",", orderIdList));
            }
            else
            {
                throw new InvalidOperationException("order_id count must be greater than 0 and less than or equal to 10");
            }
            if (string.IsNullOrWhiteSpace(contractCode) && (string.IsNullOrWhiteSpace(pair) || string.IsNullOrWhiteSpace(contractType)))
            {
                throw new ArgumentException($"At least one of \"contract code\" and \"pair + contract type\" is not null");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(contractCode))
                {
                    parameters.AddOptionalParameter("contract_code", contractCode);
                }
                else
                {
                    parameters.AddOptionalParameter("pair", pair);
                    parameters.AddOptionalParameter("contract_type", contractType);
                }
            }
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossTriggerCancel>(
                uri: _baseClient.GetUrl(LinearSwapCrossTpslCancelEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapTriggerCancel>> LinearSwapTpslCancelAllAsync(
            string contractCode,
            string? direction,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
            };
            parameters.AddOptionalParameter("direction", direction);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapTriggerCancel>(
                uri: _baseClient.GetUrl(LinearSwapTpslCancelAllEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTriggerCancel>> LinearSwapCrossTpslCancelAllAsync(
            string? contractCode,
            string? pair,
            string? contractType,
            string? direction,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object> { };
            if (string.IsNullOrWhiteSpace(contractCode) && (string.IsNullOrWhiteSpace(pair) || string.IsNullOrWhiteSpace(contractType)))
            {
                throw new ArgumentException($"At least one of \"contract code\" and \"pair + contract type\" is not null");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(contractCode))
                {
                    parameters.AddOptionalParameter("contract_code", contractCode);
                }
                else
                {
                    parameters.AddOptionalParameter("pair", pair);
                    parameters.AddOptionalParameter("contract_type", contractType);
                }
            }
            parameters.AddOptionalParameter("direction", direction);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossTriggerCancel>(
                uri: _baseClient.GetUrl(LinearSwapCrossTpslCancelAllEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapTpslOpenOrders>> GetLinearSwapTpslOpenordersAsync(
            string contractCode,
            int? pageIndex = null,
            int? pageSize = null,
            int? tradeType = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
            };
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("trade_ype", tradeType);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapTpslOpenOrders>(
                uri: _baseClient.GetUrl(GetLinearSwapTpslOpenordersEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTpslOpenOrders>> GetLinearSwapCrossTpslOpenordersAsync(
            string? contractCode,
            string? pair,
            int? pageIndex = null,
            int? pageSize = null,
            int? tradeType = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("trade_ype", tradeType);
            if (string.IsNullOrWhiteSpace(contractCode) && string.IsNullOrWhiteSpace(pair))
            {
                throw new ArgumentException($"At least one of \"contract code\" and \"pair\" is not null");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(contractCode))
                {
                    parameters.AddOptionalParameter("contract_code", contractCode);
                }
                else
                {
                    parameters.AddOptionalParameter("pair", pair);
                }
            }
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossTpslOpenOrders>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossTpslOpenordersEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapTpslHisorders>> GetLinearSwapTpslHisordersAsync(
            string contractCode,
            string status,
            long createDate,
            int? pageIndex = null,
            int? pageSize = null,
            string? sortBy = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
                {"status", status },
                {"create_date",  createDate}
            };
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapTpslHisorders>(
                uri: _baseClient.GetUrl(GetLinearSwapTpslHisordersEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTpslHisorders>> GetLinearSwapCrossTpslHisordersAsync(
            string status,
            long createDate,
            string? contractCode,
            string? pair,
            int? pageIndex = null,
            int? pageSize = null,
            string? sortBy = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"status", status },
                {"create_date", createDate }
            };
            if (string.IsNullOrWhiteSpace(contractCode) && string.IsNullOrWhiteSpace(pair))
            {
                throw new ArgumentException($"At least one of \"contract code\" and \"pair\" is not null");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(contractCode))
                {
                    parameters.AddOptionalParameter("contract_code", contractCode);
                }
                else
                {
                    parameters.AddOptionalParameter("pair", pair);
                }
            }
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossTpslHisorders>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossTpslHisordersEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapRelationTpslOrder>> GetLinearSwapRelationTpslOrderAsync(
             string contractCode,
             long orderId,
             CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
                {"order_id", orderId }                
            };
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapRelationTpslOrder>(
                uri: _baseClient.GetUrl(GetLinearSwapRelationTpslOrderEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossRelationTpslOrder>> GetLinearSwapCrossRelationTpslOrderAsync(
            long orderId,
            string? contractCode,
            string? pair,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"order_id", orderId }
            };
            if (string.IsNullOrWhiteSpace(contractCode) && string.IsNullOrWhiteSpace(pair))
            {
                throw new ArgumentException($"At least one of \"contract code\" and \"pair\" is not null");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(contractCode))
                {
                    parameters.AddOptionalParameter("contract_code", contractCode);
                }
                else
                {
                    parameters.AddOptionalParameter("pair", pair);
                }
            }
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossRelationTpslOrder>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossRelationTpslOrderEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapTrackOrder>> LinearSwapTrackOrderAsync(
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
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
                {"direction", direction },
                {"volume", volume },
                {"callback_rate", callbackRate },
                {"active_price", activePrice },
                {"order_price_type", orderPriceType },
            };
            parameters.AddOptionalParameter("reduce_only", reduceOnly);
            parameters.AddOptionalParameter("lever_rate", leverRate);
            parameters.AddOptionalParameter("offset", offset);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapTrackOrder>(
                uri: _baseClient.GetUrl(LinearSwapTrackOrderEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTrackOrder>> LinearSwapCrossTrackOrderAsync(
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
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"direction", direction },
                {"volume", volume },
                {"callback_rate", callbackRate },
                {"active_price", activePrice },
                {"order_price_type", orderPriceType },
            };
            if (string.IsNullOrWhiteSpace(contractCode) && (string.IsNullOrWhiteSpace(pair) || string.IsNullOrWhiteSpace(contractType)))
            {
                throw new ArgumentException($"At least one of \"contract code\" and \"pair + contract type\" is not null");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(contractCode))
                {
                    parameters.AddOptionalParameter("contract_code", contractCode);
                }
                else
                {
                    parameters.AddOptionalParameter("pair", pair);
                    parameters.AddOptionalParameter("contract_type", contractType);
                }
            }
            parameters.AddOptionalParameter("reduce_only", reduceOnly);
            parameters.AddOptionalParameter("lever_rate", leverRate);
            parameters.AddOptionalParameter("offset", offset);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossTrackOrder>(
                uri: _baseClient.GetUrl(LinearSwapCrossTrackOrderEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapTrackCancel>> LinearSwapTrackCancelAsync(
            string contractCode,
            IEnumerable<long>? orderIdList,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
            };
            if (orderIdList != null && orderIdList.Count() > 0 && orderIdList.Count() <= 10)
            {
                parameters.AddOptionalParameter("order_id", string.Join(",", orderIdList));
            }
            else
            {
                throw new InvalidOperationException("order_id count must be greater than 0 and less than or equal to 10");
            }
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapTrackCancel>(
                uri: _baseClient.GetUrl(LinearSwapTrackCancelEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTrackCancel>> LinearSwapCrossTrackCancelAsync(
            IEnumerable<long>? orderIdList,
            string? contractCode,
            string? pair,
            string? contractType,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object> { };
            if (orderIdList != null && orderIdList.Count() > 0 && orderIdList.Count() <= 10)
            {
                parameters.AddOptionalParameter("order_id", string.Join(",", orderIdList));
            }
            else
            {
                throw new InvalidOperationException("order_id count must be greater than 0 and less than or equal to 10");
            }
            if (string.IsNullOrWhiteSpace(contractCode) && (string.IsNullOrWhiteSpace(pair) || string.IsNullOrWhiteSpace(contractType)))
            {
                throw new ArgumentException($"At least one of \"contract code\" and \"pair + contract type\" is not null");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(contractCode))
                {
                    parameters.AddOptionalParameter("contract_code", contractCode);
                }
                else
                {
                    parameters.AddOptionalParameter("pair", pair);
                    parameters.AddOptionalParameter("contract_type", contractType);
                }
            }
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossTrackCancel>(
                uri: _baseClient.GetUrl(LinearSwapCrossTrackCancelEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapTrackCancel>> LinearSwapTrackCancelAllAsync(
             string contractCode,
             string? direction,
             string? offset,
             CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode }
            };
            parameters.AddOptionalParameter("direction", direction);
            parameters.AddOptionalParameter("offset", offset);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapTrackCancel>(
                uri: _baseClient.GetUrl(LinearSwapTrackCancelAllEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTrackCancel>> LinearSwapCrossTrackCancelAllAsync(
            string? contractCode,
            string? pair,
            string? contractType,
            string? direction,
            string? offset,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object> { };
            if (string.IsNullOrWhiteSpace(contractCode) && (string.IsNullOrWhiteSpace(pair) || string.IsNullOrWhiteSpace(contractType)))
            {
                throw new ArgumentException($"At least one of \"contract code\" and \"pair + contract type\" is not null");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(contractCode))
                {
                    parameters.AddOptionalParameter("contract_code", contractCode);
                }
                else
                {
                    parameters.AddOptionalParameter("pair", pair);
                    parameters.AddOptionalParameter("contract_type", contractType);
                }
            }
            parameters.AddOptionalParameter("direction", direction);
            parameters.AddOptionalParameter("offset", offset);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossTrackCancel>(
                uri: _baseClient.GetUrl(LinearSwapCrossTrackCancelAllEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapTrackOpenOrders>> GetLinearSwapTrackOpenOrdersAsync(
            string contractCode,
            int? tradeType,
            int? pageIndex = null,
            int? pageSize = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode }
            };
            parameters.AddOptionalParameter("trade_type", tradeType);
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapTrackOpenOrders>(
                uri: _baseClient.GetUrl(GetLinearSwapTrackOpenOrdersEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTrackOpenOrders>> GetLinearSwapCrossTrackOpenordersAsync(
            string? contractCode,
            string? pair,
            int? tradeType,
            int? pageIndex = null,
            int? pageSize = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object> { };
            if (string.IsNullOrWhiteSpace(contractCode) && string.IsNullOrWhiteSpace(pair))
            {
                throw new ArgumentException($"At least one of \"contract code\" and \"pair\" is not null");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(contractCode))
                {
                    parameters.AddOptionalParameter("contract_code", contractCode);
                }
                else
                {
                    parameters.AddOptionalParameter("pair", pair);
                }
            }
            parameters.AddOptionalParameter("trade_type", tradeType);
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossTrackOpenOrders>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossTrackOpenordersEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapTrackHisorders>> GetLinearSwapTrackHisordersAsync(
            string contractCode,
            string status,
            int tradeType,
            long createDate,
            int? pageIndex = null,
            int? pageSize = null,
            string? sortBy = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code", contractCode },
                {"status", status },
                {"trade_type", tradeType },
                {"create_date", createDate }
            };
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapTrackHisorders>(
                uri: _baseClient.GetUrl(GetLinearSwapTrackHisordersEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapCrossTrackHisorders>> GetLinearSwapCrossTrackHisordersAsync(
            string status,
            int tradeType,
            long createDate,
            string? contractCode,
            string? pair,
            int? pageIndex = null,
            int? pageSize = null,
            string? sortBy = null,
            CancellationToken ct = default
            )
        {
            var parameters = new Dictionary<string, object>
            {
                {"status", status },
                {"trade_type", tradeType },
                {"create_date", createDate }
            };
            if (string.IsNullOrWhiteSpace(contractCode) && string.IsNullOrWhiteSpace(pair))
            {
                throw new ArgumentException($"At least one of \"contract code\" and \"pair\" is not null");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(contractCode))
                {
                    parameters.AddOptionalParameter("contract_code", contractCode);
                }
                else
                {
                    parameters.AddOptionalParameter("pair", pair);
                }
            }
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);
            parameters.AddOptionalParameter("sort_by", sortBy);
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapCrossTrackHisorders>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossTrackHisordersEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }
    }
}
