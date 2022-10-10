using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Socket;

namespace Huobi.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Spot streams
    /// 现货数据流
    /// </summary>
    public interface IHuobiSocketClientSpotStreams : IDisposable
    {
        /// <summary>
        /// This topic sends a new candlestick whenever it is available.
        /// K线数据 主题订阅 一旦K线数据产生，Websocket服务器将通过此订阅主题接口推送至客户端：
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-candlestick" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#k-2" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <returns></returns>
        Task<CallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, KlineInterval period);

        /// <summary>
        /// Subscribes to candlestick updates for a symbol
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default);

        /// <summary>
        /// Gets the current order book for a symbol
        /// 市场深度行情数据 此主题发送最新市场深度快照。快照频率为每秒1次
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-depth" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#8742b7d9f7" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <returns></returns>
        Task<CallResult<HuobiOrderBook>> GetOrderBookWithMergeStepAsync(string symbol, int mergeStep);

        /// <summary>
        /// Gets the current order book for a symbol
        /// 获取市场深度MBP行情数据
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#mbp" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="levels">The amount of rows. 5, 20, 150 or 400</param>
        /// <returns></returns>
        Task<CallResult<HuobiIncementalOrderBook>> GetOrderBookAsync(string symbol, int levels);

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// 订阅增量推送 市场深度MBP行情数据（增量推送）
        /// 用户可订阅此频道以接收最新深度行情Market By Price (MBP) 的增量数据推送；同时，该频道支持用户以req方式请求获取全量数据。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#mbp" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="levels">The number of price levels. 5, 10 or 20</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates100MilisecondAsync(string symbol, int levels, Action<DataEvent<HuobiOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to order book updates for a symbol
        /// 主题订阅 市场深度行情数据 此主题发送最新市场深度快照。快照频率为每秒1次。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-depth" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#8742b7d9f7" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="mergeStep">The way the results will be merged together</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates1SecondAsync(string symbol, int mergeStep, Action<DataEvent<HuobiOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to order book updates for a symbol, 
        /// 市场深度MBP行情数据（全量推送） 用户可订阅此频道以接收最新深度行情Market By Price (MBP) 的全量数据推送。推送频率为大约100毫秒一次。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#mbp" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="levels">The number of price levels. 5, 20, 150 or 400</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookChangeUpdatesAsync(string symbol, int levels, Action<DataEvent<HuobiIncementalOrderBook>> onData, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of trades for a symbol
        /// 市场最新成交逐笔明细
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#trade-detail" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#56c6c47284-2" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get trades for</param>
        /// <returns></returns>
        Task<CallResult<IEnumerable<HuobiSymbolTradeDetails>>> GetTradeHistoryAsync(string symbol);

        /// <summary>
        /// Subscribes to trade updates for a symbol
        /// 成交明细 此主题提供市场最新成交逐笔明细。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#trade-detail" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#56c6c47284-2" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolTrade>> onData, CancellationToken ct = default);

        /// <summary>
        /// Gets details for a symbol
        /// 获取指定交易代码的详情
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-details" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#7c47ef3411" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get data for</param>
        /// <returns></returns>
        Task<CallResult<HuobiSymbolDetails>> GetSymbolDetailsAsync(string symbol);

        /// <summary>
        /// Subscribes to symbol detail updates for a symbol
        /// 主题订阅 此主题提供24小时内最新市场概要快照。快照频率不超过每秒10次。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-details" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#7c47ef3411" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe to</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToSymbolDetailUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolDetails>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to updates for a symbol
        /// 主题订阅 获取指定交易代码市场聚合行情数据，每100ms推送一次。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-ticker" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#ticker-2" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe</param>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to updates for all tickers
        /// 主题订阅 获取全部交易代码市场聚合行情数据，每100ms推送一次。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-ticker" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#ticker-2" /></para>
        /// </summary>
        /// <param name="onData">The handler for updates</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<HuobiSymbolDatas>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to changes of a symbol's best ask/bid
        /// 主题订阅 买一卖一逐笔行情 当买一价、买一量、卖一价、卖一量，其中任一数据发生变化时，此主题推送逐笔更新。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#best-bid-offer" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#mbp-2" /></para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe to</param>
        /// <param name="onData">Data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string symbol,
            Action<DataEvent<HuobiBestOffer>> onData, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates of orders
        /// 订阅订单更新
        /// 订单的更新推送由任一以下事件触发：
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#subscribe-order-updates" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#f810bc2ca6" /></para>
        /// </summary>
        /// <param name="symbol">Subscribe on a specific symbol</param>
        /// <param name="onOrderSubmitted">Event handler for the order submitted event 订单创建</param>
        /// <param name="onOrderMatched">Event handler for the order matched event 订单成交</param>
        /// <param name="onOrderCancelation">Event handler for the order cancelled event 订单撤销</param>
        /// <param name="onConditionalOrderTriggerFailure">Event handler for the conditional order trigger failed event 计划委托或追踪委托触发失败事件</param>
        /// <param name="onConditionalOrderCanceled">Event handler for the condition order canceled event 计划委托或追踪委托触发前撤单事件</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
            string? symbol = null,
            Action<DataEvent<HuobiSubmittedOrderUpdate>>? onOrderSubmitted = null,
            Action<DataEvent<HuobiMatchedOrderUpdate>>? onOrderMatched = null,
            Action<DataEvent<HuobiCanceledOrderUpdate>>? onOrderCancelation = null,
            Action<DataEvent<HuobiTriggerFailureOrderUpdate>>? onConditionalOrderTriggerFailure = null,
            Action<DataEvent<HuobiOrderUpdate>>? onConditionalOrderCanceled = null,
            CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates of account balances
        /// 订阅账户变更
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#subscribe-account-change" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#f2e38456dd" /></para>
        /// </summary>
        /// <param name="onAccountUpdate">Event handler 订阅账户的余额更新</param>
        /// <param name="updateMode">The update mode. Defaults to 1, see API docs for more info 用户可选择以下任一账户变更推送的触发方式 1、仅在账户余额发生变动时推送； 2、在账户余额发生变动或可用余额发生变动时均推送，且分别推送。 3、在账户余额发生变动或可用余额发生变动时均推送，且一起推送。</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<HuobiAccountUpdate>> onAccountUpdate, int? updateMode = null, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to detailed order matched/canceled updates
        /// 订阅清算后成交及撤单更新
        /// 仅当用户订单成交或撤销时推送。其中，订单成交为逐笔推送，如一张 taker 订单同时与多张 maker 订单成交，该接口将推送逐笔更新。但用户收到的这几笔成交消息的次序，有可能与实际的成交次序不完全一致。另外，如果一张订单的成交及撤销几乎同时发生，例如 IOC 订单成交后剩余部分被自动撤销，用户可能会先收到撤单推送，再收到成交推送。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#subscribe-trade-details-amp-order-cancellation-post-clearing" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#7ae76c1c98" /></para>
        /// </summary>
        /// <param name="symbol">Subscribe to a specific symbol</param>
        /// <param name="onOrderMatch">Event handler for the order matched event</param>
        /// <param name="onOrderCancel">Event handler for the order canceled event</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderDetailsUpdatesAsync(string? symbol = null,
            Action<DataEvent<HuobiTradeUpdate>>? onOrderMatch = null, Action<DataEvent<HuobiOrderCancelationUpdate>>? onOrderCancel = null, CancellationToken ct = default);

        /// <summary>
        /// Req Specified time candlestick
        /// WebSocket获取指定时间段的K线
        /// 单次获取：WebSocket行情数据 
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#market-candlestick" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#k-2" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get the data for</param>
        /// <param name="period">The period of a single candlestick</param>
        /// <param name="startTimeStamp">The candlestick start time stamp</param>
        /// <param name="endTimeStamp">The candlestick end time stamp</param>
        /// <returns></returns>
        Task<CallResult<IEnumerable<HuobiSpecifiedTimeKLine>>> GetSpecifiedTimeKLinesAsync(string symbol, KlineInterval period, long startTimeStamp, long endTimeStamp);
    }
}