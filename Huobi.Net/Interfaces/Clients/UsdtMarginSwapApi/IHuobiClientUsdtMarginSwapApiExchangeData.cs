using CryptoExchange.Net.Objects;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapMarketData;
using Huobi.Net.Objects.Models.UsdtMarginSwap;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// Huobi usdt margin swap exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IHuobiClientUsdtMarginSwapApiExchangeData
    {
        /// <summary>
        /// Get basis data
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-basis-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="limit">Limit</param>
        /// <param name="basisPriceType">Price type (open, close, high, low, average)</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiBasisData>>> GetBasisDataAsync(string contractCode, KlineInterval interval, int limit, string? basisPriceType = null, CancellationToken ct = default);
        /// <summary>
        /// Get the best current offer
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-market-bbo-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="type">Type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiSwapBestOffer>>> GetBestOfferAsync(string? contractCode = null, BusinessType? type = null, CancellationToken ct = default);
        /// <summary>
        /// Get contract info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-swap-info" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="supportMarginMode">Support margin mode</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiContractInfo>>> GetContractInfoAsync(string? contractCode = null, MarginMode? supportMarginMode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin adjust factor info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-tiered-adjustment-factor" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="asset">Asset</param>
        /// <param name="contractType">Type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiCrossSwapAdjustFactorInfo>>> GetCrossMarginAdjustFactorInfoAsync(string? contractCode = null, string? asset = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin trade status
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-trade-state" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Asset</param>
        /// <param name="contractType">Type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiCrossMarginTradeStatus>>> GetCrossMarginTradeStatusAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross margin transfer status
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-transfer-state" /></para>
        /// </summary>
        /// <param name="marginAccount">Margin account</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiCrossMarginTransferStatus>>> GetCrossMarginTransferStatusAsync(string? marginAccount = null, CancellationToken ct = default);
        /// <summary>
        /// Get cross tiered margin info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-tiered-margin" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiTieredCrossMarginInfo>>> GetCrossTieredMarginInfoAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, CancellationToken ct = default);
        /// <summary>
        /// Get estimated funding rate kliens
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-estimated-funding-rate-kline-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="limit">Limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiKline>>> GetEstimatedFundingRateKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default);
        /// <summary>
        /// Get estimated settlement price
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-the-estimated-settlement-price" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiEstimatedSettlementPrice>>> GetEstimatedSettlementPriceAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get funding rate
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-funding-rate" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiFundingRate>> GetFundingRateAsync(string contractCode, CancellationToken ct = default);
        /// <summary>
        /// Get funding rates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-a-batch-of-funding-rate" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiFundingRate>>> GetFundingRatesAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get historical funding rates
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-historical-funding-rate" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiFundingRatePage>> GetHistoricalFundingRatesAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get historical settlement records
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-historical-settlement-records-of-the-platform-interface" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiSettlementPage>> GetHistoricalSettlementRecordsAsync(string contractCode, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get insurance fund history
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-history-records-of-insurance-fund-balance" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiInsuranceInfo>> GetInsuranceFundHistoryAsync(string contractCode, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin adjust factor info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-information-on-tiered-adjustment-factor" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiSwapAdjustFactorInfo>>> GetIsolatedMarginAdjustFactorInfoAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin status
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-information-on-system-status" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiContractStatus>>> GetIsolatedStatusAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get isolated margin tier info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-information-on-tiered-margin" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiTieredMarginInfo>>> GetIsolatedMarginTieredInfoAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get klines
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-kline-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="limit">Limit</param>
        /// <param name="from">Filter by start time</param>
        /// <param name="to">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string contractCode, KlineInterval interval, int? limit = null, DateTime? from = null, DateTime? to = null, CancellationToken ct = default);
        /// <summary>
        /// Get last trades
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-the-last-trade-of-a-contract" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiLastTrade>> GetLastTradesAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get liquidation orders
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-liquidation-orders" /></para>
        /// </summary>
        /// <param name="createDate">Create date</param>
        /// <param name="tradeType">Trade type</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiLiquidationOrderPage>> GetLiquidationOrdersAsync(int createDate, LiquidationTradeType tradeType, string? contractCode = null, string? symbol = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        /// <summary>
        /// Get market data
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-market-data-overview" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiMarketData>> GetMarketDataAsync(string contractCode, CancellationToken ct = default);
        /// <summary>
        /// Get market datas
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-a-batch-of-market-data-overview" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiMarketData>>> GetMarketDatasAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get open interest
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-information-on-open-interest" /></para>
        /// </summary>
        /// <param name="period">Period</param>
        /// <param name="unit">Unit</param>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="type">Type</param>
        /// <param name="limit">Limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiOpenInterestValue>> GetOpenInterestAsync(InterestPeriod period, Unit unit, string? contractCode = null, string? symbol = null, ContractType? type = null, int? limit = null, CancellationToken ct = default);
        /// <summary>
        /// Get order book
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-market-depth" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="step">Merge step</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiOrderBook>> GetOrderBookAsync(string contractCode, string step, CancellationToken ct = default);
        /// <summary>
        /// Get premium index klines
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-premium-index-kline-data" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="interval">Interval</param>
        /// <param name="limit">Limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiKline>>> GetPremiumIndexKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default);
        /// <summary>
        /// Get recent trades
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-a-batch-of-trade-records-of-a-contract" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="limit">Limit</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiTrade>>> GetRecentTradesAsync(string contractCode, int limit, CancellationToken ct = default);
        /// <summary>
        /// Get server time
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#get-current-system-timestamp" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);
        /// <summary>
        /// Get swap index price
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-swap-index-price-information" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiSwapIndex>>> GetSwapIndexPriceAsync(string? contractCode = null, CancellationToken ct = default);
        /// <summary>
        /// Get swap open interest
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-swap-open-interest-information" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract type</param>
        /// <param name="businessType">Business tpye</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiOpenInterest>>> GetSwapOpenInterestAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get swap price limitation
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-swap-price-limitation" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="contractType">Contract tpye</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiPriceLimitation>>> GetSwapPriceLimitationAsync(string? contractCode = null, string? symbol = null, ContractType? contractType = null, BusinessType? businessType = null, CancellationToken ct = default);
        /// <summary>
        /// Get swap risk info
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-information-on-contract-insurance-fund-balance-and-estimated-clawback-rate" /></para>
        /// </summary>
        /// <param name="contractCode">Contract code</param>
        /// <param name="businessType">Business type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiSwapRiskInfo>>> GetSwapRiskInfoAsync(string? contractCode = null, BusinessType? businessType = null, CancellationToken ct = default);





        /// <summary>
        /// WWT新增接口 获取行情深度数据(PublicData)
        /// <para><a href="GET /linear-swap-ex/market/depth"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#da161d5f98"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识 	永续："BTC-USDT" ... ，交割：“BTC-USDT-210625”... 或 BTC-USDT-CW（当周合约标识）、BTC-USDT-NW（次周合约标识）、BTC-USDT-CQ（当季合约标识）、BTC-USDT-NQ（次季合约标识）</param>
        /// <param name="depthType">深度类型	(150档数据) step0, step1, step2, step3, step4, step5, step14, step15, step16, step17（合并深度1-5,14-17）；step0时，不合并深度, (20档数据) step6, step7, step8, step9, step10, step11, step12, step13, step18, step19（合并深度7-13,18-19）；step6时，不合并深度</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<WWTHuobiUsdtMarginedMarketDepth>> GetLinearSwapDepthAsync(string contractCode, string depthType, CancellationToken ct = default);

        /// <summary>
        /// WWT新增接口 获取市场最优挂单(PublicData)
        /// <para><a href="GET /linear-swap-ex/market/bbo"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#a8b3d9b85f"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识	永续："BTC-USDT" ... ，交割：“BTC-USDT-210625”... 或 BTC-USDT-CW（当周合约标识）、BTC-USDT-NW（次周合约标识）、BTC-USDT-CQ（当季合约标识）、BTC-USDT-NQ（次季合约标识）</param>
        /// <param name="businessType">业务类型，不填默认永续</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketBbo>>> GetLinearSwapBboAsync(string? contractCode = null, string? businessType = null, CancellationToken ct = default);


        /// <summary>
        /// WWT新增接口 获取K线数据(PublicData)
        /// <para><a href="GET /linear-swap-ex/market/history/kline"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#k"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识	永续："BTC-USDT" ... ，交割：“BTC-USDT-210625”... 或 BTC-USDT-CW（当周合约标识）、BTC-USDT-NW（次周合约标识）、BTC-USDT-CQ（当季合约标识）、BTC-USDT-NQ（次季合约标识）</param>
        /// <param name="period">K线类型	1min, 5min, 15min, 30min, 60min,4hour,1day,1week,1mon</param>
        /// <param name="from">开始时间戳 10位 单位S</param>
        /// <param name="to">结束时间戳 10位 单位S</param>
        /// <param name="size">获取数量，默认150	</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketHistoryKline>>> GetLinearSwapHistoryKlineAsync(string contractCode, string period, long from, long to, int? size = null, CancellationToken ct = default);

        /// <summary>
        /// WWT新增接口 获取标记价格的 K 线数据(PublicData)
        /// <para><a href="GET /index/market/history/linear_swap_mark_price_kline"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#k-2"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识	永续："BTC-USDT" ... ，交割：“BTC-USDT-210625”... 或 BTC-USDT-CW（当周合约标识）、BTC-USDT-NW（次周合约标识）、BTC-USDT-CQ（当季合约标识）、BTC-USDT-NQ（次季合约标识）</param>
        /// <param name="period">K线类型	1min, 5min, 15min, 30min, 60min,4hour,1day, 1week,1mon</param>
        /// <param name="size">K线获取数量</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketHistoryMarkKline>>> GetLinearSwapMarkPriceKlineAsync(string contractCode, string period, int size, CancellationToken ct = default);

        /// <summary>
        /// WWT新增接口 获取聚合行情(PublicData)
        /// <para><a href="GET /linear-swap-ex/market/detail/merged"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#e194051200"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识	永续："BTC-USDT" ... ，交割：“BTC-USDT-210625”... 或 BTC-USDT-CW（当周合约标识）、BTC-USDT-NW（次周合约标识）、BTC-USDT-CQ（当季合约标识）、BTC-USDT-NQ（次季合约标识）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<WWTHuobiUsdtMarginedMarketDetailMerged>> GetLinearSwapMergedAsync(string contractCode, CancellationToken ct = default);

        /// <summary>
        /// WWT新增接口 批量获取聚合行情(V2)(PublicData)
        /// <para><a href="GET /v2/linear-swap-ex/market/detail/batch_merged"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#v2"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识	永续："BTC-USDT" ... ，交割：“BTC-USDT-210625”... 或 BTC-USDT-CW（当周合约标识）、BTC-USDT-NW（次周合约标识）、BTC-USDT-CQ（当季合约标识）、BTC-USDT-NQ（次季合约标识）</param>
        /// <param name="businessType">业务类型，不填默认永续</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketDetailBatchMerged>>> GetLinearSwapBatchMergedV2Async(string? contractCode = null, string? businessType = null, CancellationToken ct = default);


        /// <summary>
        /// WWT新增接口 获取市场最近成交记录(PublicData)
        /// <para><a href="GET /linear-swap-ex/market/trade"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#09bcc28ca0"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识	永续："BTC-USDT" ... ，交割：“BTC-USDT-210625”... 或 BTC-USDT-CW（当周合约标识）、BTC-USDT-NW（次周合约标识）、BTC-USDT-CQ（当季合约标识）、BTC-USDT-NQ（次季合约标识）</param>
        /// <param name="businessType">业务类型，不填默认永续</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<WWTHuobiUsdtMarginedMarketTrade>> GetLinearSwapMarketTradeAsync(string? contractCode = null, string? businessType = null, CancellationToken ct = default);

        /// <summary>
        /// WWT新增接口 批量获取最近的交易记录(PublicData)
        /// <para><a href="GET /linear-swap-ex/market/history/trade"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#481f3a0ae8"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识	永续："BTC-USDT" ... ，交割：“BTC-USDT-210625”... 或 BTC-USDT-CW（当周合约标识）、BTC-USDT-NW（次周合约标识）、BTC-USDT-CQ（当季合约标识）、BTC-USDT-NQ（次季合约标识）</param>
        /// <param name="size">获取交易记录的数量，默认1	</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketHistoryTrade>>> GetLinearSwapMarketHistoryTradeAsync(string contractCode, int size, CancellationToken ct = default);

        /// <summary>
        /// WWT新增接口 【通用】平台历史持仓量查询(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_his_open_interest"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#903bf620df"/></para>
        /// </summary>
        /// <param name="pair">交易对	BTC-USDT</param>
        /// <param name="amountType">计价单位	1:张，2:币</param>
        /// <param name="contractCode">合约代码	永续："BTC-USDT"... ，交割："BTC-USDT-210625"...</param>
        /// <param name="contractType">合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）</param>
        /// <param name="period">时间周期类型	1小时:"60min"，4小时:"4hour"，12小时:"12hour"，1天:"1day"</param>
        /// <param name="size">获取数量,默认为：48</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<WWTHuobiUsdtMarginedMarketHisOpenInterest>> GetLinearSwapHisOpenInterestAsync(string period, int amountType, string contractCode, string? pair = null, string? contractType = null, int? size = null, CancellationToken ct = default);

        /// <summary>
        /// WWT新增接口 【通用】获取溢价指数K线数据(PublicData)
        /// <para><a href="GET /index/market/history/linear_swap_premium_index_kline"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#k-3"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	"BTC-USDT","ETH-USDT"...</param>
        /// <param name="period">K线类型	1min, 5min, 15min, 30min, 60min,4hour,1day, 1week,1mon</param>
        /// <param name="size">K线获取数量	[1,2000]</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketPremiumIndexKline>>> GetLinearSwapPremiumIndexKlineAsync(string contractCode, string period, int size, CancellationToken ct = default);

        /// <summary>
        /// WWT新增接口 获取预测资金费率的K线数据(PublicData)
        /// <para><a href="GET /index/market/history/linear_swap_estimated_rate_kline"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#k-4"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	"BTC-USDT","ETH-USDT"...</param>
        /// <param name="period">K线类型	1min, 5min, 15min, 30min, 60min,4hour,1day, 1week,1mon</param>
        /// <param name="size">K线获取数量	[1,2000]</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketEstimatedRateKline>>> GetLinearSwapEstimatedRateKlineAsync(string contractCode, string period, int size, CancellationToken ct = default);

        /// <summary>
        /// WWT新增接口 获取基差数据(PublicData)
        /// <para><a href="GET /index/market/history/linear_swap_basis"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#98b476b452"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	"BTC-USDT","ETH-USDT"...</param>
        /// <param name="period">K线类型	1min, 5min, 15min, 30min, 60min,4hour,1day, 1week,1mon</param>
        /// <param name="size">K线获取数量	[1,2000]</param>
        /// <param name="basisPriceType">基差价格类型，表示在周期内计算基差使用的价格类型， 不填，默认使用开盘价	开盘价：open，收盘价：close，最高价：high，最低价：low，平均价=（最高价+最低价）/2：average </param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<WWTHuobiUsdtMarginedMarketHistoryBasis>>> GetLinearSwapBasisAsync(string contractCode, string period, int size, string? basisPriceType = null, CancellationToken ct = default);
    }
}