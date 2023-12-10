using Huobi.Net.Clients;
using Huobi.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Huobi.Net.Enums;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json;
using Huobi.Net.Objects.Models.Socket;
using Huobi.Net.Interfaces;
using CryptoExchange.Net.Logging;
using Huobi.Net.Clients.SpotApi;
using Huobi.Net.Objects.Models;
using CryptoExchange.Net.CommonObjects;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapTrade.Request;
using System.Linq;
using System.Diagnostics;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapReferenceData;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapMarketData;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount.CommonBaseModels;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapTrade;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapStrategy;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapTransferring;
using Huobi.Net.Objects.Models.Socket.Futures.UsdtMargined.WebSocketOrderAndAccounts;
#region Provide you API key/secret in these fields to retrieve data related to your account
const string mainAccessKey = "bewr5drtmh-07ef7902-a9b54b77-001d1";
const string mainSecretKey = "3b835c77-e1c650c1-74057036-6ba29";
const string mainUserId = "291452314";
const string mainSportAccountId = "36724729";
const string mainUsdtMarginedAccountId = "53229551";

const string subAccessKey = "mn8ikls4qg-27357ab4-0f30c695-1a418";
const string subSecretKey = "edfda710-62c5bab3-8389e2ef-ad66d";
const string subUserId = "292046353";
const string subSportAccountId = "36845384";
const string subUsdtMarginedAccountId = "36845384";

const string testBaseCurrency = "btc";
const string testQuoteCurrency = "usdt";
const string testSymbol = "btcusdt";
const string testETPSymbol = "btc3susdt";
const string loanOrderId = "XXXXXXXX";
const string testOrderId = "1111111111111111111";
const string testClientOrderId = "XXXXXXXXXXXXXXXX";
string listenKey = string.Empty;
#endregion

//配置一个默认的Rest Api 客户端
HuobiClient.SetDefaultOptions(options: new HuobiClientOptions()
{
    //使用accessKey, secretKey生成一个新的API凭证
    ApiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey),
    //LogLevel = LogLevel.Debug
    LogLevel = LogLevel.Trace,
    //OutputOriginalData = true
});

//配置一个默认的webSocket Api 客户端
HuobiSocketClient.SetDefaultOptions(new HuobiSocketClientOptions()
{
    ApiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey),
    //LogLevel = LogLevel.Debug
    LogLevel = LogLevel.Trace,
    //OutputOriginalData = true
});

