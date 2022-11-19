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
using CryptoExchange.Net.CommonObjects;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapAccount;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapAccount.CommonBaseModels;

namespace Huobi.Net.Clients.UsdtMargined
{
    /// <inheritdoc />
    public class HuobiClientUsdtMarginedApiAccount : IHuobiClientUsdtMarginedApiAccount
    {
        // 账户接口
        private const string GetLinearSwapBalanceValuationEndpoint = "swap_balance_valuation";                          // 【通用】获取账户总资产估值(PrivateData)
        private const string GetLinearSwapAccountInfoEndpoint = "swap_account_info";                                    // 【逐仓】获取用户的合约账户信息(PrivateData)
        private const string GetLinearSwapCrossAccountInfoEndpoint = "swap_cross_account_info";                         // 【全仓】获取用户的合约账户信息(PrivateData)
        private const string GetLinearSwapPositionInfoEndpoint = "swap_position_info";                                  // 【逐仓】获取用户的合约持仓信息(PrivateData)
        private const string GetLinearSwapCrossPositionInfoEndpoint = "swap_cross_position_info";                       // 【全仓】获取用户的合约持仓信息(PrivateData)
        private const string GetLinearSwapAccountPositionInfoEndpoint = "swap_account_position_info";                   // 【逐仓】获取用户资产和持仓信息(PrivateData)
        private const string GetLinearSwapCrossAccountPositionInfoEndpoint = "swap_cross_account_position_info";        // 【全仓】获取用户资产和持仓信息(PrivateData)
        private const string SetLinearSwapSubAuthEndpoint = "swap_sub_auth";                                            // 【通用】批量设置子账户交易权限(PrivateData)
        private const string GetLinearSwapSubAccountListEndpoint = "swap_sub_account_list";                             // 【逐仓】查询母账户下所有子账户资产信息(PrivateData)
        private const string GetLinearSwapCrossSubAccountListEndpoint = "swap_cross_sub_account_list";                  // 【全仓】查询母账户下所有子账户资产信息(PrivateData)
        private const string GetLinearSwapSubAccountInfoListEndpoint = "swap_sub_account_info_list";                    // 【逐仓】批量获取子账户资产信息(PrivateData)
        private const string GetLinearSwapCrossSubAccountInfoListEndpoint = "swap_cross_sub_account_info_list";         // 【全仓】批量获取子账户资产信息(PrivateData)
        private const string GetLinearSwapSubAccountInfoEndpoint = "swap_sub_account_info";                             // 【逐仓】查询母账户下的单个子账户资产信息(PrivateData)
        private const string GetLinearSwapCrossSubAccountInfoEndpoint = "swap_cross_sub_account_info";                  // 【全仓】查询母账户下的单个子账户资产信息(PrivateData)
        private const string GetLinearSwapSubPositionInfoEndpoint = "swap_sub_position_info";                           // 【逐仓】查询母账户下的单个子账户持仓信息(PrivateData)
        private const string GetLinearSwapCrossSubPositionInfoEndpoint = "swap_cross_sub_position_info";                // 【全仓】查询母账户下的单个子账户持仓信息(PrivateData)
        private const string GetLinearSwapFinancialRecordEndpoint = "swap_financial_record";                            // 【通用】查询用户财务记录(PrivateData)
        private const string GetLinearSwapFinancialRecordExactEndpoint = "swap_financial_record_exact";                 // 【通用】组合查询用户财务记录(PrivateData)
        private const string GetLinearSwapUserSettlementRecordsEndpoint = "swap_user_settlement_records";               // 【逐仓】查询用户结算记录(PrivateData)
        private const string GetLinearSwapCrossUserSettlementRecordsEndpoint = "swap_cross_user_settlement_records";    // 【全仓】查询用户结算记录(PrivateData)
        private const string GetLinearSwapAvailableLevelRateEndpoint = "swap_available_level_rate";                     // 【逐仓】查询用户可用杠杆倍数(PrivateData)
        private const string GetLinearSwapCrossAvailableLevelRateEndpoint = "swap_cross_available_level_rate";          // 【全仓】查询用户可用杠杆倍数(PrivateData)
        private const string GetLinearSwapOrderLimitEndpoint = "swap_order_limit";                                      // 【通用】获取用户的合约下单量限制(PrivateData)
        private const string GetLinearSwapFeeEndpoint = "swap_fee";                                                     // 【通用】获取用户的合约手续费费率(PrivateData)
        private const string GetLinearSwapTransferLimitEndpoint = "swap_transfer_limit";                                // 【逐仓】获取用户的合约划转限制(PrivateData)
        private const string GetLinearSwapCrossTransferLimitEndpoint = "swap_cross_transfer_limit";                     // 【全仓】获取用户的合约划转限制(PrivateData)              
        private const string GetLinearSwapPositionLimitEndpoint = "swap_position_limit";                                // 【逐仓】获取用户的合约持仓量限制(PrivateData)
        private const string GetLinearSwapCrossPositionLimitEndpoint = "swap_cross_position_limit";                     // 【全仓】获取用户的合约持仓量限制(PrivateData)
        private const string GetLinearSwapLeverPositionLimitEndpoint = "swap_lever_position_limit";                     // 【逐仓】查询用户所有杠杆持仓量限制(PrivateData)  
        private const string GetLinearSwapCrossLeverPositionLimitEndpoint = "swap_cross_lever_position_limit";          // 【全仓】查询用户所有杠杆持仓量限制(PrivateData)
        private const string LinearSwapMasterSubTransferEndpoint = "swap_master_sub_transfer";                          // 【通用】母子账户划转(PrivateData)
        private const string GetLinearSwapMasterSubTransferRecordEndpoint = "swap_master_sub_transfer_record";          // 【通用】获取母账户下的所有母子账户划转记录(PrivateData)
        private const string LinearSwapTransferInnerEndpoint = "swap_transfer_inner";                                   // 【通用】同账号不同保证金账户的划转(PrivateData)
        private const string GetLinearSwapTradingStatusEndpoint = "swap_api_trading_status";                            // 【通用】获取用户API指标禁用信息(PrivateData)

