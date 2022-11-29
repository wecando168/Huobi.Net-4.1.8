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
using CryptoExchange.Net.Converters;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapMarketData;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount;

namespace Huobi.Net.Clients.UsdtMarginSwapApi
{
    /// <inheritdoc />
    public class HuobiClientUsdtMarginSwapApiMarketData : IHuobiClientUsdtMarginSwapMarketData
    {
        // 市场行情接口
        private const string GetLinearSwapDepthEndpoint = "/depth";                                             // 【通用】获取行情深度数据(PublicData)
        private const string GetLinearSwapBboEndpoint = "/bbo";                                                 // 【通用】获取市场最优挂单(PublicData)
        private const string GetLinearSwapHistoryKlineEndpoint = "/kline";                                      // 【通用】获取K线数据(PublicData)
        private const string GetLinearSwapMarkPriceKlineEndpoint = "/linear_swap_mark_price_kline";             // 【通用】获取标记价格的 K 线数据(PublicData)
        private const string GetLinearSwapMergedEndpoint = "/merged";                                           // 【通用】获取聚合行情(PublicData)
        private const string GetLinearSwapBatchMergedV2Endpoint = "/batch_merged";                              // 【通用】批量获取聚合行情(V2)(PublicData)
        private const string GetLinearSwapMarketTradeEndpoint = "/trade";                                       // 【通用】获取市场最近成交记录(PublicData)
        private const string GetLinearSwapMarketHistoryTradeEndpoint = "/trade";                                // 【通用】批量获取最近的交易记录(PublicData)
        private const string GetLinearSwapHisOpenInterestEndpoint = "/swap_his_open_interest";                  // 【通用】平台历史持仓量查询(PublicData)
        private const string GetLinearSwapPremiumIndexKlineEndpoint = "/linear_swap_premium_index_kline";       // 【通用】获取溢价指数K线数据(PublicData)
        private const string GetLinearSwapEstimatedRateKlineEndpoint = "/linear_swap_estimated_rate_kline";     // 【通用】获取预测资金费率的K线数据(PublicData)
        private const string GetLinearSwapBasisEndpoint = "/linear_swap_basis";                                 // 【通用】获取基差数据(PublicData)

        private readonly HuobiClientUsdtMarginSwapApi _baseClient;

        internal HuobiClientUsdtMarginSwapApiMarketData(HuobiClientUsdtMarginSwapApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketHistoryBasis>>> GetLinearSwapBasisAsync(string contractCode, string period, int size, string? basisPriceType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "period", period },
                { "size", size }
            };
            parameters.AddOptionalParameter("basis_price_type", basisPriceType);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketHistoryBasis>>(
                uri: _baseClient.GetUrl(GetLinearSwapBasisEndpoint, ApiPath.IndexMarketHistory),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketDetailBatchMerged>>> GetLinearSwapBatchMergedV2Async(string? contractCode = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", businessType);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketDetailBatchMerged>>(
                uri: _baseClient.GetUrl(GetLinearSwapBatchMergedV2Endpoint, ApiPath.V2LinearSwapExMarketDetail),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketBbo>>> GetLinearSwapBboAsync(string? contractCode = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", businessType);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketBbo>>(
                uri: _baseClient.GetUrl(GetLinearSwapBboEndpoint, ApiPath.LinearSwapExMarket),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketDepth>> GetLinearSwapDepthAsync(string contractCode, string depthType, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "type", depthType }
            };

            var result = await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketDepth>(
                uri: _baseClient.GetUrl(GetLinearSwapDepthEndpoint, ApiPath.LinearSwapExMarket),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketEstimatedRateKline>>> GetLinearSwapEstimatedRateKlineAsync(string contractCode, string period, int size, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "period", period },
                { "size", size }
            };

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketEstimatedRateKline>>(
                uri: _baseClient.GetUrl(GetLinearSwapEstimatedRateKlineEndpoint, ApiPath.IndexMarketHistory),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketHisOpenInterest>> GetLinearSwapHisOpenInterestAsync(string period, int amountType, string contractCode, string? pair = null, string? contractType = null, int? size = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "period", period },
                { "amount_type", amountType }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("size", size);

            var result = await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketHisOpenInterest>(
                uri: _baseClient.GetUrl(GetLinearSwapHisOpenInterestEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketHistoryKline>>> GetLinearSwapHistoryKlineAsync(string contractCode, string period, long from, long to, int? size = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "period", period },
                { "from", from },
                { "to", to }
            };
            parameters.AddOptionalParameter("size", size);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketHistoryKline>>(
                uri: _baseClient.GetUrl(GetLinearSwapHistoryKlineEndpoint, ApiPath.LinearSwapExMarketHistory),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketTrade>> GetLinearSwapMarketTradeAsync(string? contractCode = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", businessType);

            var result = await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketTrade>(
                uri: _baseClient.GetUrl(GetLinearSwapMarketTradeEndpoint, ApiPath.LinearSwapExMarket),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketHistoryTrade>>> GetLinearSwapMarketHistoryTradeAsync(string contractCode, int size, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> 
            {
                { "contract_code", contractCode },
                { "size", size }
            };

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketHistoryTrade>>(
                uri: _baseClient.GetUrl(GetLinearSwapMarketHistoryTradeEndpoint, ApiPath.LinearSwapExMarketHistory),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketHistoryMarkKline>>> GetLinearSwapMarkPriceKlineAsync(string contractCode, string period, int size, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "period", period },
                { "size", size }
            };

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketHistoryMarkKline>>(
                uri: _baseClient.GetUrl(GetLinearSwapMarkPriceKlineEndpoint, ApiPath.IndexMarketHistory),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketDetailMerged>> GetLinearSwapMergedAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
            };

            var result = await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketDetailMerged>(
                uri: _baseClient.GetUrl(GetLinearSwapMergedEndpoint, ApiPath.LinearSwapExMarketDetail),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketPremiumIndexKline>>> GetLinearSwapPremiumIndexKlineAsync(string contractCode, string period, int size, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "period", period },
                { "size", size }
            };

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketPremiumIndexKline>>(
                uri: _baseClient.GetUrl(GetLinearSwapPremiumIndexKlineEndpoint, ApiPath.IndexMarketHistory),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }
    }
}