string? read = "";
while (read != "R" && read != "r" && read != "P" && read != "p" && read != "U" && read != "u")
{
    Console.WriteLine("Run [R]est or [P]ublicSocket  or [U]serSocket example?");
    read = Console.ReadLine();
}
if (read == "R" || read == "r")
{
    //一、交易所现货数据接口测试-已完成
    Console.WriteLine($"Press enter to test spot api exchange data endpoints, Press [S] to skip current test!");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        await TestSpotApiExchangeDataEndpoints();
    }
    //二、交易所现货账户接口测试-开发中...
    Console.WriteLine($"Press enter to test spot api account endpoints, Press [S] to skip current test!");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        await TestSpotApiAccountEndpoints();
    }
    //三、交易所现货交易接口测试-已完成
    Console.WriteLine($"Press enter to test spot api trading endpoints, Press [S] to skip current test!");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        await TestSpotApiTradingEndpoints();
    }
    //四、交易所U本位合约基础信息接口测试-已完成
    Console.WriteLine($"Press enter to test usdt margined api reference data endpoints, Press [S] to skip current test!");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        await TestUsdtMarginSwapApiReferenceDataEndpoints();
    }
    //五、交易所U本位合约市场行情数据接口测试-已完成
    Console.WriteLine($"Press enter to test usdt margined api market data endpoints, Press [S] to skip current test!");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        await TestUsdtMarginSwapApiMarketDataEndpoints();
    }
    //六、交易所U本位合约账户接口测试-已完成
    Console.WriteLine($"Press enter to test usdt margined api account endpoints, Press [S] to skip current test!");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        await TestUsdtMarginSwapApiAccountEndpoints();
    }    
    //七、交易所U本位合约交易接口测试-已完成
    Console.WriteLine($"Press enter to test usdt margined api Trade endpoints, Press [S] to skip current test!");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        await TestUsdtMarginSwapApiTradeEndpoints();
    }
    //八、交易所U本位合约策略订单接口测试-开发中...
    Console.WriteLine($"Press enter to test usdt margined api strategy order endpoints, Press [S] to skip current test!");
    read = Console.ReadLine();
    if (read != "S" && read != "s")                        
    {
        await TestUsdtMarginSwapApiStrategyOrderEndpoints();
    }
    //九、交易所U本位合约划转接口测试-已完成
    Console.WriteLine($"Press enter to test usdt margined api Trade endpoints, Press [S] to skip current test!");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        await TestUsdtMarginSwapApiTransferringEndpoints();
    }
}
else if (read == "P" || read == "p")
{
    //现货公有数据WebSocket客户端（无需签名的现货数据订阅）
    HuobiSocketClient? huobiPublicSocketClient = new();
    CallResult<UpdateSubscription>? publicSubscription = null;
    //U本位合约公有数据WebSocket客户端（无需签名的U本位合约数据订阅）
    HuobiSocketClient? huobiPublicUsdtMarginedSocketClient = new();
    CallResult<UpdateSubscription>? publicUsdtMarginedSubscription = null;
    #region 现货订阅 全部交易代码市场聚合行情数据
    //订阅全部交易代码市场聚合行情数据
    Console.WriteLine($"Press enter to subscribe spot symbols ticker stream, Press [S] to skip current subscription");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        Console.WriteLine($"现货Websocket主题订阅：获取全部交易代码市场聚合行情数据");
        publicSubscription = await huobiPublicSocketClient.SpotStreams.SubscribeToTickerUpdatesAsync(data =>
        {
            if (!object.Equals(data, null))
            {
                //Console.WriteLine($"{JsonConvert.SerializeObject(data)}");
                foreach (var item in data.Data.Ticks)
                {
                    Console.WriteLine($"现货交易代码：{item.Symbol} 开盘价格：{item.OpenPrice} 收盘价格：{item.ClosePrice} 最高价格：{item.HighPrice} 最低价格：{item.LowPrice}");
                }
            }
            else
            {
                Console.WriteLine($"订阅火币公有数据异常：未正常接收到数据");
            }
        });
        if (!publicSubscription.Success)
        {
            Console.WriteLine("Failed to sub" + publicSubscription.Error);
            Console.ReadLine();
            return;
        }
        publicSubscription.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
        publicSubscription.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    }
    #endregion
    #region 现货订阅 订阅K线
    //订阅一个交易代码，单时间区间
    Console.WriteLine($"Press enter to subscribe spot one symbol Kline stream, Press [S] to skip current subscription");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        string symbol = "BTCUSDT";
        KlineInterval klineInterval = KlineInterval.FiveMinutes;
        publicSubscription = await huobiPublicSocketClient.SpotStreams.SubscribeToKlineUpdatesAsync(
            symbol,
            klineInterval, data =>
        {
            if (!object.Equals(data, null))
            {
                Console.WriteLine($"现货交易代码：{symbol} 开盘价格：{data.Data.OpenPrice} 收盘价格：{data.Data.ClosePrice} 最高价格：{data.Data.HighPrice} 最低价格：{data.Data.LowPrice}");
            }
            else
            {
                Console.WriteLine($"订阅火币公有数据异常：未正常接收到数据");
            }
        });
        if (!publicSubscription.Success)
        {
            Console.WriteLine("Failed to sub" + publicSubscription.Error);
            Console.ReadLine();
            return;
        }
        publicSubscription.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
        publicSubscription.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    }
    #endregion
    #region U本位合约订阅 WebSocket 获取K线
    //获取一个交易代码，指定时间区间
    Console.WriteLine($"Press enter to get usdt margined contract code klines, Press [S] to skip current subscription");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        string contractCode = "BTC-USDT";
        KlineInterval klineInterval = KlineInterval.FiveMinutes;
        string clientId = $"火币U本位合约{contractCode} 获取K线数据";
        long from = 1667232000; //2022-11-01 00:00:00
        long to = 1667260800;   //2022-11-01 08:00:00
        Console.WriteLine($"U本位合约Websocket数据获取：获取U本位合约{contractCode} {klineInterval} K线");
        CallResult<IEnumerable<HuobiContractCodeKlineTick>>? huobiContractCodeTickList = await huobiPublicUsdtMarginedSocketClient.UsdtMarginSwapStreams.GetMarketContractCodeKlineAsync(
            contractCode: contractCode,
            period: klineInterval,
            clientId: clientId,
            from: from,
            to:to
            );
        if (!object.Equals(huobiContractCodeTickList, null))
        {
            foreach (var item in huobiContractCodeTickList.Data)
            {
                if (item != null)
                {
                    Console.WriteLine($"K线编号：{item.Id} 开盘时间：{DateTimeConverter.ConvertFromMilliseconds(double.Parse(item.Id.ToString()))} 订单编号：{item.MarginedId}");
                    Console.WriteLine($"开盘价：{item.OpenPrice} 收盘价：{item.ClosePrice} 最低价：{item.LowPrice}  最高价：{item.HighPrice}");
                    Console.WriteLine($"成交量(币)：{item.Amount} 成交额：{item.TradeTurnover} 成交笔数：{item.Count} 成交张数：{item.Vol}");
                }
            }
        }
        else
        {
            Console.WriteLine($"订阅火币U本位合约公有数据异常：未正常接收到数据");
        }
    }
    #endregion
    #region U本位合约订阅 WebSocket 订阅K线
    //订阅一个交易代码，单时间区间
    Console.WriteLine($"Press enter to subscribe usdt margined contract code kline stream, Press [S] to skip current subscription");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        string contractCode = "BTC-USDT";
        KlineInterval klineInterval = KlineInterval.FiveMinutes;
        string clientId = $"火币U本位合约{contractCode}订阅 K线数据";
        Console.WriteLine($"U本位合约Websocket主题订阅：订阅U本位合约{contractCode} {klineInterval} K线");
        publicUsdtMarginedSubscription = await huobiPublicUsdtMarginedSocketClient.UsdtMarginSwapStreams.SubscribeMarketContractCodeKlineAsync(
            contractCode,
            klineInterval,
            clientId, data =>
        {
            if (!object.Equals(data, null))
            {
                HuobiContractCodeKlineTick huobiContractCodeTick = data.Data;
                if (huobiContractCodeTick != null)
                {
                    Console.WriteLine($"合约代码：{data.Topic} K线编号：{huobiContractCodeTick.Id} 开盘时间：{DateTimeConverter.ConvertFromMilliseconds(double.Parse(huobiContractCodeTick.Id.ToString()))} 订单编号：{huobiContractCodeTick.MarginedId}");
                    Console.WriteLine($"开盘价：{huobiContractCodeTick.OpenPrice} 收盘价：{huobiContractCodeTick.ClosePrice} 最低价：{huobiContractCodeTick.LowPrice}  最高价：{huobiContractCodeTick.HighPrice}");
                    Console.WriteLine($"成交量(币)：{huobiContractCodeTick.Amount} 成交额：{huobiContractCodeTick.TradeTurnover} 成交笔数：{huobiContractCodeTick.Count} 成交张数：{huobiContractCodeTick.Vol}");
                }
            }
            else
            {
                Console.WriteLine($"订阅火币U本位合约公有数据异常：未正常接收到数据");
            }
        });
        if (!publicUsdtMarginedSubscription.Success)
        {
            Console.WriteLine("Failed to sub" + publicUsdtMarginedSubscription.Error);
            Console.ReadLine();
            return;
        }
        publicUsdtMarginedSubscription.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
        publicUsdtMarginedSubscription.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    }
    #endregion
    #region U本位合约订阅 WebSocket 订阅 Market Depth 数据
    //订阅一个交易代码，单时间区间
    Console.WriteLine($"Press enter to subscribe usdt margined contract code market depth stream, Press [S] to skip current subscription");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        string contractCode = "BTC-USDT";
        string type = "step5";
        string clientId = $"火币U本位合约{contractCode}订阅 Market Depth 数据";
        Console.WriteLine($"U本位合约Websocket主题订阅：订阅U本位合约{contractCode} {type} Market Depth");
        publicUsdtMarginedSubscription = await huobiPublicUsdtMarginedSocketClient.UsdtMarginSwapStreams.SubscribeMarketContractCodeDepthAsync(
            contractCode,
            type,
            clientId, data =>
            {
                if (!object.Equals(data, null))
                {
                    Console.WriteLine($"合约代码：{data.Topic} 生成编号：{data.Data.Id} 生成时间：{DateTimeConverter.ConvertFromMilliseconds(double.Parse(data.Data.Id.ToString()))} 订单编号：{data.Data.MarginedId}");
                    foreach(var item in data.Data.Bids)
                    {
                        Console.WriteLine($"卖盘：挂单价 {item.Price} 挂单张数 {item.Quantity}");
                    }
                    foreach (var item in data.Data.Asks)
                    {
                        Console.WriteLine($"买盘：挂单价 {item.Price} 挂单张数 {item.Quantity}");
                    }
                }
                else
                {
                    Console.WriteLine($"订阅火币U本位合约公有数据异常：未正常接收到数据");
                }
            });
        if (!publicUsdtMarginedSubscription.Success)
        {
            Console.WriteLine("Failed to sub" + publicUsdtMarginedSubscription.Error);
            Console.ReadLine();
            return;
        }
        publicUsdtMarginedSubscription.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
        publicUsdtMarginedSubscription.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    }
    #endregion
}
else if (read == "U" || read == "u")
{
    //现货私有数据WebSocket客户端（需要签名的现货数据订阅）
    HuobiSocketClient? huobiPrivateSpotSocketClient = new();
    CallResult<UpdateSubscription>? priavteSub = null;
    //U本位合约私有数据WebSocket客户端（需要签名的合约数据订阅）
    HuobiSocketClient? huobiPrivateUsdtMarginedSocketClient = new();
    CallResult<UpdateSubscription>? privateSub = null;
    #region 对现货、合约WebSocket客户端的新实例使用新的设置(这里不设置则使用之前的默认设置）
    //使用accessKey, secretKey生成一个新的API凭证
    ApiCredentials apiCredentials = new (subAccessKey, subSecretKey);
    //当前客户端使用新生成的API凭证
    huobiPrivateSpotSocketClient.SetApiCredentials(apiCredentials);
    huobiPrivateUsdtMarginedSocketClient.SetApiCredentials(apiCredentials);
    #endregion

    #region 现货订阅 账户资产变更（实时）
    Console.WriteLine($"Press enter to subscribe account deals stream, Press [S] to skip current subscription");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        //订阅账户变更
        priavteSub = await huobiPrivateSpotSocketClient.SpotStreams.SubscribeToAccountUpdatesAsync(OnHuobiAccountUpdate, 1);
        /// <summary>
        /// 火币订阅账户变更：当账户更新时–HuobiAccountUpdate
        /// </summary>
        /// <param name="data"></param>
        void OnHuobiAccountUpdate(DataEvent<HuobiAccountUpdate> data)
        {
            if (!object.Equals(data, null))
            {
                Console.WriteLine($"账户编号：{data.Data.AccountId} 账户类型：{data.Data.AccountType} 币种名称：{data.Data.Asset} 数量：{data.Data.Balance} 更新时间：{data.Data.ChangeTime}");
            }
            else
            {
                Console.WriteLine($"订阅火币公有数据异常：未正常接收到数据");
            }
        }
        if (!priavteSub.Success)
        {
            Console.WriteLine("Failed to sub" + priavteSub.Error);
            Console.WriteLine($"Please check if \"AccessKey\", \"SecretKey\" and \"WebSocket Api Server Host\" are valid");
            Console.ReadLine();
            return;
        }
        priavteSub.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
        priavteSub.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    }
    #endregion

    #region 现货订阅 火币订阅清算后成交及撤单更新（实时）
    Console.WriteLine($"Press enter to subscribe order details updates stream, Press [S] to skip current subscription");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        //订阅火币订阅清算后成交及撤单更新
        priavteSub = await huobiPrivateSpotSocketClient.SpotStreams.SubscribeToOrderDetailsUpdatesAsync(null, OnHuobiTradeUpdate, null);
        /// <summary>
        /// 火币订阅清算后成交及撤单更新：当订单成交后–OnHuobiTradeUpdate
        /// （策略补单功能在这里实现）
        /// </summary>
        /// <param name="data"></param>
        void OnHuobiTradeUpdate(DataEvent<Huobi.Net.Objects.Models.Socket.HuobiTradeUpdate> data)
        {
            DataEvent<HuobiTradeUpdate> response = data;
            HuobiTradeUpdate huobiTradeUpdate = data.Data;
            if (!object.Equals(data, null))
            {
                Console.WriteLine($"账户编号：{huobiTradeUpdate.AccountId} 订单编号：{huobiTradeUpdate.OrderId} 用户自定义单号：{huobiTradeUpdate.ClientOrderId}\r\n" +
                    $"订单价格：{huobiTradeUpdate.OrderPrice} 订单数量：{huobiTradeUpdate.OrderQuantity} 订单状态：{huobiTradeUpdate.OrderStatus}\r\n" +
                    $"成交价格：{huobiTradeUpdate.Price} 成交数量：{huobiTradeUpdate.Quantity} 成交金额：{huobiTradeUpdate.Price * huobiTradeUpdate.Quantity}");
            }
            else
            {
                Console.WriteLine($"订阅火币公有数据异常：未正常接收到数据");
            }
        }
        if (!priavteSub.Success)
        {
            Console.WriteLine("Failed to sub" + priavteSub.Error);
            Console.WriteLine($"Please check if \"AccessKey\", \"SecretKey\" and \"WebSocket Api Server Host\" are valid");
            Console.ReadLine();
            return;
        }
        priavteSub.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
        priavteSub.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    }
    #endregion

    #region U本位合约订阅 WebSocket 订阅【逐仓】订单成交数据
    //订阅【逐仓】订单成交数据
    Console.WriteLine($"Press enter to subscribe usdt margined isolated order data stream, Press [S] to skip current subscription");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        #region 对合约WebSocket客户端的新实例使用新的设置(主账号）
        //使用accessKey, secretKey生成一个新的API凭证
        apiCredentials = new(mainAccessKey, mainSecretKey);
        //当前客户端使用新生成的API凭证
        huobiPrivateUsdtMarginedSocketClient.SetApiCredentials(apiCredentials);
        #endregion
        string contractCode = "*";
        string clientId = $"火币U本位合约【逐仓】订阅{contractCode}订单成交数据";
        Console.WriteLine($"U本位合约Websocket主题订阅：【逐仓】订阅{contractCode}订单成交数据");
        privateSub = await huobiPrivateUsdtMarginedSocketClient.UsdtMarginSwapStreams.SubscribeOrderContractCodeAsync(contractCode, clientId, OnIsolatedOrderDataUpdate);
        
        void OnIsolatedOrderDataUpdate(DataEvent<HuobiUsdtMarginedMarketSubscribeOrderData> data)
        {
            if (!object.Equals(data, null))
            {
                Console.WriteLine($"用户编号：{data.Data.Uid} 合约代码：{data.Topic} 生成时间：{DateTimeConverter.ConvertFromMilliseconds(double.Parse(data.Data.Timestamp.ToString()))} 订单编号：{data.Data.OrderId}");
                foreach (HuobiUsdtMarginedWSTrade item in data.Data.HuobiUsdtMarginedIsolatedWSTrades)
                {
                    try
                    {
                        Console.WriteLine($"{JsonConvert.DeserializeObject(item.ToString())}");
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"{exception}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"订阅火币U本位合约私有数据异常：未正常接收到数据");
            }
        }
        if (!privateSub.Success)
        {
            Console.WriteLine("Failed to sub" + privateSub.Error);
            Console.ReadLine();
            return;
        }
        privateSub.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
        privateSub.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    }
    #endregion

    #region U本位合约订阅 WebSocket 订阅【全仓】订单成交数据
    //订阅【全仓】订单成交数据
    Console.WriteLine($"Press enter to subscribe usdt margined cross order data stream, Press [S] to skip current subscription");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        string contractCode = "*";
        string clientId = $"火币U本位合约【全仓】订阅{contractCode}订单成交数据";
        Console.WriteLine($"U本位合约Websocket主题订阅：【全仓】订阅{contractCode}订单成交数据");
        privateSub = await huobiPrivateUsdtMarginedSocketClient.UsdtMarginSwapStreams.SubscribeOrderCrossContractCodeAsync(contractCode, clientId, OnCrossOrderDataUpdate);
        
        void OnCrossOrderDataUpdate(DataEvent<HuobiUsdtMarginedMarketSubscribeCrossOrderData> data)
        {
            if (!object.Equals(data , null) && !object.Equals(data.Data, null))
            {
                Console.WriteLine($"用户编号：{data.Data.Uid} 合约代码：{data.Data.Topic} 生成时间：{DateTimeConverter.ConvertFromMilliseconds(double.Parse(data.Data.Timestamp.ToString()))} 订单编号：{data.Data.OrderId}");
                foreach (var item in data.Data.HuobiUsdtMarginedCrossWSTrades)
                {
                    try
                    {
                        Console.WriteLine($"{JsonConvert.DeserializeObject(item.ToString())}");
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"{exception}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"订阅火币U本位合约私有数据异常：未正常接收到数据");
            }
        }
        if (!privateSub.Success)
        {
            Console.WriteLine("Failed to sub" + privateSub.Error);
            Console.ReadLine();
            return;
        }
        privateSub.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
        privateSub.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    }
    #endregion

    #region U本位合约订阅 WebSocket 订阅【逐仓】资产变动
    //订阅【逐仓】资产变动
    Console.WriteLine($"Press enter to subscribe usdt margined isolated account equity updates stream, Press [S] to skip current subscription");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        #region 对合约WebSocket客户端的新实例使用新的设置(主账号）
        //使用accessKey, secretKey生成一个新的API凭证
        apiCredentials = new(mainAccessKey, mainSecretKey);
        //当前客户端使用新生成的API凭证
        huobiPrivateUsdtMarginedSocketClient.SetApiCredentials(apiCredentials);
        #endregion
        string contractCode = "DOGE-USDT";
        string clientId = $"火币U本位合约【逐仓】订阅{contractCode}资产变动";
        Console.WriteLine($"U本位合约Websocket主题订阅：【逐仓】订阅{contractCode}资产变动");
        privateSub = await huobiPrivateUsdtMarginedSocketClient.UsdtMarginSwapStreams.SubscribeAccountsContractCodeAsync(contractCode, clientId, OnIsolatedAccountUpdate);
        
        void OnIsolatedAccountUpdate(DataEvent<IEnumerable<HuobiUsdtMarginedAccountSebscribePositionInfo>> data)
        {
            if (!object.Equals(data, null))
            {
                foreach (var item in data.Data)
                {
                    Console.WriteLine($"保证金账户：{item.MarginAccount} 保证金模式：{item.MarginMode} 持仓模式：{item.PositionMode} \r\n" +
                        $"合约代码：{item.ContractCode} 账户权益：{item.MarginBalance} 可用保证金：{item.MarginAvailable} 冻结保证金：{item.MarginFrozen}");
                }
            }
            else
            {
                Console.WriteLine($"订阅火币U本位合约私有数据异常：未正常接收到数据");
            }
        }
        if (!privateSub.Success)
        {
            Console.WriteLine("Failed to sub" + privateSub.Error);
            Console.ReadLine();
            return;
        }
        privateSub.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
        privateSub.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    }
    #endregion

    #region U本位合约订阅 WebSocket 订阅【全仓】资产变动
    //订阅【全仓】资产变动
    Console.WriteLine($"Press enter to subscribe usdt margined cross account equity updates stream, Press [S] to skip current subscription");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        string marginAccount = "USDT";
        string clientId = $"火币U本位合约【全仓】订阅{marginAccount}资产变动";
        Console.WriteLine($"U本位合约Websocket主题订阅：【全仓】订阅{marginAccount}资产变动");
        privateSub = await huobiPrivateUsdtMarginedSocketClient.UsdtMarginSwapStreams.SubscribeAccountsCrossContractCodeAsync(marginAccount, clientId, OnCrossAccountUpdate);
        
        void OnCrossAccountUpdate(DataEvent<IEnumerable<HuobiUsdtMarginedAccountSebscribeCrossPositionInfo>> data)
        {
            if (!object.Equals(data, null))
            {
                foreach (var item in data.Data)
                {
                    Console.WriteLine($"保证金账户：{item.MarginAccount} 保证金模式：{item.MarginMode} 持仓模式：{item.PositionMode}");
                    foreach (var contractDetail in item.ContractDetails)
                    {
                        Console.WriteLine($"合约代码：{contractDetail.ContractCode} 持仓保证金：{contractDetail.MarginPosition} 可用保证金：{contractDetail.MarginAvailable} 冻结保证金：{contractDetail.MarginFrozen}");
                    }
                    foreach (var futuresContractDetail in item.FuturesContractDetails)
                    {
                        Console.WriteLine($"合约代码：{futuresContractDetail.ContractCode} 持仓保证金：{futuresContractDetail.MarginPosition} 可用保证金：{futuresContractDetail.MarginAvailable} 冻结保证金：{futuresContractDetail.MarginFrozen}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"订阅火币U本位合约私有数据异常：未正常接收到数据");
            }
        }

        if (!privateSub.Success)
        {
            Console.WriteLine("Failed to sub" + privateSub.Error);
            Console.ReadLine();
            return;
        }
        privateSub.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
        privateSub.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    }
    #endregion

    //#region U本位合约订阅 WebSocket 订阅【逐仓】订单撮合数据
    ////订阅【逐仓】订单撮合数据
    //Console.WriteLine($"Press enter to subscribe usdt margined isolated account equity updates stream, Press [S] to skip current subscription");
    //read = Console.ReadLine();
    //if (read != "S" && read != "s")
    //{
    //    #region 对合约WebSocket客户端的新实例使用新的设置(主账号）
    //    //使用accessKey, secretKey生成一个新的API凭证
    //    apiCredentials = new(mainAccessKey, mainSecretKey);
    //    //当前客户端使用新生成的API凭证
    //    huobiPrivateUsdtMarginedSocketClient.SetApiCredentials(apiCredentials);
    //    #endregion
    //    string contractCode = "DOGE-USDT";
    //    string clientId = $"火币U本位合约【逐仓】订阅{contractCode}订单撮合数据";
    //    Console.WriteLine($"U本位合约Websocket主题订阅：【逐仓】订阅{contractCode}订单撮合数据");
    //    privateUsdtMarginedSubscription = await huobiPrivateUsdtMarginedSocketClient.UsdtMarginSwapStreams.SubscribeMatchOrdersContractCodeAsync(
    //        contractCode,
    //        clientId, data =>
    //        {
    //            if (!object.Equals(data, null))
    //            {
    //                Console.WriteLine($"保证金账户：{data.Data.MarginAccount} 合约代码：{data.Data.ContractCode} 订单编号：{data.Data.OrderId} 客户订单编号：{data.Data.OrderId} \r\n" +
    //                    $"订单总委托数量：{data.Data.Volume} 委托价格：{data.Data.Price} 订单已成交数量：{data.Data.TradeVolume} 买卖方向：{data.Data.Direction} 开平方向：{data.Data.Offset} 杠杆倍数：{data.Data.LeverRate}");
    //                foreach (var item in data.Data.WSIsolatedTradeInfos)
    //                {
    //                    Console.WriteLine($"交易标识：{item.Id} 撮合编号：{item.TradeId} 成交价格：{item.TradePrice} 成交量（张）：{item.TradeVolume} 成交金额：{item.TradeTurnover}");
    //                }
    //            }
    //            else
    //            {
    //                Console.WriteLine($"订阅火币U本位合约私有数据异常：未正常接收到数据");
    //            }
    //        });
    //    if (!privateUsdtMarginedSubscription.Success)
    //    {
    //        Console.WriteLine("Failed to sub" + privateUsdtMarginedSubscription.Error);
    //        Console.ReadLine();
    //        return;
    //    }
    //    privateUsdtMarginedSubscription.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
    //    privateUsdtMarginedSubscription.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    //}
    //#endregion

    //#region U本位合约订阅 WebSocket 订阅【全仓】订单撮合数据
    ////订阅【全仓】订单撮合数据
    //Console.WriteLine($"Press enter to subscribe usdt margined cross account equity updates stream, Press [S] to skip current subscription");
    //read = Console.ReadLine();
    //if (read != "S" && read != "s")
    //{
    //    string contractCode = "DOGE-USDT";
    //    string clientId = $"火币U本位合约【全仓】订阅{contractCode}订单撮合数据";
    //    Console.WriteLine($"U本位合约Websocket主题订阅：【全仓】订阅{contractCode}订单撮合数据");
    //    privateUsdtMarginedSubscription = await huobiPrivateUsdtMarginedSocketClient.UsdtMarginSwapStreams.SubscribeMatchOrdersCrossContractCodeAsync(
    //        contractCode,
    //        clientId, data =>
    //        {
    //            if (!object.Equals(data, null))
    //            {
    //                Console.WriteLine($"保证金账户：{data.Data.MarginAccount} 合约代码：{data.Data.ContractCode} 订单编号：{data.Data.OrderId} 客户订单编号：{data.Data.OrderId} \r\n" +
    //                    $"订单总委托数量：{data.Data.Volume} 委托价格：{data.Data.Price} 订单已成交数量：{data.Data.TradeVolume} 买卖方向：{data.Data.Direction} 开平方向：{data.Data.Offset} 杠杆倍数：{data.Data.LeverRate}");
    //                foreach (var item in data.Data.WSCrossTradeInfos)
    //                {
    //                    Console.WriteLine($"交易标识：{item.Id} 撮合编号：{item.TradeId} 成交价格：{item.TradePrice} 成交量（张）：{item.TradeVolume} 成交金额：{item.TradeTurnover}");
    //                }
    //            }
    //            else
    //            {
    //                Console.WriteLine($"订阅火币U本位合约私有数据异常：未正常接收到数据");
    //            }
    //        });
    //    if (!privateUsdtMarginedSubscription.Success)
    //    {
    //        Console.WriteLine("Failed to sub" + privateUsdtMarginedSubscription.Error);
    //        Console.ReadLine();
    //        return;
    //    }
    //    privateUsdtMarginedSubscription.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
    //    privateUsdtMarginedSubscription.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    //}
    //#endregion
}
static void ErrorInfoOutput<T>(WebCallResult<T> webCallResult, string serverName, string taskName)
{
    Console.WriteLine($"{serverName}：{taskName}异常\r\n" +
    $"错误信息：{(webCallResult.Error == null ? "null" : webCallResult.Error)}\r\n" +
    $"错误代码：{(webCallResult.Error == null ? "null" : webCallResult.Error.Code)}\r\n" +
    $"错误提示：{(webCallResult.Error == null ? "null" : webCallResult.Error.Data)}");
    string? read = string.Empty;
    Console.WriteLine($"Return an error, press Enter to ignore the error and continue to run, Press [S] to stop test!");
    read = Console.ReadLine();
    if (read == "S" || read == "s")
    {
        Process.GetCurrentProcess().Kill();
    }
}
//交易所现货数据接口测试-已完成
static async Task TestSpotApiExchangeDataEndpoints()
{
    using (var huobiSpotRestClient = new HuobiClient())
    {
        #region 获取当前市场最新状态
        {
            await HandleRequest("Market Status", () => huobiSpotRestClient.SpotApi.ExchangeData.GetMarketStatusAsync(),
                result => $"{result.Status}"
                );
            //Console.WriteLine("获取当前市场最新状态");
            //var result = await huobiSpotRestClient.SpotApi.ExchangeData.GetMarketStatusAsync();
            //if (result.Success)
            //{
            //    if (result.Data.Status == MarketStatus.Normal)
            //    {
            //        Console.WriteLine($"现货市场最新状态:正常");
            //    }
            //    else
            //    {
            //        Console.WriteLine(
            //            $"现货市场暂停原因:{result.Data.HaltReason}\r\n" +
            //            $"现货市场暂停开始时间:{result.Data.HaltStartTime}\r\n" +
            //            $"现货市场暂停恢复时间:{result.Data.HaltEndTime}"
            //            );
            //    }
            //}
            //else
            //{
            //    ErrorInfoOutput<HuobiMarketStatus>(result, "火币现货服务器", "获取服务器当前时间");
            //}
        }
        #endregion
        #region 获取服务器当前时间
        {
            Console.WriteLine("获取服务器当前时间");
            var result = await huobiSpotRestClient.SpotApi.ExchangeData.GetServerTimeAsync();
            if (result.Success)
            {
                Console.WriteLine(
                    $"现货市场服务器当前时间:{result.Data}\r\n" +
                    $"本地时间:{result.Data.ToLocalTime()}\r\n" +
                    $"时差:{(result.Data.ToLocalTime() - result.Data).TotalHours}小时"
                    );
            }
            else
            {
                ErrorInfoOutput<DateTime>(result, "火币现货服务器", "获取服务器当前时间");
            }
        }
        #endregion
        #region 获取币链参考信息
        {
            Console.WriteLine("获取币链参考信息");
            var result = await huobiSpotRestClient.SpotApi.ExchangeData.GetAssetDetailsAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiAssetInfo>>(result, "火币现货服务器", "获取币链参考信息");
            }
        }
        #endregion
        #region 获取现货币种信息
        {
            Console.WriteLine("获取现货币种信息");
            var result = await huobiSpotRestClient.SpotApi.ExchangeData.GetAssetsAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<string>>(result, "火币现货服务器", "获取现货币种信息");
            }
        }
        #endregion
        #region 获取现货K线数据
        {
            Console.WriteLine("获取现货K线数据");
            var result = await huobiSpotRestClient.SpotApi.ExchangeData.GetKlinesAsync(
                symbol: testSymbol,
                period: KlineInterval.OneMinute,
                limit: 500
                );
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiKline>>(result, "火币现货服务器", "获取现货K线数据");
            }
        }
        #endregion
        #region 获取现货交易代码
        {
            Console.WriteLine("获取现货交易代码");
            var result = await huobiSpotRestClient.SpotApi.ExchangeData.GetSymbolsAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiSymbol>>(result, "火币现货服务器", "获取现货交易代码");
            }
        }
        #endregion
        #region 获取所有现货交易代码最新行情
        {
            Console.WriteLine("获取所有现货交易代码最新行情");
            var result = await huobiSpotRestClient.SpotApi.ExchangeData.GetTickersAsync();
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<HuobiSymbolTicks>(result, "火币现货服务器", "获取所有现货交易代码最新行情");
            }
        }
        #endregion
        #region 获取指定现货杠杆代码ETP实时净值
        {
            Console.WriteLine("获取指定现货杠杆代码ETP实时净值");
            var result = await huobiSpotRestClient.SpotApi.ExchangeData.GetNavAsync(symbol: testETPSymbol);
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<HuobiNav>(result, "火币现货服务器", "获取指定现货杠杆代码ETP实时净值");
            }
        }
        #endregion
        #region 获取指定现货交易代码市场行情深度
        {
            Console.WriteLine("获取指定现货交易代码市场行情深度");
            var result = await huobiSpotRestClient.SpotApi.ExchangeData.GetOrderBookAsync(
                symbol: testSymbol,
                mergeStep: 5
                );
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<HuobiOrderBook>(result, "火币现货服务器", "获取指定现货交易代码市场行情深度");
            }
        }
        #endregion
        #region 获取指定现货交易代码24小时行情汇总
        {
            Console.WriteLine("获取指定现货交易代码24小时行情汇总");
            var result = await huobiSpotRestClient.SpotApi.ExchangeData.GetSymbolDetails24HAsync(
                symbol: testSymbol
                );
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<HuobiSymbolDetails>(result, "火币现货服务器", "获取指定现货交易代码24小时行情汇总");
            }
        }
        #endregion
        #region 获取指定现货交易代码24小时行情汇总以及最佳买入卖出价格
        {
            Console.WriteLine("获取指定现货交易代码24小时行情汇总以及最佳买入卖出价格");
            var result = await huobiSpotRestClient.SpotApi.ExchangeData.GetTickerAsync(
                 symbol: testSymbol
                );
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<HuobiSymbolTickMerged>(result, "火币现货服务器", "获取指定现货交易代码24小时行情汇总以及最佳买入卖出价格");
            }
        }
        #endregion
        #region 获取指定现货交易代码最近成交记录
        {
            Console.WriteLine("获取指定现货交易代码最近成交记录");
            var result = await huobiSpotRestClient.SpotApi.ExchangeData.GetLastTradeAsync(symbol: testSymbol);
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<HuobiSymbolTrade>(result, "火币现货服务器", "获取指定现货交易代码最近成交记录");
            }
        }
        #endregion
        #region 获取指定现货交易代码近期的所有交易记录
        {
            Console.WriteLine("获取指定现货交易代码近期的所有交易记录");
            var result = await huobiSpotRestClient.SpotApi.ExchangeData.GetTradeHistoryAsync(
                symbol: testSymbol,
                limit: 50
                );
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiSymbolTrade>>(result, "火币现货服务器", "获取指定现货交易代码近期的所有交易记录");
            }            
        }
        #endregion
    }
}
//交易所现货账户接口测试-已完成
static async Task TestSpotApiAccountEndpoints()
{
    decimal loanAmount = 0.0M;
    using (var huobiSpotRestClient = new HuobiClient())
    {
        #region 对HuobiClient客户端的新实例使用新的设置(这里不设置则使用之前的默认设置）
        //使用accessKey, secretKey生成一个新的API凭证
        ApiCredentials apiCredentials = new (mainAccessKey, mainSecretKey);
        //当前客户端使用新生成的API凭证
        huobiSpotRestClient.SetApiCredentials(apiCredentials);
        #endregion
        #region 查询母子用户API Key信息
        {
            {
                Console.WriteLine("查询母用户API Key信息");
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiSpotRestClient.SetApiCredentials(apiCredentials);
                var result = await huobiSpotRestClient.SpotApi.Account.APIKeyQueryAsync(long.Parse(mainUserId));
                if (result.Success)
                {
                    foreach (var item in result.Data)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                    }
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiAPIKeyQuery>>(result, "火币现货服务器", "查询母用户API Key信息");
                }
            }
            {
                Console.WriteLine("查询子用户API Key信息");
                apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
                huobiSpotRestClient.SetApiCredentials(apiCredentials);
                var result = await huobiSpotRestClient.SpotApi.Account.APIKeyQueryAsync(long.Parse(subUserId));
                if (result.Success)
                {
                    foreach (var item in result.Data)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                    }
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiAPIKeyQuery>>(result, "火币现货服务器", "查询子用户API Key信息");
                }
            }
        }
        #endregion
        #region 通过用户账户Id查询账户流水
        {
            {
                Console.WriteLine("通过用户账户Id查询账户流水(母用户示范）");
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiSpotRestClient.SetApiCredentials(apiCredentials);
                var result = await huobiSpotRestClient.SpotApi.Account.GetAccountHistoryAsync(long.Parse(mainSportAccountId));
                if (result.Success)
                {
                    foreach (var item in result.Data)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                    }
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiAccountHistory>>(result, "火币现货服务器", "通过用户账户Id查询账户流水(母用户示范）");
                }
            }
            {
                Console.WriteLine("通过用户账户Id查询账户流水(子用户示范）");
                apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
                huobiSpotRestClient.SetApiCredentials(apiCredentials);
                var result = await huobiSpotRestClient.SpotApi.Account.GetAccountHistoryAsync(long.Parse(subSportAccountId));
                if (result.Success)
                {
                    foreach (var item in result.Data)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                    }
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiAccountHistory>>(result, "火币现货服务器", "通过用户账户Id查询账户流水(子母用户示范）");
                }
            }
        }
        #endregion
        #region 通过用户账户Id查询财务流水
        {
            {
                Console.WriteLine("通过用户账户Id查询财务流水(母用户示范）");
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiSpotRestClient.SetApiCredentials(apiCredentials);
                var result = await huobiSpotRestClient.SpotApi.Account.GetAccountLedgerAsync(long.Parse(mainSportAccountId));
                if (result.Success)
                {
                    foreach (var item in result.Data)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                    }
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiLedgerEntry>>(result, "火币现货服务器", "通过用户账户Id查询财务流水(母用户示范）");
                }
            }
            {
                Console.WriteLine("通过用户账户Id查询财务流水(子用户示范）");
                apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
                huobiSpotRestClient.SetApiCredentials(apiCredentials);
                var result = await huobiSpotRestClient.SpotApi.Account.GetAccountLedgerAsync(long.Parse(subSportAccountId));
                if (result.Success)
                {
                    foreach (var item in result.Data)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                    }
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiLedgerEntry>>(result, "火币现货服务器", "通过用户账户Id查询财务流水(子用户示范）");
                }
            }
        }
        #endregion
        #region 查询当前用户的所有账户ID
        {
            {
                Console.WriteLine("查询当前用户的所有账户ID(母用户示范）");
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiSpotRestClient.SetApiCredentials(apiCredentials);
                var result = await huobiSpotRestClient.SpotApi.Account.GetAccountsAsync();
                if (result.Success)
                {
                    foreach (var item in result.Data)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                    }
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiAccount>>(result, "火币现货服务器", "查询当前用户的所有账户ID(母用户示范）");
                }
            }
            {
                Console.WriteLine("查询当前用户的所有账户ID(子用户示范）");
                apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
                huobiSpotRestClient.SetApiCredentials(apiCredentials);
                var result = await huobiSpotRestClient.SpotApi.Account.GetAccountsAsync();
                if (result.Success)
                {
                    foreach (var item in result.Data)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                    }
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiAccount>>(result, "火币现货服务器", "查询当前用户的所有账户ID(子用户示范）");
                }
            }
        }
        #endregion
        #region 获取平台资产总估值(按照BTC或法币计价单位）
        {
            {
                Console.WriteLine("获取平台资产总估值(母用户示范）");
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiSpotRestClient.SetApiCredentials(apiCredentials);
                var result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.Spot);
                if (result.Success)
                {
                    Console.WriteLine($"现货账户：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiAccountValuation>(result, "火币现货服务器", "现货账户获取平台资产总估值(母用户示范）");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.Margin);
                if (result.Success)
                {
                    Console.WriteLine($"杠杆账户：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiAccountValuation>(result, "火币现货服务器", "杠杆账户获取平台资产总估值(母用户示范）");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.Point);
                if (result.Success)
                {
                    Console.WriteLine($"点卡账户{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiAccountValuation>(result, "火币现货服务器", "点卡账户获取平台资产总估值(母用户示范）");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.CrossMargin);
                if (result.Success)
                {
                    Console.WriteLine($"全仓杠杆账户{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiAccountValuation>(result, "火币现货服务器", "全仓杠杆账户获取平台资产总估值(母用户示范）");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.Minepool);
                if (result.Success)
                {
                    Console.WriteLine($"矿池账户{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiAccountValuation>(result, "火币现货服务器", "矿池账户获取平台资产总估值(母用户示范）");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.Etf);
                if (result.Success)
                {
                    Console.WriteLine($"ETF账户{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiAccountValuation>(result, "火币现货服务器", "ETF账户获取平台资产总估值(母用户示范）");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.CryptoLoans);
                if (result.Success)
                {
                    Console.WriteLine($"抵押借贷{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiAccountValuation>(result, "火币现货服务器", "抵押借贷获取平台资产总估值(母用户示范）");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.Otc);
                if (result.Success)
                {
                    Console.WriteLine($"OTC账户{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiAccountValuation>(result, "火币现货服务器", "OTC账户获取平台资产总估值(母用户示范）");
                }
            }
            {
                Console.WriteLine("平台资产总估值(子用户示范）");
                apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
                huobiSpotRestClient.SetApiCredentials(apiCredentials);
                var result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.Spot);
                if (result.Success)
                {
                    Console.WriteLine($"现货账户{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiAccountValuation>(result, "火币现货服务器", "现货账户获取平台资产总估值(子用户示范）");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.Margin);
                if (result.Success)
                {
                    Console.WriteLine($"杠杆账户{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiAccountValuation>(result, "火币现货服务器", "杠杆账户获取平台资产总估值(子用户示范）");
                }
            }
        }
        #endregion
        #region 获取账户余额
        {
            {
                Console.WriteLine("获取账户余额(母用户示范）");
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiSpotRestClient.SetApiCredentials(apiCredentials);
                var result = await huobiSpotRestClient.SpotApi.Account.GetBalancesAsync(long.Parse(mainSportAccountId));
                if (result.Success)
                {
                    Console.WriteLine($"现货账户");
                    foreach (var item in result.Data)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                    }
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiBalance>>(result, "火币现货服务器", "现货账户获取账户余额(母用户示范）");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetBalancesAsync(long.Parse(mainUsdtMarginedAccountId));
                if (result.Success)
                {
                    Console.WriteLine($"U本位合约账户");
                    foreach (var item in result.Data)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                    }
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiBalance>>(result, "火币现货服务器", "U本位合约账户获取账户余额(母用户示范）");
                }
            }
            {
                Console.WriteLine("获取账户余额(子用户示范）");
                apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
                huobiSpotRestClient.SetApiCredentials(apiCredentials);
                var result = await huobiSpotRestClient.SpotApi.Account.GetBalancesAsync(long.Parse(subSportAccountId));
                if (result.Success)
                {
                    Console.WriteLine($"现货账户");
                    foreach (var item in result.Data)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                    }
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiBalance>>(result, "火币现货服务器", "现货账户获取账户余额(子用户示范）");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetBalancesAsync(long.Parse(subUsdtMarginedAccountId));
                if (result.Success)
                {
                    Console.WriteLine($"U本位合约账户(拿不到合约账户的Id？");
                    foreach (var item in result.Data)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                    }
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiBalance>>(result, "火币现货服务器", "U本位合约账户获取账户余额(子用户示范）");
                }
            }
        }
        #endregion
        #region 【全仓】查询借币币息率及额度
        {
            Console.WriteLine("【全仓】查询借币币息率及额度");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.GetCrossLoanInterestRateAndQuotaAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiLoanInfoAsset>>(result, "火币现货服务器", "【全仓】查询借币币息率及额度");
            }
        }
        #endregion
        #region 【全仓】查询借币账户详情
        {
            Console.WriteLine("【全仓】查询借币账户详情");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.GetCrossMarginBalanceAsync();
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiMarginBalances>>(result, "火币现货服务器", "【全仓】查询借币账户详情");
            }
        }
        #endregion
        #region 【全仓】查询借币订单
        {
            Console.WriteLine("【全仓】查询借币订单");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.GetCrossMarginClosedOrdersAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiMarginOrder>>(result, "火币现货服务器", "【全仓】查询借币订单");
            }
        }
        #endregion
        #region 查询指定币种充币地址
        {
            Console.WriteLine("查询指定币种充币地址");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.GetDepositAddressesAsync(testBaseCurrency);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiDepositAddress>>(result, "火币现货服务器", "查询指定币种充币地址");
            }
            result = await huobiSpotRestClient.SpotApi.Account.GetDepositAddressesAsync(testQuoteCurrency);
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result)}");
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiDepositAddress>>(result, "火币现货服务器", "查询指定币种充币地址");
            }
        }
        #endregion
        #region 【逐仓】查询借币币息率及额度
        {
            Console.WriteLine("【逐仓】查询借币币息率及额度");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.GetIsolatedLoanInterestRateAndQuotaAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiLoanInfo>>(result, "火币现货服务器", "【逐仓】查询借币币息率及额度");
            }
        }
        #endregion
        #region 【逐仓】查询借币账户详情
        {
            Console.WriteLine("【逐仓】查询借币账户详情");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.GetIsolatedMarginBalanceAsync(testSymbol);
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiMarginBalances>>(result, "火币现货服务器", "【逐仓】查询借币账户详情");
            }
        }
        #endregion
        #region 【逐仓】查询借币订单
        {
            Console.WriteLine("【逐仓】查询借币订单");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.GetIsolatedMarginClosedOrdersAsync(testSymbol);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiMarginOrder>>(result, "火币现货服务器", "【逐仓】查询借币订单");
            }
        }
        #endregion
        #region 查询还币交易记录
        {
            Console.WriteLine("查询还币交易记录");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.GetRepaymentHistoryAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiRepayment>>(result, "火币现货服务器", "查询还币交易记录");
            }
        }
        #endregion
        #region 主用户查询子用户余额
        {
            Console.WriteLine("主用户查询子用户余额");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.GetSubAccountBalancesAsync(long.Parse(subUserId));
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiBalance>>(result, "火币现货服务器", "主用户查询子用户余额");
            }
        }
        #endregion
        #region 主用户获取子用户列表
        {
            Console.WriteLine("主用户获取子用户列表");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.GetSubAccountUsersAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUser>>(result, "火币现货服务器", "主用户获取子用户列表");
            }
        }
        #endregion
        #region 主用户获取指定子用户的账户列表
        {
            Console.WriteLine("主用户获取指定子用户的账户列表");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.GetSubUserAccountsAsync(long.Parse(subUserId));
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<HuobiSubUserAccounts>(result, "火币现货服务器", "主用户获取指定子用户的账户列表");
            }
        }
        #endregion
        #region 主用户获取自身Uid
        {
            Console.WriteLine("主用户获取自身Uid");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.GetUserIdAsync();
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<long>(result, "火币现货服务器", "主用户获取自身Uid");
            }
        }
        #endregion
        #region 查询充提记录
        {
            Console.WriteLine("查询充提记录");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.GetWithdrawDepositAsync(WithdrawDepositType.Withdraw);
            if (result.Success)
            {
                Console.WriteLine("查询充值记录");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiWithdrawDeposit>>(result, "火币现货服务器", "查询充值记录");
            }
            result = await huobiSpotRestClient.SpotApi.Account.GetWithdrawDepositAsync(WithdrawDepositType.Deposit);
            if (result.Success)
            {
                Console.WriteLine("查询提币记录");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiWithdrawDeposit>>(result, "火币现货服务器", "查询提币记录");
            }
        }
        #endregion
        #region 执行冻结或解冻子用户
        {
            Console.WriteLine("执行冻结或解冻子用户");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var Action = SubAccountManageAction.Unlock;
            if (Action == SubAccountManageAction.Unlock)
            {
                var result = await huobiSpotRestClient.SpotApi.Account.LockOrUnlockSubUserAsync(long.Parse(subUserId), Action);
                if (result.Success)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiLockOrUnlockSubUser>(result, "火币现货服务器", "执行解冻子用户");
                }
            }
            else
            {
                bool runActionLock = false;
                if (runActionLock == false)
                {
                    Console.WriteLine($"火币现货服务器：" + "当前执行冻结子用户，终止测试");
                }
                else
                {
                    var result = await huobiSpotRestClient.SpotApi.Account.LockOrUnlockSubUserAsync(long.Parse(subUserId), Action);
                    if (result.Success)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                    }
                    else
                    {
                        ErrorInfoOutput<HuobiLockOrUnlockSubUser>(result, "火币现货服务器", "执行冻结子用户");
                    }
                }
            }
        }
        #endregion
        #region 【全仓】归还借币
        {
            Console.WriteLine("【全仓】归还借币");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            if (loanAmount == 0)
            {
                var result = await huobiSpotRestClient.SpotApi.Account.RepayCrossMarginLoanAsync(loanOrderId, loanAmount);
                if (result.Success)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<object>(result, "火币现货服务器", "【全仓】归还借币");
                }
            }
            else
            {
                Console.WriteLine($"火币现货服务器：" + "【全仓】归还借币数量不为零，会产生实际归还操作，终止测试");
            }
        }
        #endregion
        #region 【逐仓】归还借币
        {
            Console.WriteLine("【逐仓】归还借币");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            if (loanAmount == 0)
            {
                var result = await huobiSpotRestClient.SpotApi.Account.RepayIsolatedMarginLoanAsync(loanOrderId, loanAmount);
                if (result.Success)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<long>(result, "火币现货服务器", "【逐仓】归还借币");
                }
            }
            else
            {
                Console.WriteLine($"火币现货服务器：" + "【逐仓】归还借币数量不为零，会产生实际归还操作，终止测试");
            }
        }
        #endregion
        #region 【通用】归还借币
        {
            Console.WriteLine("【通用】归还借币");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            if (loanAmount == 0)
            {
                var result = await huobiSpotRestClient.SpotApi.Account.RepayMarginLoanAsync(mainSportAccountId, loanOrderId, loanAmount);
                if (result.Success)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiRepaymentResult>>(result, "火币现货服务器", "【通用】归还借币");
                }
            }
            else
            {
                Console.WriteLine($"火币现货服务器：" + "【通用】归还借币数量不为零，会产生实际归还操作，终止测试");
            }
        }
        #endregion
        #region 【全仓】申请借币
        {
            Console.WriteLine("【全仓】申请借币");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            if (loanAmount == 0)
            {
                var result = await huobiSpotRestClient.SpotApi.Account.RequestCrossMarginLoanAsync(
                    asset: testBaseCurrency,
                    quantity: loanAmount
                    );
                if (result.Success)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<long>(result, "火币现货服务器", "【全仓】申请借币");
                }
            }
            else
            {
                Console.WriteLine($"火币现货服务器：" + "【全仓】申请借币数量不为零，会产生实际借币操作，终止测试");
            }
        }
        #endregion
        #region 【逐仓】申请借币
        {
            Console.WriteLine("【逐仓】申请借币");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            if (loanAmount == 0)
            {
                var result = await huobiSpotRestClient.SpotApi.Account.RequestIsolatedMarginLoanAsync(
                symbol: testBaseCurrency,
                asset: testQuoteCurrency,
                quantity: loanAmount
                );
                if (result.Success)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<long>(result, "火币现货服务器", "【逐仓】申请借币");
                }
            }
            else
            {
                Console.WriteLine($"火币现货服务器：" + "【逐仓】申请借币数量不为零，会产生实际借币操作，终止测试");
            }
        }
        #endregion
        #region 子用户创建API Key
        {
            Console.WriteLine("子用户创建API Key");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.SubUserAPIKeyCreationAsync("otpToken", long.Parse(subUserId));
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<HuobiSubUserAPIKeyCreation>(result, "火币现货服务器", "子用户创建API Key");
            }
        }
        #endregion
        #region 子用户删除API Key
        {
            Console.WriteLine("子用户删除API Key");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.SubUserAPIKeyDeletionAsync(long.Parse(subUserId), "AccessKeyValue");
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<HuobiSubUserAPIKeyDeletion>(result, "火币现货服务器", "子用户删除API Key");
            }
        }
        #endregion
        #region 子用户修改API Key
        {
            Console.WriteLine("子用户修改API Key");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.SubUserAPIKeyModificationAsync(long.Parse(subUserId), "AccessKeyValue");
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<HuobiSubUserAPIKeyModification>(result, "火币现货服务器", "子用户修改API Key");
            }
        }
        #endregion
        #region 创建子用户
        {
            Console.WriteLine("创建子用户");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            HuobiCreateSubUserAccountRequestInfo huobiCreateSubUserAccountRequestInfo = new()
            {
                UserName = "DaiPaxUsdt",
                Note = "NoteText"
            };
            IEnumerable<HuobiCreateSubUserAccountRequestInfo> UserList = new HuobiCreateSubUserAccountRequestInfo[] { huobiCreateSubUserAccountRequestInfo };
            HuobiCreateSubUserAccountRequest huobiCreateSubUserAccountRequest = new(UserList);
            var result = await huobiSpotRestClient.SpotApi.Account.SubUserCreationAsync(huobiCreateSubUserAccountRequest);
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiSubUserCreation>>(result, "火币现货服务器", "创建子用户");
            }
        }
        #endregion
        #region 母子用户币种互转
        {
            Console.WriteLine("母子用户币种互转");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.TransferAssetAsync(
                fromUserId: long.Parse(mainUserId),
                fromAccountType: AccountType.Spot,
                fromAccountId: long.Parse(mainSportAccountId),
                toUserId: long.Parse(subUserId),
                toAccountType: AccountType.Spot,
                toAccountId: long.Parse(subSportAccountId),
                asset: testBaseCurrency,
                quantity: 0.0M
                );
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<HuobiTransactionResult>(result, "火币现货服务器", "母子用户币种互转");
            }
        }
        #endregion
        #region 全仓杠杆账户对现货账户进行资产划转
        {
            Console.WriteLine("全仓杠杆账户对现货账户进行资产划转");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.TransferCrossMarginToSpotAsync(
                asset: testBaseCurrency,
                quantity: 0.0M
                );
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<long>(result, "火币现货服务器", "全仓杠杆账户对现货账户进行资产划转");
            }
        }
        #endregion
        #region 逐仓杠杆账户对现货账户进行资产划转
        {
            Console.WriteLine("逐仓杠杆账户对现货账户进行资产划转");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.TransferIsolatedMarginToSpotAsync(
                symbol: testSymbol,
                asset: testBaseCurrency,
                quantity: 0.0M
                );
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<long>(result, "火币现货服务器", "逐仓杠杆账户对现货账户进行资产划转");
            }
        }
        #endregion
        #region 现货账户对全仓杠杆账户进行资产划转
        {
            Console.WriteLine("现货账户对全仓杠杆账户进行资产划转");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.TransferSpotToCrossMarginAsync(
                asset: testBaseCurrency,
                quantity: 0.0M
                );
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<long>(result, "火币现货服务器", "现货账户对全仓杠杆账户进行资产划转");
            }
        }
        #endregion
        #region 现货账户对逐仓杠杆账户进行资产划转
        {
            Console.WriteLine("现货账户对逐仓杠杆账户进行资产划转");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.TransferSpotToIsolatedMarginAsync(
                symbol: testSymbol,
                asset: testBaseCurrency,
                quantity: 0.0M
                );
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<long>(result, "火币现货服务器", "现货账户对逐仓杠杆账户进行资产划转");
            }
        }
        #endregion
        #region 母子用户间资产划转
        {
            Console.WriteLine("母子用户间资产划转");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.TransferWithSubAccountAsync(
                subAccountId: long.Parse(subSportAccountId),
                asset: testBaseCurrency,
                quantity: 0.0M,
                transferType:TransferType.ToSubAccount
                );
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<long>(result, "火币现货服务器", "母子用户间资产划转");
            }
        }
        #endregion
        #region 主用户现货账户数字币提取到区块链地址
        //（已存在于提币地址列表）而不需要多重（短信、邮件）验证
        {
            Console.WriteLine("主用户现货账户数字币提取到区块链地址");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Account.WithdrawAsync(
                address: "",
                asset: testBaseCurrency,
                quantity: 0.0M,
                fee:0.0M,
                network:"",
                addressTag:""
                ); ;
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<long>(result, "火币现货服务器", "主用户现货账户数字币提取到区块链地址");
            }
        }
        #endregion
    }
}
//现货账户和交易接口测试-已完成
static async Task TestSpotApiTradingEndpoints()
{
    string? testCancelOrderId = string.Empty;
    string? testCancelClientOrderId = string.Empty;
    string? testCancelConditionalClientOrderId = string.Empty;
    using (var huobiSpotRestClient = new HuobiClient())
    {
        #region 对HuobiClient客户端的新实例使用新的设置(这里不设置则使用之前的默认设置）
        //使用accessKey, secretKey生成一个新的API凭证
        ApiCredentials apiCredentials = new(mainAccessKey, mainSecretKey);
        //当前客户端使用新生成的API凭证
        huobiSpotRestClient.SetApiCredentials(apiCredentials);
        #endregion
        #region 现货普通订单下单
        {
            Console.WriteLine("现货普通订单下单");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow)}";
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Trading.PlaceOrderAsync(
                accountId: long.Parse(mainSportAccountId),
                symbol: "USDCUSDT",
                side: OrderSide.Buy,
                type: OrderType.Limit,
                quantity: (decimal)10,
                price: (decimal)0.9,
                clientOrderId: $"{testPlaceClientOrderId}",
                source: null,
                stopPrice: null,
                stopOperator: null
                );
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                testCancelOrderId = result.Data.ToString();
                testCancelClientOrderId = testPlaceClientOrderId;
            }
            else
            {
                ErrorInfoOutput<long>(result, "火币现货服务器", "现货普通订单下单");
            }
        }
        #endregion
        #region 通过订单编号撤销一条订单
        {
            Console.WriteLine("通过订单编号撤销一条订单");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            if (!string.IsNullOrWhiteSpace(testCancelOrderId))
            {
                var result = await huobiSpotRestClient.SpotApi.Trading.CancelOrderAsync(
                    orderId: long.Parse(testCancelOrderId));
                if (result.Success)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                    testCancelOrderId = string.Empty;
                    testCancelClientOrderId = string.Empty;
                }
                else
                {
                    ErrorInfoOutput<long>(result, "火币现货服务器", "通过订单编号撤销一条订单");
                }
            }
            else
            {
                Console.WriteLine($"火币现货服务器：" + "撤销订单无效" + "异常");
            }
        }
        #endregion
        #region 通过用户自定义单号撤销一条订单
        {
            Console.WriteLine("通过用户自定义单号撤销一条订单");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            if (!string.IsNullOrWhiteSpace(testCancelClientOrderId))
            {
                var result = await huobiSpotRestClient.SpotApi.Trading.CancelOrderByClientOrderIdAsync(
                clientOrderId: testCancelClientOrderId);
                if (result.Success)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                    testCancelOrderId = string.Empty;
                    testCancelClientOrderId = string.Empty;
                }
                else
                {
                    ErrorInfoOutput<long>(result, "火币现货服务器", "通过用户自定义单号撤销一条订单");
                }
            }
            else
            {
                Console.WriteLine($"火币现货服务器：" + "撤销用户自定义单号无效" + "异常");
            }
        }
        #endregion
        #region 通过订单编号列表或用户自定义订单编号列表撤销订单
        {
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            Console.WriteLine("通过订单编号列表撤销订单");
            {
                List<long> orderIdList = new();
                if(!string.IsNullOrWhiteSpace(testCancelOrderId))
                    orderIdList.Add(long.Parse(testCancelOrderId));
                IEnumerable<long> orderIds = orderIdList;
                if (orderIds.Any())
                {
                    var result = await huobiSpotRestClient.SpotApi.Trading.CancelOrdersAsync(
                    orderIds: orderIds);
                    if (result.Success)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                    }
                    else
                    {
                        ErrorInfoOutput<HuobiBatchCancelResult>(result, "火币现货服务器", "通过订单编号列表撤销订单");
                    }
                }
                else
                {
                    Console.WriteLine($"火币现货服务器：" + "撤销订单订单编号列表" + "异常");
                }
            }
            {
                Console.WriteLine("通过用户自定义单号列表撤销多条订单");
                List<string> clientOrderIdList = new();
                if (!string.IsNullOrWhiteSpace(testCancelClientOrderId))
                    clientOrderIdList.Add(testCancelClientOrderId);
                IEnumerable<string> clientOrderIds = clientOrderIdList;
                if (clientOrderIds.Any())
                {
                    var result = await huobiSpotRestClient.SpotApi.Trading.CancelOrdersAsync(
                        orderIds: null,
                        clientOrderIds: clientOrderIds
                        );
                    if (result.Success)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                    }
                    else
                    {
                        ErrorInfoOutput<HuobiBatchCancelResult>(result, "火币现货服务器", "通过用户自定义单号列表撤销订单");
                    }
                }
                else
                {
                    Console.WriteLine($"火币现货服务器：" + "撤销订单用户自定义单号列表" + "异常");
                }
            }
        }
        #endregion
        #region 批量撤销所有订单（可限制账户Id/交易代码/交易方向/撤销数量）
        {
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            Console.WriteLine("批量撤销所有订单（可限制账户Id/交易代码/交易方向/撤销数量）");
            {
                List<long> orderIdList = new()
                {
                    long.Parse(testOrderId)
                };
                IEnumerable<long> orderIds = orderIdList;
                List<string> symbolList = new()
                {
                    "TestSymbol"
                };
                IEnumerable<string> symbols = symbolList;
                var result = await huobiSpotRestClient.SpotApi.Trading.CancelOrdersByCriteriaAsync(
                    accountId: long.Parse(mainSportAccountId),
                    symbols: symbols,
                    side: OrderSide.Buy,
                    limit: 1
                    );
                if (result.Success)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiByCriteriaCancelResult>(result, "火币现货服务器", "批量撤销所有订单（可限制账户Id/交易代码/交易方向/撤销数量）");
                }
            }
        }
        #endregion
        #region 现货策略委托下单
        {
            Console.WriteLine("现货策略委托下单");
            string testPlaceConditionalClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow)}";
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Trading.PlaceConditionalOrderAsync(
                accountId: long.Parse(mainSportAccountId),
                symbol: "USDCUSDT",
                side: OrderSide.Buy,
                type: ConditionalOrderType.Limit,
                stopPrice: 0.91M,
                quantity: 5M,
                price: 0.90M,
                quoteQuantity:null,
                trailingRate:null,
                timeInForce:TimeInForce.GoodTillCancel,
                clientOrderId: $"{testPlaceConditionalClientOrderId}"
                );
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                testCancelConditionalClientOrderId = testPlaceConditionalClientOrderId;
            }
            else
            {
                ErrorInfoOutput<HuobiPlacedConditionalOrder>(result, "火币现货服务器", "现货策略委托下单");
            }
        }
        #endregion
        #region 通过用户自定义单号列表撤销未触发策略委托订单
        {
            Console.WriteLine("通过用户自定义单号列表撤销未触发策略委托订单");
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            List<string> clientOrderIdList = new();
            if (!string.IsNullOrWhiteSpace(testCancelConditionalClientOrderId))
                clientOrderIdList.Add(testCancelConditionalClientOrderId);
            IEnumerable<string> clientOrderIds = clientOrderIdList;
            if (clientOrderIds.Any())
            {
                var result = await huobiSpotRestClient.SpotApi.Trading.CancelConditionalOrdersAsync(
                clientOrderIds: clientOrderIds);
                if (result.Success)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiConditionalOrderCancelResult>(result, "火币现货服务器", "通过用户自定义单号列表撤销未触发策略委托订单");
                }
            }
            else
            {
                Console.WriteLine($"火币现货服务器：" + "撤销未触发策略委托用户自定义单号列表" + "异常");
            }
        }
        #endregion
        #region 通过订单编号获取指定订单详情
        {
            Console.WriteLine("通过订单编号获取指定订单详情");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow)}";
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Trading.GetOrderAsync(
                orderId:long.Parse(testOrderId)
                );
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<HuobiOrder>(result, "火币现货服务器", "通过订单编号获取指定订单详情");
            }
        }
        #endregion
        #region 通过用户自定义单号获取指定订单详情
        {
            Console.WriteLine("通过用户自定义单号获取指定订单详情");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow)}";
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Trading.GetOrderByClientOrderIdAsync(
                clientOrderId: testClientOrderId
                );
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<HuobiOrder>(result, "火币现货服务器", "通过用户自定义单号获取指定订单详情");
            }
        }
        #endregion
        #region 查询当前未成交订单
        {
            Console.WriteLine("查询当前未成交订单");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow)}";
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Trading.GetOpenOrdersAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiOpenOrder>>(result, "火币现货服务器", "查询当前未成交订单");
            }
        }
        #endregion
        #region 查询当前未触发策略委托订单
        {
            Console.WriteLine("查询当前未触发策略委托订单");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow)}";
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Trading.GetOpenConditionalOrdersAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiConditionalOrder>>(result, "火币现货服务器", "查询当前未触发策略委托订单");
            }
        }
        #endregion
        #region 查询指定交易代码的历史订单
        {
            Console.WriteLine("查询指定交易代码的历史订单");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow)}";
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Trading.GetClosedOrdersAsync(
                symbol:testSymbol
                );
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiOrder>>(result, "火币现货服务器", "查询指定交易代码的历史订单");
            }
        }
        #endregion
        #region 查询指定交易代码的策略委托历史
        {
            Console.WriteLine("查询指定交易代码的策略委托历史");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow)}";
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Trading.GetClosedConditionalOrdersAsync(
                symbol: testSymbol,
                status: ConditionalOrderStatus.Created
                );
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiConditionalOrder>>(result, "火币现货服务器", "查询指定交易代码的策略委托历史");
            }
        }
        #endregion
        #region 查询最近48小时内历史订单
        {
            Console.WriteLine("查询最近48小时内历史订单");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow)}";
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Trading.GetHistoricalOrdersAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiOrder>>(result, "火币现货服务器", "查询最近48小时内历史订单");
            }
        }
        #endregion
        #region 通过用户自定义单号查询策略委托订单
        {
            Console.WriteLine("通过用户自定义单号查询策略委托订单");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow)}";
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Trading.GetConditionalOrderAsync(
                clientOrderId: testClientOrderId
                );
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<HuobiConditionalOrder>(result, "火币现货服务器", "通过用户自定义单号查询策略委托订单");
            }
        }
        #endregion
        #region 查询指定订单编号的成交明细
        {
            Console.WriteLine("查询指定订单编号的成交明细");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow)}";
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Trading.GetOrderTradesAsync(
                orderId: long.Parse(testOrderId)
                );
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiOrderTrade>>(result, "火币现货服务器", "查询指定订单编号的成交明细");
            }
        }
        #endregion
        #region 查询当前和历史成交记录
        {
            Console.WriteLine("查询当前和历史成交记录");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow)}";
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);
            var result = await huobiSpotRestClient.SpotApi.Trading.GetUserTradesAsync();
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiOrderTrade>>(result, "火币现货服务器", "查询当前和历史成交记录");
            }
        }
        #endregion
    }
}
//交易所U本位合约基础信息接口测试-已完成
static async Task TestUsdtMarginSwapApiReferenceDataEndpoints()
{
    using (var huobiUsdtMarginedClient = new HuobiClient())
    {
        #region 对huobiUsdtMarginedClient客户端的新实例使用新的设置(这里不设置则使用之前的默认设置）
        //使用accessKey, secretKey生成一个新的API凭证
        ApiCredentials apiCredentials = new(mainAccessKey, mainSecretKey);
        //当前客户端使用新生成的API凭证
        huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
        #endregion
        #region 获取账户类型
        {
            await HandleRequest("Swap Unified  Account Type \r\n", () => huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapUnifiedAccountTypeAsync(),
                result => $"当前账户类型：{(result.AccountType == 1 ? "非统一账户（全仓逐仓账户）" : (result.AccountType == 2 ? "统一账户" : "未知账户类型"))}" 
                );
            //#region 方法二 不需要等待输入回车后才执行
            //Console.WriteLine("获取账户类型");
            //var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapUnifiedAccountTypeAsync();
            //if (result.Success)
            //{
            //    switch (result.Data.AccountType)
            //    {
            //        case 1:
            //            Console.WriteLine($"当前账户类型:非统一账户（全仓逐仓账户）(支持API下单)");
            //            break;
            //        case 2:
            //            Console.WriteLine($"当前账户类型:统一账户(不支持API下单)");
            //            break;
            //    }
            //}
            //else
            //{
            //    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapUnifiedAccountType>(result, "火币合约API服务器", "获取账户类型");
            //}
            //#endregion
        }
        #endregion
        #region 账户类型更改接口
        {
            Console.WriteLine("账户类型更改接口");
            int accountType = 1;                    //账户类型	1:非统一账户（全仓逐仓账户）2:统一账户
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.LinearSwapSwitchAccountTypeAsync(accountType);
            if (result.Success)
            {
                switch (result.Data.AccountType)
                {
                    case 1:
                        Console.WriteLine($"已切换到账户类型:非统一账户（全仓逐仓账户）(支持API下单)");
                        break;
                    case 2:
                        Console.WriteLine($"已切换到账户类型:统一账户(不支持API下单)");
                        break;
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapSwitchAccountType>(result, "火币合约API服务器", "账户类型更改接口");
            }
        }
        #endregion
        #region 【通用】获取合约资金费率
        {
            Console.WriteLine("【通用】获取合约资金费率");
            string contractCode = "BTC-USDT";
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapFundingRateAsync(contractCode);
            if (result.Success)
            {
                Console.WriteLine($"合约代码{contractCode}资金费率：{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapFundingRate>(result, "火币合约API服务器", "【通用】获取合约资金费率");
            }
        }
        #endregion
        #region 【通用】批量获取合约资金费率
        {
            Console.WriteLine("【通用】批量获取合约资金费率");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapBatchFundingRateAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码{item.ContractCode}资金费率：\r\n{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapFundingRate>>(result, "火币合约API服务器", "【通用】批量获取合约资金费率");
            }
        }
        #endregion
        #region 【通用】获取合约的历史资金费率
        {
            string contractCode = "DOGE-USDT";
            Console.WriteLine("【通用】获取合约的历史资金费率");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapHistoricalFundingRateAsync(contractCode);
            if (result.Success)
            {
                foreach (var item in result.Data.Data)
                {
                    Console.WriteLine($"合约代码{item.ContractCode}资金费率：\r\n{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapHistoricalFundingRate>(result, "火币合约API服务器", "【通用】获取合约的历史资金费率"); 
            }
        }
        #endregion
        #region 【通用】获取强平订单(新)
        {
            int trade_type = 0;
            string contractCode = "DOGE-USDT";
            Console.WriteLine("【通用】获取强平订单(新)");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapLiquidationOrdersAsync(trade_type, contractCode);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 业务类型：{item.BusinessType} 强平方向：{(item.Direction == "sell" ? "多单" : "空单")}  强平数量:{item.Amount} 强平金额（计价币种）：{item.TradeTurnover}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapLiquidationOrders>>(result, "火币合约API服务器", "【通用】获取强平订单(新)");
            }
        }
        #endregion
        #region 【通用】查询平台历史结算记录
        {
            string contractCode = "DOGE-USDT";
            Console.WriteLine("【通用】查询平台历史结算记录");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapSettlementRecordsAsync(contractCode);
            if (result.Success)
            {
                //Console.WriteLine($"{JsonConvert.SerializeObject(result)}");
                foreach (var item in result.Data.SettlementRecords)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 业务类型：{item.BusinessType} 结算时间：{item.SettlementTime}  结算价格:{item.SettlementPrice}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedSettlementRecords>(result, "火币合约API服务器", "【通用】查询平台历史结算记录");
            }
        }
        #endregion
        #region 【通用】精英账户多空持仓对比-账户数
        {
            string contractCode = "DOGE-USDT";
            string period = "15min";
            Console.WriteLine("【通用】精英账户多空持仓对比-账户数");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapEliteAccountRatioAsync(contractCode, period);
            if (result.Success)
            {
                Console.WriteLine($"合约代码：{result.Data.ContractCode} 交易代码：{result.Data.Pair} 业务类型：{result.Data.BusinessType}");
                foreach (var item in result.Data.AccountRatioDetails)
                {
                    Console.WriteLine($"净多仓的账户比例：{item.BuyRatio} 净空仓的账户比例：{item.SellRatio} 锁仓的账户比例：{item.LockedRatio}  生成时间:{item.Timestamp}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedEliteAccountRatio>(result, "火币合约API服务器", "【通用】精英账户多空持仓对比-账户数");
            }
        }
        #endregion
        #region 【通用】精英账户多空持仓对比-持仓量
        {
            string contractCode = "DOGE-USDT";
            string period = "15min";
            Console.WriteLine("【通用】精英账户多空持仓对比-持仓量");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapElitePositionRatioAsync(contractCode, period);
            if (result.Success)
            {
                Console.WriteLine($"合约代码：{result.Data.ContractCode} 交易代码：{result.Data.Pair} 业务类型：{result.Data.BusinessType}");
                foreach (var item in result.Data.PositionRatioDetails)
                {
                    Console.WriteLine($"多仓的总持仓量占比：{item.BuyRatio} 空仓的总持仓量占比：{item.SellRatio} 生成时间:{item.Timestamp}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedElitePositionRatio>(result, "火币合约API服务器", "【通用】精英账户多空持仓对比-持仓量");
            }
        }
        #endregion
        #region 【逐仓】查询系统状态
        {
            Console.WriteLine("【逐仓】查询系统状态");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapApiStateAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapIsolatedApiState>>(result, "火币合约API服务器", "【逐仓】查询系统状态");
            }
        }
        #endregion
        #region 【全仓】获取平台阶梯保证金
        {
            string contractCode = "DOGE-USDT";
            Console.WriteLine("【全仓】获取平台阶梯保证金");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapCrossLadderMarginAsync(contractCode);
            if (result.Success)
            {
                Console.WriteLine($"【全仓】平台阶梯保证金：");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 保证金模式：{item.MarginMode}");
                    foreach (var list in item.CrossLadderMarginLists)
                    {
                        Console.WriteLine($"杠杆倍数：{list.LeverRate}");
                        foreach (var ladders in list.CrossLadderMarginDetails)
                        {
                            Console.WriteLine($"最大可用保证金：{ladders.MaxMarginAvailable} 最小可用保证金：{ladders.MinMarginAvailable}");
                        }
                    }
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketCrossLadderMargin>>(result, "火币合约API服务器", "【全仓】获取平台阶梯保证金");
            }
        }
        #endregion
        #region 【逐仓】获取平台阶梯保证金
        {
            string contractCode = "DOGE-USDT";
            Console.WriteLine("【逐仓】获取平台阶梯保证金");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapLadderMarginAsync(contractCode);
            if (result.Success)
            {
                Console.WriteLine($"【逐仓】平台阶梯保证金：");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 保证金模式：{item.MarginMode}");
                    foreach (var list in item.LadderMarginLists)
                    {
                        Console.WriteLine($"杠杆倍数：{list.LeverRate}");
                        foreach (var ladders in list.LadderMarginDetails)
                        {
                            Console.WriteLine($"最大可用保证金：{ladders.MaxMarginAvailable} 最小可用保证金：{ladders.MinMarginAvailable}");
                        }
                    }
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketIsolatedLadderMargin>>(result, "火币合约API服务器", "【逐仓】获取平台阶梯保证金");
            }
        }
        #endregion
        #region 【通用】获取预估结算价
        {
            string contractCode = "DOGE-USDT";
            string pair = "DOGE-USDT";
            string contractType = "swap";
            string businessType = "swap";
            Console.WriteLine("【通用】获取预估结算价");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapEstimatedSettlementPriceAsync(contractCode, pair, contractType, businessType);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 合约类型：{item.ContractCode} 业务类型:{item.BusinessType} 本期预估结算价：{item.EstimatedSettlementPrice}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedEliteSettlementPrice>>(result, "火币合约API服务器", "【通用】获取预估结算价");
            }
        }
        #endregion
        #region 【逐仓】查询平台阶梯调整系数
        {
            string contractCode = "DOGE-USDT";
            Console.WriteLine("【逐仓】查询平台阶梯调整系数");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapAdjustfactorAsync(contractCode);
            if (result.Success)
            {
                Console.WriteLine($"【逐仓】平台阶梯调整系数：");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 保证金模式：{item.MarginMode}");
                    foreach (var list in item.Adjustfactors)
                    {
                        Console.WriteLine($"杠杆倍数：{list.LeverRate}");
                        foreach (var ladders in list.AdjustfactorLadderDetails)
                        {
                            Console.WriteLine($"档位：{ladders.Ladder} 净持仓量的最小值：{ladders.MinSize} 净持仓量的最大值：{ladders.MaxSize} 调整系数：{ladders.AdjustFactor}");
                        }
                    }
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapIsolatedAdjustfactor>>(result, "火币合约API服务器", "【逐仓】查询平台阶梯调整系数");
            }
        }
        #endregion
        #region 【全仓】查询平台阶梯调整系数
        {
            string contractCode = "DOGE-USDT";
            Console.WriteLine("【全仓】查询平台阶梯调整系数");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapCrossAdjustfactorAsync(contractCode);
            if (result.Success)
            {
                Console.WriteLine($"【全仓】平台阶梯调整系数：");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 保证金模式：{item.MarginMode}");
                    foreach (var list in item.CrossAdjustfactors)
                    {
                        Console.WriteLine($"杠杆倍数：{list.LeverRate}");
                        foreach (var ladders in list.CrossAdjustfactorLadderDetails)
                        {
                            Console.WriteLine($"档位：{ladders.Ladder} 净持仓量的最小值：{ladders.MinSize} 净持仓量的最大值：{ladders.MaxSize} 调整系数：{ladders.AdjustFactor}");
                        }
                    }
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapCrossAdjustfactor>>(result, "火币合约API服务器", "【全仓】查询平台阶梯调整系数");
            }
        }
        #endregion
        #region 【通用】查询合约风险准备金余额历史数据
        {
            string contractCode = "DOGE-USDT";
            Console.WriteLine("【通用】查询合约风险准备金余额历史数据");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapInsuranceFundAsync(contractCode);
            if (result.Success)
            {
                Console.WriteLine($"合约代码：{result.Data.ContractCode} 业务类型：{result.Data.BusinessType}");
                foreach (var item in result.Data.InsuranceFundTicks)
                {
                    Console.WriteLine($"风险准备金余额：{item.InsuranceFund} 数据时间点：{item.Timestamp}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedInsuranceFund>(result, "火币合约API服务器", "【通用】查询合约风险准备金余额历史数据");
            }
        }
        #endregion
        #region 【通用】查询合约风险准备金余额和预估分摊比例
        {
            string contractCode = "DOGE-USDT";
            string businessType = "swap";
            Console.WriteLine("【通用】查询合约风险准备金余额和预估分摊比例");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapRiskInfoAsync(contractCode, businessType);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 合约类型：{item.ContractCode} 业务类型:{item.BusinessType} 风险准备金余额：{item.InsuranceFund} 预估分摊比例：{item.EstimatedClawback}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedRiskInfo>>(result, "火币合约API服务器", "【通用】查询合约风险准备金余额和预估分摊比例");
            }
        }
        #endregion
        #region 【通用】获取合约最高限价和最低限价
        {
            string contractCode = "DOGE-USDT";
            string pair = "DOGE-USDT";
            string contractType = "swap";
            string businessType = "swap";
            Console.WriteLine("【通用】获取合约最高限价和最低限价");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapPriceLimitAsync(contractCode, pair, contractType, businessType);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码{item.ContractCode}最高限价和最低限价");
                    Console.WriteLine($"合约类型：{item.ContractType} 交易代码：{item.Pair} 业务类型：{item.BusinessType} 最高买价：{item.HighLimit} 最低卖价：{item.LowLimit}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapPriceLimit>>(result, "火币合约API服务器", "【通用】获取合约最高限价和最低限价");
            }
        }
        #endregion
        #region 【通用】获取当前合约总持仓量
        {
            Console.WriteLine("【通用】获取当前合约总持仓量");
            string contractCode = "BTC-USDT";
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapOpenInterestAsync(contractCode);
            if (result.Success)
            {
                Console.WriteLine($"合约代码{contractCode}总持仓量");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)} ");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapOpenInterest>>(result, "火币合约API服务器", "【通用】获取当前合约总持仓量");
            }
        }
        #endregion
        #region 【通用】获取合约信息
        {
            Console.WriteLine("【通用】获取合约信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapContractInfoAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}\r\n{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapContractInfo>>(result, "火币合约API服务器", "【通用】获取合约信息");
            }
        }
        #endregion
        #region 【通用】获取合约指数信息
        {
            Console.WriteLine("【通用】获取合约指数信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapIndexAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约指数：{item.ContractCode}\r\n{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapIndex>>(result, "火币合约API服务器", "【通用】获取合约指数信息");
            }
        }
        #endregion
        #region 【合约服务器】获取当前系统时间戳
        {
            //await HandleRequest("Market Status", () => huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapServerTimestampAsync(),
            //    result => $"{result.ToString()}"
            //    );
            Console.WriteLine("【合约服务器】获取当前系统时间戳");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapServerTimestampAsync();
            if (result.Success)
            {
                Console.WriteLine($"当前系统时间戳:{result.Data}\r\n");
            }
            else
            {
                ErrorInfoOutput<long>(result, "火币合约API服务器", "【合约服务器】获取当前系统时间戳");
            }
        }
        #endregion
        #region 【合约服务器】获取当前系统时间
        {
            Console.WriteLine("【合约服务器】获取当前系统时间");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapServerDateTimeAsync();
            if (result.Success)
            {
                Console.WriteLine(
                    $"合约服务器时间:{result.Data}\r\n" +
                    $"本地时间:{result.Data.ToLocalTime()}\r\n" +
                    $"时差:{(result.Data.ToLocalTime() - result.Data).TotalHours}小时"
                    );
            }
            else
            {
                ErrorInfoOutput<DateTime>(result, "火币合约API服务器", "【合约服务器】获取当前系统时间");
            }
        }
        #endregion
        #region 【U本位合约服务器】查询系统是否可用
        {
            //await HandleRequest("Market Status \r\n", () => huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapHeartbeatAsync(),
            //    result =>
            //    $"交割合约市场：{(result.Heartbeat == 1 ? "正常" : "停服维护")}\r\n" +
            //    $"币本位永续合约市场：{(result.SwapHeartbeat == 1 ? "正常" : "停服维护")}\r\n" +
            //    $"U本位合约市场：{(result.LinearSwapHeartbeat == 1 ? "正常" : "停服维护")}\r\n"
            //    );
            #region 方法二 不需要等待输入回车后才执行
            Console.WriteLine("【U本位合约服务器】查询系统是否可用");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapHeartbeatAsync();
            if (result.Success)
            {
                //Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                if (result.Data.Heartbeat == 1)
                {
                    Console.WriteLine($"交割合约市场：正常");
                }
                else
                {
                    Console.WriteLine(
                        $"交割合约市场：停服维护)" +
                        $"预计恢复时间：{(result.Data.EstimatedRecoveryTime == null ? "unknown" : TimeSpan.FromMilliseconds((double)result.Data.EstimatedRecoveryTime))}"
                        );
                }
                if (result.Data.SwapHeartbeat == 1)
                {
                    Console.WriteLine($"币本位永续合约市场：正常");
                }
                else
                {
                    Console.WriteLine(
                        $"币本位永续合约市场：停服维护)" +
                        $"预计恢复时间：{(result.Data.EstimatedRecoveryTime == null ? "unknown" : TimeSpan.FromMilliseconds((double)result.Data.EstimatedRecoveryTime))}"
                        );
                }
                if (result.Data.LinearSwapHeartbeat == 1)
                {
                    Console.WriteLine($"U本位合约市场：正常");
                }
                else
                {
                    Console.WriteLine(
                        $"U本位合约市场：停服维护)" +
                        $"预计恢复时间：{(result.Data.LinearSwapEstimatedRecoveryTime == null ? "unknown" : TimeSpan.FromMilliseconds((double)result.Data.LinearSwapEstimatedRecoveryTime))}"
                        );
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketHeartbeat>(result, "火币合约API服务器", "获取合约服务器可用状态");
            }
            #endregion
        }
        #endregion
        #region 【U本位合约服务器】获取当前系统状态
        {
            bool interfacePaused = true;
            if (interfacePaused == false)
            {
                Console.WriteLine("【U本位合约服务器】获取当前系统状态");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.ReferenceData.GetLinearSwapSummaryAsync();
                if (result.Success)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketStatus>(result, "火币合约API服务器", "【U本位合约服务器】获取当前系统状态");
                }
            }
            else
            {
                Console.WriteLine("获取合约服务器当前状态 功能下线状态，等待恢复！");
            }
        }
        #endregion
    }
}
//交易所U本位合约市场行情数据接口测试-已完成
static async Task TestUsdtMarginSwapApiMarketDataEndpoints()
{
    using (var huobiUsdtMarginedClient = new HuobiClient())
    {
        #region 对huobiUsdtMarginedClient客户端的新实例使用新的设置(这里不设置则使用之前的默认设置）
        //使用accessKey, secretKey生成一个新的API凭证
        //ApiCredentials apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
        //当前客户端使用新生成的API凭证
        //huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
        #endregion
        #region 【通用】获取合约K线数据
        {
            string contractCode = "BTC-USDT";
            string period = "1day";
            long from = 1668355200;
            long to = 1668534210;
            await HandleRequest("Linear Swap Ex Market History Kline \r\n", () => huobiUsdtMarginedClient.UsdtMarginSwapApi.MarketData.GetLinearSwapHistoryKlineAsync(contractCode, period, from, to),
                result => $"{contractCode}" + string.Join($" ", result.Select(s => $"\r\n开盘价：{s.Open}\t收盘价:{s.Close}\t最低价:{s.Low}\t最高价:{s.High}").Take(100)) + "\r\n......");
            #region 方法二 不需要等待输入回车后才执行，测试通过保留勿删！！！
            //Console.WriteLine("【通用】获取合约K线数据");
            //var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.MarketData.GetLinearSwapHistoryKlineAsync(contractCode, period, from, to);
            //if (result.Success)
            //{
            //    Console.WriteLine($"{contractCode}合约K线数据");
            //    foreach (var item in result.Data)
            //    {
            //        Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
            //    }
            //}
            //else
            //{
            //    ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketHistoryKline>>(result, "火币合约API服务器", "【通用】获取合约K线数据");
            //}
            #endregion
        }
        #endregion
        #region 【通用】获取合约行情深度数据
        {
            string contractCode = "BTC-USDT";
            string depthType = "step6";
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.MarketData.GetLinearSwapDepthAsync(contractCode, depthType);
            if (result.Success)
            {
                Console.WriteLine($"{contractCode}合约深度数据");
                Console.WriteLine($"数据来源频道：{result.Data.Channel}\r\n" +
                    $"订单Id：{result.Data.Mrid}" +
                    $"消息Id：{result.Data.Id}");
                Console.WriteLine($"卖盘");
                foreach (var item in result.Data.Asks)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
                Console.WriteLine($"买盘");
                foreach (var item in result.Data.Bids)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketDepth>(result, "火币合约API服务器", "【通用】获取合约行情深度数据");
            }
        }
        #endregion
        #region 【通用】获取合约市场最优挂单
        {
            {
                //string contractCode = "BTC-USDT";
                //string businessType = "swap";
                //var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.MarketData.GetLinearSwapBboAsync(contractCode, businessType);
            }
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.MarketData.GetLinearSwapBboAsync(null, "All");
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}\t业务类型：{item.BusinessType}");
                    Console.WriteLine($"买一价格：{(object.Equals(item.BestBid, null) ? "--" : item.BestBid.Price)} 买一数量：{(object.Equals(item.BestBid, null) ? "--" : item.BestBid.Quantity)}\t卖一价格：{(object.Equals(item.BestAsk, null) ? "--" : item.BestAsk.Price)} 卖一数量：{(object.Equals(item.BestAsk, null) ? "--" : item.BestAsk.Quantity)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketBbo>>(result, "火币合约API服务器", "【通用】获取合约市场最优挂单");
            }
        }
        #endregion
        #region 【通用】获取标记价格的K线数据
        {
            string contractCode = "BTC-USDT";
            string period = "1day";
            int size = 30;
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.MarketData.GetLinearSwapMarkPriceKlineAsync(contractCode, period, size);
            if (result.Success)
            {
                Console.WriteLine($"{contractCode}标记价格K线数据");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketHistoryMarkKline>>(result, "火币合约API服务器", "【通用】获取标记价格的K线数据");
            }
        }
        #endregion
        #region 【通用】获取聚合行情
        {
            string contractCode = "BTC-USDT";
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.MarketData.GetLinearSwapMergedAsync(contractCode);
            if (result.Success)
            {
                Console.WriteLine($"{contractCode}聚合行情");
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketDetailMerged>(result, "火币合约API服务器", "【通用】获取聚合行情");
            }
        }
        #endregion
        #region 【通用】批量获取聚合行情（V2)
        {
            //string contractCode = "BTC-USDT";
            //string businessType = "swap";
            //var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.MarketData.GetLinearSwapBatchMergedV2Async(contractCode, businessType);
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.MarketData.GetLinearSwapBatchMergedV2Async();
            if (result.Success)
            {
                Console.WriteLine($"批量获取聚合行情（V2)");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{item.ContractCode} {item.BusinessType} 聚合行情（V2)");
                    Console.WriteLine($"买一价格：{(object.Equals(item.BestBid, null) ? "--" : item.BestBid.Price)} 买一数量：{(object.Equals(item.BestBid, null) ? "--" : item.BestBid.Quantity)}\t卖一价格：{(object.Equals(item.BestAsk, null) ? "--" : item.BestAsk.Price)} 卖一数量：{(object.Equals(item.BestAsk, null) ? "--" : item.BestAsk.Quantity)}");
                    Console.WriteLine($"开盘价：{item.Open}\t收盘价：{item.Close}\t最低价：{item.Low}\t最高价：{item.High}");
                    //Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketDetailBatchMerged>>(result, "火币合约API服务器", "【通用】批量获取聚合行情（V2)");
            }
        }
        #endregion
        #region 【通用】获取市场最近成交记录
        {
            string contractCode = "BTC-USDT";
            string businessType = "swap";
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.MarketData.GetLinearSwapMarketTradeAsync(contractCode, businessType);
            if (result.Success)
            {
                Console.WriteLine($"{contractCode}市场最近成交记录");
                foreach (var item in result.Data.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketTrade>(result, "火币合约API服务器", "【通用】获取市场最近成交记录");
            }
        }
        #endregion
        #region 【通用】批量获取市场最近成交记录
        {
            string contractCode = "BTC-USDT";
            int size = 10;
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.MarketData.GetLinearSwapMarketHistoryTradeAsync(contractCode, size);
            if (result.Success)
            {
                Console.WriteLine($"{contractCode}市场最近成交记录");
                foreach (var item in result.Data)
                {
                    foreach (var itemData in item.Data)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(itemData)}");
                    }
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketHistoryTrade>>(result, "火币合约API服务器", "【通用】批量获取市场最近成交记录");
            }
        }
        #endregion
        #region 【通用】平台历史持仓量查询
        {
            string period = "1day";
            int amountType = 2;
            string contractCode = "BTC-USDT";
            string pair = "BTC-USDT";
            string contractType = "swap";            
            int size = 200;
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.MarketData.GetLinearSwapHisOpenInterestAsync(period, amountType, contractCode, pair, contractType, size);
            if (result.Success)
            {
                Console.WriteLine($"{result.Data.ContractCode}平台历史持仓量查询");
                foreach (var item in result.Data.Ticks)
                {
                    Console.WriteLine($"持仓量：{item.Volume:N3} {(item.AmountType == 1 ? "张" : "币")}\t总持仓额：{item.Value:N3}\t统计时间{(DateTimeConverter.ConvertFromMilliseconds(item.Timestamp))}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketHisOpenInterest>(result, "火币合约API服务器", "【通用】平台历史持仓量查询");
            }
        }
        #endregion
        #region 【通用】获取合约的溢价指数K线
        {
            string contractCode = "BTC-USDT";
            string period = "1day";
            int size = 10;
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.MarketData.GetLinearSwapPremiumIndexKlineAsync(contractCode, period, size);
            if (result.Success)
            {
                Console.WriteLine($"{contractCode}合约的溢价指数K线");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"开盘溢价指数：{item.Open}\t收盘溢价指数：{item.Close}\r\n最低溢价指数：{item.Low}\t最高溢价指数：{item.High}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketPremiumIndexKline>>(result, "火币合约API服务器", "【通用】获取合约的溢价指数K线");
            }
        }
        #endregion
        #region 【通用】获取合约实时预测资金费率的K线数据
        {
            string contractCode = "BTC-USDT";
            string period = "1day";
            int size = 10;
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.MarketData.GetLinearSwapEstimatedRateKlineAsync(contractCode, period, size);
            if (result.Success)
            {
                Console.WriteLine($"{contractCode}合约实时预测资金费率的K线数据");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"开盘值（预测资金费率）：{item.Open}\t收盘值（预测资金费率）：{item.Close}\r\n最低值（预测资金费率）：{item.Low}\t最高值（预测资金费率）：{item.High}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketEstimatedRateKline>>(result, "火币合约API服务器", "【通用】获取合约实时预测资金费率的K线数据");
            }
        }
        #endregion
        #region 【通用】获取合约基差数据
        {
            string contractCode = "BTC-USDT";
            string period = "1day";
            int size = 20;
            string basisPriceType = "open";
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.MarketData.GetLinearSwapBasisAsync(contractCode, period, size, basisPriceType);
            if (result.Success)
            {
                Console.WriteLine($"{contractCode}合约基差数据");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"最新成交价：{item.ContractPrice}\t指数基准价：{item.IndexPrice}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketHistoryBasis>>(result, "火币合约API服务器", "【通用】获取合约基差数据");
            }
        }
        #endregion
    }
}
//交易所U本位合约账户接口测试-已完成
static async Task TestUsdtMarginSwapApiAccountEndpoints()
{
    using (var huobiUsdtMarginedClient = new HuobiClient())
    {
        #region 对huobiUsdtMarginedClient客户端的新实例使用新的设置(这里不设置则使用之前的默认设置）
        //使用accessKey, secretKey生成一个新的API凭证
        ApiCredentials apiCredentials = new(mainAccessKey, mainSecretKey);
        //当前客户端使用新生成的API凭证
        huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
        #endregion
        #region 【通用】获取账户合约总资产估值(PrivateData)
        {
            //资产估值币种，即按该币种为单位进行估值，不填默认"BTC"	"BTC", "USD", "USDT", "CNY", "EUR", "GBP", "VND", "HKD", "TWD", "MYR", "SGD", "KRW", "RUB", "TRY"
            string valuationAsset = "USDT";
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            await HandleRequest("Linear Swap Api Main Account Balance Valuation \r\n", () => huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapBalanceValuationAsync(valuationAsset),
            result => $"" + string.Join($" ", result.Select(s => $"\r\n合约总资产估值：{s.Balance} {s.ValuationAsset}").Take(100)) + "\r\n......");
            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            await HandleRequest("Linear Swap Api Sub Account Balance Valuation \r\n", () => huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapBalanceValuationAsync(valuationAsset),
            result => $"" + string.Join($" ", result.Select(s => $"\r\n合约总资产估值：{s.Balance} {s.ValuationAsset}").Take(100)) + "\r\n......");
            #region 方法二 不需要等待输入回车后才执行，测试通过保留勿删！！！
            //#region 主用户客户端
            //apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            //huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            //#endregion
            //Console.WriteLine("【通用】获取账户合约总资产估值");
            //var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapBalanceValuationAsync(valuationAsset);
            //if (result.Success)
            //{
            //    foreach (var item in result.Data)
            //    {
            //        Console.WriteLine($"账户合约总资产估值：{item.Balance} {item.ValuationAsset}");
            //    }
            //}
            //else
            //{
            //    ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedBalanceValuation>>(result, "火币合约API服务器", "【通用】获取账户合约总资产估值");
            //}
            #endregion
        }
        #endregion
        #region 【逐仓】获取用户的合约账户信息(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】获取用户的合约账户信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapAccountInfoAsync(contractCode);
            if (result.Success)
            {
                Console.WriteLine($"【逐仓】获取用户的合约账户信息");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"保证金账户：{item.MarginAccount}\t币种名称：{item.Symbol}\t合约代码：{item.ContractCode}\t持仓模式：{item.PositionMode}\r\n" +
                        $"保证金币种：{item.MarginAsset}\t保证金模式：{item.MarginMode}\t持仓保证金：{item.MarginPosition}\t冻结保证金：{item.MarginFrozen}\t可用保证金：{item.MarginAvailable}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedIsolatedAccountInfo>>(result, "火币合约API服务器", "【逐仓】获取用户的合约账户信息");
            }
        }
        #endregion
        #region 【全仓】获取用户的合约账户信息(PrivateData)
        {
            string marginAccount = "USDT";
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】获取用户的合约账户信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapCrossAccountInfoAsync(marginAccount);
            if (result.Success)
            {
                Console.WriteLine($"【全仓】获取用户的合约账户信息");
                foreach (var item in result.Data)
                {
                    foreach(var contractDetail in item.ContractDetails)
                    {
                        Console.WriteLine($"永续的所有合约的相关字段：{JsonConvert.SerializeObject(contractDetail)}");
                    }
                    foreach (var futuresContractDetail in item.FuturesContractDetails)
                    {
                        Console.WriteLine($"交割的所有合约的相关字段：{JsonConvert.SerializeObject(futuresContractDetail)}");
                    }
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedCrossAccountInfo>>(result, "火币合约API服务器", "【全仓】获取用户的合约账户信息");
            }
        }
        #endregion
        #region 【逐仓】获取用户的合约持仓信息(PrivateData)
        {
            string contractCode = "BTC-USDT";
            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】获取用户的合约持仓信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapPositionInfoAsync(contractCode);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedIsolatedPositionInfo>>(result, "火币合约API服务器", "【逐仓】获取用户的合约持仓信息");
            }
        }
        #endregion
        #region 【全仓】获取用户的合约持仓信息(PrivateData)
        {
            string contractCode = "BTC-USDT";
            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】获取用户的合约持仓信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapCrossPositionInfoAsync(contractCode);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedCrossPositionInfo>>(result, "火币合约API服务器", "【全仓】获取用户的合约持仓信息");
            }
        }
        #endregion
        #region 【逐仓】查询用户账户和持仓信息(PrivateData)
        {
            string contractCode = "BTC-USDT";
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】查询用户账户和持仓信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapAccountPositionInfoAsync(contractCode);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedIsolatedAccountPositionInfo>>(result, "火币合约API服务器", "【逐仓】查询用户账户和持仓信息");
            }
        }
        #endregion
        #region 【全仓】查询用户账户和持仓信息(PrivateData)
        {
            string marginAccount = "USDT";
            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】查询用户账户和持仓信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapCrossAccountPositionInfoAsync(marginAccount);
            if (result.Success)
            {
                foreach (var contractDetail in result.Data.ContractDetails)
                {
                    Console.WriteLine($"永续的所有合约的相关字段：{JsonConvert.SerializeObject(contractDetail)}");
                }
                foreach (var futuresContractDetail in result.Data.FuturesContractDetails)
                {
                    Console.WriteLine($"交割的所有合约的相关字段：{JsonConvert.SerializeObject(futuresContractDetail)}");
                }
                foreach (var position in result.Data.Positions)
                {
                    Console.WriteLine($"持仓状况：{JsonConvert.SerializeObject(position)}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedCrossAccountPositionInfo>(result, "火币合约API服务器", "【全仓】查询用户账户和持仓信息");
            }
        }
        #endregion
        #region 【通用】批量设置子账户交易权限(PrivateData)
        {
            string subUid1 = "400293761";
            string subUid2 = "666666666";
            int subAuth = 1;
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【通用】批量设置子账户交易权限");
            List<string> subUidList = new()
            {
                subUid1,
                subUid2
            };
            IEnumerable<string> subUids = subUidList;
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.SetLinearSwapSubAuthAsync(subUids, subAuth);
            if (result.Success)
            { 
                Console.WriteLine($"设置子账户交易权限返回结果：");
                if(result.Data.Errors != null)
                {                   
                    foreach (var item in result.Data.Errors)
                    {
                        Console.WriteLine($"子账户用户编号：{item.SubUid}\t错误代码：{item.ErrCode}\t错误说明：{item.ErrMsg}");
                    }
                }
                if (!string.IsNullOrWhiteSpace(result.Data.Successes))
                {
                    Console.WriteLine($"子账户用户编号：{result.Data.Successes} {(subAuth == 1 ? "开通合约交易功能" : (subAuth == 0 ? "关闭合约交易功能" : "错误的权限状态"))}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedSubAuth>(result, "火币合约API服务器", "【通用】批量设置子账户交易权限");
            }
        }
        #endregion
        #region 【逐仓】查询母账户下所有子账户资产信息(PrivateData)
        {
            string contractCode = "BTC-USDT";
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】查询母账户下所有子账户资产信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapSubAccountListAsync(contractCode);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"子账户用户编号：{item.SubUid}");
                    foreach (var assetList in item.List)
                    {
                        Console.WriteLine($"保证金账户：{assetList.MarginAccount} 合约代码：{assetList.ContractCode} 账户权益：{assetList.MarginBalance} 预估强平价：{assetList.LiquidationPrice}");
                    }
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedIsolatedSubAccountList>>(result, "火币合约API服务器", "【逐仓】查询母账户下所有子账户资产信息");
            }
        }
        #endregion
        #region 【全仓】查询母账户下所有子账户资产信息(PrivateData)
        {
            string marginAccount = "USDT";
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】查询母账户下所有子账户资产信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapCrossSubAccountListAsync(marginAccount);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"子账户用户编号：{item.SubUid}");
                    foreach (var assetList in item.List)
                    {
                        Console.WriteLine($"保证金账户：{assetList.MarginAccount} 保证金率：{assetList.RiskRate} 保证金模式：{assetList.MarginMode} 账户权益：{assetList.MarginBalance}");
                    }
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedCrossSubAccountList>>(result, "火币合约API服务器", "【全仓】查询母账户下所有子账户资产信息");
            }
        }
        #endregion
        #region 【逐仓】批量获取子账户资产信息(PrivateData)
        {
            string contractCode = "BTC-USDT";
            int pageIndex = 1;
            int pageSize = 50;
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】批量获取子账户资产信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapSubAccountInfoListAsync(contractCode, pageIndex, pageSize);
            if (result.Success)
            {
                foreach (var item in result.Data.SubAccountIsolatedInfoLists)
                {
                    Console.WriteLine($"子账户用户编号：{item.SubUid}");
                    foreach (var assetList in item.AccountInfoList)
                    {
                        Console.WriteLine($"保证金账户：{assetList.MarginAccount} 合约代码：{assetList.ContractCode} 账户权益：{assetList.MarginBalance} 预估强平价：{assetList.LiquidationPrice}");
                    }
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedIsolatedSubAccountInfoList>(result, "火币合约API服务器", "【逐仓】批量获取子账户资产信息");
            }
        }
        #endregion
        #region 【全仓】批量获取子账户资产信息(PrivateData)
        {
            string marginAccount = "USDT";
            int pageIndex = 1;
            int pageSize = 50;
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】批量获取子账户资产信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapCrossSubAccountInfoListAsync(marginAccount, pageIndex, pageSize);
            if (result.Success)
            {
                foreach (var item in result.Data.SubAccountCrossInfoLists)
                {
                    Console.WriteLine($"子账户用户编号：{item.SubUid}");
                    foreach (var assetList in item.AccountInfoList)
                    {
                        Console.WriteLine($"保证金账户：{assetList.MarginAccount} 保证金率：{assetList.RiskRate} 保证金模式：{assetList.MarginMode} 账户权益：{assetList.MarginBalance}");
                    }
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedCrossSubAccountInfoList>(result, "火币合约API服务器", "【全仓】批量获取子账户资产信息");
            }
        }
        #endregion
        #region 【逐仓】查询单个子账户资产信息(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            string subUid = "400293761";
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】查询单个子账户资产信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapSubAccountInfoAsync(long.Parse(subUid), contractCode);
            if (result.Success)
            {
                Console.WriteLine($"子账户用户编号：{subUid}");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"保证金账户：{item.MarginAccount} 合约代码：{item.ContractCode} 账户权益：{item.MarginBalance} 预估强平价：{item.LiquidationPrice}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedIsolatedAccountPositionInfo>>(result, "火币合约API服务器", "【逐仓】查询单个子账户资产信息");
            }
        }
        #endregion
        #region 【全仓】查询单个子账户资产信息(PrivateData)
        {
            string marginAccount = "USDT";
            string subUid = "400293761";
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】查询单个子账户资产信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapCrossSubAccountInfoAsync(long.Parse(subUid), marginAccount);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    foreach (var contractDetail in item.ContractDetails)
                    {
                        Console.WriteLine($"永续的所有合约的相关字段：{JsonConvert.SerializeObject(contractDetail)}");
                    }
                    foreach (var futuresContractDetail in item.FuturesContractDetails)
                    {
                        Console.WriteLine($"交割的所有合约的相关字段：{JsonConvert.SerializeObject(futuresContractDetail)}");
                    }
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedCrossAccountPositionInfo>>(result, "火币合约API服务器", "【全仓】查询单个子账户资产信息");
            }
        }
        #endregion
        #region 【逐仓】查询单个子账户持仓信息(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            string subUid = "400293761";
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】查询单个子账户持仓信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapSubPositionInfoAsync(long.Parse(subUid), contractCode);
            if (result.Success)
            {
                Console.WriteLine($"子账户用户编号：{subUid}");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"保证金账户：{item.MarginAccount} 合约代码：{item.ContractCode} 开仓均价：{item.CostOpen} 持仓均价：{item.CostHold}");
                    Console.WriteLine($"杠杆倍数：{item.LeverRate} 	持仓模式{item.PositionMode} 仓位方向：{item.Direction} 未实现盈亏：{item.ProfitUnreal}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiPosition>>(result, "火币合约API服务器", "【逐仓】查询单个子账户持仓信息");
            }
        }
        #endregion
        #region 【全仓】查询单个子账户持仓信息(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            string subUid = "292046353";
            string pair = "DOGE-USDT";
            string contractType = "swap";
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】查询单个子账户持仓信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapCrossSubPositionInfoAsync(long.Parse(subUid), contractCode, pair, contractType);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码{item.ContractCode} 持仓方向：{item.Direction} 持仓均价：{item.CostHold} 开仓均价：{item.CostOpen} 业务类型：{item.businessType}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedCrossSubPositionInfo>>(result, "火币合约API服务器", "【全仓】查询单个子账户持仓信息");
            }
        }
        #endregion
        #region 【通用】查询用户财务记录(新)(PrivateData)
        {
            string marginAccount = "USDT";
            {
                #region 主用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【通用】查询用户财务记录(新)");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapFinancialRecordAsync(marginAccount);
                if (result.Success)
                {
                    Console.WriteLine($"主用户财务记录：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedFinancialRecord>>(result, "火币合约API服务器", "【通用】查询用户财务记录(新)");
                }
            }
            {
                #region 子用户客户端
                apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【通用】查询用户财务记录(新)");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapFinancialRecordAsync(marginAccount);
                if (result.Success)
                {
                    Console.WriteLine($"子用户财务记录：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedFinancialRecord>>(result, "火币合约API服务器", "【通用】查询用户财务记录(新)");
                }
            }
        }
        #endregion
        #region 【通用】组合查询用户财务记录(新)(PrivateData)
        {
            string marginAccount = "USDT";
            {
                #region 主用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【通用】组合查询用户财务记录(新)");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapFinancialRecordExactAsync(marginAccount);
                if (result.Success)
                {
                    Console.WriteLine($"主用户组合查询财务记录：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedFinancialRecord>>(result, "火币合约API服务器", "【通用】查询用户财务记录(新)(PrivateData)");
                }
            }
            {
                #region 子用户客户端
                apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【通用】组合查询用户财务记录(新)");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapFinancialRecordExactAsync(marginAccount);
                if (result.Success)
                {
                    Console.WriteLine($"子用户组合查询财务记录：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedFinancialRecord>>(result, "火币合约API服务器", "【通用】组合查询用户财务记录(新)");
                }
            }
        }
        #endregion
        #region 【逐仓】查询用户结算记录(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            {
                #region 主用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【逐仓】查询用户结算记录");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapUserSettlementRecordsAsync(contractCode);
                if (result.Success)
                {
                    Console.WriteLine($"主用户查询结算记录：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedIsolatedUserSettlementRecords>(result, "火币合约API服务器", "【逐仓】查询用户结算记录");
                }
            }
            {
                #region 子用户客户端
                apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【逐仓】查询用户结算记录");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapUserSettlementRecordsAsync(contractCode);
                if (result.Success)
                {
                    Console.WriteLine($"子用户查询结算记录：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedIsolatedUserSettlementRecords>(result, "火币合约API服务器", "【逐仓】查询用户结算记录");
                }
            }
        }
        #endregion
        #region 【全仓】查询用户结算记录(PrivateData)
        {
            string marginAccount = "USDT";
            {
                #region 主用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【全仓】查询用户结算记录");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapCrossUserSettlementRecordsAsync(marginAccount);
                if (result.Success)
                {
                    Console.WriteLine($"主用户查询结算记录：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedCrossUserSettlementRecords>(result, "火币合约API服务器", "【全仓】查询用户结算记录");
                }
            }
            {
                #region 子用户客户端
                apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【全仓】查询用户结算记录");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapCrossUserSettlementRecordsAsync(marginAccount);
                if (result.Success)
                {
                    Console.WriteLine($"子用户查询结算记录：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedCrossUserSettlementRecords>(result, "火币合约API服务器", "【全仓】查询用户结算记录");
                }
            }
        }
        #endregion
        #region 【逐仓】查询用户可用杠杆倍数(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            {
                #region 主用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【逐仓】查询用户可用杠杆倍数");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapAvailableLevelRateAsync(contractCode);
                if (result.Success)
                {
                    Console.WriteLine($"主用户查询可用杠杆倍数：");
                    foreach (var item in result.Data)
                    {
                        Console.WriteLine($"合约代码：{item.ContractCode} 保证金模式：{item.MarginMode} 可用倍数：{item.AvailableLevelRate}");
                    }                    
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedIsolatedAvailableLevelRate>>(result, "火币合约API服务器", "【逐仓】查询用户可用杠杆倍数");
                }
            }
            {
                #region 子用户客户端
                apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【逐仓】查询用户可用杠杆倍数");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapAvailableLevelRateAsync(contractCode);
                if (result.Success)
                {
                    Console.WriteLine($"子用户查询可用杠杆倍数：");
                    foreach (var item in result.Data)
                    {
                        Console.WriteLine($"合约代码：{item.ContractCode} 保证金模式：{item.MarginMode} 可用倍数：{item.AvailableLevelRate}");
                    }
                }
                else
                {
                    ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedIsolatedAvailableLevelRate>>(result, "火币合约API服务器", "【逐仓】查询用户可用杠杆倍数");
                }
            }
        }
        #endregion
        #region 【全仓】查询用户可用杠杆倍数(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】查询用户可用杠杆倍数");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapCrossAvailableLevelRateAsync(contractCode);
            if (result.Success)
            {
                Console.WriteLine($"子用户查询可用杠杆倍数：");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 保证金模式：{item.MarginMode} 可用倍数：{item.AvailableLevelRate}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedCrossUserAvailableLevelRate>>(result, "火币合约API服务器", "【全仓】查询用户可用杠杆倍数");
            }
        }
        #endregion
        #region 【通用】查询用户当前的下单量限制(PrivateData)
        {
            string orderPriceType = "limit";
            //string contractCode = "DOGE-USDT";            
            //string pair = "DOGE-USDT";
            //string contractType = "swap";
            //string businessType = "swap";
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【通用】查询用户当前的下单量限制");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapOrderLimitAsync(orderPriceType);
            if (result.Success)
            {
                Console.WriteLine($"下单类型：{result.Data.OrderPriceType}");
                foreach (var item in result.Data.SwapOrderLimitList)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 合约类型：{item.ContractType} 开仓单笔最大值：{item.OpenLimit} 平仓单笔最大值：{item.CloseLimit} ");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedSwapOrderLimit>(result, "火币合约API服务器", "【通用】查询用户当前的下单量限制");
            }
        }
        #endregion
        #region 【通用】查询用户当前的手续费费率(PrivateData)
        {
            string contractCode = "DOGE-USDT";            
            //string pair = "DOGE-USDT";
            //string contractType = "swap";
            //string businessType = "swap";
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【通用】查询用户当前的手续费费率");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapFeeAsync(contractCode);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码{item.ContractCode} 业务类型：{item.BusinessType} 开仓吃单手续费率：{item.OpenTakerFee} 开仓挂单手续费率：{item.OpenMakerFee}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedSwapFee>>(result, "火币合约API服务器", "【通用】查询用户当前的手续费费率");
            }
        }
        #endregion
        #region 【逐仓】查询用户当前的划转限制(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】查询用户当前的划转限制");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapTransferLimitAsync(contractCode);
            if (result.Success)
            {
                Console.WriteLine($"子用户划转限制：");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 保证金模式：{item.MarginMode} 单笔最大转入量：{item.TransferInMaxEach} 单笔最大转出量：{item.TransferOutMaxEach}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedIsolatedTransferLimit>>(result, "火币合约API服务器", "【逐仓】查询用户当前的划转限制");
            }
        }
        #endregion
        #region 【全仓】查询用户当前的划转限制(PrivateData)
        {
            string marginAccount = "USDT";            
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】查询用户当前的划转限制");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapCrossTransferLimitAsync(marginAccount);
            if (result.Success)
            {
                Console.WriteLine($"子用户划转限制：");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"保证金模式：{item.MarginMode} 单笔最大转入量：{item.TransferInMaxEach} 单笔最大转出量：{item.TransferOutMaxEach}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedCrossTransferLimit>>(result, "火币合约API服务器", "【全仓】查询用户当前的划转限制");
            }
        }
        #endregion
        #region 【逐仓】用户持仓量限制的查询(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】用户持仓量限制的查询");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapPositionLimitAsync(contractCode);
            if (result.Success)
            {
                Console.WriteLine($"子用户持仓量限制：");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 保证金模式：{item.MarginMode} 多仓持仓上限：{item.BuyLimitValue} USDT 空仓持仓上限：{item.SellLimitValue} USDT");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedIsolatedPositionLimit>>(result, "火币合约API服务器", "【逐仓】用户持仓量限制的查询");
            }
        }
        #endregion
        #region 【全仓】用户持仓量限制的查询(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】用户持仓量限制的查询");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapCrossPositionLimitAsync(contractCode);
            if (result.Success)
            {
                Console.WriteLine($"子用户持仓量限制：");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 保证金模式：{item.MarginMode} 多仓持仓上限：{item.BuyLimitValue} USDT 空仓持仓上限：{item.SellLimitValue} USDT");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedCrossPositionLimit>>(result, "火币合约API服务器", "【全仓】用户持仓量限制的查询");
            }
        }
        #endregion
        #region 【逐仓】查询用户所有杠杆持仓量限制(PrivateData)
        {
            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】查询用户所有杠杆持仓量限制");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapLeverPositionLimitAsync();
            if (result.Success)
            {
                Console.WriteLine($"子用户所有杠杆持仓量限制：");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 品种代码：{item.Symbol} 保证金模式：{item.MarginMode}");
                    foreach (var Detail in item.LeverPositionLimitDetailList)
                    {
                        Console.WriteLine($"杠杆倍数：{Detail.LeverRate} 多仓持仓价值上限：{Detail.BuyLimitValue} USDT\t空仓持仓价值上限：{Detail.SellLimitValue} USDT");
                    }
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedIsolatedLeverPositionLimit>>(result, "火币合约API服务器", "【逐仓】查询用户所有杠杆持仓量限制");
            }
        }
        #endregion
        #region 【全仓】查询用户所有杠杆持仓量限制(PrivateData)
        {
            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】查询用户所有杠杆持仓量限制");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapCrossLeverPositionLimitAsync();
            if (result.Success)
            {
                Console.WriteLine($"子用户所有杠杆持仓量限制：");
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 品种代码：{item.Symbol} 保证金模式：{item.MarginMode}");
                    foreach (var Detail in item.CrossLeverPositionLimitDetailList)
                    {
                        Console.WriteLine($"杠杆倍数：{Detail.LeverRate} 多仓持仓价值上限：{Detail.BuyLimitValue} USDT\t空仓持仓价值上限：{Detail.SellLimitValue} USDT");
                    }
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedCrossLeverPositionLimit>>(result, "火币合约API服务器", "【全仓】查询用户所有杠杆持仓量限制");
            }
        }
        #endregion
        #region 【通用】母子账户划转(PrivateData)
        {
            string subUid = "400293761";
            string asset = "USDT";
            string fromMarginAccount = "DOGE-USDT";
            string toMarginAccount = "DOGE-USDT";
            decimal amount = 1;
            string type = "master_to_sub";
            long clientOrderId = 1000000;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【通用】母子账户划转");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.LinearSwapMasterSubTransferAsync(
                subUid: long.Parse(subUid),
                asset: asset,
                fromMarginAccount: fromMarginAccount,
                toMarginAccount: toMarginAccount,
                amount: amount,
                type: type,
                clientOrderId: clientOrderId
                );
            if (result.Success)
            {
                if (result.Data != null)
                {
                    Console.WriteLine($"划转单号：{result.Data.OrderId}");
                }
                else
                {
                    Console.WriteLine($"划转失败！！！");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMasterSubTransfer>(result, "火币合约API服务器", "【通用】母子账户划转");
            }
        }
        #endregion
        #region 【通用】获取母账户下的所有母子账户划转记录(PrivateData)
        {
            string marginAccount = "BTC-USDT";
            string transferType = "34";
            int createDate = 90;
            int pageIndex = 1;
            int pageSize = 20;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【通用】获取母账户下的所有母子账户划转记录");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapMasterSubTransferRecordAsync(
                marginAccount: marginAccount,
                transferType: transferType,
                createDate: createDate,
                pageIndex: pageIndex,
                pageSize: pageSize
                );
            if (result.Success)
            {
                Console.WriteLine($"划转记录查询结果：{JsonConvert.SerializeObject(result)}");
            }
            else
            {
                ErrorInfoOutput<IEnumerable<object>>(result, "火币合约API服务器", "【通用】获取母账户下的所有母子账户划转记录");
            }
        }
        #endregion
        #region 【通用】同账号不同保证金账户的划转(PrivateData)
        {
            string asset = "USDT";
            string fromMarginAccount = "DOGE-USDT";
            string toMarginAccount = "DOGE-USDT";
            decimal amount = 1;
            long clientOrderId = 1000000;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【通用】同账号不同保证金账户的划转");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.LinearSwapTransferInnerAsync(
                asset: asset,
                fromMarginAccount: fromMarginAccount,
                toMarginAccount: toMarginAccount,
                amount: amount,
                clientOrderId: clientOrderId
                );
            if (result.Success)
            {
                if (result.Data != null)
                {
                    Console.WriteLine($"划转单号：{result.Data.OrderId}");
                }
                else
                {
                    Console.WriteLine($"划转失败！！！");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedTransferInner>(result, "火币合约API服务器", "【通用】同账号不同保证金账户的划转");
            }
        }
        #endregion
        #region 【通用】获取用户的API指标禁用信息(PrivateData)
        {
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【通用】获取用户的API指标禁用信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Account.GetLinearSwapApiTradingStatusAsync();
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");                
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedApiTradingStatus>(result, "火币合约API服务器", "【通用】获取用户的API指标禁用信息");
            }
        }
        #endregion
    }
}
//交易所U本位合约交易接口测试-已完成
static async Task TestUsdtMarginSwapApiTradeEndpoints()
{
    using (var huobiUsdtMarginedClient = new HuobiClient())
    {
        List<long> cancelIsolatedOrderIdList = new();
        List<long> cancelIsolatedClientOrderIdList = new();
        List<long> cancelCrossOrderIdList = new();
        List<long> cancelCrossClientOrderIdList = new();
        #region 对huobiUsdtMarginedClient客户端的新实例使用新的设置(这里不设置则使用之前的默认设置）
        //使用accessKey, secretKey生成一个新的API凭证
        ApiCredentials apiCredentials = new(mainAccessKey, mainSecretKey);
        //当前客户端使用新生成的API凭证
        huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
        #endregion
        #region 【全仓】查询系统交易权限(PrivateData)
        {
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】查询系统交易权限");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.GetLinearSwapCrossTradeStateAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 业务类型：{item.BusinessType} 保证金模式：{item.MarginMode} 保证金账户：{item.MarginAccount}");
                    Console.WriteLine($"开仓下单权限：{(item.Open == 1 ? "可用" : "不可用")} 平仓下单权限：{(item.Close == 1 ? "可用" : "不可用")} 撤单权限：{(item.Cancel == 1 ? "可用" : "不可用")} ");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapCrossTradeState>>(result, "火币合约API服务器", "【全仓】查询系统交易权限");
            }
        }
        #endregion
        #region 【逐仓】切换持仓模式(PrivateData)
        {
            string marginAccount = "DOGE-USDT";
            string positionMode = "dual_side";
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】切换持仓模式");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.LinearSwapSwitchPositionModeAsync(marginAccount, positionMode);
            if (result.Success)
            {
                Console.WriteLine($"保证金账户：{result.Data.MarginAccount} 持仓模式：{result.Data.PositionMode}");
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedSwitchPositionMode>(result, "火币合约API服务器", "【逐仓】切换持仓模式");
            }
        }
        #endregion
        #region 【全仓】切换持仓模式(PrivateData)
        {
            string marginAccount = "USDT";
            string positionMode = "dual_side";
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】切换持仓模式");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.LinearSwapCrossSwitchPositionModeAsync(marginAccount, positionMode);
            if (result.Success)
            {
                Console.WriteLine($"保证金账户：{result.Data.MarginAccount} 持仓模式：{result.Data.PositionMode}");
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossSwitchPositionMode>(result, "火币合约API服务器", "【全仓】切换持仓模式");
            }
        }
        #endregion
        string? read = "";
        Console.WriteLine($"Note: The following operations(PlanOrder/BatchPlanOrder/CancelOrder/CancelAllOrder) will generate real order submission, cancellation and other operations, Press [Y] to run test!");
        read = Console.ReadLine();
        if (read == "Y" || read == "y")
        {
            #region 【逐仓】合约下单(PrivateData)
            {
                string contractCode = "DOGE-USDT";
                UmDirection direction = UmDirection.buy;
                UmOffset offset = UmOffset.open;
                decimal? price = 0.04M;
                UmLeverRate leverRate = UmLeverRate.LeverRate_20;
                long volume = 1;
                UmOrderPriceType orderPriceType = UmOrderPriceType.Limit;
                decimal? tpTriggerPrice = null;
                decimal? tpOrderPrice = null;
                UmOrderPriceType? tpOrderPriceType = null;
                decimal? slTriggerPrice = null;
                decimal? slOrderPrice = null;
                UmOrderPriceType? slOrderPriceType = null;
                int? reduceOnly = null;
                long? clientOrderId = DateTimeConverter.ConvertToMicroseconds(DateTime.Now);
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【逐仓】合约下单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.LinearSwapOrderAsync(
                    contractCode: contractCode,
                    direction: direction,
                    offset: offset,
                    price: price,
                    leverRate: leverRate,
                    volume: volume,
                    orderPriceType: orderPriceType,
                    tpTriggerPrice: tpTriggerPrice,
                    tpOrderPrice: tpOrderPrice,
                    tpOrderPriceType: tpOrderPriceType,
                    slTriggerPrice: slTriggerPrice,
                    slOrderPrice: slOrderPrice,
                    slOrderPriceType: slOrderPriceType,
                    reduceOnly: reduceOnly,
                    clientOrderId: clientOrderId
                );
                if (result.Success)
                {
                    if (result.Data != null && result.Data.OrderId != null && result.Data.ClientOrderId != null)
                    {
                        Console.WriteLine($"订单编号：{result.Data.OrderId} 用户自定义单号：{result.Data.ClientOrderId}");
                        cancelIsolatedOrderIdList.Add((long)result.Data.OrderId);
                        cancelIsolatedClientOrderIdList.Add((long)result.Data.ClientOrderId);
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedOrder>(result, "火币合约API服务器", "【逐仓】合约下单");
                }
            }
            #endregion
            #region 【全仓】合约下单(PrivateData)
            {
                string contractCode = "DOGE-USDT";
                UmDirection direction = UmDirection.buy;
                UmOffset offset = UmOffset.open;
                decimal price = 0.04M;
                UmLeverRate leverRate = UmLeverRate.LeverRate_20;
                long volume = 1;
                UmOrderPriceType orderPriceType = UmOrderPriceType.Limit;
                decimal? tpTriggerPrice = null;
                decimal? tpOrderPrice = null;
                UmOrderPriceType? tpOrderPriceType = null;
                decimal? slTriggerPrice = null;
                decimal? slOrderPrice = null;
                UmOrderPriceType? slOrderPriceType = null;
                string? pair = "DOGE-USDT";
                UmContractType? contractType = UmContractType.swap;
                UmReduceOnly? reduceOnly = null;
                long? clientOrderId = DateTimeConverter.ConvertToMicroseconds(DateTime.Now);
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【全仓】合约下单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.LinearSwapCrossOrderAsync(
                    contractCode: contractCode,
                    direction: direction,
                    offset: offset,
                    price: price,
                    leverRate: leverRate,
                    volume: volume,
                    orderPriceType: orderPriceType,
                    tpTriggerPrice: tpTriggerPrice,
                    tpOrderPrice: tpOrderPrice,
                    tpOrderPriceType: tpOrderPriceType,
                    slTriggerPrice: slTriggerPrice,
                    slOrderPrice: slOrderPrice,
                    slOrderPriceType: slOrderPriceType,
                    pair: pair,
                    contractType: contractType,
                    reduceOnly: reduceOnly,
                    clientOrderId: clientOrderId
                );
                if (result.Success)
                {
                    if (result.Data != null && result.Data.OrderId != null && result.Data.ClientOrderId != null)
                    {
                        Console.WriteLine($"订单编号：{result.Data.OrderId} 用户自定义单号：{result.Data.ClientOrderId}");
                        cancelCrossOrderIdList.Add((long)result.Data.OrderId);
                        cancelCrossClientOrderIdList.Add((long)result.Data.ClientOrderId);
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossOrder>(result, "火币合约API服务器", "【全仓】合约下单");
                }
            }
            #endregion
            #region 【逐仓】合约批量下单(PrivateData)
            {
                HuobiUsdtMarginedIsolatedOrder isolatedOrder = new()
                {
                    ContractCode = "DOGE-USDT",
                    Direction = EnumConverter.GetString(UmDirection.buy),
                    Offset = EnumConverter.GetString(UmOffset.open),
                    Price = 0.04M,
                    LeverRate = UmLeverRate.LeverRate_20.GetHashCode(),
                    Volume = 1,
                    OrderPriceType = EnumConverter.GetString(UmOrderPriceType.Limit),
                    TpTriggerPrice = null,
                    TpOrderPrice = null,
                    TpOrderPriceType = null,
                    SlTriggerPrice = null,
                    SlOrderPrice = null,
                    SlOrderPriceType = null,
                    ReduceOnly = null,
                    ClientOrderId = DateTimeConverter.ConvertToMicroseconds(DateTime.Now)
                };
                List<HuobiUsdtMarginedIsolatedOrder> isolatedOrderList = new()
                {
                    isolatedOrder
                };
                IEnumerable<HuobiUsdtMarginedIsolatedOrder> isolatedOrders = isolatedOrderList;
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【逐仓】合约批量下单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.LinearSwapBatchorderAsync(isolatedOrders);
                if (result.Success)
                {
                    foreach (var item in result.Data.ErrorsList)
                    {
                        Console.WriteLine($"错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                    }
                    foreach (var item in result.Data.SuccessList)
                    {
                        Console.WriteLine($"订单编号：{item.OrderId} 用户自定义单号：{item.ClientOrderId}");
                        if (item.OrderId != null)
                        {
                            cancelIsolatedOrderIdList.Add((long)item.OrderId);
                        }
                        if (item.ClientOrderId != null)
                        {
                            cancelIsolatedClientOrderIdList.Add((long)item.ClientOrderId);
                        }
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedBatchOrder>(result, "火币合约API服务器", "【逐仓】合约批量下单");
                }
            }
            #endregion
            #region 【全仓】合约批量下单(PrivateData)
            {
                HuobiUsdtMarginedCrossOrder crossOrder = new()
                {
                    ContractCode = "DOGE-USDT",
                    Direction = EnumConverter.GetString(UmDirection.buy),
                    Offset = EnumConverter.GetString(UmOffset.open),
                    Price = 0.04M,
                    LeverRate = UmLeverRate.LeverRate_20.GetHashCode(),
                    Volume = 1,
                    OrderPriceType = EnumConverter.GetString(UmOrderPriceType.Limit),
                    TpTriggerPrice = null,
                    TpOrderPrice = null,
                    TpOrderPriceType = null,
                    SlTriggerPrice = null,
                    SlOrderPrice = null,
                    SlOrderPriceType = null,
                    Pair = "DOGE-USDT",
                    ContractType = "swap",
                    ReduceOnly = null,
                    ClientOrderId = DateTimeConverter.ConvertToMicroseconds(DateTime.Now)
                };
                List<HuobiUsdtMarginedCrossOrder> crossOrderList = new()
                {
                    crossOrder
                };
                IEnumerable<HuobiUsdtMarginedCrossOrder> crossOrders = crossOrderList;
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【全仓】合约批量下单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.LinearSwapCrossBatchorderAsync(crossOrders);
                if (result.Success)
                {
                    foreach (var item in result.Data.ErrorsList)
                    {
                        Console.WriteLine($"错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                    }
                    foreach (var item in result.Data.SuccessList)
                    {
                        Console.WriteLine($"订单编号：{item.OrderId} 用户自定义单号：{item.ClientOrderId}");
                        if (item.OrderId != null)
                        {
                            cancelCrossOrderIdList.Add((long)item.OrderId);
                        }
                        if (item.ClientOrderId != null)
                        {
                            cancelCrossClientOrderIdList.Add((long)item.ClientOrderId);
                        }
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossBatchOrder>(result, "火币合约API服务器", "【全仓】合约批量下单");
                }
            }
            #endregion
            #region 【逐仓】撤销合约订单(PrivateData)
            {
                string contractCode = "DOGE-USDT";
                IEnumerable<long> cancelIsolatedOrderIds = cancelIsolatedOrderIdList;
                IEnumerable<long> cancelIsolatedClientOrderIds = cancelIsolatedClientOrderIdList;
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【逐仓】撤销合约订单");
                if ((cancelIsolatedOrderIds == null || !cancelIsolatedOrderIds.Any()) && (cancelIsolatedClientOrderIds == null || !cancelIsolatedClientOrderIds.Any()))
                {
                    Console.WriteLine("【逐仓】撤销合约订单：未提供需要撤销的订单编号或者用户自定义单号");
                }
                else
                {
                    var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.LinearSwapCancelAsync(contractCode, cancelIsolatedOrderIds, cancelIsolatedClientOrderIds);
                    if (result.Success)
                    {
                        foreach (var item in result.Data.ErrorsList)
                        {
                            Console.WriteLine($"撤销失败订单编号：{item.OrderId}  错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                            if (item.OrderId != null)
                            {
                                cancelIsolatedOrderIdList.Remove((long)item.OrderId);
                            }
                        }
                        if (result.Data.Successes != null)
                        {
                            Console.WriteLine($"撤销成功订单编号：{result.Data.Successes} ");
                            foreach (var item in result.Data.Successes.Split(",").ToList<string>())
                            {
                                cancelIsolatedOrderIdList.Remove(long.Parse(item));
                            }
                        }
                    }
                    else
                    {
                        ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedCancel>(result, "火币合约API服务器", "【逐仓】撤销合约订单");
                    }
                }
            }
            #endregion
            #region 【全仓】撤销合约订单(PrivateData)
            {
                string contractCode = "DOGE-USDT";
                string pair = "DOGE-USDT";
                string contractType = "swap";
                IEnumerable<long> cancelCrossOrderIds = cancelCrossOrderIdList;
                IEnumerable<long> cancelCrossClientOrderIds = cancelCrossClientOrderIdList;
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【全仓】撤销合约订单");
                if ((cancelCrossOrderIds == null || !cancelCrossOrderIds.Any()) && (cancelCrossClientOrderIds == null || !cancelCrossClientOrderIds.Any()))
                {
                    Console.WriteLine("【全仓】撤销合约订单：未提供需要撤销的订单编号或者用户自定义单号");
                }
                else
                {
                    var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.LinearSwapCrossCancelAsync(contractCode, cancelCrossOrderIds, cancelCrossClientOrderIds, pair, contractType);
                    if (result.Success)
                    {
                        foreach (var item in result.Data.ErrorsList)
                        {
                            Console.WriteLine($"撤销失败订单编号：{item.OrderId}  错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                            if (item.OrderId != null)
                            {
                                cancelCrossOrderIdList.Remove((long)item.OrderId);
                            }
                        }
                        if (result.Data.Successes != null)
                        {
                            Console.WriteLine($"撤销成功订单编号：{result.Data.Successes} ");
                            foreach (var item in result.Data.Successes.Split(",").ToList<string>())
                            {
                                cancelCrossOrderIdList.Remove(long.Parse(item));
                            }
                        }
                    }
                    else
                    {
                        ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossCancel>(result, "火币合约API服务器", "【全仓】撤销合约订单");
                    }
                }
            }
            #endregion
            #region 【逐仓】撤销全部合约单(PrivateData)
            {
                string contractCode = "DOGE-USDT";
                string direction = "buy";
                string offset = "open";
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【逐仓】撤销全部合约单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.LinearSwapCancelAllAsync(contractCode, direction, offset);
                if (result.Success)
                {
                    foreach (var item in result.Data.ErrorsList)
                    {
                        Console.WriteLine($"撤销失败订单编号：{item.OrderId}  错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                        if (item.OrderId != null)
                        {
                            cancelIsolatedOrderIdList.Remove((long)item.OrderId);
                        }
                    }
                    if (result.Data.Successes != null)
                    {
                        Console.WriteLine($"撤销全部合约单：{result.Data.Successes} ");
                        foreach (var item in result.Data.Successes.Split(",").ToList<string>())
                        {
                            cancelIsolatedOrderIdList.Remove(long.Parse(item));
                        }
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedCancel>(result, "火币合约API服务器", "【逐仓】撤销全部合约单");
                }
            }
            #endregion
            #region 【全仓】撤销全部合约单(PrivateData)
            {
                string contractCode = "DOGE-USDT";
                string direction = "buy";
                string offset = "open";
                string pair = "DOGE-USDT";
                string contractType = "swap";
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【全仓】撤销全部合约单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.LinearSwapCrossCancelAllAsync(contractCode, pair, contractType, direction, offset);
                if (result.Success)
                {
                    foreach (var item in result.Data.ErrorsList)
                    {
                        Console.WriteLine($"撤销失败订单编号：{item.OrderId}  错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                        if (item.OrderId != null)
                        {
                            cancelCrossOrderIdList.Remove((long)item.OrderId);
                        }
                    }
                    if (result.Data.Successes != null)
                    {
                        Console.WriteLine($"撤销成功订单编号：{result.Data.Successes} ");
                        foreach (var item in result.Data.Successes.Split(",").ToList<string>())
                        {
                            cancelCrossOrderIdList.Remove(long.Parse(item));
                        }
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossCancel>(result, "火币合约API服务器", "【全仓】撤销全部合约单");
                }
            }
            #endregion
        }
        #region 【逐仓】切换杠杆(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            int leverRate = 10;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】切换杠杆");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.LinearSwapSwitchLeverRateAsync(contractCode, leverRate);
            if (result.Success)
            {
                Console.WriteLine($"合约代码：{result.Data.ContractCode}  保证金模式：{result.Data.MarginMode} 当前杠杆倍数：{result.Data.LeverRate}");
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedSwitchLeverRate>(result, "火币合约API服务器", "【逐仓】切换杠杆");
            }
        }
        #endregion
        #region 【全仓】切换杠杆(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            int leverRate = 10;
            string pair = "DOGE-USDT";
            string contractType = "swap";
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】切换杠杆");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.LinearSwapCrossSwitchLeverRateAsync(contractCode, leverRate, pair, contractType);
            if (result.Success)
            {
                Console.WriteLine($"合约代码：{result.Data.ContractCode}  保证金模式：{result.Data.MarginMode} 当前杠杆倍数：{result.Data.LeverRate}\r\n" +
                    $"合约类型：{result.Data.ContractType} 交易对：{result.Data.Pair} 业务类型：{result.Data.BusinessType}");               
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossSwitchLeverRate>(result, "火币合约API服务器", "【全仓】切换杠杆");
            }
        }
        #endregion
        #region 【逐仓】获取用户的合约订单信息(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            IEnumerable<long> isolatedOrderIds = new List<long>() { 1044606768686141440, 987654321 };
            IEnumerable<long> isolatedClientOrderIds = new List<long>() { 123456789000, 987654321000 };
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】获取用户的合约订单信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.GetLinearSwapOrderInfoAsync(contractCode, isolatedOrderIds, isolatedClientOrderIds);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"订单编号：{item.OrderId} 用户自定义单号：{item.ClientOrderId} 订单来源：{item.OrderSource} \r\n" +
                        $"合约代码：{item.ContractCode} 保证金模式：{item.MarginMode} 当前杠杆倍数：{item.LeverRate} \r\n" +
                        $"报价类型：{item.OrderPriceType} 委托价格：{item.Price} 委托数量：{item.Volume} 订单状态：{item.Status} ");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapIsolatedOrderInfo>>(result, "火币合约API服务器", "【逐仓】获取用户的合约订单信息");
            }
        }
        #endregion
        #region 【全仓】获取用户的合约订单信息(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            string pair = "DOGE-USDT";
            IEnumerable<long> isolatedOrderIds = new List<long>() { 1043342971513040896, 987654321 };
            IEnumerable<long> isolatedClientOrderIds = new List<long>() { 123456789000, 987654321000 };
            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】获取用户的合约订单信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.GetLinearSwapCrossOrderInfoAsync(contractCode, isolatedOrderIds, isolatedClientOrderIds, pair);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"业务类型：{item.BusinessType} 合约类型：{item.ContractType}\r\n" +
                        $"订单编号：{item.OrderId} 用户自定义单号：{item.ClientOrderId} 订单来源：{item.OrderSource} \r\n" +
                        $"合约代码：{item.ContractCode} 保证金模式：{item.MarginMode} 当前杠杆倍数：{item.LeverRate} \r\n"+
                        $"报价类型：{item.OrderPriceType} 委托价格：{item.Price} 委托数量：{item.Volume} 订单状态：{item.Status} ");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapCrossOrderInfo>>(result, "火币合约API服务器", "【全仓】获取用户的合约订单信息");
            }
        }
        #endregion
        #region 【逐仓】获取用户的合约订单明细信息(PrivateData)
        {
            long orderId = 1044606768686141440;
            string contractCode = "DOGE-USDT";
            long? createdTimestamp = null;
            int? orderType = null;
            int? pageIndex = null;
            int? pageSize = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】获取用户的合约订单明细信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.GetLinearSwapOrderDetailAsync(orderId, contractCode, createdTimestamp, orderType, pageIndex, pageSize);
            if (result.Success)
            {
                Console.WriteLine($"合约代码：{result.Data.ContractCode}  保证金模式：{result.Data.MarginMode} 当前杠杆倍数：{result.Data.LeverRate}");
                foreach (var item in result.Data.HuobiUsdtMarginedIsolatedTrades)
                {
                    Console.WriteLine($"唯一的交易标识：{item.Id} 非唯一撮合结果Id：{item.TradeId}\r\n" +
                        $"成交价格：{item.TradePrice} 成交量（张）：{item.TradeVolume} 成交金额（成交数量 * 合约面值 * 成交价格）：{item.TradeTurnover} \r\n" +
                        $"成交手续费：{item.TradeFee} 成交角色：{item.Role} 真实收益：{item.RealProfit}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedOrderDetail>(result, "火币合约API服务器", "【逐仓】获取用户的合约订单明细信息");
            }
        }
        #endregion
        #region 【全仓】获取用户的合约订单明细信息(PrivateData)
        {
            long orderId = 1043342971513040896;
            string contractCode = "DOGE-USDT";
            string? pair = "DOGE-USDT";
            long? createdTimestamp = null;
            int? orderType = null;
            int? pageIndex = null;
            int? pageSize = null;
            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】获取用户的合约订单明细信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.GetLinearSwapCrossOrderDetailAsync(orderId, contractCode, pair, createdTimestamp, orderType, pageIndex, pageSize);
            if (result.Success)
            {
                Console.WriteLine($"合约代码：{result.Data.ContractCode}  保证金模式：{result.Data.MarginMode} 当前杠杆倍数：{result.Data.LeverRate}");
                foreach (var item in result.Data.HuobiUsdtMarginedCrossTrades)
                {
                    Console.WriteLine($"唯一的交易标识：{item.Id} 非唯一撮合结果Id：{item.TradeId}\r\n" +
                        $"成交价格：{item.TradePrice} 成交量（张）：{item.TradeVolume} 成交金额（成交数量 * 合约面值 * 成交价格）：{item.TradeTurnover} \r\n" +
                        $"成交手续费：{item.TradeFee} 成交角色：{item.Role} 真实收益：{item.RealProfit}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossOrderDetail>(result, "火币合约API服务器", "【全仓】获取用户的合约订单明细信息");
            }
        }
        #endregion
        #region 【逐仓】获取用户的合约当前未成交委托(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            int? pageIndex = null;
            int? pageSize = null;
            string? sortBy = "created_at";
            int? tradeType = 0;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】获取用户的合约当前未成交委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.GetLinearSwapOpenordersAsync(contractCode, pageIndex, pageSize, sortBy, tradeType);
            if (result.Success)
            {
                Console.WriteLine($"总页数：{result.Data.TotalPage}  当前页：{result.Data.CurrentPage} 总条数：{result.Data.TotalSize}");
                foreach (var item in result.Data.HuobiUsdtMarginedIsolatedOpenOrders)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode} 当前杠杆倍数：{item.LeverRate}");
                    Console.WriteLine($"委托数量：{item.Volume} 委托价格：{item.Price} 订单报价类型：{item.OrderPriceType}\r\n" +
                        $"订单类型：{item.OrderType} 买卖方向：{item.Direction} 开平方向：{item.Offset} 成交数量：{item.TradeVolume} 成交总金额：{item.TradeTurnover} 手续费：{item.Fee} 手续费币种：{item.FeeAsset}\r\n" +
                        $"订单编号：{item.OrderId} 用户自定义编号：{item.ClientOrderId} 创建时间戳：{item.CreatedTimestamp} 更新时间戳：{item.UpdateTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedOpenOrders>(result, "火币合约API服务器", "【逐仓】获取用户的合约当前未成交委托");
            }
        }
        #endregion
        #region 【全仓】获取用户的合约当前未成交委托(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            string pair = "DOGE-USDT";
            int? pageIndex = null;
            int? pageSize = null;
            string? sortBy = "created_at";
            int? tradeType = 0;
            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】获取用户的合约当前未成交委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.GetLinearSwapCrossOpenordersAsync(contractCode, pair, pageIndex, pageSize, sortBy, tradeType);
            if (result.Success)
            {
                Console.WriteLine($"总页数：{result.Data.TotalPage}  当前页：{result.Data.CurrentPage} 总条数：{result.Data.TotalSize}");
                foreach (var item in result.Data.HuobiUsdtMarginedCrossOpenOrders)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode} 当前杠杆倍数：{item.LeverRate}");
                    Console.WriteLine($"委托数量：{item.Volume} 委托价格：{item.Price} 订单报价类型：{item.OrderPriceType}\r\n" +
                        $"订单类型：{item.OrderType} 买卖方向：{item.Direction} 开平方向：{item.Offset} 成交数量：{item.TradeVolume} 成交总金额：{item.TradeTurnover} 手续费：{item.Fee} 手续费币种：{item.FeeAsset}\r\n" +
                        $"订单编号：{item.OrderId} 用户自定义编号：{item.ClientOrderId} 创建时间戳：{item.CreatedTimestamp} 更新时间戳：{item.UpdateTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossOpenOrders>(result, "火币合约API服务器", "【全仓】获取用户的合约当前未成交委托");
            }
        }
        #endregion
        #region 【逐仓】获取用户的合约历史委托(PrivateData)
        {
            int tradeType = 0;
            int type = 1;
            string status = "0";
            string? contractCode = "DOGE-USDT";
            long? startTime = null;
            long? endTime = null;
            string? direct = null;
            long? fromId = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】获取用户的合约历史委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.GetLinearSwapHisordersAsync(tradeType, type, status, contractCode, startTime, endTime, direct, fromId);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode} 当前杠杆倍数：{item.LeverRate}");
                    Console.WriteLine($"委托数量：{item.Volume} 委托价格：{item.Price} 订单报价类型：{item.OrderPriceType}\r\n" +
                        $"订单类型：{item.OrderType} 买卖方向：{item.Direction} 开平方向：{item.Offset} 成交数量：{item.TradeVolume} 成交总金额：{item.TradeTurnover} 手续费：{item.Fee} 手续费币种：{item.FeeAsset}\r\n" +
                        $"订单编号：{item.OrderId} 成交时间戳：{item.CreatedTimestamp} 更新时间戳：{item.UpdateTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapIsolatedHisOrder>>(result, "火币合约API服务器", "【逐仓】获取用户的合约历史委托");
            }
        }
        #endregion
        #region 【全仓】获取用户的合约历史委托(PrivateData)
        {
            int tradeType = 0;
            int type = 1;
            string status = "0";
            string? contractCode = "DOGE-USDT";
            string? pair = "DOGE-USDT";
            long? startTime = null;
            long? endTime = null;
            string? direct = null;
            long? fromId = null;
            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】获取用户的合约历史委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.GetLinearSwapCrossHisordersAsync(tradeType, type, status, contractCode, pair, startTime, endTime, direct, fromId);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode} 当前杠杆倍数：{item.LeverRate}");
                    Console.WriteLine($"委托数量：{item.Volume} 委托价格：{item.Price} 订单报价类型：{item.OrderPriceType}\r\n" +
                        $"订单类型：{item.OrderType} 买卖方向：{item.Direction} 开平方向：{item.Offset} 成交数量：{item.TradeVolume} 成交总金额：{item.TradeTurnover} 手续费：{item.Fee} 手续费币种：{item.FeeAsset}\r\n" +
                        $"订单编号：{item.OrderId} 成交时间戳：{item.CreatedTimestamp} 更新时间戳：{item.UpdateTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapCrossHisOrder>>(result, "火币合约API服务器", "【全仓】获取用户的合约历史委托");
            }
        }
        #endregion
        #region 【逐仓】组合查询合约历史委托(PrivateData)
        {
            int tradeType = 0;
            int type = 1;
            string status = "0";
            string? contractCode = "DOGE-USDT";
            string? pair = "DOGE-USDT";
            long? startTime = null;
            long? endTime = null;
            string? direct = null;
            long? fromId = null;
            string? priceType = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】组合查询合约历史委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.GetLinearSwapHisordersExactAsync(tradeType, type, status, contractCode, pair, startTime, endTime, direct, fromId, priceType);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode} 当前杠杆倍数：{item.LeverRate}");
                    Console.WriteLine($"委托数量：{item.Volume} 委托价格：{item.Price} 订单报价类型：{item.OrderPriceType}\r\n" +
                        $"订单类型：{item.OrderType} 买卖方向：{item.Direction} 开平方向：{item.Offset} 成交数量：{item.TradeVolume} 成交总金额：{item.TradeTurnover} 手续费：{item.Fee} 手续费币种：{item.FeeAsset}\r\n" +
                        $"订单编号：{item.OrderId} 成交时间戳：{item.CreatedTimestamp} 更新时间戳：{item.UpdateTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapIsolatedHisOrder>>(result, "火币合约API服务器", "【逐仓】组合查询合约历史委托");
            }
        }
        #endregion
        #region 【全仓】组合查询合约历史委托(PrivateData)
        {
            int tradeType = 0;
            int type = 1;
            string status = "0";
            string? contractCode = "DOGE-USDT";
            string? pair = "DOGE-USDT";
            long? startTime = null;
            long? endTime = null;
            string? direct = null;
            long? fromId = null;
            string? priceType = null;
            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】组合查询合约历史委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.GetLinearSwapCrossHisordersExactAsync(tradeType, type, status, contractCode, pair, startTime, endTime, direct, fromId, priceType);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode} 当前杠杆倍数：{item.LeverRate}");
                    Console.WriteLine($"委托数量：{item.Volume} 委托价格：{item.Price} 订单报价类型：{item.OrderPriceType}\r\n" +
                        $"订单类型：{item.OrderType} 买卖方向：{item.Direction} 开平方向：{item.Offset} 成交数量：{item.TradeVolume} 成交总金额：{item.TradeTurnover} 手续费：{item.Fee} 手续费币种：{item.FeeAsset}\r\n" +
                        $"订单编号：{item.OrderId} 成交时间戳：{item.CreatedTimestamp} 更新时间戳：{item.UpdateTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapCrossHisOrder>>(result, "火币合约API服务器", "【全仓】组合查询合约历史委托");
            }
        }
        #endregion
        #region 【逐仓】获取用户的合约历史成交记录(PrivateData)
        {
            int tradeType = 0;
            string? contractCode = "DOGE-USDT";           
            string? pair = "DOGE-USDT";
            long? startTime = null;
            long? endTime = null;
            string? direct = null;
            long? fromId = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】获取用户的合约历史成交记录");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.GetLinearSwapMatchresultsAsync(tradeType, contractCode, pair, startTime, endTime, direct, fromId);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 保证金账户：{item.MarginAccount} 保证金模式：{item.MarginMode}");
                    Console.WriteLine($"唯一成交编号：{item.Id}  订单编号：{item.OrderId} 撮合结果编号：{item.MatchId}");
                    Console.WriteLine($"买卖方向：{item.Direction} 开平方向：{item.Offset} 订单来源：{item.OrderSource}\r\n" +
                        $"成交数量：{item.TradeVolume} 成交价格：{item.TradePrice} 成交总金额：{item.TradeTurnover} 成交时间：{item.CreateTimestamp}\r\n");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapIsolatedMatchResults>>(result, "火币合约API服务器", "【逐仓】获取用户的合约历史成交记录");
            }
        }
        #endregion
        #region 【全仓】获取用户的合约历史成交记录(PrivateData)
        {
            int tradeType = 0;
            string? contractCode = "DOGE-USDT";
            string? pair = "DOGE-USDT";
            long? startTime = null;
            long? endTime = null;
            string? direct = null;
            long? fromId = null;
            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】获取用户的合约历史成交记录");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.GetLinearSwapCrossMatchresultsAsync(tradeType, contractCode, pair, startTime, endTime, direct, fromId);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 保证金账户：{item.MarginAccount} 保证金模式：{item.MarginMode}");
                    Console.WriteLine($"唯一成交编号：{item.Id}  订单编号：{item.OrderId} 撮合结果编号：{item.MatchId}");
                    Console.WriteLine($"买卖方向：{item.Direction} 开平方向：{item.Offset} 订单来源：{item.OrderSource}\r\n" +
                        $"成交数量：{item.TradeVolume} 成交价格：{item.TradePrice} 成交总金额：{item.TradeTurnover} 成交时间：{item.CreateTimestamp}\r\n");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapCrossMatchResults>>(result, "火币合约API服务器", "【全仓】获取用户的合约历史成交记录");
            }
        }
        #endregion
        #region 【逐仓】组合查询用户历史成交记录(PrivateData)
        {
            int tradeType = 0;
            string? contractCode = "DOGE-USDT";
            long? startTime = null;
            long? endTime = null;
            string? direct = null;
            long? fromId = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】组合查询用户历史成交记录");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.GetLinearSwapMatchresultsExactAsync(contractCode, tradeType, startTime, endTime, direct, fromId);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 保证金账户：{item.MarginAccount} 保证金模式：{item.MarginMode}");
                    Console.WriteLine($"唯一成交编号：{item.Id}  订单编号：{item.OrderId} 撮合结果编号：{item.MatchId}");
                    Console.WriteLine($"买卖方向：{item.Direction} 开平方向：{item.Offset} 订单来源：{item.OrderSource}\r\n" +
                        $"成交数量：{item.TradeVolume} 成交价格：{item.TradePrice} 成交总金额：{item.TradeTurnover} 成交时间：{item.CreateTimestamp}\r\n");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapIsolatedMatchResults>>(result, "火币合约API服务器", "【逐仓】组合查询用户历史成交记录");
            }
        }
        #endregion
        #region 【全仓】组合查询用户历史成交记录(PrivateData)
        {
            int tradeType = 0;
            string? contractCode = "DOGE-USDT";
            string? pair = "DOGE-USDT";
            long? startTime = null;
            long? endTime = null;
            string? direct = null;
            long? fromId = null;
            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】组合查询用户历史成交记录");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.GetLinearSwapCrossMatchresultsExactAsync(tradeType, contractCode, pair, startTime, endTime, direct, fromId);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 保证金账户：{item.MarginAccount} 保证金模式：{item.MarginMode}");
                    Console.WriteLine($"唯一成交编号：{item.Id}  订单编号：{item.OrderId} 撮合结果编号：{item.MatchId}");
                    Console.WriteLine($"买卖方向：{item.Direction} 开平方向：{item.Offset} 订单来源：{item.OrderSource}\r\n" +
                        $"成交数量：{item.TradeVolume} 成交价格：{item.TradePrice} 成交总金额：{item.TradeTurnover} 成交时间：{item.CreateTimestamp}\r\n");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedMarketSwapCrossMatchResults>>(result, "火币合约API服务器", "【全仓】组合查询用户历史成交记录");
            }
        }
        #endregion
        Console.WriteLine($"Note: The following operations(LightningClosePosition) will generate real order submission, cancellation and other operations, Press [Y] to run test!");
        read = Console.ReadLine();
        if (read == "Y" || read == "y")
        {
            #region 【逐仓】合约闪电平仓下单(PrivateData)
            {
                string contractCode = "DOGE-USDT";
                long volume = 1;
                string direction = UmDirection.buy.ToString();
                long clientOrderId = 123456789000;
                string OrderPriceType = "lightning";
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【逐仓】合约闪电平仓下单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.LinearSwapLightningClosePositionAsync(contractCode, volume, direction, clientOrderId, OrderPriceType);
                if (result.Success)
                {
                    if (result.Data != null && result.Data.OrderId != null && result.Data.ClientOrderId != null)
                    {
                        Console.WriteLine($"订单编号：{result.Data.OrderId} 用户自定义单号：{result.Data.ClientOrderId}");
                        cancelIsolatedOrderIdList.Add((long)result.Data.OrderId);
                        cancelIsolatedClientOrderIdList.Add((long)result.Data.ClientOrderId);
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedOrder>(result, "火币合约API服务器", "【逐仓】合约闪电平仓下单");
                }
            }
            #endregion
            #region 【全仓】合约闪电平仓下单(PrivateData)
            {
                long volume = 1;
                string direction = UmDirection.buy.ToString();
                string? contractCode = "DOGE-USDT";
                string? pair = "DOGE-USDT";
                string? contract_type = "swap";
                long clientOrderId = 123456789000;
                string OrderPriceType = "lightning";
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【全仓】合约闪电平仓下单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Trade.LinearSwapCrossLightningClosePositionAsync(volume, direction, contractCode, pair, contract_type, clientOrderId, OrderPriceType);
                if (result.Success)
                {
                    if (result.Data != null && result.Data.OrderId != null && result.Data.ClientOrderId != null)
                    {
                        Console.WriteLine($"订单编号：{result.Data.OrderId} 用户自定义单号：{result.Data.ClientOrderId}");
                        cancelIsolatedOrderIdList.Add((long)result.Data.OrderId);
                        cancelIsolatedClientOrderIdList.Add((long)result.Data.ClientOrderId);
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossOrder>(result, "火币合约API服务器", "【全仓】合约闪电平仓下单");
                }
            }
            #endregion
        }
    }
}
//交易所U本位合约策略订单接口测试-开发中...
static async Task TestUsdtMarginSwapApiStrategyOrderEndpoints()
{
    using (var huobiUsdtMarginedClient = new HuobiClient())
    {
        List<long> cancelIsolatedTriggerOrderIdList = new();
        List<long> cancelIsolatedTpslOrderIdList = new();
        List<long> cancelIsolatedTrackOrderIdList = new();
        List<long> cancelCrossTriggerOrderIdList = new();
        List<long> cancelCrossTpslOrderIdList = new();
        List<long> cancelCrossTrackOrderIdList = new();
        #region 对huobiUsdtMarginedClient客户端的新实例使用新的设置(这里不设置则使用之前的默认设置）
        //使用accessKey, secretKey生成一个新的API凭证
        ApiCredentials apiCredentials = new(mainAccessKey, mainSecretKey);
        //当前客户端使用新生成的API凭证
        huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
        #endregion
        string? read = "";
        Console.WriteLine($"Note: The following operations(triggerOrder,triggerCancel) will generate real order submission, cancellation and other operations, Press [Y] to run test!");
        read = Console.ReadLine();
        if (read == "Y" || read == "y")
        {
            #region 【逐仓】合约计划委托下单(PrivateData)
            {
                //逐仓 20X 双向，卖出开空，高于0.095下单，限价0.10下单2张
                string contractCode = "DOGE-USDT";
                string triggerType = "ge";
                decimal triggerPrice = 0.095M;
                long volume = 2;
                string direction = "sell";
                int? reduceOnly = 0;
                decimal? orderPrice = 0.10M;
                string? orderPriceType = "limit";
                string? offset = "open";
                int? leverRate = 20;
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【逐仓】合约计划委托下单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapTriggerOrderAsync(
                    contractCode, 
                    triggerType, 
                    triggerPrice,
                    volume,
                    direction,
                    reduceOnly,
                    orderPrice,
                    orderPriceType,
                    offset,
                    leverRate
                    );
                if (result.Success)
                {
                    if (result.Data != null && result.Data.OrderId != null)
                    {
                        Console.WriteLine($"订单编号：{result.Data.OrderId}");
                        cancelIsolatedTriggerOrderIdList.Add((long)result.Data.OrderId);
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedTriggerOrder>(result, "火币合约API服务器", "【逐仓】合约计划委托下单");
                }
            }
            #endregion
            #region 【全仓】合约计划委托下单(PrivateData)
            {
                //全仓 20X 双向，卖出开空，高于0.095下单，限价0.10下单2张
                UmTriggerType? triggerType = UmTriggerType.ge;
                decimal triggerPrice = 0.095M;
                long volume = 2;
                UmDirection? direction = UmDirection.sell;
                string? contractCode = "DOGE-USDT";
                string? pair = "DOGE-USDT";
                UmContractType? contractType = UmContractType.swap;
                UmReduceOnly? reduceOnly = UmReduceOnly.NotReduceOnly;
                decimal? orderPrice = 0.10M;
                UmOrderPriceType orderPriceType = UmOrderPriceType.Limit;
                UmOffset offset = UmOffset.open;
                UmLeverRate? leverRate = UmLeverRate.LeverRate_20;
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【全仓】合约计划委托下单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapCrossTriggerOrderAsync(
                    triggerType,
                    triggerPrice,
                    volume,
                    direction,
                    contractCode,
                    pair,
                    contractType,
                    reduceOnly,
                    orderPrice,
                    orderPriceType,
                    offset,
                    leverRate
                    );
                if (result.Success)
                {
                    if (result.Data != null && result.Data.OrderId != null)
                    {
                        Console.WriteLine($"订单编号：{result.Data.OrderId}");
                        cancelCrossTriggerOrderIdList.Add((long)result.Data.OrderId);
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossTriggerOrder>(result, "火币合约API服务器", "【全仓】合约计划委托下单");
                }
            }
            #endregion
            #region 【逐仓】合约计划委托撤单(PrivateData)
            {
                string contractCode = "DOGE-USDT";
                IEnumerable<long> cancelIsolatedTriggerOrderIds = cancelIsolatedTriggerOrderIdList;
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【逐仓】合约计划委托撤单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapTriggerCancelAsync(
                    contractCode,
                    cancelIsolatedTriggerOrderIds
                    );
                if (result.Success)
                {
                    foreach (var item in result.Data.ErrorsList)
                    {
                        Console.WriteLine($"撤销失败合约计划委托订单编号：{item.OrderId}  错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                        if (item.OrderId != null)
                        {
                            cancelIsolatedTriggerOrderIdList.Remove((long)item.OrderId);
                        }
                    }
                    if (result.Data.Successes != null)
                    {
                        Console.WriteLine($"撤销成功合约计划委托订单编号：{result.Data.Successes} ");
                        foreach (var item in result.Data.Successes.Split(",").ToList<string>())
                        {
                            cancelIsolatedTriggerOrderIdList.Remove(long.Parse(item));
                        }
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedTriggerCancel>(result, "火币合约API服务器", "【逐仓】合约计划委托撤单");
                }
            }
            #endregion
            #region 【全仓】合约计划委托撤单(PrivateData)
            {
                IEnumerable<long> cancelCrossTriggerOrderIds = cancelCrossTriggerOrderIdList;
                string? contractCode = "DOGE-USDT";
                string? pair = "DOGE-USDT";
                string? contractType = "swap";                
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【全仓】合约计划委托撤单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapCrossTriggerCancelAsync(
                    cancelCrossTriggerOrderIds,
                    contractCode,
                    pair,
                    contractType
                    );
                if (result.Success)
                {
                    foreach (var item in result.Data.ErrorsList)
                    {
                        Console.WriteLine($"撤销失败合约计划委托订单编号：{item.OrderId}  错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                        if (item.OrderId != null)
                        {
                            cancelCrossTriggerOrderIdList.Remove((long)item.OrderId);
                        }
                    }
                    if (result.Data.Successes != null)
                    {
                        Console.WriteLine($"撤销成功合约计划委托订单编号：{result.Data.Successes} ");
                        foreach (var item in result.Data.Successes.Split(",").ToList<string>())
                        {
                            cancelCrossTriggerOrderIdList.Remove(long.Parse(item));
                        }
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossTriggerCancel>(result, "火币合约API服务器", "【全仓】合约计划委托撤单");
                }
            }
            #endregion
            #region 【逐仓】合约计划委托全部撤单(PrivateData)
            {
                string contractCode = "DOGE-USDT";
                string? direction = null;
                string? offset = null;
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【逐仓】合约计划委托全部撤单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapTriggerCancelAllAsync(
                    contractCode,
                    direction,
                    offset
                    );
                if (result.Success)
                {
                    foreach (var item in result.Data.ErrorsList)
                    {
                        Console.WriteLine($"撤销失败订单编号：{item.OrderId}  错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                        if (item.OrderId != null)
                        {
                            cancelIsolatedTriggerOrderIdList.Remove((long)item.OrderId);
                        }
                    }
                    if (result.Data.Successes != null)
                    {
                        Console.WriteLine($"撤销全部合约计划委托：{result.Data.Successes} ");
                        foreach (var item in result.Data.Successes.Split(",").ToList<string>())
                        {
                            cancelIsolatedTriggerOrderIdList.Remove(long.Parse(item));
                        }
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedTriggerCancel>(result, "火币合约API服务器", "【逐仓】合约计划委托全部撤单");
                }
            }
            #endregion
            #region 【全仓】合约计划委托全部撤单(PrivateData)
            {
                string? contractCode = "DOGE-USDT";
                string? pair = "DOGE-USDT";
                string? contractType = "swap";
                string? direction = "buy";
                string? offset = "open";
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【全仓】合约计划委托全部撤单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapCrossTriggerCancelAllAsync(
                    contractCode,
                    pair,
                    contractType,
                    direction,
                    offset
                    );
                if (result.Success)
                {
                    foreach (var item in result.Data.ErrorsList)
                    {
                        Console.WriteLine($"撤销失败合约计划委托订单编号：{item.OrderId}  错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                        if (item.OrderId != null)
                        {
                            cancelCrossTriggerOrderIdList.Remove((long)item.OrderId);
                        }
                    }
                    if (result.Data.Successes != null)
                    {
                        Console.WriteLine($"撤销成功合约计划委托订单编号：{result.Data.Successes} ");
                        foreach (var item in result.Data.Successes.Split(",").ToList<string>())
                        {
                            cancelCrossTriggerOrderIdList.Remove(long.Parse(item));
                        }
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossTriggerCancel>(result, "火币合约API服务器", "【全仓】合约计划委托全部撤单");
                }
            }
            #endregion
        }
        #region 【逐仓】获取计划委托当前委托(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            int? pageIndex = null;
            int? pageSize = null;
            int? tradeType = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】获取计划委托当前委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.GetLinearSwapTriggerOpenordersAsync(
                contractCode,
                pageIndex,
                pageSize,
                tradeType
                );
            if (result.Success)
            {
                Console.WriteLine($"总页数：{result.Data.TotalPage}  当前页：{result.Data.CurrentPage} 总条数：{result.Data.TotalSize}");
                foreach (var item in result.Data.HuobiUsdtMarginedIsolatedTriggerOpenOrders)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode} 当前杠杆倍数：{item.LeverRate}");
                    Console.WriteLine($"委托数量(张)：{item.Volume} 触发价价格：{item.TriggerPrice} 委托价格：{item.OrderPrice} 订单报价类型：{item.OrderPriceType}\r\n" +
                        $"订单状态：{item.Status} 订单类型：{item.OrderType} 买卖方向：{item.Direction} 开平方向：{item.Offset} \r\n" +
                        $"计划委托订单编号：{item.OrderId} 创建时间戳：{item.CreatedTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedTriggerOpenOrders>(result, "火币合约API服务器", "【逐仓】获取计划委托当前委托");
            }
        }
        #endregion
        #region 【全仓】获取计划委托当前委托(PrivateData)
        {
            string? contractCode = "DOGE-USDT";
            string? pair = "DOGE-USDT";
            int? pageIndex = null;
            int? pageSize = null;
            int? tradeType = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】获取计划委托当前委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.GetLinearSwapCrossTriggerOpenordersAsync(
                contractCode,
                pair,
                pageIndex,
                pageSize,
                tradeType
                );
            if (result.Success)
            {
                Console.WriteLine($"总页数：{result.Data.TotalPage}  当前页：{result.Data.CurrentPage} 总条数：{result.Data.TotalSize}");
                foreach (var item in result.Data.HuobiUsdtMarginedCrossTriggerOpenOrders)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode} 当前杠杆倍数：{item.LeverRate}");
                    Console.WriteLine($"委托数量(张)：{item.Volume} 触发价价格：{item.TriggerPrice} 委托价格：{item.OrderPrice} 订单报价类型：{item.OrderPriceType}\r\n" +
                        $"订单状态：{item.Status} 订单类型：{item.OrderType} 买卖方向：{item.Direction} 开平方向：{item.Offset} \r\n" +
                        $"计划委托订单编号：{item.OrderId} 创建时间戳：{item.CreatedTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossTriggerOpenOrders>(result, "火币合约API服务器", "【全仓】合约计划委托全部撤单");
            }
        }
        #endregion
        #region 【逐仓】获取计划委托历史委托(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            int tradeType = 0;
            string status = "0";
            int createDate = 90;
            int? pageIndex = null;
            int? pageSize = null;
            string? sortBy = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】获取计划委托历史委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.GetLinearSwapTriggerHisordersAsync(
                contractCode,
                tradeType,
                status,
                createDate,
                pageIndex,
                pageSize,
                sortBy
                );
            if (result.Success)
            {
                Console.WriteLine($"总页数：{result.Data.TotalPage}  当前页：{result.Data.CurrentPage} 总条数：{result.Data.TotalSize}");
                foreach (var item in result.Data.HuobiUsdtMarginedIsolatedTriggerHisorders)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode} 当前杠杆倍数：{item.LeverRate}");
                    Console.WriteLine($"委托数量(张)：{item.Volume} 触发价价格：{item.TriggerPrice} 委托价格：{item.OrderPrice} 订单报价类型：{item.OrderPriceType}\r\n" +
                        $"订单状态：{item.Status} 订单类型：{item.OrderType} 买卖方向：{item.Direction} 开平方向：{item.Offset} \r\n" +
                        $"计划委托订单编号：{item.OrderId} 创建时间戳：{item.CreatedTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedTriggerHisorders>(result, "火币合约API服务器", "【逐仓】获取计划委托历史委托");
            }
        }
        #endregion
        #region 【全仓】获取计划委托历史委托(PrivateData)
        {
            int tradeType = 0;
            string status = "0";
            int createDate = 90;
            string? contractCode = "DOGE-USDT";
            string? pair = "DOGE-USDT";
            int? pageIndex = null;
            int? pageSize = null;
            string? sortBy = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】获取计划委托历史委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.GetLinearSwapCrossTriggerHisordersAsync(
                tradeType,
                status,
                createDate,
                contractCode,
                pair,
                pageIndex,
                pageSize,
                sortBy
                );
            if (result.Success)
            {
                Console.WriteLine($"总页数：{result.Data.TotalPage}  当前页：{result.Data.CurrentPage} 总条数：{result.Data.TotalSize}");
                foreach (var item in result.Data.HuobiUsdtMarginedCrossOpenOrders)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode} 当前杠杆倍数：{item.LeverRate}");
                    Console.WriteLine($"委托数量(张)：{item.Volume} 触发价价格：{item.TriggerPrice} 委托价格：{item.OrderPrice} 订单报价类型：{item.OrderPriceType}\r\n" +
                        $"订单状态：{item.Status} 订单类型：{item.OrderType} 买卖方向：{item.Direction} 开平方向：{item.Offset} \r\n" +
                        $"计划委托订单编号：{item.OrderId} 创建时间戳：{item.CreatedTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossTriggerHisorders>(result, "火币合约API服务器", "【全仓】获取计划委托历史委托");
            }
        }
        #endregion
        Console.WriteLine($"Note: The following operations(TpslOrder,TpslCancel) will generate real order submission, cancellation and other operations, Press [Y] to run test!");
        read = Console.ReadLine();
        if (read == "Y" || read == "y")
        {
            #region 【逐仓】对仓位设置止盈止损订单(PrivateData)
            {
                string contractCode = "DOGE-USDT";
                UmDirection direction = UmDirection.sell;
                decimal volume = 2;
                decimal? tpTriggerPrice = 0.095M;
                decimal? tpOrderPrice = 0.10M;
                UmOrderPriceType tpOrderPriceType = UmOrderPriceType.Limit;
                decimal? slTriggerPrice = 0.045M;
                decimal? slOrderPrice = 0.04M;
                UmOrderPriceType slOrderPriceType = UmOrderPriceType.Limit;
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【逐仓】对仓位设置止盈止损订单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapTpslOrderAsync(
                    contractCode,
                    direction,
                    volume,
                    tpTriggerPrice,
                    tpOrderPrice,
                    tpOrderPriceType,
                    slTriggerPrice,
                    slOrderPrice,
                    slOrderPriceType
                    );
                if (result.Success)
                {
                    Console.WriteLine($"【逐仓】止盈止损订单下单结果");
                    if (result.Data.TpOrder != null && result.Data.TpOrder.OrderId != null)
                    {
                        Console.WriteLine($"止盈订单编号：{result.Data.TpOrder.OrderId}");
                        cancelIsolatedTpslOrderIdList.Add((long)result.Data.TpOrder.OrderId);
                    }
                    if (result.Data.SlOrder != null && result.Data.SlOrder.OrderId != null)
                    {
                        Console.WriteLine($"止损订单编号：{result.Data.SlOrder.OrderId}");
                        cancelIsolatedTpslOrderIdList.Add((long)result.Data.SlOrder.OrderId);
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedTpslOrder>(result, "火币合约API服务器", "【逐仓】对仓位设置止盈止损订单");
                }
            }
            #endregion
            #region 【全仓】对仓位设置止盈止损订单(PrivateData)
            {
                UmDirection? direction = UmDirection.sell;
                decimal volume = 2;
                string? contractCode = "DOGE-USDT";
                string? pair = "DOGE-USDT";
                UmContractType contractType = UmContractType.swap;
                decimal? tpTriggerPrice = 0.095M;
                decimal? tpOrderPrice = 0.10M;
                UmOrderPriceType tpOrderPriceType = UmOrderPriceType.Limit;
                decimal? slTriggerPrice = 0.045M;
                decimal? slOrderPrice = 0.04M;
                UmOrderPriceType slOrderPriceType = UmOrderPriceType.Limit;
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【全仓】对仓位设置止盈止损订单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapCrossTpslOrderAsync(
                    direction,
                    volume,
                    contractCode,
                    pair,
                    contractType,
                    tpTriggerPrice,
                    tpOrderPrice,
                    tpOrderPriceType,
                    slTriggerPrice,
                    slOrderPrice,
                    slOrderPriceType
                    );
                if (result.Success)
                {
                    Console.WriteLine($"【全仓】止盈止损订单下单结果");
                    if (result.Data.TpOrder != null && result.Data.TpOrder.OrderId != null)
                    {
                        Console.WriteLine($"止盈订单编号：{result.Data.TpOrder.OrderId}");
                        cancelCrossTpslOrderIdList.Add((long)result.Data.TpOrder.OrderId);
                    }
                    if (result.Data.SlOrder != null && result.Data.SlOrder.OrderId != null)
                    {
                        Console.WriteLine($"止损订单编号：{result.Data.SlOrder.OrderId}");
                        cancelCrossTpslOrderIdList.Add((long)result.Data.SlOrder.OrderId);
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossTpslOrder>(result, "火币合约API服务器", "【全仓】对仓位设置止盈止损订单");
                }
            }
            #endregion
            #region 【逐仓】止盈止损订单撤单(PrivateData)
            {
                string contractCode = "DOGE-USDT";
                IEnumerable<long> cancelIsolatedTpslOrderIds = cancelIsolatedTpslOrderIdList;
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                if (cancelIsolatedTpslOrderIdList != null && cancelIsolatedTpslOrderIdList.Any())
                {
                    Console.WriteLine("【逐仓】止盈止损订单撤单");
                    var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapTpslCancelAsync(
                        contractCode,
                        cancelIsolatedTpslOrderIds
                        );
                    if (result.Success)
                    {
                        foreach (var item in result.Data.ErrorsList)
                        {
                            Console.WriteLine($"撤销失败止盈止损订单编号：{item.OrderId}  错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                            if (item.OrderId != null)
                            {
                                cancelIsolatedTpslOrderIdList.Remove((long)item.OrderId);
                            }
                        }
                        if (result.Data.Successes != null)
                        {
                            Console.WriteLine($"撤销成功止盈止损订单编号：{result.Data.Successes} ");
                            foreach (var item in result.Data.Successes.Split(",").ToList<string>())
                            {
                                cancelIsolatedTpslOrderIdList.Remove(long.Parse(item));
                            }
                        }
                    }
                    else
                    {
                        ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedTpslCancel>(result, "火币合约API服务器", "【逐仓】止盈止损订单撤单");
                    }
                }
            }
            #endregion
            #region 【全仓】止盈止损订单撤单(PrivateData)
            {
                IEnumerable<long> cancelCrossTpslOrderIds = cancelCrossTpslOrderIdList;
                string? contractCode = "DOGE-USDT";
                string? pair = "DOGE-USDT";
                string? contractType = "swap";
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                if (cancelCrossTpslOrderIdList != null && cancelCrossTpslOrderIdList.Any())
                {
                    Console.WriteLine("【全仓】止盈止损订单撤单");
                    var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapCrossTpslCancelAsync(
                        cancelCrossTpslOrderIds,
                        contractCode,
                        pair,
                        contractType
                        );
                    if (result.Success)
                    {
                        foreach (var item in result.Data.ErrorsList)
                        {
                            Console.WriteLine($"撤销失败止盈止损订单编号：{item.OrderId}  错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                            if (item.OrderId != null)
                            {
                                cancelCrossTpslOrderIdList.Remove((long)item.OrderId);
                            }
                        }
                        if (result.Data.Successes != null)
                        {
                            Console.WriteLine($"撤销成功止盈止损订单编号：{result.Data.Successes} ");
                            foreach (var item in result.Data.Successes.Split(",").ToList<string>())
                            {
                                cancelCrossTpslOrderIdList.Remove(long.Parse(item));
                            }
                        }
                    }
                    else
                    {
                        ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossTpslCancel>(result, "火币合约API服务器", "【全仓】止盈止损订单撤单");
                    }
                }
            }
            #endregion
            #region 【逐仓】止盈止损订单全部撤单(PrivateData)
            {
                string contractCode = "DOGE-USDT";
                string? direction = "buy";
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                if (cancelIsolatedTpslOrderIdList != null && cancelIsolatedTpslOrderIdList.Any())
                {
                    Console.WriteLine("【逐仓】止盈止损订单全部撤单");
                    var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapTpslCancelAllAsync(
                        contractCode,
                        direction
                        );
                    if (result.Success)
                    {
                        foreach (var item in result.Data.ErrorsList)
                        {
                            Console.WriteLine($"撤销失败止盈止损订单编号：{item.OrderId}  错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                            if (item.OrderId != null)
                            {
                                cancelIsolatedTpslOrderIdList.Remove((long)item.OrderId);
                            }
                        }
                        if (result.Data.Successes != null)
                        {
                            Console.WriteLine($"撤销成功止盈止损订单编号：{result.Data.Successes} ");
                            foreach (var item in result.Data.Successes.Split(",").ToList<string>())
                            {
                                cancelIsolatedTpslOrderIdList.Remove(long.Parse(item));
                            }
                        }
                    }
                    else
                    {
                        ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedTpslCancel>(result, "火币合约API服务器", "【逐仓】止盈止损订单全部撤单");
                    }
                }
            }
            #endregion
            #region 【全仓】止盈止损订单全部撤单(PrivateData)
            {
                string? contractCode = "DOGE-USDT";
                string? pair = "DOGE-USDT";
                string? contractType = "swap";
                string? direction = "buy";
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                if (cancelCrossTpslOrderIdList != null && cancelCrossTpslOrderIdList.Any())
                {
                    Console.WriteLine("【全仓】止盈止损订单全部撤单");
                    var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapCrossTpslCancelAllAsync(
                        contractCode,
                        pair,
                        contractType,
                        direction
                        );
                    if (result.Success)
                    {
                        foreach (var item in result.Data.ErrorsList)
                        {
                            Console.WriteLine($"撤销失败止盈止损订单编号：{item.OrderId}  错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                            if (item.OrderId != null)
                            {
                                cancelCrossTpslOrderIdList.Remove((long)item.OrderId);
                            }
                        }
                        if (result.Data.Successes != null)
                        {
                            Console.WriteLine($"撤销成功止盈止损订单编号：{result.Data.Successes} ");
                            foreach (var item in result.Data.Successes.Split(",").ToList<string>())
                            {
                                cancelCrossTpslOrderIdList.Remove(long.Parse(item));
                            }
                        }
                    }
                    else
                    {
                        ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossTpslCancel>(result, "火币合约API服务器", "【全仓】止盈止损订单全部撤单");
                    }
                }
            }
            #endregion
        }
        #region 【逐仓】查询止盈止损订单当前委托(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            int? pageIndex = null;
            int? pageSize = null;
            int? tradeType = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】查询止盈止损订单当前委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.GetLinearSwapTpslOpenordersAsync(
                contractCode,
                pageIndex,
                pageSize,
                tradeType
                );
            if (result.Success)
            {
                Console.WriteLine($"总页数：{result.Data.TotalPage}  当前页：{result.Data.CurrentPage} 总条数：{result.Data.TotalSize}");
                foreach (var item in result.Data.HuobiUsdtMarginedIsolatedTpslOpenOrders)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode}");
                    Console.WriteLine($"委托数量(张)：{item.Volume} 触发价价格：{item.TriggerPrice} 委托价格：{item.OrderPrice} 订单报价类型：{item.OrderPriceType}\r\n" +
                        $"订单状态：{item.Status} 订单类型：{item.OrderType} 买卖方向：{item.Direction}\r\n" +
                        $"止盈止损订单编号：{item.OrderId} 源限价单的订单：{item.SourceOrderId} 关联的止盈止损单的订单{item.RelationTpslOrderId} 创建时间戳：{item.CreatedTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedTpslOpenOrders>(result, "火币合约API服务器", "【逐仓】查询止盈止损订单当前委托");
            }
        }
        #endregion
        #region 【全仓】查询止盈止损订单当前委托(PrivateData)
        {
            string? contractCode = "DOGE-USDT";
            string? pair = "DOGE-USDT";
            int? pageIndex = null;
            int? pageSize = null;
            int? tradeType = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】查询止盈止损订单当前委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.GetLinearSwapCrossTpslOpenordersAsync(
                contractCode,
                pair,
                pageIndex,
                pageSize,
                tradeType
                );
            if (result.Success)
            {
                Console.WriteLine($"总页数：{result.Data.TotalPage}  当前页：{result.Data.CurrentPage} 总条数：{result.Data.TotalSize}");
                foreach (var item in result.Data.HuobiUsdtMarginedCrossTpslOpenOrders)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode}");
                    Console.WriteLine($"委托数量(张)：{item.Volume} 触发价价格：{item.TriggerPrice} 委托价格：{item.OrderPrice} 订单报价类型：{item.OrderPriceType}\r\n" +
                        $"订单状态：{item.Status} 订单类型：{item.OrderType} 买卖方向：{item.Direction}\r\n" +
                        $"止盈止损订单编号：{item.OrderId} 源限价单的订单：{item.SourceOrderId} 关联的止盈止损单的订单{item.RelationTpslOrderId} 创建时间戳：{item.CreatedTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossTpslOpenOrders>(result, "火币合约API服务器", "【全仓】查询止盈止损订单当前委托");
            }
        }
        #endregion
        #region 【逐仓】查询止盈止损订单历史委托(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            string status = "0";
            long createDate = 90;
            int? pageIndex = null;
            int? pageSize = null;
            string? sortBy = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】查询止盈止损订单历史委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.GetLinearSwapTpslHisordersAsync(
                contractCode,
                status,
                createDate,
                pageIndex,
                pageSize,
                sortBy
                );
            if (result.Success)
            {
                Console.WriteLine($"总页数：{result.Data.TotalPage}  当前页：{result.Data.CurrentPage} 总条数：{result.Data.TotalSize}");
                foreach (var item in result.Data.HuobiUsdtMarginedIsolatedTpslHisOrders)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode}");
                    Console.WriteLine($"委托数量(张)：{item.Volume} 触发价价格：{item.TriggerPrice} 委托价格：{item.OrderPrice} 订单报价类型：{item.OrderPriceType}\r\n" +
                        $"订单状态：{item.Status} 订单类型：{item.OrderType} 买卖方向：{item.Direction}\r\n" +
                        $"止盈止损订单编号：{item.OrderId} 源限价单的订单：{item.SourceOrderId} 关联的止盈止损单的订单{item.RelationTpslOrderId} 创建时间戳：{item.CreatedTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedTpslHisorders>(result, "火币合约API服务器", "【逐仓】查询止盈止损订单历史委托");
            }
        }
        #endregion
        #region 【全仓】查询止盈止损订单历史委托(PrivateData)
        {
            string status = "0";
            long createDate = 90;
            string? contractCode = "DOGE-USDT";
            string? pair = "DOGE-USDT";
            int? pageIndex = null;
            int? pageSize = null;
            string? sortBy = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】查询止盈止损订单历史委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.GetLinearSwapCrossTpslHisordersAsync(
                status,
                createDate,
                contractCode,
                pair,
                pageIndex,
                pageSize,
                sortBy
                );
            if (result.Success)
            {
                Console.WriteLine($"总页数：{result.Data.TotalPage}  当前页：{result.Data.CurrentPage} 总条数：{result.Data.TotalSize}");
                foreach (var item in result.Data.HuobiUsdtMarginedCrossTpslHisOrders)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode}");
                    Console.WriteLine($"委托数量(张)：{item.Volume} 触发价价格：{item.TriggerPrice} 委托价格：{item.OrderPrice} 订单报价类型：{item.OrderPriceType}\r\n" +
                        $"订单状态：{item.Status} 订单类型：{item.OrderType} 买卖方向：{item.Direction}\r\n" +
                        $"止盈止损订单编号：{item.OrderId} 源限价单的订单：{item.SourceOrderId} 关联的止盈止损单的订单{item.RelationTpslOrderId} 创建时间戳：{item.CreatedTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossTpslHisorders>(result, "火币合约API服务器", "【全仓】查询止盈止损订单历史委托");
            }
        }
        #endregion
        #region 【逐仓】查询开仓单关联的止盈止损订单详情(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            long orderId = 123456789;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】查询开仓单关联的止盈止损订单详情");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.GetLinearSwapRelationTpslOrderAsync(
                contractCode,
                orderId
                );
            if (result.Success)
            {
                Console.WriteLine($"合约代码：{result.Data.ContractCode}  保证金模式：{result.Data.MarginMode} 保证金账户：{result.Data.MarginAccount}");
                Console.WriteLine($"委托数量：{result.Data.Volume}  委托价格：{result.Data.Price} 订单报价类型：{result.Data.OrderPriceType}");
                Console.WriteLine($"买卖方向：{result.Data.Direction}  开平方向：{result.Data.Offset} 杠杆倍数：{result.Data.LeverRate}");
                Console.WriteLine($"订单编号：{result.Data.OrderId}  客户订单编号：{result.Data.ClientOrderId}");
                Console.WriteLine($"创建时间：{result.Data.CreatedTimestamp}  成交数量：{result.Data.TradeVolume} 成交总金额：{result.Data.TradeTurnover}");
                foreach (var item in result.Data.HuobiUsdtMarginedIsolatedTpslOrderInfos)
                {
                    Console.WriteLine($"关联订单信息：{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedRelationTpslOrder>(result, "火币合约API服务器", "【逐仓】查询开仓单关联的止盈止损订单详情");
            }
        }
        #endregion
        #region 【全仓】查询开仓单关联的止盈止损订单详情(PrivateData)
        {
            long orderId = 123456789;
            string? contractCode = "DOGE-USDT";
            string? pair = "DOGE-USDT";
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】查询开仓单关联的止盈止损订单详情");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.GetLinearSwapCrossRelationTpslOrderAsync(
                orderId,
                contractCode,
                pair
                );
            if (result.Success)
            {
                Console.WriteLine($"合约代码：{result.Data.ContractCode}  保证金模式：{result.Data.MarginMode} 保证金账户：{result.Data.MarginAccount}");
                Console.WriteLine($"委托数量：{result.Data.Volume}  委托价格：{result.Data.Price} 订单报价类型：{result.Data.OrderPriceType}");
                Console.WriteLine($"买卖方向：{result.Data.Direction}  开平方向：{result.Data.Offset} 杠杆倍数：{result.Data.LeverRate}");
                Console.WriteLine($"订单编号：{result.Data.OrderId}  客户订单编号：{result.Data.ClientOrderId}");
                Console.WriteLine($"创建时间：{result.Data.CreatedTimestamp}  成交数量：{result.Data.TradeVolume} 成交总金额：{result.Data.TradeTurnover}");
                foreach (var item in result.Data.HuobiUsdtMarginedCrossTpslOrderInfos)
                {
                    Console.WriteLine($"关联订单信息：{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossRelationTpslOrder>(result, "火币合约API服务器", "【全仓】查询开仓单关联的止盈止损订单详情");
            }
        }
        #endregion
        Console.WriteLine($"Note: The following operations(TrackOrder,TrackCancel) will generate real order submission, cancellation and other operations, Press [Y] to run test!");
        read = Console.ReadLine();
        if (read == "Y" || read == "y")
        {
            #region 【逐仓】跟踪委托订单下单(PrivateData)
            {
                string contractCode = "DOGE-USDT";
                string direction = "sell";
                int volume = 2;
                decimal callbackRate = 0.005M;
                decimal activePrice = 0.95M;
                string orderPriceType = "formula_price";
                int? reduceOnly = 0;
                int? leverRate = 20;
                string? offset = "open";
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【逐仓】跟踪委托订单下单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapTrackOrderAsync(
                    contractCode,
                    direction,
                    volume,
                    callbackRate,
                    activePrice,
                    orderPriceType,
                    reduceOnly,
                    leverRate,
                    offset
                    );
                if (result.Success)
                {
                    if (result.Data != null && result.Data.OrderId != null)
                    {
                        Console.WriteLine($"订单编号：{result.Data.OrderId}");
                        cancelIsolatedTrackOrderIdList.Add((long)result.Data.OrderId);
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedTrackOrder>(result, "火币合约API服务器", "【逐仓】跟踪委托订单下单");
                }
            }
            #endregion
            #region 【全仓】跟踪委托订单下单(PrivateData)
            {
                UmDirection? direction = UmDirection.sell;
                int volume = 2;
                decimal callbackRate = 0.005M;
                decimal activePrice = 0.95M;
                UmOrderPriceType? orderPriceType = UmOrderPriceType.Optimal5;
                string? contractCode = "DOGE-USDT";
                string? pair = "DOGE-USDT";
                UmContractType? contractType = UmContractType.swap;
                UmReduceOnly? reduceOnly = UmReduceOnly.NotReduceOnly;
                UmOffset? offset = UmOffset.open;
                UmLeverRate? leverRate = UmLeverRate.LeverRate_20;
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【全仓】跟踪委托订单下单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapCrossTrackOrderAsync(
                    direction,
                    volume,
                    callbackRate,
                    activePrice,
                    orderPriceType,
                    contractCode,
                    pair,
                    contractType,
                    reduceOnly,
                    offset,
                    leverRate
                    );
                if (result.Success)
                {
                    if (result.Data != null && result.Data.OrderId != null)
                    {
                        Console.WriteLine($"订单编号：{result.Data.OrderId}");
                        cancelCrossTrackOrderIdList.Add((long)result.Data.OrderId);
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossTrackOrder>(result, "火币合约API服务器", "【全仓】跟踪委托订单下单");
                }
            }
            #endregion
            #region 【逐仓】跟踪委托订单撤单(PrivateData)
            {
                string contractCode = "DOGE-USDT";
                IEnumerable<long> cancelIsolatedTrackOrderIds = cancelIsolatedTrackOrderIdList;
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【逐仓】跟踪委托订单撤单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapTrackCancelAsync(
                    contractCode,
                    cancelIsolatedTrackOrderIds
                    );
                if (result.Success)
                {
                    foreach (var item in result.Data.ErrorsList)
                    {
                        Console.WriteLine($"撤销失败跟踪委托订单订单编号：{item.OrderId}  错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                        if (item.OrderId != null)
                        {
                            cancelIsolatedTriggerOrderIdList.Remove((long)item.OrderId);
                        }
                    }
                    if (result.Data.Successes != null)
                    {
                        Console.WriteLine($"撤销成功跟踪委托订单订单编号：{result.Data.Successes} ");
                        foreach (var item in result.Data.Successes.Split(",").ToList<string>())
                        {
                            cancelIsolatedTriggerOrderIdList.Remove(long.Parse(item));
                        }
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedTrackCancel>(result, "火币合约API服务器", "【逐仓】跟踪委托订单撤单");
                }
            }
            #endregion
            #region 【全仓】跟踪委托订单撤单(PrivateData)
            {
                IEnumerable<long> cancelCrossTrackOrderIds = cancelCrossTrackOrderIdList;
                string? contractCode = "DOGE-USDT";
                string? pair = "DOGE-USDT";
                string? contractType = "swap";
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【全仓】跟踪委托订单撤单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapCrossTrackCancelAsync(
                    cancelCrossTrackOrderIds,
                    contractCode,
                    pair,
                    contractType
                    );
                if (result.Success)
                {
                    foreach (var item in result.Data.ErrorsList)
                    {
                        Console.WriteLine($"撤销失败跟踪委托订单订单编号：{item.OrderId}  错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                        if (item.OrderId != null)
                        {
                            cancelIsolatedTriggerOrderIdList.Remove((long)item.OrderId);
                        }
                    }
                    if (result.Data.Successes != null)
                    {
                        Console.WriteLine($"撤销成功跟踪委托订单订单编号：{result.Data.Successes} ");
                        foreach (var item in result.Data.Successes.Split(",").ToList<string>())
                        {
                            cancelIsolatedTriggerOrderIdList.Remove(long.Parse(item));
                        }
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossTrackCancel>(result, "火币合约API服务器", "【全仓】跟踪委托订单撤单");
                }
            }
            #endregion
            #region 【逐仓】跟踪委托订单全部撤单(PrivateData)
            {
                string contractCode = "DOGE-USDT";
                string? direction = null;
                string? offset = null;
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【逐仓】跟踪委托订单全部撤单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapTrackCancelAllAsync(
                    contractCode,
                    direction,
                    offset
                    );
                if (result.Success)
                {
                    foreach (var item in result.Data.ErrorsList)
                    {
                        Console.WriteLine($"撤销失败跟踪委托订单订单编号：{item.OrderId}  错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                        if (item.OrderId != null)
                        {
                            cancelIsolatedTriggerOrderIdList.Remove((long)item.OrderId);
                        }
                    }
                    if (result.Data.Successes != null)
                    {
                        Console.WriteLine($"撤销成功跟踪委托订单订单编号：{result.Data.Successes} ");
                        foreach (var item in result.Data.Successes.Split(",").ToList<string>())
                        {
                            cancelIsolatedTriggerOrderIdList.Remove(long.Parse(item));
                        }
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedTrackCancel>(result, "火币合约API服务器", "【逐仓】跟踪委托订单全部撤单");
                }
            }
            #endregion
            #region 【全仓】跟踪委托订单全部撤单(PrivateData)
            {
                string? contractCode = "DOGE-USDT";
                string? pair = "DOGE-USDT";
                string? contractType = "swap";
                string? direction = "buy";
                string? offset = "open";
                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion
                Console.WriteLine("【全仓】跟踪委托订单全部撤单");
                var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.LinearSwapCrossTrackCancelAllAsync(
                    contractCode,
                    pair,
                    contractType,
                    direction,
                    offset
                    );
                if (result.Success)
                {
                    foreach (var item in result.Data.ErrorsList)
                    {
                        Console.WriteLine($"撤销失败跟踪委托订单订单编号：{item.OrderId}  错误码：{item.ErrCode} 错误信息：{item.ErrMsg}");
                        if (item.OrderId != null)
                        {
                            cancelIsolatedTriggerOrderIdList.Remove((long)item.OrderId);
                        }
                    }
                    if (result.Data.Successes != null)
                    {
                        Console.WriteLine($"撤销成功跟踪委托订单订单编号：{result.Data.Successes} ");
                        foreach (var item in result.Data.Successes.Split(",").ToList<string>())
                        {
                            cancelIsolatedTriggerOrderIdList.Remove(long.Parse(item));
                        }
                    }
                }
                else
                {
                    ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossTrackCancel>(result, "火币合约API服务器", "【全仓】跟踪委托订单全部撤单");
                }
            }
            #endregion
        }
        #region 【逐仓】跟踪委托订单当前委托(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            int? tradeType = null;
            int? pageIndex = null;
            int? pageSize = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】跟踪委托订单当前委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.GetLinearSwapTrackOpenOrdersAsync(
                contractCode,
                tradeType,
                pageIndex,
                pageSize
                );
            if (result.Success)
            {
                Console.WriteLine($"总页数：{result.Data.TotalPage}  当前页：{result.Data.CurrentPage} 总条数：{result.Data.TotalSize}");
                foreach (var item in result.Data.HuobiUsdtMarginedIsolatedTrackOpenOrders)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode}");
                    Console.WriteLine($"委托数量(张)：{item.Volume} 激活价格：{item.ActivePrice} 回调幅度：{item.CallbackRate} 激活价格是否已激活：{item.IsActive}\r\n" +
                        $"订单状态：{item.Status} 订单类型：{item.OrderType} 买卖方向：{item.Direction}\r\n" +
                        $"跟踪委托订单编号：{item.OrderId} 创建时间戳：{item.CreatedTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedTrackOpenOrders>(result, "火币合约API服务器", "【逐仓】跟踪委托订单当前委托");
            }
        }
        #endregion
        #region 【全仓】跟踪委托订单当前委托(PrivateData)
        {
            string? contractCode = "DOGE-USDT";
            string? pair = "DOGE-USDT";
            int? tradeType = null;
            int? pageIndex = null;
            int? pageSize = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】跟踪委托订单当前委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.GetLinearSwapCrossTrackOpenordersAsync(
                contractCode,
                pair,
                tradeType,
                pageIndex,
                pageSize
                );
            if (result.Success)
            {
                Console.WriteLine($"总页数：{result.Data.TotalPage}  当前页：{result.Data.CurrentPage} 总条数：{result.Data.TotalSize}");
                foreach (var item in result.Data.HuobiUsdtMarginedCrossTrackOpenOrders)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode}");
                    Console.WriteLine($"委托数量(张)：{item.Volume} 激活价格：{item.ActivePrice} 回调幅度：{item.CallbackRate} 激活价格是否已激活：{item.IsActive}\r\n" +
                      $"订单状态：{item.Status} 订单类型：{item.OrderType} 买卖方向：{item.Direction}\r\n" +
                      $"跟踪委托订单编号：{item.OrderId} 创建时间戳：{item.CreatedTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossTrackOpenOrders>(result, "火币合约API服务器", "【全仓】跟踪委托订单当前委托");
            }
        }
        #endregion
        #region 【逐仓】跟踪委托订单历史委托(PrivateData)
        {
            string contractCode = "DOGE-USDT";
            string status = "0";
            int tradeType = 0;
            int createDate = 90;
            int? pageIndex = null;
            int? pageSize = null;
            string? sortBy = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【逐仓】跟踪委托订单历史委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.GetLinearSwapTrackHisordersAsync(
                contractCode,
                status,
                tradeType,
                createDate,
                pageIndex,
                pageSize,
                sortBy
                );
            if (result.Success)
            {
                Console.WriteLine($"总页数：{result.Data.TotalPage}  当前页：{result.Data.CurrentPage} 总条数：{result.Data.TotalSize}");
                foreach (var item in result.Data.HuobiUsdtMarginedIsolatedTrackHisorders)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode}");
                    Console.WriteLine($"委托数量(张)：{item.Volume} 激活价格：{item.ActivePrice} 回调幅度：{item.CallbackRate} 激活价格是否已激活：{item.IsActive}\r\n" +
                       $"订单状态：{item.Status} 订单类型：{item.OrderType} 买卖方向：{item.Direction}\r\n" +
                       $"跟踪委托订单编号：{item.OrderId} 市场最低/最高价：{item.MarketLimitPrice} 理论价格{item.FormulaPrice} 创建时间戳：{item.CreatedTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapIsolatedTrackHisorders>(result, "火币合约API服务器", "【逐仓】跟踪委托订单历史委托");
            }
        }
        #endregion
        #region 【全仓】跟踪委托订单历史委托(PrivateData)
        {
            string status = "0";
            int tradeType = 0;
            int createDate = 90;
            string? contractCode = "DOGE-USDT";
            string? pair = "DOGE-USDT";
            int? pageIndex = null;
            int? pageSize = null;
            string? sortBy = null;
            #region 母用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】跟踪委托订单历史委托");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Strategy.GetLinearSwapCrossTrackHisordersAsync(
                status,
                tradeType,
                createDate,
                contractCode,
                pair,
                pageIndex,
                pageSize,
                sortBy
                );
            if (result.Success)
            {
                Console.WriteLine($"总页数：{result.Data.TotalPage}  当前页：{result.Data.CurrentPage} 总条数：{result.Data.TotalSize}");
                foreach (var item in result.Data.HuobiUsdtMarginedCrossTrackHisorders)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}  保证金模式：{item.MarginMode}");
                    Console.WriteLine($"委托数量(张)：{item.Volume} 激活价格：{item.ActivePrice} 回调幅度：{item.CallbackRate} 激活价格是否已激活：{item.IsActive}\r\n" +
                        $"订单状态：{item.Status} 订单类型：{item.OrderType} 买卖方向：{item.Direction}\r\n" +
                        $"跟踪委托订单编号：{item.OrderId} 市场最低/最高价：{item.MarketLimitPrice} 理论价格{item.FormulaPrice} 创建时间戳：{item.CreatedTimestamp} 订单来源：{item.OrderSource}");
                }
            }
            else
            {
                ErrorInfoOutput<HuobiUsdtMarginedMarketSwapCrossTrackHisorders>(result, "火币合约API服务器", "【全仓】跟踪委托订单历史委托");
            }
        }
        #endregion
    }
}
//交易所U本位合约划转接口测试-已完成
static async Task TestUsdtMarginSwapApiTransferringEndpoints()
{
    using (var huobiUsdtMarginedClient = new HuobiClient())
    {
        #region 对huobiUsdtMarginedClient客户端的新实例使用新的设置(这里不设置则使用之前的默认设置）
        //使用accessKey, secretKey生成一个新的API凭证
        ApiCredentials apiCredentials = new(mainAccessKey, mainSecretKey);
        //当前客户端使用新生成的API凭证
        huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
        #endregion
        #region 【全仓】查询系统划转权限(PrivateData)
        {
            string? marginAccount = "USDT";
            #region 主用户客户端
            apiCredentials = new(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【全仓】查询系统划转权限");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Transferring.GetLinearSwapCrossTransferStateAsync(marginAccount);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"保证金账户：{item.MarginAccount} 保证金模式：{item.MarginMode}");
                    Console.WriteLine($"从币币转入的权限：{(item.TransferIn == 1 ? "可用": (item.TransferIn == 0 ? "不可用" : "未知"))}");
                    Console.WriteLine($"转出至币币的权限：{(item.TransferOut == 1 ? "可用" : (item.TransferOut == 0 ? "不可用" : "未知"))}");
                    Console.WriteLine($"从母账号划转到子账号的权限：{(item.MasterTransferSub == 1 ? "可用" : (item.MasterTransferSub == 0 ? "不可用" : "未知"))}");
                    Console.WriteLine($"从子账号划转到母账号的权限：{(item.SubTransferMaster == 1 ? "可用" : (item.SubTransferMaster == 0 ? "不可用" : "未知"))}");
                    Console.WriteLine($"同账号不同保证金账户划转的转入权限：{(item.TransferInnerIn == 1 ? "可用" : (item.TransferIn == 0 ? "不可用" : "未知"))}");
                    Console.WriteLine($"同账号不同保证金账户划转的转出权限：{(item.TransferInnerOut == 1 ? "可用" : (item.TransferIn == 0 ? "不可用" : "未知"))}");
                }
            }
            else
            {
                ErrorInfoOutput<IEnumerable<HuobiUsdtMarginedSwapCrossTransferState>>(result, "火币合约API服务器", "【全仓】查询系统划转权限");
            }
        }
        #endregion
        #region 【通用】现货-U本位合约账户间进行资金的划转(PrivateData)
        {
            string from = "spot";
            string to = "linear-swap";
            string currency = "usdt";
            decimal amount = 20;
            string marginAccount = "USDT";
            #region 主用户客户端
            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion
            Console.WriteLine("【通用】现货-U本位合约账户间进行资金的划转");
            var result = await huobiUsdtMarginedClient.UsdtMarginSwapApi.Transferring.LinearSwapAccountTransferAsync(from, to, currency, amount, marginAccount);
            if (result.Success)
            {
                Console.WriteLine($"从{from}账户划转{amount} {currency}到{to} {marginAccount}账户成功，划转单号：{result.Data}");
            }
            else
            {
                ErrorInfoOutput<long>(result, "火币合约API服务器", "【通用】现货-U本位合约账户间进行资金的划转");
            }
        }
        #endregion
    }
}
static async Task HandleRequest<T>(string action, Func<Task<WebCallResult<T>>> request, Func<T, string> outputData)
{
    Console.WriteLine("Press enter to continue");
    Console.ReadLine();
    Console.Clear();
    Console.WriteLine("Requesting " + action + " ..");
    var result = await request();
    if (result.Success)
    {
        Console.WriteLine($"{action}: " + outputData(result.Data));
    }
    else
    {
        if (result.ResponseStatusCode == System.Net.HttpStatusCode.OK && result.Error.Code == null && result.Data == null)
        {
            Console.WriteLine($"No related records found");
        }
        else
        {
            Log _log = new("HandleRequest");
            _log.Write(LogLevel.Error, $"Failed to retrieve data: {result.Error}");
        }
        Console.WriteLine();
    }
}
Console.ReadLine();
