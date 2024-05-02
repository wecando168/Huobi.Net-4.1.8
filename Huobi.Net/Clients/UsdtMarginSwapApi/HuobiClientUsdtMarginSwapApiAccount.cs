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
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;
using CryptoExchange.Net.CommonObjects;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount.CommonBaseModels;

namespace Huobi.Net.Clients.UsdtMarginSwapApi
{
    /// <inheritdoc />
    public class HuobiClientUsdtMarginSwapApiAccount : IHuobiClientUsdtMarginSwapAccount
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

        private readonly HuobiClientUsdtMarginSwapApi _baseClient;

        internal HuobiClientUsdtMarginSwapApiAccount(HuobiClientUsdtMarginSwapApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedBalanceValuation>>> GetLinearSwapBalanceValuationAsync(string? valuationAsset = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("valuation_asset", valuationAsset);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedBalanceValuation>>(
                uri: _baseClient.GetUrl(GetLinearSwapBalanceValuationEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method:HttpMethod.Post,
                cancellationToken:ct,
                parameters: parameters, 
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedIsolatedAccountInfo>>> GetLinearSwapAccountInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);

            var result =  await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedIsolatedAccountInfo>>(
                uri:_baseClient.GetUrl(GetLinearSwapAccountInfoEndpoint, WWTApiPath.LinearSwapApi, "1"), 
                method:HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedCrossAccountInfo>>> GetLinearSwapCrossAccountInfoAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("margin_account", marginAccount);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedCrossAccountInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossAccountInfoEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false); 
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedIsolatedPositionInfo>>> GetLinearSwapPositionInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedIsolatedPositionInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapPositionInfoEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedCrossPositionInfo>>> GetLinearSwapCrossPositionInfoAsync(string? contractCode = null, string? pair = null, UmContractType? umContractType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", object.Equals(umContractType, null) ? null : umContractType.Value.ToString());

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedCrossPositionInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossPositionInfoEndpoint, WWTApiPath.LinearSwapApi, "1"), 
                method: HttpMethod.Post, 
                cancellationToken: ct, 
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedIsolatedAccountPositionInfo>>> GetLinearSwapAccountPositionInfoAsync(string contractCode, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "contract_code", contractCode }
            };

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedIsolatedAccountPositionInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapAccountPositionInfoEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedCrossAccountPositionInfo>> GetLinearSwapCrossAccountPositionInfoAsync(string marginAccount, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "margin_account", marginAccount }
            };

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedCrossAccountPositionInfo>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossAccountPositionInfoEndpoint, WWTApiPath.LinearSwapApi, "1"), 
                method: HttpMethod.Post, 
                cancellationToken: ct, 
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedSubAuth>> SetLinearSwapSubAuthAsync(IEnumerable<string> subUidList, int subAuth, CancellationToken ct = default)
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

