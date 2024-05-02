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
using Huobi.Net.Objects;
using Huobi.Net.Objects.Models;

namespace Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// Usdt Margined API endpoints
    /// U本位合约 API 端点
    /// </summary>
    public interface IHuobiClientUsdtMarginSwapApi : IDisposable
    {
        /// <summary>
        /// The factory for creating requests. Used for unit testing
        /// </summary>
        IRequestFactory RequestFactory { get; set; }

        /// <summary>
        /// Reference Data
        /// 基础信息接口
        /// </summary>
        IWWTHuobiClientUsdtMarginSwapReferenceData ReferenceData { get; }

        /// <summary>
        /// Market Data
        /// 市场行情接口
        /// </summary>
        IWWTHuobiClientUsdtMarginSwapMarketData MarketData { get; }

        /// <summary>
        /// Account
        /// 账户接口
        /// </summary>
        IHuobiClientUsdtMarginSwapAccount Account { get; }

        /// <summary>
        /// Trade
        /// </summary>
        IHuobiClientUsdtMarginSwapApiTrading Trading { get; }


        /// <summary>
        /// Strategy
        /// 策略接口
        /// </summary>
        IWWTHuobiClientUsdtMarginSwapStrategyOrder Strategy { get; }

        /// <summary>
        /// Transferring
        /// 划转接口
        /// </summary>
        IWWTHuobiClientUsdtMarginSwapTransferring Transferring { get; }
    }
}
