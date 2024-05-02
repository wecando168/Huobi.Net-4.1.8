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
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapReferenceData;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapMarketData;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapTrade.Request;

namespace Huobi.Net.Clients.UsdtMarginSwapApi
{
    /// <inheritdoc />
    public class HuobiClientUsdtMarginSwapApiReferenceData : IHuobiClientUsdtMarginSwapReferenceData
    {
        //基础信息接口
        private const string GetLinearSwapUnifiedAccountTypeEndpoint = "/swap_unified_account_type";                            // 【通用】账户类型查询(PublicData)
        private const string LinearSwapSwitchAccountTypeEndpoint = "/swap_switch_account_type";                                 // 【通用】账户类型更改接口(PublicData)
        private const string GetLinearSwapFundingRateEndpoint = "/swap_funding_rate";                                           // 【通用】获取合约的资金费率
        private const string GetLinearSwapBatchFundingRateEndpoint = "/swap_batch_funding_rate";                                // 【通用】批量获取合约资金费率
        private const string GetLinearSwapHistoricalFundingRateEndpoint = "/swap_historical_funding_rate";                      // 【通用】获取合约的历史资金费率
        private const string GetLinearSwapLiquidationOrdersEndpoint = "/swap_liquidation_orders";                               // 【通用】获取强平订单(PublicData)
        private const string GetLinearSwapSettlementRecordsEndpoint = "/swap_settlement_records";                               // 【通用】平台历史结算记录(PublicData)
        private const string GetLinearSwapEliteAccountRatioEndpoint = "/swap_elite_account_ratio";                              // 【通用】精英账户多空持仓对比-账户数(PublicData)
        private const string GetLinearSwapElitePositionRatioEndpoint = "/swap_elite_position_ratio";                            // 【通用】精英账户多空持仓对比-持仓量(PublicData)
        private const string GetLinearSwapApiStateEndpoint = "/swap_api_state";                                                 // 【逐仓】查询系统状态(PublicData)
        private const string GetLinearSwapCrossLadderMarginEndpoint = "/swap_api_state";                                        // 【全仓】获取平台阶梯保证金
        private const string GetLinearSwapLadderMarginEndpoint = "/swap_ladder_margin";                                         // 【逐仓】获取平台阶梯保证金
        private const string GetLinearSwapEstimatedSettlementPriceEndpoint = "/swap_estimated_settlement_price";                // 【通用】获取预估结算价
        private const string GetLinearSwapAdjustfactorEndpoint = "/swap_adjustfactor";                                          // 【逐仓】查询平台阶梯调整系数(PublicData)
        private const string GetLinearSwapCrossAdjustfactorEndpoint = "/swap_cross_adjustfactor";                               // 【全仓】查询平台阶梯调整系数(PublicData)
        private const string GetLinearSwapInsuranceFundEndpoint = "/swap_insurance_fund";                                       // 【通用】获取风险准备金历史数据(PublicData)
        private const string GetLinearSwapRiskInfoEndpoint = "/swap_risk_info";                                                 // 【通用】查询合约风险准备金和预估分摊比例(PublicData)
        private const string GetLinearSwapPriceLimitEndpoint = "/swap_price_limit";                                             // 【通用】获取合约最高限价和最低限价(PublicData)
        private const string GetLinearSwapOpenInterestEndpoint = "/swap_open_interest";                                         // 【通用】获取当前合约总持仓量(PublicData)
        private const string GetLinearSwapContractInfoEndpoint = "/swap_contract_info";                                         // 【通用】获取合约信息(PublicData)
        private const string GetLinearSwapIndexEndpoint = "/swap_index";                                                        // 【通用】获取合约指数信息(PublicData)
        private const string GetLinearSwapTimestampEndpoint = "https://api.hbdm.com/api/v1/timestamp";                          // 【通用】获取当前系统时间戳(PublicData)
        private const string GetLinearSwapHeartbeatEndpoint = "https://api.hbdm.com/heartbeat/";                                // 【通用】查询系统是否可用(PublicData)
        private const string GetLinearSwapSummaryEndpoint = "https://status-linear-swap.huobigroup.com/api/v2/summary.json";    // 【通用】获取当前系统状态(PublicData)

        private readonly HuobiClientUsdtMarginSwapApi _baseClient;