            var result  = await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedSubAuth>(uri: _baseClient.GetUrl(
                SetLinearSwapSubAuthEndpoint, WWTApiPath.LinearSwapApi, "1"), 
                method: HttpMethod.Post, 
                cancellationToken: ct, 
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedIsolatedSubAccountList>>> GetLinearSwapSubAccountListAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedIsolatedSubAccountList>>(
                uri: _baseClient.GetUrl(GetLinearSwapSubAccountListEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedCrossSubAccountList>>> GetLinearSwapCrossSubAccountListAsync(string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("margin_account", marginAccount);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedCrossSubAccountList>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossSubAccountListEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedIsolatedSubAccountInfoList>> GetLinearSwapSubAccountInfoListAsync(string? contratcCode = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contratc_code", contratcCode);
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedIsolatedSubAccountInfoList>(
                uri: _baseClient.GetUrl(GetLinearSwapSubAccountInfoListEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedCrossSubAccountInfoList>> GetLinearSwapCrossSubAccountInfoListAsync(string? marginAccount = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("margin_account", marginAccount);
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedCrossSubAccountInfoList>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossSubAccountInfoListEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedIsolatedAccountPositionInfo>>> GetLinearSwapSubAccountInfoAsync(long subUid, string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"sub_uid",subUid }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedIsolatedAccountPositionInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapSubAccountInfoEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedCrossAccountPositionInfo>>> GetLinearSwapCrossSubAccountInfoAsync(long subUid, string? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"sub_uid",subUid }
            };
            parameters.AddOptionalParameter("margin_account", marginAccount);
            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedCrossAccountPositionInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossSubAccountInfoEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiPosition>>> GetLinearSwapSubPositionInfoAsync(long subUid, string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"sub_uid",subUid }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiPosition>>(
                uri: _baseClient.GetUrl(GetLinearSwapSubPositionInfoEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedCrossSubPositionInfo>>> GetLinearSwapCrossSubPositionInfoAsync(long subUid, string? contractCode = null, string? pair = null, string? contractType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"sub_uid",subUid }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedCrossSubPositionInfo>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossSubPositionInfoEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedFinancialRecord>>> GetLinearSwapFinancialRecordAsync(string marginAccount, string? contractCode = null, string? type = null, long? startTime = null, long? endTime = null, string? direct = null, long? fromId = null, CancellationToken ct = default)
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

            return await _baseClient.SendHuobiV3Request<IEnumerable<WWTHuobiUsdtMarginedFinancialRecord>>(
                uri: _baseClient.GetUrl(GetLinearSwapFinancialRecordEndpoint, WWTApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedFinancialRecord>>> GetLinearSwapFinancialRecordExactAsync(string marginAccount, string? contractCode = null, string? type = null, long? startTime = null, long? endTime = null, string? direct = null, long? fromId = null, CancellationToken ct = default)
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

            return await _baseClient.SendHuobiV3Request<IEnumerable<WWTHuobiUsdtMarginedFinancialRecord>>(
                uri: _baseClient.GetUrl(GetLinearSwapFinancialRecordExactEndpoint, WWTApiPath.LinearSwapApi, "3"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedIsolatedUserSettlementRecords>> GetLinearSwapUserSettlementRecordsAsync(string contractCode, long? startTime = null, long? endTime = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"contract_code",contractCode }
            };
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedIsolatedUserSettlementRecords>(
                uri: _baseClient.GetUrl(GetLinearSwapUserSettlementRecordsEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedCrossUserSettlementRecords>> GetLinearSwapCrossUserSettlementRecordsAsync(string marginAccount, long? startTime = null, long? endTime = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"margin_account",marginAccount }
            };
            parameters.AddOptionalParameter("start_time", startTime);
            parameters.AddOptionalParameter("end_time", endTime);
            parameters.AddOptionalParameter("page_index", pageIndex);
            parameters.AddOptionalParameter("page_size", pageSize);

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedCrossUserSettlementRecords>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossUserSettlementRecordsEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<HuobiUsdtMarginedIsolatedAvailableLevelRate>>> GetLinearSwapAvailableLevelRateAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);

            return await _baseClient.SendHuobiRequest<IEnumerable<HuobiUsdtMarginedIsolatedAvailableLevelRate>>(
                uri: _baseClient.GetUrl(GetLinearSwapAvailableLevelRateEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedCrossUserAvailableLevelRate>>> GetLinearSwapCrossAvailableLevelRateAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("business_type", businessType);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedCrossUserAvailableLevelRate>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossAvailableLevelRateEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedSwapOrderLimit>> GetLinearSwapOrderLimitAsync(string orderPriceType, string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"order_price_type",orderPriceType }
            };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("business_type", businessType);

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedSwapOrderLimit>(
                uri: _baseClient.GetUrl(GetLinearSwapOrderLimitEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedSwapFee>>> GetLinearSwapFeeAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("business_type", businessType);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedSwapFee>>(
                uri: _baseClient.GetUrl(GetLinearSwapFeeEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedIsolatedTransferLimit>>> GetLinearSwapTransferLimitAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedIsolatedTransferLimit>>(
                uri: _baseClient.GetUrl(GetLinearSwapTransferLimitEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedCrossTransferLimit>>> GetLinearSwapCrossTransferLimitAsync(string ? marginAccount = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("margin_account", marginAccount);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedCrossTransferLimit>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossTransferLimitEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedIsolatedPositionLimit>>> GetLinearSwapPositionLimitAsync(string? contractCode = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedIsolatedPositionLimit>>(
                uri: _baseClient.GetUrl(GetLinearSwapPositionLimitEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedCrossPositionLimit>>> GetLinearSwapCrossPositionLimitAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("business_type", businessType);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedCrossPositionLimit>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossPositionLimitEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedIsolatedLeverPositionLimit>>> GetLinearSwapLeverPositionLimitAsync(string? contractCode = null, int? leverRate = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("lever_rate", leverRate);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedIsolatedLeverPositionLimit>>(
                uri: _baseClient.GetUrl(GetLinearSwapLeverPositionLimitEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedCrossLeverPositionLimit>>> GetLinearSwapCrossLeverPositionLimitAsync(string? businessType = null, string? contractType = null, string? pair = null, string? contractCode = null, int? leverRate = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> { };
            parameters.AddOptionalParameter("business_type", businessType);
            parameters.AddOptionalParameter("contract_type", contractType);
            parameters.AddOptionalParameter("pair", pair);
            parameters.AddOptionalParameter("contract_code", contractCode);
            parameters.AddOptionalParameter("lever_rate", leverRate);

            return await _baseClient.SendHuobiRequest<IEnumerable<WWTHuobiUsdtMarginedCrossLeverPositionLimit>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossLeverPositionLimitEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedMasterSubTransfer>> LinearSwapMasterSubTransferAsync(long subUid, string asset, string fromMarginAccount, string toMarginAccount, decimal amount, string type, long? clientOrderId = null, CancellationToken ct = default)
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

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedMasterSubTransfer>(
                uri: _baseClient.GetUrl(LinearSwapMasterSubTransferEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<object>>> GetLinearSwapMasterSubTransferRecordAsync(string marginAccount, int createDate, string? transferType = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default)
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
                uri: _baseClient.GetUrl(GetLinearSwapMasterSubTransferRecordEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedTransferInner>> LinearSwapTransferInnerAsync(string asset, string fromMarginAccount, string toMarginAccount, decimal amount, long? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                {"asset",asset },
                {"from_margin_account",fromMarginAccount },
                {"to_margin_account	",toMarginAccount },
                {"amount",amount }
            };
            parameters.AddOptionalParameter("client_order_id", clientOrderId);

            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedTransferInner>(
                uri: _baseClient.GetUrl(LinearSwapTransferInnerEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Post,
                cancellationToken: ct,
                parameters: parameters,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<WWTHuobiUsdtMarginedApiTradingStatus>> GetLinearSwapApiTradingStatusAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<WWTHuobiUsdtMarginedApiTradingStatus>(
                uri: _baseClient.GetUrl(GetLinearSwapTradingStatusEndpoint, WWTApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                parameters: null,
                signed: true
                ).ConfigureAwait(false);
        }
    }
}
