using System;
using System.Collections.Generic;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Huobi.Net.Interfaces.Clients;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Client options
    /// </summary>
    public class HuobiUsdtMarginedClientOptions: BaseRestClientOptions
    {
        /// <summary>
        /// Default options for the usdt margined client
        /// </summary>
        public static HuobiUsdtMarginedClientOptions Default { get; set; } = new HuobiUsdtMarginedClientOptions();

        /// <summary>
        /// Whether public requests should be signed if ApiCredentials are provided. Needed for accurate rate limiting.
        /// </summary>
        public bool SignPublicRequests { get; set; } = false;

        private RestApiClientOptions _usdtMarginedApiOptions = new RestApiClientOptions(HuobiApiAddresses.Default.UsdtMarginedRestClientAddress)
        {
            RateLimiters = new List<IRateLimiter>
            {
                    new RateLimiter()
                    .AddPartialEndpointLimit("/v1/order", 100, TimeSpan.FromSeconds(2), null, true, true)
                    .AddApiKeyLimit(10, TimeSpan.FromSeconds(1), true, true)
                    .AddTotalRateLimit(10, TimeSpan.FromSeconds(1))
            }
        };
        /// <summary>
        /// Usdt margined API options
        /// </summary>
        public RestApiClientOptions UsdtMarginedApiOptions
        {
            get => _usdtMarginedApiOptions;
            set => _usdtMarginedApiOptions = new RestApiClientOptions(_usdtMarginedApiOptions, value);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public HuobiUsdtMarginedClientOptions() : this(Default)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn">Base the new options on other options</param>
        internal HuobiUsdtMarginedClientOptions(HuobiUsdtMarginedClientOptions baseOn) : base(baseOn)
        {
            if (baseOn == null)
                return;


            SignPublicRequests = baseOn.SignPublicRequests;
            _usdtMarginedApiOptions = new RestApiClientOptions(baseOn.UsdtMarginedApiOptions, null);
        }
    }

    /// <summary>
    /// Socket client options
    /// </summary>
    public class HuobiUsdtMarginedSocketClientOptions : BaseSocketClientOptions
    {
        /// <summary>
        /// Default options for the usdt margined client
        /// </summary>
        public static HuobiUsdtMarginedSocketClientOptions Default { get; set; } = new HuobiUsdtMarginedSocketClientOptions()
        {
            SocketSubscriptionsCombineTarget = 10
        };

        private HuobiUsdtMarginedSocketApiClientOptions _usdtMarginedStreamsOptions = new HuobiUsdtMarginedSocketApiClientOptions(HuobiApiAddresses.Default.SocketClientLinearSwapWsAddress, HuobiApiAddresses.Default.SocketClientLinearSwapNotificationAddress, HuobiApiAddresses.Default.SocketClientWsIndexAddress);
        /// <summary>
        /// Usdt margined stream options
        /// </summary>
        public HuobiUsdtMarginedSocketApiClientOptions UsdtMarginedStreamsOptions
        {
            get => _usdtMarginedStreamsOptions;
            set => _usdtMarginedStreamsOptions = new HuobiUsdtMarginedSocketApiClientOptions(_usdtMarginedStreamsOptions, value);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public HuobiUsdtMarginedSocketClientOptions() : this(Default)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn">Base the new options on other options</param>
        internal HuobiUsdtMarginedSocketClientOptions(HuobiUsdtMarginedSocketClientOptions baseOn) : base(baseOn)
        {
            if (baseOn == null)
                return;

            _usdtMarginedStreamsOptions = new HuobiUsdtMarginedSocketApiClientOptions(baseOn.UsdtMarginedStreamsOptions, null);
        }
    }

    /// <summary>
    /// Socket API client options
    /// </summary>
    public class HuobiUsdtMarginedSocketApiClientOptions : ApiClientOptions
    {
        /// <summary>
        /// The base address for the authenticated websocket
        /// </summary>
        public string BaseAddressAuthenticated { get; set; }

        /// <summary>
        /// The base address for the market by price websocket
        /// </summary>
        public string BaseAddressInrementalOrderBook { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public HuobiUsdtMarginedSocketApiClientOptions()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="baseAddressAuthenticated"></param>
        /// <param name="baseAddressIncrementalOrderBook"></param>
        internal HuobiUsdtMarginedSocketApiClientOptions(string baseAddress, string baseAddressAuthenticated, string baseAddressIncrementalOrderBook): base(baseAddress)
        {
            BaseAddressAuthenticated = baseAddressAuthenticated;
            BaseAddressInrementalOrderBook = baseAddressIncrementalOrderBook;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn"></param>
        /// <param name="newValues"></param>
        internal HuobiUsdtMarginedSocketApiClientOptions(HuobiUsdtMarginedSocketApiClientOptions baseOn, HuobiUsdtMarginedSocketApiClientOptions? newValues) : base(baseOn, newValues)
        {
            BaseAddressAuthenticated = newValues?.BaseAddressAuthenticated ?? baseOn.BaseAddressAuthenticated;
            BaseAddressInrementalOrderBook = newValues?.BaseAddressInrementalOrderBook ?? baseOn.BaseAddressInrementalOrderBook;
        }
    }

    /// <summary>
    /// Order book options
    /// </summary>
    public class HuobiUsdtMarginedOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// The way the entries are merged. 0 is no merge, 2 means to combine the entries on 2 decimal places
        /// </summary>
        public int? MergeStep { get; set; }

        /// <summary>
        /// The amount of entries to maintain. Either 5, 20 or 150. Level 5 and 20 are currently only supported for the following symbols: btcusdt, ethusdt, xrpusdt, eosusdt, ltcusdt, etcusdt, adausdt, dashusdt, bsvusdt.
        /// </summary>
        public int? Levels { get; set; }

        /// <summary>
        /// After how much time we should consider the connection dropped if no data is received for this time after the initial subscriptions
        /// </summary>
        public TimeSpan? InitialDataTimeout { get; set; }

        /// <summary>
        /// The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.
        /// </summary>
        public IHuobiSocketClient? SocketClient { get; set; }
    }
}
