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
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapReferenceData;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapTrade.Request;

namespace Huobi.Net.Interfaces.Clients.UsdtMargined
{
    /// <summary>
    /// 基础信息接口
    /// </summary>
    public interface IHuobiClientUsdtMarginedApiReferenceData
    {
        /// <summary>
        /// 
        /// 【通用】账户类型查询(PublicData)
        /// <para><a href="GET /linear-swap-api/v3/swap_unified_account_type"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#ff2f540895"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapUnifiedAccountType>> GetLinearSwapUnifiedAccountTypeAsync(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】账户类型更改接口(PublicData)
        /// <para><a href="POST /linear-swap-api/v3/swap_switch_account_type"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#37a2c416a0"/></para>
        /// </summary>
        /// <param name="accountType">账户类型	1:非统一账户（全仓逐仓账户）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapSwitchAccountType>> LinearSwapSwitchAccountTypeAsync(int accountType, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】获取合约的资金费率(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_funding_rate"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#06c36d965a"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	"BTC-USDT" ...</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapFundingRate>> GetLinearSwapFundingRateAsync(string contractCode, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】批量获取合约资金费率(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_batch_funding_rate"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#6cd6a9bbf9"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	"BTC-USDT" ...</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapFundingRate>>> GetLinearSwapBatchFundingRateAsync(string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】获取合约的历史资金费率(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_historical_funding_rate"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#dd88db946e"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	"BTC-USDT" ...</param>
        /// <param name="pageIndex">页码，不填默认第1页</param>
        /// <param name="pageSize">不填默认20，不得多于50	</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketSwapHistoricalFundingRate>> GetLinearSwapHistoricalFundingRateAsync(string contractCode, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】获取强平订单(PublicData)
        /// <para><a href="GET /linear-swap-api/v3/swap_liquidation_orders"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#3f72fdc36f"/></para>
        /// </summary>
        /// <param name="tradeType">交易类型 0:全部,5: 卖出强平,6: 买入强平</param>
        /// <param name="contractCode">合约代码	永续："BTC-USDT"... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对 BTC-USDT</param>
        /// <param name="startTime">查询开始时间, 以数据按创建时间进行查询	默认值：(now) – 48h	取值范围 [((end-time) – 48h), (end-time)] ，查询窗口最大为48小时，窗口平移范围为最近90天,已完全撤销的历史订单的查询窗口平移范围只有最近2小时</param>
        /// <param name="endTime">查询结束时间, 以数据按创建时间进行查询 默认值：now	取值范围 [(present-90d), present] ，查询窗口最大为48小时，窗口平移范围为最近90天,已完全撤销的历史订单的查询窗口平移范围只有最近2小时</param>
        /// <param name="direct">查询方向, 方向为next时，数据按照时间正序排列返回，方向为prev时，数据按照时间倒序返回	默认值：next	prev表示向前查询，next表示向后查询。</param>
        /// <param name="fromId">如果是向前(prev)查询，则赋值为上一次查询结果中得到的最小query_id ；如果是向后(next)查询，则赋值为上一次查询结果中得到的最大query_id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapLiquidationOrders>>> GetLinearSwapLiquidationOrdersAsync(int tradeType, string contractCode, string? pair = null, long? startTime = null, long? endTime = null, string? direct = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】平台历史结算记录(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_settlement_records"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#3d9c9bca4d"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续："BTC-USDT"... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="startTime">起始时间（时间戳，单位毫秒）	取值范围：[(当前时间 - 90天), 当前时间] ，默认取当前时间- 90天</param>
        /// <param name="endTime">结束时间（时间戳，单位毫秒）	取值范围：(start_time, 当前时间)，默认取当前时间</param>
        /// <param name="pageIndex">页码，不填默认第1页</param>
        /// <param name="pageSize">页长，不填默认20，不得多于50</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedSettlementRecords>> GetLinearSwapSettlementRecordsAsync(string contractCode, long? startTime = null, long? endTime = null, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】精英账户多空持仓对比-账户数(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_elite_account_ratio"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#706c6d757e"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续："BTC-USDT"... , 交割："BTC-USDT-FUTURES" ...</param>
        /// <param name="period">时间周期类型	5min, 15min, 30min, 60min,4hour,1day</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedEliteAccountRatio>> GetLinearSwapEliteAccountRatioAsync(string contractCode, string period, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】精英账户多空持仓对比-持仓量(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_elite_position_ratio"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#b2cd3c9f36"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续："BTC-USDT"... , 交割："BTC-USDT-FUTURES" ...</param>
        /// <param name="period">时间周期类型	5min, 15min, 30min, 60min,4hour,1day</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedElitePositionRatio>> GetLinearSwapElitePositionRatioAsync(string contractCode, string period, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】查询系统状态(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_api_state"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#6b6231064e"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	"BTC-USDT" ...</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapApiState>>> GetLinearSwapApiStateAsync(string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】获取平台阶梯保证金
        /// <para><a href="GET /linear-swap-api/v1/swap_cross_ladder_margin"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#183c40fdf0"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... ，交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="businessType">业务类型，不填默认永续	futures：交割、swap：永续、all：全部</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketCrossLadderMargin>>> GetLinearSwapCrossLadderMarginAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】获取平台阶梯保证金
        /// <para><a href="GET /linear-swap-api/v1/swap_ladder_margin"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#189c47872d"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... ，交割：“BTC-USDT-210625”...</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketLadderMargin>>> GetLinearSwapLadderMarginAsync(string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】获取预估结算价(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_estimated_settlement_price"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#b672a77e3b"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... ，交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="businessType">业务类型，不填默认永续	futures：交割、swap：永续、all：全部</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedEliteSettlementPrice>>> GetLinearSwapEstimatedSettlementPriceAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【逐仓】查询平台阶梯调整系数(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_adjustfactor"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#b76a62b9d4"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... ，交割：“BTC-USDT-210625”...</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapAdjustfactor>>> GetLinearSwapAdjustfactorAsync(string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【全仓】查询平台阶梯调整系数(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_cross_adjustfactor"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#563e9d6fe6"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... ，交割：“BTC-USDT-210625”...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="businessType">业务类型，不填默认永续	futures：交割、swap：永续、all：全部</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapCrossAdjustfactor>>> GetLinearSwapCrossAdjustfactorAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】获取风险准备金历史数据(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_insurance_fund"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#cb8215f17d"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续："BTC-USDT"... , 交割：“BTC-USDT-210625”...</param>
        /// <param name="pageIndex">页码，不填默认第1页</param>
        /// <param name="pageSize">不填默认100，不得多于100	[1-100]</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedInsuranceFund>> GetLinearSwapInsuranceFundAsync(string contractCode, int? pageIndex = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】查询合约风险准备金和预估分摊比例(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_risk_info"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#92ff4948a0"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续：“BTC-USDT”... ，交割：“BTC-USDT-210625”...</param>
        /// <param name="businessType">业务类型，不填默认永续	futures：交割、swap：永续、all：全部</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedRiskInfo>>> GetLinearSwapRiskInfoAsync(string? contractCode = null, string? businessType = null, CancellationToken ct = default);

