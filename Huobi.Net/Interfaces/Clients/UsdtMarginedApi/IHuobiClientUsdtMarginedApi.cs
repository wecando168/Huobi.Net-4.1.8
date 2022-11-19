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

namespace Huobi.Net.Interfaces.Clients.UsdtMargined
{
    /// <summary>
    /// Usdt Margined API endpoints
    /// U本位合约 API 端点
    /// </summary>
    public interface IHuobiClientUsdtMarginedApi : IDisposable
    {
        /// <summary>
        /// Reference Data
        /// 基础信息接口
        /// </summary>
        IHuobiClientUsdtMarginedApiReferenceData ReferenceData { get; }

        /// <summary>
        /// Market Data
        /// 市场行情接口
        /// </summary>
        IHuobiClientUsdtMarginedApiMarketData MarketData { get; }

        /// <summary>
        /// Account
        /// 账户接口
        /// </summary>
        IHuobiClientUsdtMarginedApiAccount Account { get; }

        /// <summary>
        /// Trade
        /// </summary>
        IHuobiClientUsdtMarginedApiTrade Trade { get; }


        /// <summary>
        /// Strategy
        /// 策略接口
        /// </summary>
        IHuobiClientUsdtMarginedApiStrategyOrder Strategy { get; }

        /// <summary>
        /// Transferring
        /// 划转接口
        /// </summary>
        IHuobiClientUsdtMarginedApiTransferring Transferring { get; }

        /// <summary>
        /// Get the IFuturesClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.
        /// 获取此客户端的 IFuturesClient。 这是一个通用接口，允许在不知道任何交换细节的情况下进行一些基本操作。
        /// </summary>
        /// <returns></returns>
        public IUsdtMarginedClient CommonUsdtMarginedClient { get; }
    }
}
