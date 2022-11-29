﻿using CryptoExchange.Net;
using Huobi.Net.Clients.SpotApi;
using Huobi.Net.Clients.UsdtMarginSwapApi;
using Huobi.Net.Interfaces.Clients;
using Huobi.Net.Interfaces.Clients.SpotApi;
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;
using Huobi.Net.Objects;

namespace Huobi.Net.Clients
{
    /// <inheritdoc cref="IHuobiSocketClient" />
    public class HuobiSocketClient : BaseSocketClient, IHuobiSocketClient
    {
        #region fields
        /// <inheritdoc />
        public IHuobiSocketClientSpotStreams SpotStreams { get; }

        /// <inheritdoc />
        public IHuobiSocketClientUsdtMarginSwapStreams UsdtMarginSwapStreams { get; }
        #endregion

        #region ctor
        /// <summary>
        /// Create a new instance of HuobiSocketClient with default options
        /// </summary>
        public HuobiSocketClient() : this(HuobiSocketClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of HuobiSocketClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public HuobiSocketClient(HuobiSocketClientOptions options) : base("Huobi", options)
        {
            SpotStreams = AddApiClient(new HuobiSocketClientSpotStreams(log, options));
            UsdtMarginSwapStreams = AddApiClient(new HuobiSocketClientUsdtMarginSwapStreams(log, options));
        }
        #endregion

        #region methods
        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(HuobiSocketClientOptions options)
        {
            HuobiSocketClientOptions.Default = options;
        }        
        #endregion

    }
}
