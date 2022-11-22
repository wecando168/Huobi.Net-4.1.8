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
using Huobi.Net.Interfaces.Clients.UsdtMargined;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapReferenceData;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapMarketData;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapTrade.Request;

namespace Huobi.Net.Clients.UsdtMargined
{
    /// <inheritdoc />
    public class HuobiClientUsdtMarginedApiReferenceData : IHuobiClientUsdtMarginedApiReferenceData
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

        private readonly HuobiClientUsdtMarginedApi _baseClient;

        internal HuobiClientUsdtMarginedApiReferenceData(HuobiClientUsdtMarginedApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapUnifiedAccountType>> GetLinearSwapUnifiedAccountTypeAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendHuobiV3Request<HuobiUsdtMarginedMarketSwapUnifiedAccountType>(
                uri: _baseClient.GetUrl(GetLinearSwapUnifiedAccountTypeEndpoint, ApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: null,
                signed: true
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapSwitchAccountType>> LinearSwapSwitchAccountTypeAsync(int accountType, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "account_type", accountType }
            };

            var result = await _baseClient.SendHuobiV3Request<HuobiUsdtMarginedMarketSwapSwitchAccountType>(
                uri: _baseClient.GetUrl(LinearSwapSwitchAccountTypeEndpoint, ApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapFundingRate>> GetLinearSwapFundingRateAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode }
            };

            var result = await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapFundingRate>(
                uri: _baseClient.GetUrl(GetLinearSwapFundingRateEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }


        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapFundingRate>>> GetLinearSwapBatchFundingRateAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketSwapFundingRate>>(
                uri: _baseClient.GetUrl(GetLinearSwapBatchFundingRateEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketSwapHistoricalFundingRate>> GetLinearSwapHistoricalFundingRateAsync(string contractCode, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default)
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

            var result = await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketSwapHistoricalFundingRate>(
                uri: _baseClient.GetUrl(GetLinearSwapHistoricalFundingRateEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapLiquidationOrders>>> GetLinearSwapLiquidationOrdersAsync(int tradeType, string contractCode, string? pair = null, long? startTime = null, long? endTime = null, string? direct = null, long? fromId = null, CancellationToken ct = default)
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

            var result = await _baseClient.SendHuobiV3Request<IEnumerable<HuobiUsdtMarginedMarketSwapLiquidationOrders>>(
                uri: _baseClient.GetUrl(GetLinearSwapLiquidationOrdersEndpoint, ApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedSettlementRecords>> GetLinearSwapSettlementRecordsAsync(string contractCode, long? startTime = null, long? endTime = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);

            var result = await _baseClient.SendHuobiRequest<HuobiUsdtMarginedSettlementRecords>(
                uri: _baseClient.GetUrl(GetLinearSwapSettlementRecordsEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedEliteAccountRatio>> GetLinearSwapEliteAccountRatioAsync(string contractCode, string period, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "period", period }
            };

            var result = await _baseClient.SendHuobiRequest<HuobiUsdtMarginedEliteAccountRatio>(
                uri: _baseClient.GetUrl(GetLinearSwapEliteAccountRatioEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedElitePositionRatio>> GetLinearSwapElitePositionRatioAsync(string contractCode, string period, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode },
                { "period", period }
            };

            var result = await _baseClient.SendHuobiRequest<HuobiUsdtMarginedElitePositionRatio>(
                uri: _baseClient.GetUrl(GetLinearSwapElitePositionRatioEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapApiState>>> GetLinearSwapApiStateAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketSwapApiState>>(
                uri: _baseClient.GetUrl(GetLinearSwapApiStateEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketCrossLadderMargin>>> GetLinearSwapCrossLadderMarginAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("business_type", businessType);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketCrossLadderMargin>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossLadderMarginEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketLadderMargin>>> GetLinearSwapLadderMarginAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketLadderMargin>>(
                uri: _baseClient.GetUrl(GetLinearSwapLadderMarginEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedEliteSettlementPrice>>> GetLinearSwapEstimatedSettlementPriceAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("business_type", businessType);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedEliteSettlementPrice>>(
                uri: _baseClient.GetUrl(GetLinearSwapEstimatedSettlementPriceEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapAdjustfactor>>> GetLinearSwapAdjustfactorAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketSwapAdjustfactor>>(
                uri: _baseClient.GetUrl(GetLinearSwapAdjustfactorEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapCrossAdjustfactor>>> GetLinearSwapCrossAdjustfactorAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("business_type", businessType);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketSwapCrossAdjustfactor>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossAdjustfactorEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedInsuranceFund>> GetLinearSwapInsuranceFundAsync(string contractCode, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode }
            };
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);

            var result = await _baseClient.SendHuobiRequest<HuobiUsdtMarginedInsuranceFund>(
                uri: _baseClient.GetUrl(GetLinearSwapInsuranceFundEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedRiskInfo>>> GetLinearSwapRiskInfoAsync(string? contractCode = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("business_type", businessType);

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedRiskInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapRiskInfoEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapPriceLimit>>> GetLinearSwapPriceLimitAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pair", pair?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("contract_type", contractType?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("business_type", businessType?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketSwapPriceLimit>>(
                uri: _baseClient.GetUrl(GetLinearSwapPriceLimitEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapOpenInterest>>> GetLinearSwapOpenInterestAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pair", pair?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("contract_type", contractType?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("business_type", businessType?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketSwapOpenInterest>>(
                uri: _baseClient.GetUrl(GetLinearSwapOpenInterestEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapContractInfo>>> GetLinearSwapContractInfoAsync(string? contractCode = null, string? supportMarginMode = null, string? pari = null, string? contractType = null, string? businessType = "all", CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("support_margin_mode", supportMarginMode?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("pari", pari?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("contract_type", contractType?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("business_type", businessType?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketSwapContractInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapContractInfoEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: parameters,
                signed: false
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapIndex>>> GetLinearSwapIndexAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode?.ToString(CultureInfo.InvariantCulture));

            var result = await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedMarketSwapIndex>>(
                uri: _baseClient.GetUrl(GetLinearSwapContractInfoEndpoint, ApiPath.LinearSwapApi, "1"),
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
            var result = await _baseClient.SendHuobiTimestampRequest<HuobiUsdtMarginedMarketTimestamp>(
                uri: new Uri(GetLinearSwapTimestampEndpoint),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: null,
                signed: false
                ).ConfigureAwait(false);
            return result.As((long)DateTimeConverter.ConvertToMilliseconds(result.Data.Item2));
        }

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetLinearSwapServerDateTimeAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.SendHuobiTimestampRequest<HuobiUsdtMarginedMarketTimestamp>(
                uri: new Uri(GetLinearSwapTimestampEndpoint),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: null,
                signed: false
                ).ConfigureAwait(false);
            return result.As(result.Data.Item2);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMarketHeartbeat>> GetLinearSwapHeartbeatAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketHeartbeat>(
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
        public async Task<WebCallResult<HuobiUsdtMarginedMarketStatus>> GetLinearSwapSummaryAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMarketStatus>(
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
