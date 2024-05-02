using CryptoExchange.Net.Sockets;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Socket.Futures.UsdtMargined.WebSocketOrderAndAccounts;

namespace Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// 火币U本位合约websocket数据流接口
    /// </summary>
    public interface IHuobiSocketClientUsdtMarginSwapApi
    {
        #region WeSocket市场行情接口
        /// <summary>
        /// 【通用】订阅KLine数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#kline" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="period">K线周期</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeMarketContractCodeKlineAsync(string contractCode, KlineInterval period, string clientId, Action<DataEvent<HuobiContractCodeKlineTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【通用】请求KLine数据
        /// 成功建立和 WebSocket API 的连接之后，向Server请求数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#kline-2" /></para>
        /// </summary>
        /// <param name="contractCode"> 合约代码 或 合约标识</param>
        /// <param name="period"> 合约代码 或 合约标识</param>
        /// 需要订阅的主题，该接口固定为：market.$contract_code.kline.$period，详细参数见req请求参数说明
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="from">开始时间</param>
        /// <param name="to">结束时间</param>
        /// <returns></returns>
        Task<CallResult<IEnumerable<HuobiContractCodeKlineTick>>> GetMarketContractCodeKlineAsync(string contractCode, KlineInterval period , string clientId, long from, long to);

        /// <summary>
        /// 【通用】订阅Market Depth数据
        /// 成功建立和 WebSocket API 的连接之后，向Server请求数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#market-depth" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="type">Depth 类型</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeMarketContractCodeDepthAsync(string contractCode, string type, string clientId, Action<DataEvent<HuobiContractCodeDepthTick>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【通用】订阅Market Depth 增量数据
        /// 成功建立和 WebSocket API 的连接之后，向Server请求数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#market-depth-2" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="size">档位数</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeMarketContractCodeIncrementalDepthAsync(string contractCode, string size, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【通用】订阅Market Detail 数据
        /// 成功建立和 WebSocket API 的连接之后，向Server请求数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#market-detail" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeMarketContractCodeDetailAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【通用】订阅买一卖一逐笔行情推送
        /// 成功建立和 WebSocket API 的连接之后，向Server请求数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#db0972ceb0" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeMarketContractCodeBboAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);


        /// <summary>
        /// 【通用】请求Trade Detail 数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server 发送数据来请求数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#trade-detail" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识
        /// 需要订阅的主题固定为：market.$contract_code.trade.detail，详细参数见req请求参数说明</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="size">数据条数，最多50，不填默认50</param>
        /// <returns></returns>
        Task<CallResult<IEnumerable<object>>> GetMarketContractCodeTradeDetailAsync(string contractCode, string clientId, int size = 50);

        /// <summary>
        /// 【通用】订阅Trade Detail数据
        /// 成功建立和 WebSocket API 的连接之后，向Server请求数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#trade-detail-2" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeMarketContractCodeTradeDetailAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);
        #endregion

        #region WeSocket指数与基差数据接口
        /// <summary>
        /// 【通用】订阅指数KLine数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#sub-k" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="period">K线周期</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeMarketContractCodeIndexAsync(string contractCode, KlineInterval period, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【通用】请求指数KLine数据
        /// 成功建立和 WebSocket API 的连接之后，向Server请求数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#req-k" /></para>
        /// </summary>
        /// <param name="contractCode"> 合约代码 或 合约标识</param>
        /// <param name="period"> 合约代码 或 合约标识</param>
        /// 需要订阅的主题，该接口固定为：market.$contract_code.index.$period，详细参数见req请求参数说明
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="from">开始时间</param>
        /// <param name="to">结束时间</param>
        /// <returns></returns>
        Task<CallResult<IEnumerable<object>>> GetMarketContractCodeIndexAsync(string contractCode, KlineInterval period, string clientId, long from, long to);

        /// <summary>
        /// 【通用】订阅溢价指数KLine数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#k-5" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="period">K线周期</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeMarketContractCodePremiumIndexAsync(string contractCode, KlineInterval period, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【通用】请求溢价指数KLine数据
        /// 成功建立和 WebSocket API 的连接之后，向Server请求数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#k-6" /></para>
        /// </summary>
        /// <param name="contractCode"> 合约代码 或 合约标识</param>
        /// <param name="period"> 合约代码 或 合约标识</param>
        /// 需要订阅的主题，该接口固定为：market.$contract_code.premium_index.$period，详细参数见req请求参数说明
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="from">开始时间</param>
        /// <param name="to">结束时间</param>
        /// <returns></returns>
        Task<CallResult<IEnumerable<object>>> GetMarketContractCodePremiumIndexAsync(string contractCode, KlineInterval period, string clientId, long from, long to);

        /// <summary>
        /// 【通用】订阅预测资金费率K线数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#k-7" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="period">K线周期</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeMarketContractCodeEstimatedRateAsync(string contractCode, KlineInterval period, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【通用】请求预测资金费率K线数据
        /// 成功建立和 WebSocket API 的连接之后，向Server请求数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#k-8" /></para>
        /// </summary>
        /// <param name="contractCode"> 合约代码 或 合约标识</param>
        /// <param name="period"> 合约代码 或 合约标识</param>
        /// 需要订阅的主题，该接口固定为：market.$contract_code.estimated_rate.$period，详细参数见req请求参数说明
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="from">开始时间</param>
        /// <param name="to">结束时间</param>
        /// <returns></returns>
        Task<CallResult<IEnumerable<object>>> GetMarketContractCodeEstimatedRateAsync(string contractCode, KlineInterval period, string clientId, long from, long to);

        /// <summary>
        /// 【通用】订阅基差数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#4427b892b6" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="period">K线周期</param>
        /// <param name="basisPriceType">基差价格类型，表示在周期内计算基差使用的价格类型</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeMarketContractBasisAsync(string contractCode, KlineInterval period, string basisPriceType, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【通用】请求基差数据
        /// 成功建立和 WebSocket API 的连接之后，向Server请求数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#39a868638b" /></para>
        /// </summary>
        /// <param name="contractCode"> 合约代码 或 合约标识</param>
        /// <param name="period"> 合约代码 或 合约标识</param>
        /// <param name="basisPriceType">基差价格类型，表示在周期内计算基差使用的价格类型</param>
        /// 需要订阅的主题，该接口固定为：market.$contract_code.estimated_rate.$period，详细参数见req请求参数说明
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="from">开始时间</param>
        /// <param name="to">结束时间</param>
        /// <returns></returns>
        Task<CallResult<IEnumerable<object>>> GetMarketContractBasisAsync(string contractCode, KlineInterval period, string basisPriceType, string clientId, long from, long to);

        /// <summary>
        /// 【通用】订阅标记价格K线数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#k-9" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="period">K线周期</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeMarketContractCodeMarkPriceAsync(string contractCode, KlineInterval period, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【通用】请求标记价格K线数据
        /// 成功建立和 WebSocket API 的连接之后，向Server请求数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#k-10" /></para>
        /// </summary>
        /// <param name="contractCode"> 合约代码 或 合约标识</param>
        /// <param name="period"> 合约代码 或 合约标识</param>
        /// 需要订阅的主题，该接口固定为：market.$contract_code.mark_price.$period，详细参数见req请求参数说明
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="from">开始时间</param>
        /// <param name="to">结束时间</param>
        /// <returns></returns>
        Task<CallResult<IEnumerable<object>>> GetMarketContractCodeMarkPriceAsync(string contractCode, KlineInterval period, string clientId, long from, long to);
        #endregion

        #region WeSocket订单和用户数据接口
        /// <summary>
        /// 【逐仓】订阅订单成交数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#sub-12" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeOrderContractCodeAsync(string contractCode, string clientId, Action<DataEvent<WWTHuobiUsdtMarginedMarketSubscribeOrderData>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【逐仓】取消订阅订单成交数据
        /// 成功建立和 WebSocket API 的连接之后，取消订阅订单成交数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#unsub" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        void UnsubscribeOrderContractCodeAsync(string contractCode, string clientId, CancellationToken ct = default);

        /// <summary>
        /// 【全仓】订阅订单成交数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#sub-13" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeOrderCrossContractCodeAsync(string contractCode, string clientId, Action<DataEvent<WWTHuobiUsdtMarginedMarketSubscribeCrossOrderData>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【全仓】取消订阅订单成交数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#unsub-2" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> UnsubscribeOrderCrossContractCodeAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【逐仓】订阅资产变动数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#sub-15" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeAccountsContractCodeAsync(string contractCode, string clientId, Action<DataEvent<IEnumerable<WWTHuobiUsdtMarginedAccountSebscribePositionInfo>>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【逐仓】取消订阅资产变动数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#unsub-4" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> UnsubscribeAccountsContractCodeAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【全仓】订阅资产变动数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#sub-16" /></para>
        /// </summary>
        /// <param name="marginAccount">margin account目前只有一个全仓账户（USDT）</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeAccountsCrossContractCodeAsync(string marginAccount, string clientId, Action<DataEvent<IEnumerable<WWTHuobiUsdtMarginedAccountSebscribeCrossPositionInfo>>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【全仓】取消订阅资产变动数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#unsub-5" /></para>
        /// </summary>
        /// <param name="marginAccount">margin account目前只有一个全仓账户（USDT）</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> UnsubscribeAccountsCrossContractCodeAsync(string marginAccount, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【逐仓】订阅持仓变动更新数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#sub-17" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribePositionsContractCodeAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【逐仓】取消订阅持仓变动更新数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#unsub-6" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> UnsubscribePositionsContractCodeAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【全仓】订阅持仓变动更新数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#sub-17" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribePositionsCrossContractCodeAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【全仓】取消订阅持仓变动更新数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#unsub-7" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> UnsubscribePositionsCrossContractCodeAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【逐仓】订阅合约订单撮合数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#sub-20" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeMatchOrdersContractCodeAsync(string contractCode, string clientId, Action<DataEvent<WWTHuobiUsdtMarginedMarketSubscriboMatchOrder>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【逐仓】取消订阅合约订单撮合数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#unsub-9" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> UnsubscribeMatchOrdersContractCodeAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【全仓】订阅合约订单撮合数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#sub-21" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeMatchOrdersCrossContractCodeAsync(string contractCode, string clientId, Action<DataEvent<WWTHuobiUsdtMarginedMarketSubscriboCrossMatchOrder>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【全仓】取消订阅合约订单撮合数据
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#unsub-10" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> UnsubscribeMatchOrdersCrossContractCodeAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【通用】订阅强平订单数据（免鉴权）
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#sub-23" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="businessType">业务类型，不填默认永续</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribePublicContractCodeLiquidationOrdersAsync(string contractCode, string businessType, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【通用】取消订阅强平订单数据（免鉴权）
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#unsub-12" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="businessType">业务类型，不填默认永续</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> UnsubscribePublicContractCodeLiquidationOrdersAsync(string contractCode, string businessType, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【通用】订阅资金费率推送（免鉴权）
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#sub-25" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribePublicContractCodeFundingRateAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【通用】取消订阅资金费率推送（免鉴权）
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#unsub-13" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> UnsubscribePublicContractCodeFundingRateAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【通用】订阅合约信息变动（免鉴权）
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#sub-26" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="businessType">业务类型，不填默认永续</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribePublicContractCodeContractInfoAsync(string contractCode, string businessType, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【通用】取消订阅合约信息变动（免鉴权）
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#unsub-14" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="businessType">业务类型，不填默认永续</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> UnsubscribePublicContractCodeContractInfoAsync(string contractCode, string businessType, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【逐仓】订阅计划委托订单更新
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#sub-28" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribetRiggerOrderContractCodeAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【逐仓】取消订阅计划委托订单更新
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#unsub-15" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> UnsubscribeRiggerOrderContractCodeAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【全仓】订阅计划委托订单更新
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#sub-29" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribetRiggerOrderCrossContractCodeAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        /// <summary>
        /// 【全仓】取消订阅计划委托订单更新
        /// 成功建立和 WebSocket API 的连接之后，向 Server订阅数据
        /// <para><a href="" /></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#unsub-16" /></para>
        /// </summary>
        /// <param name="contractCode">合约代码 或 合约标识</param>
        /// <param name="clientId">选填;Client 请求唯一 ID</param>
        /// <param name="onData">更新处理程序 The handler for updates</param>
        /// <param name="ct">用于关闭此订阅的取消令牌 Cancellation token for closing this subscription</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> UnsubscribeRiggerOrderCrossContractCodeAsync(string contractCode, string clientId, Action<DataEvent<Object>> onData, CancellationToken ct = default);

        #endregion
    }
}
