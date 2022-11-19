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
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapMarketData;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapAccount;

namespace Huobi.Net.Interfaces.Clients.UsdtMargined
{
    /// <summary>
    /// 市场行情接口
    /// </summary>
    public interface IHuobiClientUsdtMarginedApiMarketData
    {
        /// <summary>
        /// 
        /// 获取行情深度数据(PublicData)
        /// <para><a href="GET /linear-swap-ex/market/depth"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#da161d5f98"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识 	永续："BTC-USDT" ... ，交割：“BTC-USDT-210625”... 或 BTC-USDT-CW（当周合约标识）、BTC-USDT-NW（次周合约标识）、BTC-USDT-CQ（当季合约标识）、BTC-USDT-NQ（次季合约标识）</param>
        /// <param name="depthType">深度类型	(150档数据) step0, step1, step2, step3, step4, step5, step14, step15, step16, step17（合并深度1-5,14-17）；step0时，不合并深度, (20档数据) step6, step7, step8, step9, step10, step11, step12, step13, step18, step19（合并深度7-13,18-19）；step6时，不合并深度</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketDepth>> GetLinearSwapDepthAsync(string contractCode, string depthType, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 获取市场最优挂单(PublicData)
        /// <para><a href="GET /linear-swap-ex/market/bbo"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#a8b3d9b85f"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识	永续："BTC-USDT" ... ，交割：“BTC-USDT-210625”... 或 BTC-USDT-CW（当周合约标识）、BTC-USDT-NW（次周合约标识）、BTC-USDT-CQ（当季合约标识）、BTC-USDT-NQ（次季合约标识）</param>
        /// <param name="businessType">业务类型，不填默认永续</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketBbo>>> GetLinearSwapBboAsync(string? contractCode = null, string? businessType = null, CancellationToken ct = default);


        /// <summary>
        /// 
        /// 获取K线数据(PublicData)
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
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketHistoryKline>>> GetLinearSwapHistoryKlineAsync(string contractCode, string period, long from, long to, int? size = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 获取标记价格的 K 线数据(PublicData)
        /// <para><a href="GET /index/market/history/linear_swap_mark_price_kline"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#k-2"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识	永续："BTC-USDT" ... ，交割：“BTC-USDT-210625”... 或 BTC-USDT-CW（当周合约标识）、BTC-USDT-NW（次周合约标识）、BTC-USDT-CQ（当季合约标识）、BTC-USDT-NQ（次季合约标识）</param>
        /// <param name="period">K线类型	1min, 5min, 15min, 30min, 60min,4hour,1day, 1week,1mon</param>
        /// <param name="size">K线获取数量</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketHistoryMarkKline>>> GetLinearSwapMarkPriceKlineAsync(string contractCode, string period, int size, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 获取聚合行情(PublicData)
        /// <para><a href="GET /linear-swap-ex/market/detail/merged"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#e194051200"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识	永续："BTC-USDT" ... ，交割：“BTC-USDT-210625”... 或 BTC-USDT-CW（当周合约标识）、BTC-USDT-NW（次周合约标识）、BTC-USDT-CQ（当季合约标识）、BTC-USDT-NQ（次季合约标识）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketDetailMerged>> GetLinearSwapMergedAsync(string contractCode, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 批量获取聚合行情(V2)(PublicData)
        /// <para><a href="GET /v2/linear-swap-ex/market/detail/batch_merged"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#v2"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识	永续："BTC-USDT" ... ，交割：“BTC-USDT-210625”... 或 BTC-USDT-CW（当周合约标识）、BTC-USDT-NW（次周合约标识）、BTC-USDT-CQ（当季合约标识）、BTC-USDT-NQ（次季合约标识）</param>
        /// <param name="businessType">业务类型，不填默认永续</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketDetailBatchMerged>>> GetLinearSwapBatchMergedV2Async(string? contractCode = null, string? businessType = null, CancellationToken ct = default);


        /// <summary>
        /// 
        /// 获取市场最近成交记录(PublicData)
        /// <para><a href="GET /linear-swap-ex/market/trade"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#09bcc28ca0"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识	永续："BTC-USDT" ... ，交割：“BTC-USDT-210625”... 或 BTC-USDT-CW（当周合约标识）、BTC-USDT-NW（次周合约标识）、BTC-USDT-CQ（当季合约标识）、BTC-USDT-NQ（次季合约标识）</param>
        /// <param name="businessType">业务类型，不填默认永续</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiUsdtMarginedMarketTrade>> GetLinearSwapMarketTradeAsync(string? contractCode = null, string? businessType = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 批量获取最近的交易记录(PublicData)
        /// <para><a href="GET /linear-swap-ex/market/history/trade"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#481f3a0ae8"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识	永续："BTC-USDT" ... ，交割：“BTC-USDT-210625”... 或 BTC-USDT-CW（当周合约标识）、BTC-USDT-NW（次周合约标识）、BTC-USDT-CQ（当季合约标识）、BTC-USDT-NQ（次季合约标识）</param>
        /// <param name="size">获取交易记录的数量，默认1	</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketHistoryTrade>>> GetLinearSwapMarketHistoryTradeAsync(string contractCode, int size, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】平台历史持仓量查询(PublicData)
        /// <para><a href="GET /linear-swap-api/v1/swap_his_open_interest></para>
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
        Task<WebCallResult<HuobiUsdtMarginedMarketHisOpenInterest>> GetLinearSwapHisOpenInterestAsync(string period, int amountType, string contractCode, string? pair = null, string? contractType = null, int? size = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】获取溢价指数K线数据(PublicData)
        /// <para><a href="GET /index/market/history/linear_swap_premium_index_kline"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#k-3"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	"BTC-USDT","ETH-USDT"...</param>
        /// <param name="period">K线类型	1min, 5min, 15min, 30min, 60min,4hour,1day, 1week,1mon</param>
        /// <param name="size">K线获取数量	[1,2000]</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketPremiumIndexKline>>> GetLinearSwapPremiumIndexKlineAsync(string contractCode, string period, int size, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 获取预测资金费率的K线数据(PublicData)
        /// <para><a href="GET /index/market/history/linear_swap_estimated_rate_kline"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#k-4"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	"BTC-USDT","ETH-USDT"...</param>
        /// <param name="period">K线类型	1min, 5min, 15min, 30min, 60min,4hour,1day, 1week,1mon</param>
        /// <param name="size">K线获取数量	[1,2000]</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketEstimatedRateKline>>> GetLinearSwapEstimatedRateKlineAsync(string contractCode, string period, int size, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 获取基差数据(PublicData)
        /// <para><a href="GET /index/market/history/linear_swap_basis"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#98b476b452"/></para>
        /// </summary>
        /// <param name="contractCode">合约代码	"BTC-USDT","ETH-USDT"...</param>
        /// <param name="period">K线类型	1min, 5min, 15min, 30min, 60min,4hour,1day, 1week,1mon</param>
        /// <param name="size">K线获取数量	[1,2000]</param>
        /// param name="basisPriceType">基差价格类型，表示在周期内计算基差使用的价格类型， 不填，默认使用开盘价	开盘价：open，收盘价：close，最高价：high，最低价：low，平均价=（最高价+最低价）/2：average</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedMarketHistoryBasis>>> GetLinearSwapBasisAsync(string contractCode, string period, int size, string? basisPriceType = null, CancellationToken ct = default);
    }
}