        internal HuobiClientUsdtMarginSwapApiReferenceData(HuobiClientUsdtMarginSwapApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapUnifiedAccountType>> GetLinearSwapUnifiedAccountTypeAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendHuobiV3Request<WWTHuobiUsdtMarginedMarketSwapUnifiedAccountType>(
                uri: _baseClient.GetUrl(GetLinearSwapUnifiedAccountTypeEndpoint, WWTApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: null,
                signed: true
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapSwitchAccountType>> LinearSwapSwitchAccountTypeAsync(int accountType, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "account_type", accountType }
            };

            var result = await _baseClient.SendHuobiV3Request<WWTHuobiUsdtMarginedMarketSwapSwitchAccountType>(
                uri: _baseClient.GetUrl(LinearSwapSwitchAccountTypeEndpoint, WWTApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapFundingRate>> GetLinearSwapFundingRateAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode }
            };

            var result = await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapFundingRate>(
                uri: _baseClient.GetUrl(GetLinearSwapFundingRateEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }


        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapFundingRate>>> GetLinearSwapBatchFundingRateAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketSwapFundingRate>>(
                uri: _baseClient.GetUrl(GetLinearSwapBatchFundingRateEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketSwapHistoricalFundingRate>> GetLinearSwapHistoricalFundingRateAsync(string contractCode, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default)
        {
            if (pageSize > 50)
            {
                pageSize = 50;
            }
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("page_index", pageIndex == null ? 1 : pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize == null ? 20 : pageSize);

            var result = await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketSwapHistoricalFundingRate>(
                uri: _baseClient.GetUrl(GetLinearSwapHistoricalFundingRateEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapLiquidationOrders>>> GetLinearSwapLiquidationOrdersAsync(int tradeType, string contractCode, string? pair = null, long? startTime = null, long? endTime = null, string? direct = null, long? fromId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "trade_type", tradeType },
                { "contract", contractCode }
            };
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("direct", direct);
            parameters.AddOptionalParameter("from_id", fromId);

            var result = await _baseClient.SendHuobiV3Request<IEnumerable<WWTHuobiUsdtMarginedMarketSwapLiquidationOrders>>(
                uri: _baseClient.GetUrl(GetLinearSwapLiquidationOrdersEndpoint, WWTApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedSettlementRecords>> GetLinearSwapSettlementRecordsAsync(string contractCode, long? startTime = null, long? endTime = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);

            var result = await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedSettlementRecords>(
                uri: _baseClient.GetUrl(GetLinearSwapSettlementRecordsEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedEliteAccountRatio>> GetLinearSwapEliteAccountRatioAsync(string contractCode, string period, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "period", period }
            };

            var result = await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedEliteAccountRatio>(
                uri: _baseClient.GetUrl(GetLinearSwapEliteAccountRatioEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedElitePositionRatio>> GetLinearSwapElitePositionRatioAsync(string contractCode, string period, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "period", period }
            };

            var result = await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedElitePositionRatio>(
                uri: _baseClient.GetUrl(GetLinearSwapElitePositionRatioEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapIsolatedApiState>>> GetLinearSwapApiStateAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketSwapIsolatedApiState>>(
                uri: _baseClient.GetUrl(GetLinearSwapApiStateEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketCrossLadderMargin>>> GetLinearSwapCrossLadderMarginAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("business_type", businessType);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketCrossLadderMargin>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossLadderMarginEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketIsolatedLadderMargin>>> GetLinearSwapLadderMarginAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketIsolatedLadderMargin>>(
                uri: _baseClient.GetUrl(GetLinearSwapLadderMarginEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedEliteSettlementPrice>>> GetLinearSwapEstimatedSettlementPriceAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("business_type", businessType);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedEliteSettlementPrice>>(
                uri: _baseClient.GetUrl(GetLinearSwapEstimatedSettlementPriceEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapIsolatedAdjustfactor>>> GetLinearSwapAdjustfactorAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketSwapIsolatedAdjustfactor>>(
                uri: _baseClient.GetUrl(GetLinearSwapAdjustfactorEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapCrossAdjustfactor>>> GetLinearSwapCrossAdjustfactorAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("business_type", businessType);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketSwapCrossAdjustfactor>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossAdjustfactorEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedInsuranceFund>> GetLinearSwapInsuranceFundAsync(string contractCode, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);

            var result = await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedInsuranceFund>(
                uri: _baseClient.GetUrl(GetLinearSwapInsuranceFundEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedRiskInfo>>> GetLinearSwapRiskInfoAsync(string? contractCode = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", businessType);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedRiskInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapRiskInfoEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapPriceLimit>>> GetLinearSwapPriceLimitAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pair", pair?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("contract_type", contractType?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("business_type", businessType?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketSwapPriceLimit>>(
                uri: _baseClient.GetUrl(GetLinearSwapPriceLimitEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapOpenInterest>>> GetLinearSwapOpenInterestAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pair", pair?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("contract_type", contractType?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("business_type", businessType?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketSwapOpenInterest>>(
                uri: _baseClient.GetUrl(GetLinearSwapOpenInterestEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapContractInfo>>> GetLinearSwapContractInfoAsync(string? contractCode = null, string? supportMarginMode = null, string? pari = null, string? contractType = null, string? businessType = "all", CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("support_margin_mode", supportMarginMode?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pari", pari?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("contract_type", contractType?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("business_type", businessType?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketSwapContractInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapContractInfoEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketSwapIndex>>> GetLinearSwapIndexAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedMarketSwapIndex>>(
                uri: _baseClient.GetUrl(GetLinearSwapContractInfoEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<long>> GetLinearSwapServerTimestampAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendHuobiTimeRequestAsync(
                uri: new Uri(GetLinearSwapTimestampEndpoint),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: null,
                signed: false
                ).ConfigureAwait(false);
            return result.As((long)DateTimeConverter.ConvertToMilliseconds(result.Data));
        }

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetLinearSwapServerDateTimeAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendHuobiTimeRequestAsync(
                uri: new Uri(GetLinearSwapTimestampEndpoint),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: null,
                signed: false
                ).ConfigureAwait(false);
            return result.As(result.Data);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketHeartbeat>> GetLinearSwapHeartbeatAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketHeartbeat>(
                uri: new Uri(GetLinearSwapHeartbeatEndpoint),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: null,
                signed: false,
                weight: 5,
                ignoreRatelimit: false
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMarketStatus>> GetLinearSwapSummaryAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMarketStatus>(
                uri: new Uri(GetLinearSwapSummaryEndpoint),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: null,
                signed: false,
                weight: 5,
                ignoreRatelimit: false
                ).ConfigureAwait(false);
        }
    }
}
