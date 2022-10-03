using CryptoExchange.Net.Objects;
using Huobi.Net.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Huobi.Net.Objects.Models;

namespace Huobi.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Huobi exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// 火币交换数据端点。 交易所数据包括市场数据（代码、订单簿等）和系统状态。
    /// </summary>
    public interface IHuobiClientSpotApiExchangeData
    {
        /// <summary>
        /// Gets the latest ticker for all symbols
        /// 所有交易对的最新 Tickers
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-latest-tickers-for-all-pairs" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#tickers" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiSymbolTicks>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets the ticker, including the best bid / best ask for a symbol
        /// 聚合行情（Ticker） 此接口获取ticker信息同时提供最近24小时的交易聚合信息。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-latest-aggregated-ticker" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#ticker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the ticker for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiSymbolTickMerged>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get candlestick data for a symbol
        /// K 线数据（蜡烛图） 此接口返回历史K线数据。K线周期以新加坡时间为基准开始计算，例如日K线的起始周期为新加坡时间0时至新加坡时间次日0时。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-klines-candles" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#k" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="limit">The amount of candlesticks</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, KlineInterval period, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the order book for a symbol
        /// 市场深度数据 此接口返回指定交易对的当前市场深度数据。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-market-depth" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#0f7bd4961a" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to request for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <param name="limit">The depth of the book</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiOrderBook>> GetOrderBookAsync(string symbol, int mergeStep, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the last trade for a symbol
        /// 最近市场成交记录 此接口返回指定交易对最新的一个交易记录。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-last-trade" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#a7fb7754d7" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to request for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiSymbolTrade>> GetLastTradeAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the last x trades for a symbol
        /// 获得近期交易记录 此接口返回指定交易对近期的所有交易记录。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-most-recent-trades" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#5583b4ac64" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get trades for</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiSymbolTrade>>> GetTradeHistoryAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets 24h stats for a symbol
        /// 最近24小时行情数据 此接口返回最近24小时的行情数据汇总。
        /// 此接口返回的成交量、成交金额为24小时滚动数据（平移窗口大小24小时），有可能会出现后一个窗口内的累计成交量、累计成交额小于前一窗口的情况。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-last-24h-market-summary" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#24" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiSymbolDetails>> GetSymbolDetails24HAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets real time NAV for ETP
        /// 获取杠杆ETP实时净值 此接口返回杠杆ETP的最新净值。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-real-time-nav" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#etp" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiNav>> GetNavAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Gets the current market status
        /// 获取当前市场状态 此节点返回当前最新市场状态。
        /// 状态枚举值包括: 1 - 正常（可下单可撤单），2 - 挂起（不可下单不可撤单），3 - 仅撤单（不可下单可撤单）。
        /// 挂起原因枚举值包括: 2 - 紧急维护，3 - 计划维护。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-market-status" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#f80d403388" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiSymbolStatus>> GetSymbolStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported symbols
        /// 获取所有交易对(V1)(deprecated) 此接口返回所有火币全球站支持的交易对。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-all-supported-trading-symbol" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#v1-deprecated" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiSymbol>>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported currencies
        /// 获取所有币种(V1)(deprecated) 此接口返回所有火币全球站支持的币种。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-all-supported-currencies" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#v1-deprecated-2" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<string>>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Gets a list of supported currencies and chains
        /// APIv2 币链参考信息 此节点用于查询各币种及其所在区块链的静态参考信息（公共数据）
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#apiv2-currency-amp-chains" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#apiv2" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiAssetInfo>>> GetAssetDetailsAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Gets the server time
        /// 获取当前系统时间戳 此接口返回当前的系统时间戳，即从 UTC 1970年1月1日0时0分0秒0毫秒到现在的总毫秒数。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-current-timestamp" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#67361e2961" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);
    }
}
