using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.CommonClients;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Huobi.Net.Enums;
using Huobi.Net.Interfaces.Clients.UsdtMargined;
using Huobi.Net.Objects;
using Huobi.Net.Objects.Models;

namespace Huobi.Net.Clients.UsdtMargined
{
    /// <inheritdoc />
    public class HuobiClientUsdtMarginedApi : RestApiClient, IHuobiClientUsdtMarginedApi, IUsdtMarginedClient
    {
        private readonly HuobiUsdtMarginedClient _baseClient;
        private readonly HuobiUsdtMarginedClientOptions _options;
        private readonly Log _log;

        internal static TimeSyncState TimeSyncState = new TimeSyncState("Usdt Margined Api");

        /// <summary>
        /// Event triggered when an order is placed via this client
        /// </summary>
        public event Action<OrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is canceled via this client
        /// </summary>
        public event Action<OrderId>? OnOrderCanceled;

        /// <inheritdoc />
        public string ExchangeName => "Huobi";

        #region Api clients

        /// <inheritdoc />
        public IHuobiClientUsdtMarginedApiReferenceData ReferenceData { get; }

        /// <inheritdoc />
        public IHuobiClientUsdtMarginedApiMarketData MarketData { get; }

        /// <inheritdoc />
        public IHuobiClientUsdtMarginedApiAccount Account { get; }

        /// <inheritdoc />
        public IHuobiClientUsdtMarginedApiTrade Trade { get; }

        /// <inheritdoc />
        public IHuobiClientUsdtMarginedApiStrategyOrder Strategy { get; }

        /// <inheritdoc />
        public IHuobiClientUsdtMarginedApiTransferring Transferring { get; }


        #endregion

        #region constructor/destructor
        internal HuobiClientUsdtMarginedApi(Log log, HuobiUsdtMarginedClient baseClient, HuobiUsdtMarginedClientOptions options)
            : base(options, options.UsdtMarginedApiOptions)
        {
            _baseClient = baseClient;
            _options = options;
            _log = log;

            ReferenceData = new HuobiClientUsdtMarginedApiReferenceData(this);
            MarketData = new HuobiClientUsdtMarginedApiMarketData(this);
            Account = new HuobiClientUsdtMarginedApiAccount(this);
            Trade = new HuobiClientUsdtMarginedApiTrade(this);
            Strategy = new HuobiClientUsdtMarginedApiStrategyOrder(this);
            Transferring = new HuobiClientUsdtMarginedApiTransferring(this);

        manualParseError = true;
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new HuobiAuthenticationProvider(credentials, _options.SignPublicRequests);

        #region methods

        internal Task<WebCallResult<(T, DateTime)>> SendHuobiTimestampRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
            => _baseClient.SendHuobiTimestampRequest<T>(this, uri, method, cancellationToken, parameters, signed);

        internal Task<WebCallResult<T>> SendHuobiRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, int? weight = 1, bool ignoreRatelimit = false)
            => _baseClient.SendHuobiRequest<T>(this, uri, method, cancellationToken, parameters, signed, weight, ignoreRatelimit);

        internal Task<WebCallResult<T>> SendHuobiV2Request<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
            => _baseClient.SendHuobiV2Request<T>(this, uri, method, cancellationToken, parameters, signed);

        internal Task<WebCallResult<T>> SendHuobiV3Request<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false)
            => _baseClient.SendHuobiV3Request<T>(this, uri, method, cancellationToken, parameters, signed);

        /// <summary>
        /// Construct url
        /// </summary>
        /// <param name="endpoint">接口</param>
        /// <param name="apiPath">API路径</param>
        /// <param name="version"></param>
        /// <returns></returns>
        internal Uri GetUrl(string endpoint, ApiPath? apiPath = null, string? version = null)
        {
            var result = BaseAddress;

            if (!object.Equals(apiPath,null))
                result = BaseAddress.AppendPath(ApiPathExtensions.GetString((ApiPath)apiPath));

            if (version == null)
                result = result.AppendPath(endpoint);
            else
                result = result.AppendPath($"v{version}", endpoint);
            return new Uri(result);
        }

