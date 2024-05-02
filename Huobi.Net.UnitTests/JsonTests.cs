using Huobi.Net;
using Huobi.Net.Interfaces;
using Huobi.Net.UnitTests.TestImplementations;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Objects;
using Huobi.Net.Interfaces.Clients;

namespace Huobi.Net.UnitTests
{
    [TestFixture]
    public class JsonTests
    {
        #region 现货相关测试
        //现货返回数据结构测试
        private JsonToObjectComparer<IHuobiClient> _comparer = new JsonToObjectComparer<IHuobiClient>((json) => TestHelpers.CreateResponseClient(json, new HuobiClientOptions()
        {
            ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "123"),
            SpotApiOptions = new CryptoExchange.Net.Objects.RestApiClientOptions
            {
                OutputOriginalData = true,
                RateLimiters = new List<IRateLimiter>()
            }
        }));

        [Test]
        public async Task ValidateSpotAccountCalls()
        {   
            await _comparer.ProcessSubject("DataResponses/Spot/Account", c => c.SpotApi.Account,
                useNestedObjectPropertyForCompare: new Dictionary<string, string> 
                {
                },
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {
                    { "GetBalancesAsync", "list" }                    
                },
                useNestedJsonPropertyForAllCompare: new List<string> { "data" }
                );
        }

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            await _comparer.ProcessSubject("DataResponses/Spot/Trading", c => c.SpotApi.Trading,
                useNestedObjectPropertyForCompare: new Dictionary<string, string>
                {
                },
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {
                },
                useNestedJsonPropertyForAllCompare: new List<string> { "data" },
                ignoreProperties: new Dictionary<string, List<string>> {
                    { "GetOpenOrdersAsync", new List<string> { "Type", "Side" } },
                    { "GetOrdersAsync", new List<string> { "Type", "Side" } },
                }
                );
        }

        [Test]
        public async Task ValidateSpotExchangeDataDataCalls()
        {
            await _comparer.ProcessSubject("DataResponses/Spot/ExchangeData", c => c.SpotApi.ExchangeData,
                useNestedObjectPropertyForCompare: new Dictionary<string, string>
                {
                    { "GetTickersAsync", "Ticks" },
                },
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {
                },
                useNestedJsonPropertyForAllCompare: new List<string> { "data" }
                );
        }

        [Test]
        public async Task ValidateSpotExchangeDataTickCalls()
        {
            await _comparer.ProcessSubject("TickResponses", c => c.SpotApi.ExchangeData,
                parametersToSetNull: new [] { "limit" },
                useNestedObjectPropertyForCompare: new Dictionary<string, string>
                {
                },
                useNestedJsonPropertyForAllCompare: new List<string> { "tick" }
                );
        }
        #endregion

        #region U本位合约相关测试
        //U本位合约返回数据结构测试
        private JsonToObjectComparer<IHuobiClient> _usdtMarginedComparer = new JsonToObjectComparer<IHuobiClient>((json) => TestHelpers.CreateResponseClient(json, new HuobiClientOptions()
        {
            ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "123"),
            UsdtMarginSwapApiOptions = new CryptoExchange.Net.Objects.RestApiClientOptions
            {
                OutputOriginalData = true,
                RateLimiters = new List<IRateLimiter>()
            }
        }));

        [Test]
        public async Task ValidateFuturesUsdtMarginedLinearSwapMarketDataCalls()
        {
            await _usdtMarginedComparer.ProcessSubject("DataResponses/Futures/UsdtMargined/LinearSwapmarketData", c => c.UsdtMarginSwapApi.ExchangeData,
                //text文件名与类名对应
                useNestedObjectPropertyForCompare: new Dictionary<string, string>
                {
                    { "GetLinearSwapExMarketHistoryKlineAsync", "HuobiUsdtMarginedMarketHistoryKline" },
                },
                useNestedJsonPropertyForCompare: new Dictionary<string, string>
                {
                },
                useNestedJsonPropertyForAllCompare: new List<string> { "data" }
                );
        }
        #endregion
    }
}
