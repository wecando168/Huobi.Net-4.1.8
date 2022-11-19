using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Huobi.Net.Objects.Models;
using CryptoExchange.Net.Converters;

namespace Huobi.Net.Interfaces.Clients.UsdtMargined
{
    /// <summary>
    /// U本位合约划转接口
    /// </summary>
    public interface IHuobiClientUsdtMarginedApiTransferring
    {
        /// <summary>
        /// 
        /// 【全仓】查询系统划转权限(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_transfer_state"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#7063bffbff"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> GetLinearSwapCrossTransferState(CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】现货-U本位合约账户间进行资金的划转(PrivateData)
        /// <para><a href="https://api.huobi.pro/v2/account/transfer"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#u-3"/></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<Object>>> LinearSwapAccountTransfer(CancellationToken ct = default);
    }
}
