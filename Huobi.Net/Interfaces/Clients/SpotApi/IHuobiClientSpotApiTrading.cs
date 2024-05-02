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
    /// Huobi trading endpoints, placing and mananging orders.
    /// 火币交易端点，下单和管理订单。
    /// </summary>
    public interface IHuobiClientSpotApiTrading
    {
        /// <summary>
        /// Places an order
        /// 下单 发送一个新订单到火币以进行撮合。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#place-a-new-order" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#fd6ce2a756" /></para>
        /// </summary>
        /// <param name="accountId">The account to place the order for</param>
        /// <param name="symbol">The symbol to place the order for</param>
        /// <param name="side">The side of the order</param>
        /// <param name="type">The type of the order</param>
        /// <param name="quantity">The quantity of the order</param>
        /// <param name="price">The price of the order. Should be omitted for market orders</param>
        /// <param name="clientOrderId">The clientOrderId the order should get</param>
        /// <param name="source">Source. defaults to SpotAPI</param>
        /// <param name="stopPrice">Stop price</param>
        /// <param name="stopOperator">Operator of the stop price</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> PlaceOrderAsync(long accountId, string symbol, OrderSide side, OrderType type, decimal quantity, decimal? price = null, string? clientOrderId = null, SourceType? source = null, decimal? stopPrice = null, Operator? stopOperator = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of open orders
        /// 查询当前未成交订单 查询已提交但是仍未完全成交或未被撤销的订单。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-all-open-orders" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#95f2078356" /></para>
        /// </summary>
        /// <param name="accountId">The account id for which to get the orders for</param>
        /// <param name="symbol">The symbol for which to get the orders for</param>
        /// <param name="side">Only get buy or sell orders</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiOpenOrder>>> GetOpenOrdersAsync(long? accountId = null, string? symbol = null, OrderSide? side = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Cancels an open order
        /// 撤销订单 此接口发送一个撤销订单的请求。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#submit-cancel-for-an-order" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#4e53c0fccd" /></para>
        /// </summary>
        /// <param name="orderId">The id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> CancelOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancels an open order
        /// 撤销订单（基于client order ID） 此接口基于client-order-id（8小时内有效）发送一个撤销订单的请求。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#submit-cancel-for-an-order-based-on-client-order-id" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#client-order-id" /></para>
        /// </summary>
        /// <param name="clientOrderId">The client id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> CancelOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple open orders
        /// 批量撤销指定订单 此接口同时为多个订单（基于id）发送取消请求，建议通过order-ids来撤单，比client-order-ids更快更稳定。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#submit-cancel-for-multiple-orders-by-ids" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#5f8b337a4c" /></para>
        /// </summary>
        /// <param name="orderIds">The ids of the orders to cancel</param>
        /// <param name="clientOrderIds">The client ids of the orders to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiBatchCancelResult>> CancelOrdersAsync(IEnumerable<long>? orderIds = null, IEnumerable<string>? clientOrderIds = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple open orders
        /// 批量撤销所有订单 此接口发送批量撤销所有（单次最大100个）订单的请求。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#submit-cancel-for-multiple-orders-by-criteria" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#b9af010185" /></para>
        /// </summary>
        /// <param name="accountId">The account id used for this cancel</param>
        /// <param name="symbols">The trading symbol list (maximum 10 symbols, default value all symbols)</param>
        /// <param name="side">Filter on the direction of the trade</param>
        /// <param name="limit">The number of orders to cancel [1, 100]</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiByCriteriaCancelResult>> CancelOrdersByCriteriaAsync(long? accountId = null, IEnumerable<string>? symbols = null, OrderSide? side = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get details of an order
        /// 查询订单详情 此接口返回指定订单的最新状态和详情。通过API创建的订单，撤销超过2小时后无法查询。通过API创建的订单返回order-id，按此order-id查询订单还是返回base-record-invalid是因为系统内部处理有延迟，但是不影响成交。建议您后续重试查询或者通过订阅订单推送WebSocket消息查询。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-order-detail-of-an-order" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#92d59b6aad" /></para>
        /// </summary>
        /// <param name="orderId">The id of the order to retrieve</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiOrder>> GetOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get details of an order by client order id
        /// 查询订单详情（基于client order ID）
        /// 此接口返回指定用户自编订单号（8小时内）的订单最新状态和详情。通过API创建的订单，撤销超过2小时后无法查询。建议通过GET /v1/order/orders/{order-id}来撤单，比使用clientOrderId更快更稳定
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-order-detail-of-an-order-based-on-client-order-id" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#client-order-id-2" /></para>
        /// </summary>
        /// <param name="clientOrderId">The client id of the order to retrieve</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of trades made for a specific order
        /// 成交明细 此接口返回指定订单的成交明细。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#get-the-match-result-of-an-order" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#56c6c47284" /></para>
        /// </summary>
        /// <param name="orderId">The id of the order to get trades for</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiOrderTrade>>> GetOrderTradesAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of orders
        /// 搜索历史订单 此接口基于搜索条件查询历史订单。通过API创建的订单，撤销超过2小时后无法查询。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#search-past-orders" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#d72a5b49e7" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="states">The states of orders to return</param>
        /// <param name="types">The types of orders to return</param>
        /// <param name="startTime">Only get orders after this date</param>
        /// <param name="endTime">Only get orders before this date</param>
        /// <param name="fromId">Only get orders with ID before or after this. Used together with the direction parameter</param>
        /// <param name="direction">Direction of the results to return when using the fromId parameter</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiOrder>>> GetClosedOrdersAsync(string symbol, IEnumerable<OrderState>? states = null, IEnumerable<OrderType>? types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of trades for a specific symbol
        /// 当前和历史成交 此接口基于搜索条件查询当前和历史成交记录。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#search-match-results" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#0fa6055598" /></para>
        /// </summary>
        /// <param name="states">仅返回具有特定状态的交易 Only return trades with specific states</param>
        /// <param name="symbol">要检索交易的交易对 The symbol to retrieve trades for</param>
        /// <param name="types">要返回的订单类型 The type of orders to return</param>
        /// <param name="startTime">仅获取此日期之后的订单 Only get orders after this date</param>
        /// <param name="endTime">仅获取此日期之前的订单 Only get orders before this date</param>
        /// <param name="fromId">仅获取在此之前或之后具有ID的订单。与方向参数一起使用 Only get orders with ID before or after this. Used together with the direction parameter</param>
        /// <param name="direction">使用fromId参数时返回的结果的方向 Direction of the results to return when using the fromId parameter</param>
        /// <param name="limit">最大结果数 The max number of results</param>
        /// <param name="ct">取消令牌 Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiOrderTrade>>> GetUserTradesAsync(IEnumerable<OrderState>? states = null, string? symbol = null, IEnumerable<OrderType>? types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Gets a list of history orders
        /// 搜索最近48小时内历史订单 此接口基于搜索条件查询最近48小时内历史订单。通过API创建的订单，撤销超过2小时后无法查询。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#search-historical-orders-within-48-hours" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#48" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to get orders for</param>
        /// <param name="startTime">Only get orders after this date</param>
        /// <param name="endTime">Only get orders before this date</param>
        /// <param name="direction">Direction of the results to return</param>
        /// <param name="limit">The max number of results</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiOrder>>> GetHistoricalOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new conditional order
        /// 策略委托下单 仅可通过此节点下单策略委托，不可通过现货/杠杆交易相关接口下单策略委托，支持未触发OPEN订单最大数量为100。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#place-a-conditional-order" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#59cfd288e6" /></para>
        /// </summary>
        /// <param name="accountId">The account the order should be placed from</param>
        /// <param name="symbol">The symbol the order is for</param>
        /// <param name="side">Side of the order</param>
        /// <param name="type">Type of the order</param>
        /// <param name="stopPrice">Stop price of the order</param>
        /// <param name="quantity">Quantity of the order</param>
        /// <param name="price">Price of the order</param>
        /// <param name="quoteQuantity">Quote quantity of the order</param>
        /// <param name="trailingRate">Trailing rate of the order</param>
        /// <param name="timeInForce">Time in force</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiPlacedConditionalOrder>> PlaceConditionalOrderAsync(
            long accountId,
            string symbol,
            OrderSide side,
            ConditionalOrderType type,
            decimal stopPrice,
            decimal? quantity = null,
            decimal? price = null,
            decimal? quoteQuantity = null,
            decimal? trailingRate = null,
            TimeInForce? timeInForce = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel conditional orders
        /// 策略委托（触发前）撤单 限频值（NEW）：20次/2秒  单次请求最多批量撤销50张订单
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#cancel-conditional-orders-before-triggering" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#2a86a40df8" /></para>
        /// </summary>
        /// <param name="clientOrderIds">Client order ids of the conditional orders to cancels</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiConditionalOrderCancelResult>> CancelConditionalOrdersAsync(IEnumerable<string> clientOrderIds, CancellationToken ct = default);

        /// <summary>
        /// Get open conditional orders based on the parameters
        /// 查询未触发OPEN策略委托 以orderOrigTime检索
        /// 未触发OPEN订单，指的是已成功下单，但尚未触发，订单状态orderStatus为created的订单
        /// 未触发OPEN订单，可通过此节点查询，但不可通过现货/杠杆交易相关接口查询
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#query-open-conditional-orders-before-triggering" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#open" /></para>
        /// </summary>
        /// <param name="accountId">Filter by account id</param>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="sort">Sort direction</param>
        /// <param name="limit">Max results</param>
        /// <param name="fromId">Ids after this</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiConditionalOrder>>> GetOpenConditionalOrdersAsync(long? accountId = null, string? symbol = null, OrderSide? side = null, ConditionalOrderType? type = null, string? sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get closed conditional orders
        /// 查询策略委托历史
        /// 历史终态订单包括，触发前被主动撤销的策略委托（orderStatus=canceled），触发失败的策略委托（orderStatus=rejected），触发成功的策略委托（orderStatus=triggered）。
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#query-conditional-order-history" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#a03b914c4d" /></para>
        /// </summary>
        /// <param name="accountId">Filter by account id</param>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="side">Filter by side</param>
        /// <param name="type">Filter by type</param>
        /// <param name="sort">Sort direction</param>
        /// <param name="limit">Max results</param>
        /// <param name="fromId">Ids after this</param>
        /// <param name="ct">Cancelation token</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Return only entries after this time</param>
        /// <param name="endTime">Return only entries before this time</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiConditionalOrder>>> GetClosedConditionalOrdersAsync(
            string symbol,
            ConditionalOrderStatus status,
            long? accountId = null,
            OrderSide? side = null,
            ConditionalOrderType? type = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            string? sort = null,
            int? limit = null,
            long? fromId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get a conditional order by id
        /// 查询特定策略委托 如需查询已成功触发订单的后续状态，须通过现货/杠杆交易相关接口完成
        /// 未触发的策略委托及触发失败的策略委托，可通过此节点查询，但不可通过现货/杠杆交易相关接口查询
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/en/#query-a-specific-conditional-order" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/spot/v1/cn/#776d38c130" /></para>
        /// </summary>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancelation token</param>
        /// <returns></returns>
        Task<WebCallResult<HuobiConditionalOrder>> GetConditionalOrderAsync(string clientOrderId, CancellationToken ct = default);
    }
}
