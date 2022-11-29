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
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapTransferring;

namespace Huobi.Net.Interfaces.Clients.UsdtMarginSwapApi
{
    /// <summary>
    /// U本位合约划转接口
    /// </summary>
    public interface IHuobiClientUsdtMarginSwapTransferring
    {
        /// <summary>
        /// 
        /// 【全仓】查询系统划转权限(PrivateData)
        /// <para><a href="/linear-swap-api/v1/swap_cross_transfer_state"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#7063bffbff"/></para>
        /// </summary>
        /// <param name="marginAccount">保证金账户，不填则返回所有全仓数据	"USDT"，目前只有一个全仓账户（USDT）</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<HuobiUsdtMarginedSwapCrossTransferState>>> GetLinearSwapCrossTransferStateAsync(string marginAccount, CancellationToken ct = default);

        /// <summary>
        /// 
        /// 【通用】现货-U本位合约账户间进行资金的划转(PrivateData)
        /// <para><a href="https://api.huobi.pro/v2/account/transfer"/></para>
        /// <para><a href="https://huobiapi.github.io/docs/usdt_swap/v1/cn/#u-3"/></para>
        /// </summary>
        /// <param name="from">来源业务线账户，取值：spot(币币)、linear-swap(U本位合约)</param>
        /// <param name="to">目标业务线账户，取值：spot(币币)、linear-swap(U本位合约)</param>
        /// <param name="currency">币种,支持大小写</param>
        /// <param name="amount">划转金额</param>
        /// <param name="marginAccount">保证金账户</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<long>> LinearSwapAccountTransferAsync(string from , string to , string currency, decimal amount, string marginAccount, CancellationToken ct = default);
    }
}
