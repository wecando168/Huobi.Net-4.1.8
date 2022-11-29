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
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount.CommonBaseModels;

namespace Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// 账户接口
    /// </summary>
    public interface IHuobiClientUsdtMarginSwapAccount
    {
        /// <summary>
        /// 
        /// 【通用】获取账户合约总资产估值(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_balance_valuation"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#a3e2f535c0"/></para>
        /// </summary>
        /// <param name="valuationAsset">string	资产估值币种，即按该币种为单位进行估值，不填默认"BTC"	"BTC", "USD", "USDT", "CNY", "EUR", "GBP", "VND", "HKD", "TWD", "MYR", "SGD", "KRW", "RUB", "TRY"</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedBalanceValuation>>> GetLinearSwapBalanceValuationAsync(string? valuationAsset = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】获取用户的合约账户信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_account_info"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#0b91d90b81"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续："BTC-USDT"... ，交割："BTC-USDT-210625"...</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedIsolatedAccountInfo>>> GetLinearSwapAccountInfoAsync(string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】获取用户的合约账户信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_account_info"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#3021bf5b3b"/></para>
        /// </summary>
        /// <param name="marginAccount">保证金账户，不填则返回所有全仓保证金账户	"USDT"，目前只有一个全仓账户（USDT）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossAccountInfo>>> GetLinearSwapCrossAccountInfoAsync(string? marginAccount = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】获取用户的合约持仓信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_position_info"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#316a79c93e"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedIsolatedPositionInfo>>> GetLinearSwapPositionInfoAsync(string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】获取用户的合约持仓信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_position_info"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#5a5aead4b7"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码</param>
        /// <param name="pair">交易代码 如：BTC-USDT</param>
        /// <param name="umContractType">合约类型</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossPositionInfo>>> GetLinearSwapCrossPositionInfoAsync(string? contractCode = null, string? pair = null, UmContractType? umContractType = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】查询用户账户和持仓信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_account_position_info"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#ae79fda400"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	"BTC-USDT"...</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedIsolatedAccountPositionInfo>>> GetLinearSwapAccountPositionInfoAsync(string contractCode, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】查询用户账户和持仓信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_account_position_info"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#bb67178d99"/></para>
        /// </summary>
        /// <param name="marginAccount">保证金账户	"USDT"，目前只有一个全仓账户（USDT）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedCrossAccountPositionInfo>> GetLinearSwapCrossAccountPositionInfoAsync(string marginAccount, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】批量设置子账户交易权限(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_sub_auth"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#2a2fab2cc6"/></para>
        /// </summary>
        /// <param name="subUidList">子账户uid列表，一次最多设置10个账户</param>
        /// <param name="subAuth">子账户交易权限，1 开启，0关闭</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedSubAuth>> SetLinearSwapSubAuthAsync(IEnumerable<string> subUidList, int subAuth, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】查询母账户下所有子账户资产信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_sub_account_list"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#fa06efc766"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码,如果缺省，默认返回所有合约	"BTC-USDT"...</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedIsolatedSubAccountList>>> GetLinearSwapSubAccountListAsync(string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】查询母账户下所有子账户资产信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_sub_account_list"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#10934ce6d8"/></para>
        /// </summary>
        /// <param name="marginAccount">保证金账户，不填则返回所有全仓保证金账户	"USDT"，目前只有一个全仓账户（USDT）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossSubAccountList>>> GetLinearSwapCrossSubAccountListAsync(string? marginAccount = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】批量获取子账户资产信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_sub_account_info_list"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#01435f37f4"/></para>
        /// </summary>
        /// <param name="contratcCode">合约代码	"BTC-USDT"... ,如果缺省，默认返回所有合约</param>
        /// <param name="pageIndex">第几页,不填默认第一页	</param>
        /// <param name="pageSize">不填默认20，不得多于50 </param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedIsolatedSubAccountInfoList>> GetLinearSwapSubAccountInfoListAsync(string? contratcCode = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】批量获取子账户资产信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_sub_account_info_list"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#5a666d7967"/></para>
        /// </summary>
        /// <param name="marginAccount">保证金账户，不填则返回所有全仓保证金账户	"USDT"，目前只有一个全仓账户（USDT）</param>
        /// <param name="pageIndex">第几页,不填默认第一页	</param>
        /// <param name="pageSize">不填默认20，不得多于50 </param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedCrossSubAccountInfoList>> GetLinearSwapCrossSubAccountInfoListAsync(string? marginAccount = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】查询母账户下的单个子账户资产信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_sub_account_info"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#9f3cbaea20"/></para>
        /// </summary>
        /// <param name="subUid">子账户的UID</param>
        /// <param name="contractCode">合约代码 ,如果缺省，默认返回所有合约	"BTC-USDT"...</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedIsolatedAccountPositionInfo>>> GetLinearSwapSubAccountInfoAsync(long subUid, string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】查询母账户下的单个子账户资产信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_sub_account_info"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#8bc285762d"/></para>
        /// </summary>
        /// <param name="subUid">子账户的UID</param>
        /// <param name="marginAccount">保证金账户，不填则返回所有全仓保证金账户	"USDT"，目前只有一个全仓账户（USDT）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossAccountPositionInfo>>> GetLinearSwapCrossSubAccountInfoAsync(long subUid, string? marginAccount = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】查询母账户下的单个子账户持仓信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_sub_position_info"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#e5f06f4bd3"/></para>
        /// </summary>
        /// <param name="subUid">子账户的UID</param>
        /// <param name="contractCode">合约代码，如果缺省，默认返回所有合约	"BTC-USDT"...</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiPosition>>> GetLinearSwapSubPositionInfoAsync(long subUid, string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】查询母账户下的单个子账户持仓信息(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_sub_position_info"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#05de57f298"/></para>
        /// </summary>
        /// <param name="subUid">子账户的UID</param>
        /// <param name="contractCode">合约代码	永续："BTC-USDT"...，交割：“BTC-USDT-210625”</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossSubPositionInfo>>> GetLinearSwapCrossSubPositionInfoAsync(long subUid, string? contractCode = null, string? pair = null, string? contractType = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】查询用户财务记录(新)(PrivateData)
        /// <para><a href="POST /linear-swap-api/v3/swap_financial_record"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#61d604104e"/></para>
        /// </summary>
        /// <param name="marginAccount">保证金账户 "BTC-USDT","USDT"(查询全仓时使用)...</param>
        /// <param name="contractCode">合约代码	支持大小写,"BTC-USDT" ...</param>
        /// <param name="type">不填查询全部类型,【查询多类型中间用，隔开】		3:平多; 4:平空; 5:开仓手续费-吃单; 6:开仓手续费-挂单; 7:平仓手续费-吃单; 8:平仓手续费-挂单; 9:交割平多; 10:交割平空; 11:交割手续费; 12:强制平多; 13:强制平空; 14:从币币转入; 15:转出至币币; 16:结算未实现盈亏-多仓; 17:结算未实现盈亏-空仓; 19:穿仓分摊; 26:系统; 28:活动奖励; 29:返利; 30:资金费-收入; 31:资金费-支出; 34:转出到子账号合约账户; 35:从子账号合约账户转入; 36:转出到母账号合约账户; 37:从母账号合约账户转入;38:从其他保证金账户转入 ;39:转出到其他保证金账户 ;</param>
        /// <param name="startTime">查询开始时间, 以数据按创建时间进行查询		取值范围 [((end-time) – 48h), (end-time)] ，查询窗口最大为48小时，窗口平移范围为最近90天。	</param>
        /// <param name="endTime">查询结束时间, 以数据按创建时间进行查询 默认为now	取值范围 [(present-90d), present] ，查询窗口最大为48小时，窗口平移范围为最近90天。</param>
        /// <param name="direct">查询方向, 方向为next时，数据按照时间正序排列返回，方向为prev时，数据按照时间倒序返回	默认为next	prev表示向前查询，next表示向后查询。</param>
        /// <param name="fromId">如果是向前(prev)查询，则赋值为上一次查询结果中得到的最小query_id ；如果是向后(next)查询，则赋值为上一次查询结果中得到的最大query_id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedFinancialRecord>>> GetLinearSwapFinancialRecordAsync(string marginAccount, string ? contractCode = null, string ? type = null, long ? startTime = null, long ? endTime = null, string ? direct = null, long ? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】组合查询用户财务记录 (新)(PrivateData)
        /// <para><a href="POST /linear-swap-api/v3/swap_financial_record"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#69cab93ec7"/></para>
        /// </summary>
        /// <param name="marginAccount">保证金账户 "BTC-USDT","USDT"(查询全仓时使用)...</param>
        /// <param name="contractCode">合约代码	支持大小写,"BTC-USDT" ...</param>
        /// <param name="type">不填查询全部类型,【查询多类型中间用，隔开】		3:平多; 4:平空; 5:开仓手续费-吃单; 6:开仓手续费-挂单; 7:平仓手续费-吃单; 8:平仓手续费-挂单; 9:交割平多; 10:交割平空; 11:交割手续费; 12:强制平多; 13:强制平空; 14:从币币转入; 15:转出至币币; 16:结算未实现盈亏-多仓; 17:结算未实现盈亏-空仓; 19:穿仓分摊; 26:系统; 28:活动奖励; 29:返利; 30:资金费-收入; 31:资金费-支出; 34:转出到子账号合约账户; 35:从子账号合约账户转入; 36:转出到母账号合约账户; 37:从母账号合约账户转入;38:从其他保证金账户转入 ;39:转出到其他保证金账户 ;</param>
        /// <param name="startTime">查询开始时间, 以数据按创建时间进行查询		取值范围 [((end-time) – 48h), (end-time)] ，查询窗口最大为48小时，窗口平移范围为最近90天。	</param>
        /// <param name="endTime">查询结束时间, 以数据按创建时间进行查询 默认为now	取值范围 [(present-90d), present] ，查询窗口最大为48小时，窗口平移范围为最近90天。</param>
        /// <param name="direct">查询方向, 方向为next时，数据按照时间正序排列返回，方向为prev时，数据按照时间倒序返回	默认为next	prev表示向前查询，next表示向后查询。</param>
        /// <param name="fromId">如果是向前(prev)查询，则赋值为上一次查询结果中得到的最小query_id ；如果是向后(next)查询，则赋值为上一次查询结果中得到的最大query_id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedFinancialRecord>>> GetLinearSwapFinancialRecordExactAsync(string marginAccount, string? contractCode = null, string? type = null, long? startTime = null, long? endTime = null, string? direct = null, long? fromId = null, CancellationToken ct = default);


        /// <summary>
        /// 
        /// 【逐仓】查询用户结算记录(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_user_settlement_records"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#21bbdf0621"/></para>
        /// </summary>
        /// <param name="contractCode">合约code	"BTC-USDT"...</param>
        /// <param name="startTime">起始时间（时间戳，单位毫秒）	取值范围：[(当前时间 - 90天), 当前时间] ，默认取当前时间- 90天</param>
        /// <param name="endTime">结束时间（时间戳，单位毫秒）	取值范围：(start_time, 当前时间]，默认取当前时间</param>
        /// <param name="pageIndex">页码 不填默认第1页</param>
        /// <param name="pageSize">页大小 不填默认20，不得多于50（超过则按照50进行查询）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedIsolatedUserSettlementRecords>> GetLinearSwapUserSettlementRecordsAsync(string contractCode, long? startTime = null, long? endTime = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】查询用户结算记录(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_user_settlement_records"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#7b3a97fd70"/></para>
        /// </summary>
        /// <param name="marginAccount">保证金账户	"USDT"，目前只有一个全仓账户（USDT）</param>
        /// <param name="startTime">起始时间（时间戳，单位毫秒）	取值范围：[(当前时间 - 90天), 当前时间] ，默认取当前时间- 90天</param>
        /// <param name="endTime">结束时间（时间戳，单位毫秒）	取值范围：(start_time, 当前时间]，默认取当前时间</param>
        /// <param name="pageIndex">页码 不填默认第1页</param>
        /// <param name="pageSize">页大小 不填默认20，不得多于50（超过则按照50进行查询）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedCrossUserSettlementRecords>> GetLinearSwapCrossUserSettlementRecordsAsync(string marginAccount, long? startTime = null, long? endTime = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】查询用户可用杠杆倍数(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_available_level_rate"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#633f54cd27"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码，不填默认返回所有合约的实际可用杠杆倍数	比如： “BTC-USDT”...</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedIsolatedAvailableLevelRate>>> GetLinearSwapAvailableLevelRateAsync(string ? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】获取用户当前合约杠杆倍数(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_available_level_rate"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#030c4884b4"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... ，交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="businessType">业务类型，不填默认永续	futures：交割、swap：永续、all：全部</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossUserAvailableLevelRate>>> GetLinearSwapCrossAvailableLevelRateAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】获取用户的合约下单量限制(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_order_limit"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#b747fcf3b6"/></para>
        /// </summary>
        /// <param name="orderPriceType">订单报价类型	"limit":限价，"opponent":对手价，"lightning":闪电平仓，"optimal_5":最优5档，"optimal_10":最优10档，"optimal_20":最优20档，"fok":FOK订单，"ioc":IOC订单,opponent_ioc"： 对手价-IOC下单，"lightning_ioc"：闪电平仓-IOC下单，"optimal_5_ioc"：最优5档-IOC下单，"optimal_10_ioc"：最优10档-IOC下单，"optimal_20_ioc"：最优20档-IOC下单,"opponent_fok"： 对手价-FOK下单，"lightning_fok"：闪电平仓-FOK下单，"optimal_5_fok"：最优5档-FOK下单，"optimal_10_fok"：最优10档-FOK下单，"optimal_20_fok"：最优20档-FOK下单</param>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... ，交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="businessType">业务类型，不填默认永续	futures：交割、swap：永续、all：全部</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedSwapOrderLimit>> GetLinearSwapOrderLimitAsync(string orderPriceType, string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】获取用户的合约手续费费率(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_fee"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#36a69845bc"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... ，交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="businessType">业务类型，不填默认永续	futures：交割、swap：永续、all：全部</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedSwapFee>>> GetLinearSwapFeeAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】获取用户的合约划转限制(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_transfer_limit"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#4ccfd1cd84"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... ，交割：“BTC-USDT-210625”...</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedIsolatedTransferLimit>>> GetLinearSwapTransferLimitAsync(string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】获取用户的合约划转限制(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_transfer_limit"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#df7854dd6c"/></para>
        /// </summary>
        /// <param name="marginAccount">保证金账户，不填则返回所有全仓保证金账户	"USDT"，目前只有一个全仓账户（USDT）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossTransferLimit>>> GetLinearSwapCrossTransferLimitAsync(string? marginAccount = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】获取用户的合约持仓量限制(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_position_limit"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#c466642dfc"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	"BTC-USDT"... ,如果缺省，默认返回所有合约</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedIsolatedPositionLimit>>> GetLinearSwapPositionLimitAsync(string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】获取用户的合约持仓量限制(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_position_limit"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#adf4110e53"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... ，交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="businessType">业务类型，不填默认永续	futures：交割、swap：永续、all：全部</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossPositionLimit>>> GetLinearSwapCrossPositionLimitAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】查询用户所有杠杆持仓量限制(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_lever_position_limit"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#44c803d959"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码，不填返回全部	如"BTC-USDT"、"ETH-USDT"</param>
        /// <param name="leverRate">杠杆倍数，不填返回所有杠杆倍数	</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedIsolatedLeverPositionLimit>>> GetLinearSwapLeverPositionLimitAsync(string? contractCode = null, int? leverRate = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】查询用户所有杠杆持仓量限制(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_cross_lever_position_limit"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#e6138b004d"/></para>
        /// </summary>
        /// <param name="businessType">业务类型，不填默认永续	futures：交割、swap：永续、all：全部</param>
        /// <param name="contractType">合约类型，不填返回全部	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="pair">交易对，不填返回全部	如：“BTC-USDT”</param>
        /// <param name="contractCode">合约代码，不填返回全部	如"BTC-USDT"、"ETH-USDT"</param>
        /// <param name="leverRate">杠杆倍数，不填返回所有杠杆倍数	</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedCrossLeverPositionLimit>>> GetLinearSwapCrossLeverPositionLimitAsync(string? businessType = null, string? contractType = null, string? pair = null, string? contractCode = null, int? leverRate = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 母子账户划转(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_master_sub_transfer"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#a542a29003"/></para>
        /// </summary>
        /// <param name="subUid">子账号uid</param>
        /// <param name="asset">币种 "USDT"...</param>
        /// <param name="fromMarginAccount">转出的保证金账户 "BTC-USDT"，"USDT"...</param>
        /// <param name="toMarginAccount">转入的保证金账户 "BTC-USDT"，"USDT"...</param>
        /// <param name="amount">划转金额</param>
        /// <param name="type">划转类型	master_to_sub：母账户划转到子账户， sub_to_master：子账户划转到母账户</param>
        /// <param name="clientOrderId">客户自己填写和维护的订单号，必须为数字	[1-9223372036854775807]</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMasterSubTransfer>> LinearSwapMasterSubTransferAsync(long subUid, string asset, string fromMarginAccount, string toMarginAccount, decimal amount, string type, long? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 获取母账户下的所有母子账户划转记录(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_master_sub_transfer_record"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#3b58d4cb9e"/></para>
        /// </summary>
        /// <param name="marginAccount">保证金账户	"BTC-USDT"，"USDT"...</param>
        /// <param name="createDate">日期	可随意输入正整数，如果参数超过90则默认查询90天的数据</param>
        /// <param name="transferType">划转类型，不填查询全部类型,【查询多类型中间用，隔开】	34:转出到子账号合约账户; 35:从子账号合约账户转入;</param>
        /// <param name="pageIndex">页码，不填默认第1页	1</param>
        /// <param name="pageSize">不填默认20，不得多于50	20</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapMasterSubTransferRecordAsync(string marginAccount, int createDate, string? transferType = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 同账号不同保证金账户的划转(PrivateData)
        /// <para><a href="POST /linear-swap-api/v1/swap_transfer_inner"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#3a579f6545"/></para>
        /// </summary>
        /// <param name="asset">币种 "USDT"...</param>
        /// <param name="fromMarginAccount">转出的保证金账户 "BTC-USDT"，"USDT"...</param>
        /// <param name="toMarginAccount">转入的保证金账户 "ETH-USDT"，"USDT"...</param>
        /// <param name="amount">划转数额（单位为合约的计价币种）</param>
        /// <param name="clientOrderId">客户自己填写和维护的订单号,必须为数字	[1-9223372036854775807]</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedTransferInner>> LinearSwapTransferInnerAsync(string asset, string fromMarginAccount, string toMarginAccount, decimal amount, long? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 获取用户API指标禁用信息(PrivateData)
        /// <para><a href="GET /linear-swap-api/v1/swap_api_trading_status"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#api-2"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedApiTradingStatus>> GetLinearSwapApiTradingStatusAsync(CancellationToken ct = default);
    }
}
