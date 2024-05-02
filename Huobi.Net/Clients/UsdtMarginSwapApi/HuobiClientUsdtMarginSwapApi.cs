﻿using System;
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
using Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi;
using Huobi.Net.Objects;
using Huobi.Net.Objects.Internal;
using Huobi.Net.Objects.Models;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapMarketData;
using Newtonsoft.Json.Linq;

namespace Huobi.Net.Clients.UsdtMarginSwapApi
{
    /// <inheritdoc />
    public class HuobiClientUsdtMarginSwapApi : RestApiClient, IHuobiClientUsdtMarginSwapApi
    {
        private readonly HuobiClientOptions _options;

        internal static TimeSyncState TimeSyncState = new TimeSyncState("Usdt Margin Swap Api");

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
        public IWWTHuobiClientUsdtMarginSwapReferenceData ReferenceData { get; }

        /// <inheritdoc />
        public IHuobiClientUsdtMarginSwapApiExchangeData ExchangeData { get; }

        /// <inheritdoc />
        public IHuobiClientUsdtMarginSwapAccount Account { get; }

        /// <inheritdoc />
        public IHuobiClientUsdtMarginSwapApiTrading Trading { get; }

        /// <inheritdoc />
        public IWWTHuobiClientUsdtMarginSwapStrategyOrder Strategy { get; }

        /// <inheritdoc />
        public IWWTHuobiClientUsdtMarginSwapTransferring Transferring { get; }


        #endregion

        #region constructor/destructor
        internal HuobiClientUsdtMarginSwapApi(Log log, HuobiClientOptions options)
            : base(log, options, options.UsdtMarginSwapApiOptions)
        {
            _options = options;
            _log = log;

            ReferenceData = new WWTHuobiClientUsdtMarginSwapApiReferenceData(this);
            ExchangeData = new HuobiClientUsdtMarginSwapApiExchangeData(this);
            Account = new HuobiClientUsdtMarginSwapApiAccount(this);
            Trading = new HuobiClientUsdtMarginSwapApiTrading(this);
            Strategy = new WWTHuobiClientUsdtMarginSwapApiStrategyOrder(this);
            Transferring = new WWTHuobiClientUsdtMarginSwapApiTransferring(this);

        manualParseError = true;
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new HuobiAuthenticationProvider(credentials, _options.SignPublicRequests);

        /// <summary>
        /// Construct url
        /// </summary>
        /// <param name="endpoint">接口</param>
        /// <param name="version"></param>
        /// <returns></returns>
        internal Uri GetUrl(string endpoint, string? version = null)
        {
            if (version == null)
                return new Uri(BaseAddress.AppendPath(endpoint));
            return new Uri(BaseAddress.AppendPath($"v{version}", endpoint));
        }

        /// <summary>
        /// Construct url
        /// </summary>
        /// <param name="endpoint">接口</param>
        /// <param name="apiPath">API路径</param>
        /// <param name="version"></param>
        /// <returns></returns>
        internal Uri GetUrl(string endpoint, WWTApiPath? apiPath, string? version = null)
        {
            var result = BaseAddress;

            if (!object.Equals(apiPath, null))
                result = BaseAddress.AppendPath(WWTApiPathExtensions.GetString((WWTApiPath)apiPath));

            if (version == null)
                result = result.AppendPath(endpoint);
            else
                result = result.AppendPath($"v{version}", endpoint);
            return new Uri(result);
        }

        internal async Task<WebCallResult<DateTime>> SendTimestampRequestAsync(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, int? weight = 1, bool ignoreRatelimit = false)
        {
            var result = await SendRequestAsync<HuobiBasicResponse<string>>(uri, method, cancellationToken, parameters, signed, requestWeight: weight ?? 1, ignoreRatelimit: ignoreRatelimit).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<DateTime>(result.Error!);

            return result.As(result.Data.Timestamp);
        }

        #region methods

        internal async Task<WebCallResult<DateTime>> SendHuobiTimeRequestAsync(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, int? weight = 1, bool ignoreRatelimit = false)
        {
            var result = await SendRequestAsync<HuobiBasicResponse<string>>(uri, method, cancellationToken, parameters, signed, requestWeight: weight ?? 1, ignoreRatelimit: ignoreRatelimit).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<DateTime>(result.Error!);

            return result.As(result.Data.Timestamp);
        }

        internal async Task<WebCallResult<T>> SendHuobiRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, int? weight = 1, bool ignoreRatelimit = false)
        {
            var result = await SendRequestAsync<HuobiBasicResponse<T>>(uri, method, cancellationToken, parameters, signed, requestWeight: weight ?? 1, ignoreRatelimit: ignoreRatelimit).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<T>(result.Error!);

            if (result.Data.ErrorCode != null)
                return result.AsError<T>(new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return result.As(result.Data.Data);
        }

        internal async Task<WebCallResult<T>> SendHuobiV2Request<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, int? weight = 1, bool ignoreRatelimit = false)
        {
            var result = await SendRequestAsync<HuobiBasicResponse<T>>(uri, method, cancellationToken, parameters, signed, requestWeight: weight ?? 1, ignoreRatelimit: ignoreRatelimit).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<T>(result.Error!);

            if (result.Data.ErrorCode != null)
                return result.AsError<T>(new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return result.As(result.Data.Data);
        }

        internal async Task<WebCallResult<T>> SendHuobiV3Request<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, Dictionary<string, object>? parameters = null, bool signed = false, int? weight = 1, bool ignoreRatelimit = false)
        {
            var result = await SendRequestAsync<HuobiBasicResponse<T>>(uri, method, cancellationToken, parameters, signed, requestWeight: weight ?? 1, ignoreRatelimit: ignoreRatelimit).ConfigureAwait(false);
            if (!result || result.Data == null)
                return result.AsError<T>(result.Error!);

            if (result.Data.ErrorCode != null)
                return result.AsError<T>(new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));

            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        protected override Task<ServerError?> TryParseErrorAsync(JToken data)
        {
            if (data["code"] != null && data["code"]?.Value<int>() != 200)
            {
                if (data["err-code"] != null)
                    return Task.FromResult<ServerError?>(new ServerError($"{(string)data["err-code"]!}, {(string)data["err-msg"]!}"));

                return Task.FromResult<ServerError?>(new ServerError($"{(string)data["code"]!}, {(string)data["message"]!}"));
            }

            if (data["err-code"] == null && data["err-msg"] == null)
                return Task.FromResult<ServerError?>(null);

            return Task.FromResult<ServerError?>(new ServerError($"{(string)data["err-code"]!}, {(string)data["err-msg"]!}"));
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(JToken error)
        {
            if (error["err-code"] == null || error["err-msg"] == null)
                return new ServerError(error.ToString());

            return new ServerError($"{(string)error["err-code"]!}, {(string)error["err-msg"]!}");
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

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ReferenceData.GetLinearSwapServerDateTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, _options.UsdtMarginSwapApiOptions.AutoTimestamp, _options.UsdtMarginSwapApiOptions.TimestampRecalculationInterval, TimeSyncState);

        /// <inheritdoc />
        public override TimeSpan GetTimeOffset()
            => TimeSyncState.TimeOffset;
    }
}