        private readonly HuobiClientUsdtMarginedApi _baseClient;

        internal HuobiClientUsdtMarginedApiAccount(HuobiClientUsdtMarginedApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedBalanceValuation>>> GetLinearSwapBalanceValuation(string? valuationAsset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("valuation_asset", valuationAsset);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedBalanceValuation>>(
                uri: _baseClient.GetUrl(GetLinearSwapBalanceValuationEndpoint, ApiPath.LinearSwapApi, "1"),
                method:HttpMethod.Post,
                cancellationToken:ct,
                parameters: parameters, 
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedAccountInfo>>> GetLinearSwapAccountInfo(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);

            var result =  await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedAccountInfo>>(
                uri:_baseClient.GetUrl(GetLinearSwapAccountInfoEndpoint, ApiPath.LinearSwapApi, "1"), 
                method:HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossAccountInfo>>> GetLinearSwapCrossAccountInfo(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("margin_account", marginAccount);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedCrossAccountInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossAccountInfoEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false); 
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedPositionInfo>>> GetLinearSwapPositionInfo(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedPositionInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapPositionInfoEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossPositionInfo>>> GetLinearSwapCrossPositionInfo(string? contractCode = null, string? pair = null, UmContractType? umContractType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", object.Equals(umContractType, null) ? null : umContractType.Value.ToString());

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedCrossPositionInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossPositionInfoEndpoint, ApiPath.LinearSwapApi, "1"), 
                method: HttpMethod.Post, 
                cancellationToken: ct, 
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedAccountPositionInfo>>> GetLinearSwapAccountPositionInfo(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode }
            };

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedAccountPositionInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapAccountPositionInfoEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedCrossAccountPositionInfo>> GetLinearSwapCrossAccountPositionInfo(string marginAccount, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "margin_account", marginAccount }
            };

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedCrossAccountPositionInfo>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossAccountPositionInfoEndpoint, ApiPath.LinearSwapApi, "1"), 
                method: HttpMethod.Post, 
                cancellationToken: ct, 
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedSubAuth>> SetLinearSwapSubAuth(IEnumerable<string> subUidList, int subAuth, CancellationToken ct = default)
        {
            if(subUidList == null && subUidList.Count() > 10)
            {
                throw new ArgumentNullException("Max 10 accounts can be operated in batches each time.");
            }

            var parameters = new Dictionary<string, object>
            {
                { "sub_uid", string.Join(",", subUidList.ToArray() )},
                { "sub_auth", subAuth }
            };

            var result  = await _baseClient.SendHuobiRequest<HuobiUsdtMarginedSubAuth>(uri: _baseClient.GetUrl(
                SetLinearSwapSubAuthEndpoint, ApiPath.LinearSwapApi, "1"), 
                method: HttpMethod.Post, 
                cancellationToken: ct, 
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedSubAccountList>>> GetLinearSwapSubAccountList(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedSubAccountList>>(
                uri: _baseClient.GetUrl(GetLinearSwapSubAccountListEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossSubAccountList>>> GetLinearSwapCrossSubAccountList(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("margin_account", marginAccount);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedCrossSubAccountList>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossSubAccountListEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedSubAccountInfoList>> GetLinearSwapSubAccountInfoList(string? contratcCode = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contratc_code", contratcCode);
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedSubAccountInfoList>(
                uri: _baseClient.GetUrl(GetLinearSwapSubAccountInfoListEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedCrossSubAccountInfoList>> GetLinearSwapCrossSubAccountInfoList(string? marginAccount = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("margin_account", marginAccount);
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedCrossSubAccountInfoList>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossSubAccountInfoListEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedAccountPositionInfo>>> GetLinearSwapSubAccountInfo(long subUid, string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"sub_uid",subUid }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedAccountPositionInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapSubAccountInfoEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossAccountPositionInfo>>> GetLinearSwapCrossSubAccountInfo(long subUid, string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"sub_uid",subUid }
            };
            parameters.AddOptionalParameter("margin_account", marginAccount);
            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedCrossAccountPositionInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossSubAccountInfoEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiPosition>>> GetLinearSwapSubPositionInfo(long subUid, string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"sub_uid",subUid }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiPosition>>(
                uri: _baseClient.GetUrl(GetLinearSwapSubPositionInfoEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossSubPositionInfo>>> GetLinearSwapCrossSubPositionInfo(long subUid, string? contractCode = null, string? pair = null, string? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"sub_uid",subUid }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedCrossSubPositionInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossSubPositionInfoEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedFinancialRecord>>> GetLinearSwapFinancialRecord(string marginAccount, string? contractCode = null, string? type = null, long? startTime = null, long? endTime = null, string? direct = null, long? fromId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"mar_acct",marginAccount }
            };
            parameters.AddOptionalParameter("contract", contractCode);
            parameters.AddOptionalParameter("type", type);
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("direct", direct);
            parameters.AddOptionalParameter("from_id", fromId);

            return await _baseClient.SendHuobiV3Request<IEnumerable<HuobiUsdtMarginedFinancialRecord>>(
                uri: _baseClient.GetUrl(GetLinearSwapFinancialRecordEndpoint, ApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedFinancialRecord>>> GetLinearSwapFinancialRecordExact(string marginAccount, string? contractCode = null, string? type = null, long? startTime = null, long? endTime = null, string? direct = null, long? fromId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"mar_acct",marginAccount }
            };
            parameters.AddOptionalParameter("contract", contractCode);
            parameters.AddOptionalParameter("type", type);
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("direct", direct);
            parameters.AddOptionalParameter("from_id", fromId);

            return await _baseClient.SendHuobiV3Request<IEnumerable<HuobiUsdtMarginedFinancialRecord>>(
                uri: _baseClient.GetUrl(GetLinearSwapFinancialRecordExactEndpoint, ApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedUserSettlementRecords>> GetLinearSwapUserSettlementRecords(string contractCode, long? startTime = null, long? endTime = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code",contractCode }
            };
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedUserSettlementRecords>(
                uri: _baseClient.GetUrl(GetLinearSwapUserSettlementRecordsEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedCrossUserSettlementRecords>> GetLinearSwapCrossUserSettlementRecords(string marginAccount, long? startTime = null, long? endTime = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"margin_account",marginAccount }
            };
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedCrossUserSettlementRecords>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossUserSettlementRecordsEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedAvailableLevelRate>>> GetLinearSwapAvailableLevelRate(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedAvailableLevelRate>>(
                uri: _baseClient.GetUrl(GetLinearSwapAvailableLevelRateEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossUserAvailableLevelRate>>> GetLinearSwapCrossAvailableLevelRate(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("business_type", businessType);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedCrossUserAvailableLevelRate>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossAvailableLevelRateEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedSwapOrderLimit>> GetLinearSwapOrderLimit(string orderPriceType, string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"order_price_type",orderPriceType }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("business_type", businessType);

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedSwapOrderLimit>(
                uri: _baseClient.GetUrl(GetLinearSwapOrderLimitEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedSwapFee>>> GetLinearSwapFee(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("business_type", businessType);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedSwapFee>>(
                uri: _baseClient.GetUrl(GetLinearSwapFeeEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedTransferLimit>>> GetLinearSwapTransferLimit(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedTransferLimit>>(
                uri: _baseClient.GetUrl(GetLinearSwapTransferLimitEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossTransferLimit>>> GetLinearSwapCrossTransferLimit(string ? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("margin_account", marginAccount);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedCrossTransferLimit>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossTransferLimitEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedPositionLimit>>> GetLinearSwapPositionLimit(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedPositionLimit>>(
                uri: _baseClient.GetUrl(GetLinearSwapPositionLimitEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossPositionLimit>>> GetLinearSwapCrossPositionLimit(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("business_type", businessType);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedCrossPositionLimit>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossPositionLimitEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedLeverPositionLimit>>> GetLinearSwapLeverPositionLimit(string? contractCode = null, int? leverRate = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("lever_rate", leverRate);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedLeverPositionLimit>>(
                uri: _baseClient.GetUrl(GetLinearSwapLeverPositionLimitEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossLeverPositionLimit>>> GetLinearSwapCrossLeverPositionLimit(string? businessType = null, string? contractType = null, string? pair = null, string? contractCode = null, int? leverRate = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("business_type", businessType);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("lever_rate", leverRate);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedCrossLeverPositionLimit>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossLeverPositionLimitEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedMasterSubTransfer>> LinearSwapMasterSubTransfer(long subUid, string asset, string fromMarginAccount, string toMarginAccount, decimal amount, string type, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"sub_uid",subUid },
                {"asset",asset },
                {"from_margin_account",fromMarginAccount },
                {"to_margin_account",toMarginAccount },
                {"amount",amount },
                {"type",type }
            };
            parameters.AddOptionalParameter("client_order_id", clientOrderId);

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedMasterSubTransfer>(
                uri: _baseClient.GetUrl(LinearSwapMasterSubTransferEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<object>>> GetLinearSwapMasterSubTransferRecord(string marginAccount, int createDate, string? transferType = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"margin_account",marginAccount },
                {"create_date",createDate }
            };
            parameters.AddOptionalParameter("transfer_type", transferType);
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);

            return await _baseClient.SendHuobiRequest<IEnumerable<object>>(
                uri: _baseClient.GetUrl(GetLinearSwapMasterSubTransferRecordEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedTransferInner>> LinearSwapTransferInner(string asset, string fromMarginAccount, string toMarginAccount, decimal amount, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"asset",asset },
                {"from_margin_account",fromMarginAccount },
                {"to_margin_account	",toMarginAccount },
                {"amount",amount }
            };
            parameters.AddOptionalParameter("client_order_id", clientOrderId);

            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedTransferInner>(
                uri: _baseClient.GetUrl(LinearSwapTransferInnerEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<HuobiUsdtMarginedApiTradingStatus>> GetLinearSwapApiTradingStatus(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<HuobiUsdtMarginedApiTradingStatus>(
                uri: _baseClient.GetUrl(GetLinearSwapTradingStatusEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: null,
                signed: true
                ).ConfigureAwait(false);
        }
    }
}