        internal void InvokeOrderPlaced(OrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(OrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }
        #endregion

        #region common interface

        /// 【逐仓】U本位合约下单
        async Task<WebCallResult<OrderId>> IUsdtMarginedClient.LinearSwapOrder(string contractCode, UmDirection direction, UmOffset offset, decimal price, UmLeverRate leverRate, long volume, UmOrderPriceType orderPriceType, decimal tpTriggerPrice, decimal tpOrderPrice, UmTpOrderPriceType tpOrderPriceType, decimal slTriggerPrice, decimal slOrderPrice, UmSlOrderPriceType slOrderPriceType, string? accountId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(contractCode))
                throw new ArgumentException(nameof(contractCode) + " required for Huobi " + nameof(IUsdtMarginedClient.LinearSwapOrder), nameof(contractCode));

            if (string.IsNullOrEmpty(accountId) || !long.TryParse(accountId, out var id))
                throw new ArgumentException(nameof(accountId) + " required for Huobi " + nameof(IUsdtMarginedClient.LinearSwapOrder), nameof(accountId));

            var huobiUmOrderPriceType = GetUmOrderPriceType(orderPriceType);
            var result = await Trade.LinearSwapCrossOrder(
                contractCode: contractCode,
                direction: direction,
                offset: offset,
                price: price,
                leverRate: leverRate,
                volume: volume,
                orderPriceType: huobiUmOrderPriceType,
                tpTriggerPrice: tpTriggerPrice,
                tpOrderPrice: tpOrderPrice,
                tpOrderPriceType: tpOrderPriceType,
                slTriggerPrice: slTriggerPrice,
                slOrderPrice: slOrderPrice,
                slOrderPriceType: slOrderPriceType,
                accountId: accountId,
                clientOrderId: clientOrderId,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.As<OrderId>(null);
            return result.As(new OrderId()
            {
                SourceObject = result.Data,
                Id = result.Data.ToString()
            });
        }

        /// 【全仓】U本位合约下单      
        async Task<WebCallResult<OrderId>> IUsdtMarginedClient.LinearSwapCrossOrder(string contractCode, UmDirection direction, UmOffset offset, decimal price, UmLeverRate leverRate, long volume, UmOrderPriceType orderPriceType, decimal tpTriggerPrice, decimal tpOrderPrice, UmTpOrderPriceType tpOrderPriceType, decimal slTriggerPrice, decimal slOrderPrice, UmSlOrderPriceType slOrderPriceType, string? accountId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(contractCode))
                throw new ArgumentException(nameof(contractCode) + " required for Huobi " + nameof(IUsdtMarginedClient.LinearSwapOrder), nameof(contractCode));

            if (string.IsNullOrEmpty(accountId) || !long.TryParse(accountId, out var id))
                throw new ArgumentException(nameof(accountId) + " required for Huobi " + nameof(IUsdtMarginedClient.LinearSwapOrder), nameof(accountId));

            var huobiUmOrderPriceType = GetUmOrderPriceType(orderPriceType);
            var result = await Trade.LinearSwapCrossOrder(
                contractCode :contractCode,
                direction :direction,
                offset:offset,
                price : price,
                leverRate : leverRate,
                volume : volume,
                orderPriceType : huobiUmOrderPriceType,
                tpTriggerPrice : tpTriggerPrice,
                tpOrderPrice : tpOrderPrice,
                tpOrderPriceType : tpOrderPriceType,
                slTriggerPrice : slTriggerPrice,
                slOrderPrice : slOrderPrice,
                slOrderPriceType : slOrderPriceType,
                accountId : accountId,
                clientOrderId : clientOrderId,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.As<OrderId>(null);
            return result.As(new OrderId()
            {
                SourceObject = result.Data,
                Id = result.Data.ToString()
            });
        }

        /// 【逐仓】获取用户持仓信息
        async Task<WebCallResult<IEnumerable<Position>>> IUsdtMarginedClient.GetLinearSwapPositionInfoAsync(string? contractCode = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// 【全仓】获取用户持仓信息
        async Task<WebCallResult<IEnumerable<Position>>> IUsdtMarginedClient.GetLinearSwapCrossPositionInfoAsync(string? contractCode = null, string? pair = null, UmContractType? umContractType = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the name of a symbol for Huobi based on the base and quote asset
        /// </summary>
        /// <param name="baseAsset"></param>
        /// <param name="quoteAsset"></param>
        /// <returns></returns>
        public string GetSymbolName(string baseAsset, string quoteAsset) 
            => (baseAsset + "-" + quoteAsset).ToUpperInvariant();

        async Task<WebCallResult<IEnumerable<Symbol>>> IBaseRestClient.GetSymbolsAsync(CancellationToken ct)
        {
            //var symbols = await MarketData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            //if (!symbols)
            //    return symbols.As<IEnumerable<Symbol>>(null);

            //return symbols.As(symbols.Data.Select(d => new Symbol
            //{
            //    SourceObject = d,
            //    Name = d.Name,
            //    MinTradeQuantity = d.MinLimitOrderQuantity,
            //    PriceDecimals = d.PricePrecision,
            //    QuantityDecimals = d.QuantityPrecision
            //}));
            return null;
        }

        async Task<WebCallResult<Ticker>> IBaseRestClient.GetTickerAsync(string symbol, CancellationToken ct)
        {
            //if (string.IsNullOrEmpty(symbol))
            //    throw new ArgumentException(nameof(symbol) + " required for Huobi " + nameof(IUsdtMarginedClient.GetTickerAsync), nameof(symbol));

            //var tickers = await MarketData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            //if (!tickers)
            //    return tickers.As<Ticker>(null);

            //var ticker = tickers.Data.Ticks.SingleOrDefault(s => s.Symbol == symbol);
            //return tickers.As(new Ticker
            //{
            //    SourceObject =ticker,
            //    HighPrice = ticker.HighPrice,
            //    Symbol = ticker.Symbol,
            //    LastPrice = ticker.ClosePrice,
            //    LowPrice = ticker.LowPrice,
            //    Price24H = ticker.OpenPrice,
            //    Volume = ticker.Volume
            //});
            return null;
        }

        async Task<WebCallResult<IEnumerable<Ticker>>> IBaseRestClient.GetTickersAsync(CancellationToken ct)
        {
            //var tickers = await MarketData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            //if (!tickers)
            //    return tickers.As<IEnumerable<Ticker>>(null);

            //return tickers.As(tickers.Data.Ticks.Select(t => new Ticker
            //{
            //    SourceObject = t,
            //    HighPrice = t.HighPrice,
            //    Symbol = t.Symbol,
            //    LastPrice = t.ClosePrice,
            //    LowPrice = t.LowPrice,
            //    Price24H = t.OpenPrice,
            //    Volume = t.Volume
            //}));
            return null;
        }

        async Task<WebCallResult<IEnumerable<Kline>>> IBaseRestClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime, DateTime? endTime, int? limit, CancellationToken ct)
        {
            //if (string.IsNullOrEmpty(symbol))
            //    throw new ArgumentException(nameof(symbol) + " required for Huobi " + nameof(IUsdtMarginedClient.GetKlinesAsync), nameof(symbol));

            //if (startTime != null || endTime != null)
            //    throw new ArgumentException($"Huobi does not support the {nameof(startTime)}/{nameof(endTime)} parameters for the method {nameof(IBaseRestClient.GetKlinesAsync)}");

            //var klines = await MarketData.GetKlinesAsync(symbol, GetKlineIntervalFromTimespan(timespan), limit ?? 500, ct: ct).ConfigureAwait(false);
            //if (!klines)
            //    return klines.As<IEnumerable<Kline>>(null);

            //return klines.As(klines.Data.Select(d => new Kline
            //{
            //    SourceObject = d,
            //    ClosePrice = d.ClosePrice,
            //    HighPrice = d.HighPrice,
            //    LowPrice = d.LowPrice,
            //    OpenPrice = d.OpenPrice,
            //    OpenTime = d.OpenTime,
            //    Volume = d.Volume
            //}));
            return null;
        }

        async Task<WebCallResult<OrderBook>> IBaseRestClient.GetOrderBookAsync(string symbol, CancellationToken ct)
        {
            //if (string.IsNullOrEmpty(symbol))
            //    throw new ArgumentException(nameof(symbol) + " required for Huobi " + nameof(IUsdtMarginedClient.GetOrderBookAsync), nameof(symbol));

            //var book = await MarketData.GetOrderBookAsync(symbol, 0, ct: ct).ConfigureAwait(false);
            //if (!book)
            //    return book.As<OrderBook>(null);

            //return book.As(new OrderBook
            //{
            //    SourceObject = book.Data,
            //    Asks = book.Data.Asks.Select(a => new OrderBookEntry { Price = a.Price, Quantity = a.Quantity }),
            //    Bids = book.Data.Bids.Select(b => new OrderBookEntry { Price = b.Price, Quantity = b.Quantity })
            //});
            return null;
        }

        async Task<WebCallResult<IEnumerable<Trade>>> IBaseRestClient.GetRecentTradesAsync(string symbol, CancellationToken ct)
        {
            //if (string.IsNullOrEmpty(symbol))
            //    throw new ArgumentException(nameof(symbol) + " required for Huobi " + nameof(IUsdtMarginedClient.GetRecentTradesAsync), nameof(symbol));

            //var trades = await MarketData.GetTradeHistoryAsync(symbol, 100, ct).ConfigureAwait(false);
            //if (!trades)
            //    return trades.As<IEnumerable<Trade>>(null);

            //return trades.As(trades.Data.SelectMany(t => t.Details).Select(t => new Trade
            //{
            //    SourceObject = t,
            //    Price = t.Price,
            //    Quantity = t.Quantity,
            //    Symbol = symbol,
            //    Timestamp = t.Timestamp
            //}));
            return null;
        }

        async Task<WebCallResult<Order>> IBaseRestClient.GetOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            //if(!long.TryParse(orderId, out var id))
            //    throw new ArgumentException("Invalid order id for Huobi " + nameof(IUsdtMarginedClient.GetOrderAsync), nameof(orderId));

            //var order = await Trade.GetOrderAsync(id, ct: ct).ConfigureAwait(false);
            //if (!order)
            //    return order.As<Order>(null);

            //return order.As(new Order
            //{
            //    SourceObject = order.Data,
            //    Id = order.Data.Id.ToString(CultureInfo.InvariantCulture),
            //    Price = order.Data.Price,
            //    Quantity = order.Data.Quantity,
            //    QuantityFilled = order.Data.QuantityFilled,
            //    Symbol = order.Data.Symbol,
            //    Timestamp = order.Data.CreateTime,
            //    Side = order.Data.Side == OrderSide.Buy ? CommonOrderSide.Buy: CommonOrderSide.Sell,
            //    Type = order.Data.Type == OrderType.Limit ? CommonOrderType.Limit: order.Data.Type == OrderType.Market ? CommonOrderType.Market: CommonOrderType.Other,
            //    Status = order.Data.State == OrderState.Canceled || order.Data.State == OrderState.PartiallyCanceled ? CommonOrderStatus.Canceled: order.Data.State == OrderState.Filled ? CommonOrderStatus.Filled: CommonOrderStatus.Active
            //});
            return null;
        }

        async Task<WebCallResult<IEnumerable<UserTrade>>> IBaseRestClient.GetOrderTradesAsync(string orderId, string? symbol, CancellationToken ct)
        {
            //if (!long.TryParse(orderId, out var id))
            //    throw new ArgumentException("Invalid order id for Huobi " + nameof(IUsdtMarginedClient.GetOrderAsync), nameof(orderId));

            //var result = await Trade.GetOrderTradesAsync(id, ct: ct).ConfigureAwait(false);
            //if (!result)
            //    return result.As<IEnumerable<UserTrade>>(null);

            //return result.As(result.Data.Select(t => new UserTrade
            //{
            //    SourceObject = t,
            //    Id = t.Id.ToString(CultureInfo.InvariantCulture),
            //    OrderId = t.OrderId.ToString(CultureInfo.InvariantCulture),
            //    Symbol = t.Symbol,
            //    Fee = t.Fee,
            //    FeeAsset = t.FeeAsset,
            //    Price = t.Price,
            //    Quantity = t.Quantity,
            //    Timestamp = t.Timestamp
            //}));
            return null;
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetOpenOrdersAsync(string? symbol, CancellationToken ct)
        {
            //var orders = await Trade.GetOpenOrdersAsync(symbol: symbol, ct: ct).ConfigureAwait(false);
            //if (!orders)
            //    return orders.As<IEnumerable<Order>>(null);

            //return orders.As(orders.Data.Select(o =>            
            //    new Order
            //    {
            //        SourceObject = o,
            //        Id = o.Id.ToString(CultureInfo.InvariantCulture),
            //        Price = o.Price,
            //        Quantity = o.Quantity,
            //        QuantityFilled = o.QuantityFilled,
            //        Symbol = o.Symbol,
            //        Timestamp = o.CreateTime,
            //        Side = o.Side == OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
            //        Type = o.Type == OrderType.Limit ? CommonOrderType.Limit : o.Type == OrderType.Market ? CommonOrderType.Market : CommonOrderType.Other,
            //        Status = o.State == OrderState.Canceled || o.State == OrderState.PartiallyCanceled ? CommonOrderStatus.Canceled : o.State == OrderState.Filled ? CommonOrderStatus.Filled : CommonOrderStatus.Active
            //    }
            //));
            return null;
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetClosedOrdersAsync(string? symbol, CancellationToken ct)
        {
            //if (string.IsNullOrEmpty(symbol))
            //    throw new ArgumentException(nameof(symbol) + " required for Huobi " + nameof(IUsdtMarginedClient.GetClosedOrdersAsync), nameof(symbol));

            //var result = await Trade.GetClosedOrdersAsync(symbol!, ct: ct).ConfigureAwait(false);
            //if (!result)
            //    return result.As<IEnumerable<Order>>(null);

            //return result.As(result.Data.Select(o =>
            //    new Order
            //    {
            //        SourceObject = o,
            //        Id = o.Id.ToString(CultureInfo.InvariantCulture),
            //        Price = o.Price,
            //        Quantity = o.Quantity,
            //        QuantityFilled = o.QuantityFilled,
            //        Symbol = o.Symbol,
            //        Timestamp = o.CreateTime,
            //        Side = o.Side == OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
            //        Type = o.Type == OrderType.Limit ? CommonOrderType.Limit : o.Type == OrderType.Market ? CommonOrderType.Market : CommonOrderType.Other,
            //        Status = o.State == OrderState.Canceled || o.State == OrderState.PartiallyCanceled ? CommonOrderStatus.Canceled : o.State == OrderState.Filled ? CommonOrderStatus.Filled : CommonOrderStatus.Active
            //    }
            //));
            return null;
        }

        async Task<WebCallResult<OrderId>> IBaseRestClient.CancelOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            //if (!long.TryParse(orderId, out var id))
            //    throw new ArgumentException("Invalid order id for Huobi " + nameof(IUsdtMarginedClient.CancelOrderAsync), nameof(orderId));

            //var result = await Trade.CancelOrderAsync(id, ct: ct).ConfigureAwait(false);
            //if (!result)
            //    return result.As<OrderId>(null);

            //return result.As(new OrderId
            //{
            //    SourceObject = result.Data,
            //    Id = result.Data.ToString(CultureInfo.InvariantCulture)
            //});
            return null;
        }

        async Task<WebCallResult<IEnumerable<Balance>>> IBaseRestClient.GetBalancesAsync(string? accountId, CancellationToken ct)
        {
            //if (string.IsNullOrEmpty(accountId) || !long.TryParse(accountId, out var id))
            //    throw new ArgumentException(nameof(accountId) + " required for Huobi " + nameof(IUsdtMarginedClient.GetBalancesAsync), nameof(accountId));

            //var balances = await Account.GetLinearSwapPositionInfo(long.Parse(accountId), ct: ct).ConfigureAwait(false);
            //if (!balances)
            //    return balances.As<IEnumerable<Balance>>(null);

            //var result = new List<Balance>();
            //foreach (var balance in balances.Data)
            //{
            //    if (balance.Type == BalanceType.Interest || balance.Type == BalanceType.Loan)
            //        continue;

            //    var existing = result.SingleOrDefault(b => b.Asset == balance.Asset);
            //    if (existing == null)
            //    {
            //        existing = new Balance() { Asset = balance.Asset };
            //        result.Add(existing);
            //    }

            //    if (balance.Type == BalanceType.Frozen)
            //        existing.Total += balance.Balance;
            //    else
            //    {
            //        existing.Total += balance.Balance;
            //        existing.Available = balance.Balance;
            //    }
            //}

            //return balances.As<IEnumerable<Balance>>(result);
            return null;
        }

        private static UmOrderPriceType GetUmOrderPriceType(UmOrderPriceType type)
        {
            switch (type)
            {
                case UmOrderPriceType.limit: 
                    return UmOrderPriceType.limit;
                case UmOrderPriceType.opponent: 
                    return UmOrderPriceType.opponent;
                case UmOrderPriceType.post_only: 
                    return UmOrderPriceType.post_only;
                case UmOrderPriceType.optimal_5: 
                    return UmOrderPriceType.optimal_5;
                case UmOrderPriceType.optimal_10: 
                    return UmOrderPriceType.optimal_10;
                case UmOrderPriceType.optimal_20: 
                    return UmOrderPriceType.optimal_20;
                case UmOrderPriceType.ioc:
                    return UmOrderPriceType.ioc;
                case UmOrderPriceType.fok:
                    return UmOrderPriceType.fok;
                case UmOrderPriceType.opponent_ioc:
                    return UmOrderPriceType.opponent_ioc;
                case UmOrderPriceType.optimal_5_ioc:
                    return UmOrderPriceType.optimal_5_ioc;
                case UmOrderPriceType.optimal_10_ioc:
                    return UmOrderPriceType.optimal_10_ioc;
                case UmOrderPriceType.optimal_20_ioc:
                    return UmOrderPriceType.optimal_20_ioc;
                case UmOrderPriceType.opponent_fok:
                    return UmOrderPriceType.opponent_fok;
                case UmOrderPriceType.optimal_5_fok:
                    return UmOrderPriceType.optimal_5_fok;
                case UmOrderPriceType.optimal_10_fok:
                    return UmOrderPriceType.optimal_10_fok;
                case UmOrderPriceType.optimal_20_fok:
                    return UmOrderPriceType.optimal_20_fok;
                default:
                    return UmOrderPriceType.limit;
            }
        }

        private static KlineInterval GetKlineIntervalFromTimespan(TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.FromMinutes(1)) return KlineInterval.OneMinute;
            if (timeSpan == TimeSpan.FromMinutes(5)) return KlineInterval.FiveMinutes;
            if (timeSpan == TimeSpan.FromMinutes(15)) return KlineInterval.FifteenMinutes;
            if (timeSpan == TimeSpan.FromMinutes(30)) return KlineInterval.ThirtyMinutes;
            if (timeSpan == TimeSpan.FromHours(1)) return KlineInterval.OneHour;
            if (timeSpan == TimeSpan.FromHours(4)) return KlineInterval.FourHours;
            if (timeSpan == TimeSpan.FromDays(1)) return KlineInterval.OneDay;
            if (timeSpan == TimeSpan.FromDays(7)) return KlineInterval.OneWeek;
            if (timeSpan == TimeSpan.FromDays(30) || timeSpan == TimeSpan.FromDays(31)) return KlineInterval.OneMonth;
            if (timeSpan == TimeSpan.FromDays(365)) return KlineInterval.OneYear;

            throw new ArgumentException("Unsupported timespan for Huobi Klines, check supported intervals using Huobi.Net.Objects.HuobiPeriod");
        }
        #endregion

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ReferenceData.GetLinearSwapServerDateTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, _options.UsdtMarginedApiOptions.AutoTimestamp, _options.UsdtMarginedApiOptions.TimestampRecalculationInterval, TimeSyncState);

        /// <inheritdoc />
        public override TimeSpan GetTimeOffset()
            => TimeSyncState.TimeOffset;

        /// <inheritdoc />
        /// TODO make this take an accountId param so we don't need it in the interface?
        public IUsdtMarginedClient CommonUsdtMarginedClient => this;
    }
}
