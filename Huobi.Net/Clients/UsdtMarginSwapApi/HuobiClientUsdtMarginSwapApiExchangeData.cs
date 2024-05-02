using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapMarketData;
using Huobi.Net.Objects.Models.UsdtMarginSwap;

namespace Huobi.Net.Clients.UsdtMarginSwapApi
{
    /// <inheritdoc />
    public class HuobiClientUsdtMarginSwapApiExchangeData : IHuobiClientUsdtMarginSwapApiExchangeData
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

        internal HuobiClientUsdtMarginSwapApiExchangeData(HuobiClientUsdtMarginSwapApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendTimestampRequestAsync(_baseClient.GetUrl("api/v1/timestamp"), HttpMethod.Get, ct, ignoreRatelimit: true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiContractInfo>>> GetContractInfoAsync(string? contractCode = null, MarginMode? supportMarginMode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("support_margin_mode", supportMarginMode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiContractInfo>>(_baseClient.GetUrl("linear-swap-api/v1/swap_contract_info"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiSwapIndex>>> GetSwapIndexPriceAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiSwapIndex>>(_baseClient.GetUrl("linear-swap-api/v1/swap_index"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiPriceLimitation>>> GetSwapPriceLimitationAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiPriceLimitation>>(_baseClient.GetUrl("linear-swap-api/v1/swap_price_limit"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiOpenInterest>>> GetSwapOpenInterestAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiOpenInterest>>(_baseClient.GetUrl("linear-swap-api/v1/swap_open_interest"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiOrderBook>> GetOrderBookAsync(string contractCode, string step, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "type", step },
            };
            return await _baseClient.SendHuobiRequest<HuobiOrderBook>(_baseClient.GetUrl("linear-swap-ex/market/depth"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiSwapBestOffer>>> GetBestOfferAsync(string? contractCode = null, BusinessType? type = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(type));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiSwapBestOffer>>(_baseClient.GetUrl("linear-swap-ex/market/bbo"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string contractCode, KlineInterval interval, int? limit = null, DateTime? from = null, DateTime? to = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) }
            };
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("from", DateTimeConverter.ConvertToSeconds(from));
            parameters.AddOptionalParameter("to", DateTimeConverter.ConvertToSeconds(to));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiKline>>(_baseClient.GetUrl("linear-swap-ex/market/history/kline"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiMarketData>> GetMarketDataAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            return await _baseClient.SendHuobiRequest<HuobiMarketData>(_baseClient.GetUrl("linear-swap-ex/market/detail/merged"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiMarketData>>> GetMarketDatasAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiMarketData>>(_baseClient.GetUrl("linear-swap-ex/market/detail/batch_merged"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiLastTrade>> GetLastTradesAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            var result = await _baseClient.SendHuobiRequest<HuobiLastTradeWrapper>(_baseClient.GetUrl("linear-swap-ex/market/trade"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return result.As(result.Data?.Data?.First()!);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiTrade>>> GetRecentTradesAsync(string contractCode, int limit, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "size", limit }
            };
            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiTradeWrapper>>(_baseClient.GetUrl("/linear-swap-ex/market/history/trade"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
            return result.As<IEnumerable<HuobiTrade>>(result.Data?.SelectMany(d => d.Data)!);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiSwapRiskInfo>>> GetSwapRiskInfoAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiSwapRiskInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_risk_info"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiInsuranceInfo>> GetInsuranceFundHistoryAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiInsuranceInfo>(_baseClient.GetUrl("linear-swap-api/v1/swap_insurance_fund"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiSwapAdjustFactorInfo>>> GetIsolatedMarginAdjustFactorInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiSwapAdjustFactorInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_adjustfactor"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiCrossSwapAdjustFactorInfo>>> GetCrossMarginAdjustFactorInfoAsync(string? contractCode = null, string? asset = null, ContractType? type = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", asset);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(type));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiCrossSwapAdjustFactorInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_adjustfactor"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiOpenInterestValue>> GetOpenInterestAsync(InterestPeriod period, Unit unit, string? contractCode = null, string? symbol = null, ContractType? type = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "period", EnumConverter.GetString(period) },
                { "amount_type", EnumConverter.GetString(unit) },
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("size", limit);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(type));
            return await _baseClient.SendHuobiRequest<HuobiOpenInterestValue>(_baseClient.GetUrl("/linear-swap-api/v1/swap_his_open_interest"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiTieredMarginInfo>>> GetIsolatedMarginTieredInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiTieredMarginInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_ladder_margin"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiTieredCrossMarginInfo>>> GetCrossTieredMarginInfoAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiTieredCrossMarginInfo>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_ladder_margin"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiContractStatus>>> GetIsolatedStatusAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiContractStatus>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_api_state"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiCrossMarginTransferStatus>>> GetCrossMarginTransferStatusAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("margin_account", marginAccount);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiCrossMarginTransferStatus>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_transfer_state"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiCrossMarginTradeStatus>>> GetCrossMarginTradeStatusAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiCrossMarginTradeStatus>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_cross_trade_state"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiLiquidationOrderPage>> GetLiquidationOrdersAsync(int createDate, LiquidationTradeType tradeType, string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "create_date", createDate },
                { "trade_type", EnumConverter.GetString(tradeType) },
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiLiquidationOrderPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_liquidation_orders"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiSettlementPage>> GetHistoricalSettlementRecordsAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("start_time", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("end_time", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiSettlementPage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_settlement_records"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiFundingRate>> GetFundingRateAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>() {
                { "contract_code", contractCode }
            };
            return await _baseClient.SendHuobiRequest<HuobiFundingRate>(_baseClient.GetUrl("/linear-swap-api/v1/swap_funding_rate"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiFundingRate>>> GetFundingRatesAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiFundingRate>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_batch_funding_rate"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiFundingRatePage>> GetHistoricalFundingRatesAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("page_index", page);
            parameters.AddOptionalParameter("page_size", pageSize);
            return await _baseClient.SendHuobiRequest<HuobiFundingRatePage>(_baseClient.GetUrl("/linear-swap-api/v1/swap_historical_funding_rate"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiKline>>> GetPremiumIndexKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) },
                { "size", limit }
            };
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiKline>>(_baseClient.GetUrl("/index/market/history/linear_swap_premium_index_kline"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiKline>>> GetEstimatedFundingRateKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) },
                { "size", limit }
            };
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiKline>>(_baseClient.GetUrl("/index/market/history/linear_swap_estimated_rate_kline"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiBasisData>>> GetBasisDataAsync(string contractCode, KlineInterval interval, int limit, string? basisPriceType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) },
                { "size", limit }
            };
            parameters.AddOptionalParameter("basis_price_type", basisPriceType);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiBasisData>>(_baseClient.GetUrl("/index/market/history/linear_swap_basis"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiEstimatedSettlementPrice>>> GetEstimatedSettlementPriceAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", symbol);
            parameters.AddOptionalParameter("contract_type", EnumConverter.GetString(contractType));
            parameters.AddOptionalParameter("business_type", EnumConverter.GetString(businessType));
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiEstimatedSettlementPrice>>(_baseClient.GetUrl("/linear-swap-api/v1/swap_estimated_settlement_price"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }







        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketHistoryBasis>>> GetLinearSwapBasisAsync(string contractCode, string period, int size, string? basisPriceType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "period", period },
                { "size", size }
            };
            parameters.AddOptionalParameter("basis_price_type", basisPriceType);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketHistoryBasis>>(
                uri: _baseClient.GetUrl(GetLinearSwapBasisEndpoint, WWTApiPath.IndexMarketHistory.ToString()),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketDetailBatchMerged>>> GetLinearSwapBatchMergedV2Async(string? contractCode = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", businessType);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketDetailBatchMerged>>(
                uri: _baseClient.GetUrl(GetLinearSwapBatchMergedV2Endpoint, WWTApiPath.V2LinearSwapExMarketDetail.ToString()),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketBbo>>> GetLinearSwapBboAsync(string? contractCode = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", businessType);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketBbo>>(
                uri: _baseClient.GetUrl(GetLinearSwapBboEndpoint, WWTApiPath.LinearSwapExMarket.ToString()),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketDepth>> GetLinearSwapDepthAsync(string contractCode, string depthType, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "type", depthType }
            };

            var result = await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketDepth>(
                uri: _baseClient.GetUrl(GetLinearSwapDepthEndpoint, WWTApiPath.LinearSwapExMarket.ToString()),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketEstimatedRateKline>>> GetLinearSwapEstimatedRateKlineAsync(string contractCode, string period, int size, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "period", period },
                { "size", size }
            };

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketEstimatedRateKline>>(
                uri: _baseClient.GetUrl(GetLinearSwapEstimatedRateKlineEndpoint, WWTApiPath.IndexMarketHistory.ToString()),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketHisOpenInterest>> GetLinearSwapHisOpenInterestAsync(string period, int amountType, string contractCode, string? pair = null, string? contractType = null, int? size = null, CancellationToken ct = default)
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

            var result = await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketHisOpenInterest>(
                uri: _baseClient.GetUrl(GetLinearSwapHisOpenInterestEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketHistoryKline>>> GetLinearSwapHistoryKlineAsync(string contractCode, string period, long from, long to, int? size = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "period", period },
                { "from", from },
                { "to", to }
            };
            parameters.AddOptionalParameter("size", size);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketHistoryKline>>(
                uri: _baseClient.GetUrl(GetLinearSwapHistoryKlineEndpoint, WWTApiPath.LinearSwapExMarketHistory.ToString()),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketTrade>> GetLinearSwapMarketTradeAsync(string? contractCode = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", businessType);

            var result = await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketTrade>(
                uri: _baseClient.GetUrl(GetLinearSwapMarketTradeEndpoint, WWTApiPath.LinearSwapExMarket.ToString()),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketHistoryTrade>>> GetLinearSwapMarketHistoryTradeAsync(string contractCode, int size, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> 
            {
                { "contract_code", contractCode },
                { "size", size }
            };

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketHistoryTrade>>(
                uri: _baseClient.GetUrl(GetLinearSwapMarketHistoryTradeEndpoint, WWTApiPath.LinearSwapExMarketHistory.ToString()),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketHistoryMarkKline>>> GetLinearSwapMarkPriceKlineAsync(string contractCode, string period, int size, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "period", period },
                { "size", size }
            };

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketHistoryMarkKline>>(
                uri: _baseClient.GetUrl(GetLinearSwapMarkPriceKlineEndpoint, WWTApiPath.IndexMarketHistory.ToString()),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketDetailMerged>> GetLinearSwapMergedAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
            };

            var result = await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketDetailMerged>(
                uri: _baseClient.GetUrl(GetLinearSwapMergedEndpoint, WWTApiPath.LinearSwapExMarketDetail.ToString()),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketPremiumIndexKline>>> GetLinearSwapPremiumIndexKlineAsync(string contractCode, string period, int size, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "period", period },
                { "size", size }
            };

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketPremiumIndexKline>>(
                uri: _baseClient.GetUrl(GetLinearSwapPremiumIndexKlineEndpoint, WWTApiPath.IndexMarketHistory.ToString()),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }
    }
}