        /// <summary>
        /// 
        ///【通用】 获取合约最高限价和最低限价(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_price_limit"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#40f2009a2a"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续："BTC-USDT"... ，交割："BTC-USDT-210625"...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="businessType">业务类型，不填默认永续	futures：交割、swap：永续、all：全部</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapPriceLimit>>> GetLinearSwapPriceLimitAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】获取当前合约总持仓量(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_open_interest"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#3218e7531a"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续："BTC-USDT"... ，交割："BTC-USDT-210625"...</param>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="businessType">业务类型，不填默认永续	futures：交割、swap：永续、all：全部</param>
        /// <param name="ct">Cancellation token</param></param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapOpenInterest>>> GetLinearSwapOpenInterestAsync(string? contractCode = null, string? pair = null, string? contractType = null, string? businessType = null, CancellationToken ct = default);

        /// <summary>T
        /// Query Swap InfoT
        /// 【通用】获取合约信息(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_contract_info"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-swap-info"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#57601c57f3"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	永续："BTC-USDT"... ，交割："BTC-USDT-210625"...</param>
        /// <param name="supportMarginMode">cross：仅支持全仓模式；isolated：仅支持逐仓模式；all：全逐仓都支持</param>
        /// <param name="pari">交易代码</param>
        /// <param name="contractType">swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="businessType">futures：交割、swap：永续、all：全部</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapContractInfo>>> GetLinearSwapContractInfoAsync(string? contractCode = null,string? supportMarginMode = null, string? pari = null, string? contractType = null, string? businessType = "all", CancellationToken ct = default);

        /// <summary>
        /// Query Swap Indoex Price Information
        /// 【通用】获取合约指数信息(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_index"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-swap-index-price-information"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#57601c57f3"/></para>
        /// </summary>
        /// <param name="contractCode">指数代码	"BTC-USDT","ETH-USDT"...</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketSwapIndex>>> GetLinearSwapIndexAsync(string? contractCode = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】获取当前系统时间戳(PublicData)
        /// <para><a href="GET https://api.hbdm.com/api/v1/timestamp"/></para>
        /// <para><a href=""/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#67361e2961"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> GetLinearSwapServerTimestampAsync(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】获取当前系统时间(PublicData)
        /// <para><a href="GET https://api.hbdm.com/api/v1/timestamp"/></para>
        /// <para><a href=""/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#67361e2961"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetLinearSwapServerDateTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】查询系统是否可用(PublicData)
        /// <para><a href="GET https://api.hbdm.com/heartbeat/"/></para>
        /// <para><a href=""/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#bef5ec9210"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketHeartbeat>> GetLinearSwapHeartbeatAsync(CancellationToken ct = default); 

        /// <summary>
        /// 
        /// 【通用】获取当前系统状态(PublicData)
        /// <para><a href="GET https://status-linear-swap.huobigroup.com/api/v2/summary.json"/></para>
        /// <para><a href=""/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#cd63bde415"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketStatus>> GetLinearSwapSummaryAsync(CancellationToken ct = default);
    }
}
