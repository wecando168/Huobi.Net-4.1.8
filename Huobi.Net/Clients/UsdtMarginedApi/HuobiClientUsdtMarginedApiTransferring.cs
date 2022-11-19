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
using Huobi.Net.Interfaces.Clients.UsdtMargined;

namespace Huobi.Net.Clients.UsdtMargined
{
    /// <inheritdoc />
    public class HuobiClientUsdtMarginedApiTransferring : IHuobiClientUsdtMarginedApiTransferring
    {
        // U本位合约划转接口
        private const string GetLinearSwapCrossTransferStateEndpoint = "/swap_cross_transfer_state";                            // 【全仓】查询系统划转权限
        private const string LinearSwapAccountTransferEndpoint = "https://api.huobi.pro/v2/account/transfer";                   // 现货-U本位合约账户间进行资金的划转(PrivateData)

        private readonly HuobiClientUsdtMarginedApi _baseClient;

        internal HuobiClientUsdtMarginedApiTransferring(HuobiClientUsdtMarginedApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<object>>> GetLinearSwapCrossTransferState(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<IEnumerable<object>>(
                uri: _baseClient.GetUrl(GetLinearSwapCrossTransferStateEndpoint, ApiPath.LinearSwapApi, "1"),
                method: HttpMethod.Get,
                cancellationToken: ct,
                signed: true
                ).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<object>>> LinearSwapAccountTransfer(CancellationToken ct = default)
        {
            return await _baseClient.SendHuobiRequest<IEnumerable<object>>(
                uri: new Uri(LinearSwapAccountTransferEndpoint),
                method: HttpMethod.Post,
                cancellationToken: ct,
                signed: true
                ).ConfigureAwait(false);
        }
    }
}
