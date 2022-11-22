﻿using Huobi.Net.Clients;
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
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapTrade.Request;
using System.Linq;

#region Provide you API key/secret in these fields to retrieve data related to your account
const string mainAccessKey = "Use Your Exchange Main Account Access Key";
const string mainSecretKey = "Use Your Exchange Main Account SecretKey Key";
const string mainUserId = "Use Your Exchange Main User Id";
const string mainSportAccountId = "Use Your Exchange Main User Spot AccountId";
const string mainUsdtMarginedAccountId = "Use Your Exchange Main User Usdt Margined AccountId";

const string subAccessKey = "Use Your Exchange Sub Account Access Key";
const string subSecretKey = "Use Your Exchange Sub Account SecretKey Key";
const string subUserId = "Use Your Exchange Sub User Id";
const string subSportAccountId = "Use Your Exchange Sub User Spot AccountId";
const string subUsdtMarginedAccountId = "Use Your Exchange Sub User Margined AccountId";

const string testBaseCurrency = "btc";
const string testQuoteCurrency = "usdt";
const string testSymbol = "btcusdt";
const string testETPSymbol = "btc3susdt";
const string loanOrderId = "XXXXXXXX";

const string testOrderId = "1111111111111111111";
const string testClientOrderId = "XXXXXXXXXXXXXXXX";

string listenKey = string.Empty;
#endregion

//配置一个默认的现货Rest Api 客户端
HuobiSpotClient.SetDefaultOptions(options: new HuobiSpotClientOptions()
{
    //使用accessKey, secretKey生成一个新的API凭证
    ApiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey),
    //LogLevel = LogLevel.Debug
    LogLevel = LogLevel.Trace,
    //OutputOriginalData = true

});

//配置一个默认的U本位合约Rest Api 客户端
HuobiUsdtMarginedClient.SetDefaultOptions(options: new HuobiUsdtMarginedClientOptions()
{
    //使用accessKey, secretKey生成一个新的API凭证
    ApiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey),
    //LogLevel = LogLevel.Debug
    LogLevel = LogLevel.Trace,
    //OutputOriginalData = true

});

//配置一个默认的现货webSocket Api 客户端
HuobiSocketClient.SetDefaultOptions(new HuobiSpotSocketClientOptions()
{
    ApiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey),
    //LogLevel = LogLevel.Debug
    LogLevel = LogLevel.Trace,
    //OutputOriginalData = true
});

//配置一个默认的U本位合约webSocket Api 客户端
HuobiUsdtMarginedSocketClient.SetDefaultOptions(new HuobiUsdtMarginedSocketClientOptions()
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
    /*
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
        await TestUsdtMarginedApiReferenceDataEndpoints();
    }

    //五、交易所U本位合约市场行情数据接口测试-已完成
    Console.WriteLine($"Press enter to test usdt margined api market data endpoints, Press [S] to skip current test!");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        await TestUsdtMarginedApiMarketDataEndpoints();
    }

    //六、交易所U本位合约账户接口测试-已完成
    Console.WriteLine($"Press enter to test usdt margined api account endpoints, Press [S] to skip current test!");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        await TestUsdtMarginedApiAccountEndpoints();
    }
    */

    //七、交易所U本位合约交易接口测试-开发中...
    Console.WriteLine($"Press enter to test usdt margined api Trade endpoints, Press [S] to skip current test!");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        await TestUsdtMarginedApiTradeEndpoints();
    }

    //八、交易所U本位合约策略订单接口测试-开发中...
    Console.WriteLine($"Press enter to test usdt margined api strategy order endpoints, Press [S] to skip current test!");
    read = Console.ReadLine();
    if (read != "S" && read != "s")                        
    {
        await TestUsdtMarginedApiStrategyOrderEndpoints();
    }

    //九、交易所U本位合约划转接口测试-开发中...
    Console.WriteLine($"Press enter to test usdt margined api Trade endpoints, Press [S] to skip current test!");
    read = Console.ReadLine();
    if (read != "S" && read != "s")
    {
        await TestUsdtMarginedApiTransferringEndpoints();
    }
}
else if (read == "P" || read == "p")
{
    //HuobiSocketClient? huobiSocketClient = new HuobiSocketClient();
    //CallResult<UpdateSubscription>? publicSubscription = null;

    //#region 订阅逐笔交易
    ////订阅一个交易代码逐笔交易
    //Console.WriteLine($"Press enter to subscribe one symbol trade stream, Press [S] to skip current subscription");
    //read = Console.ReadLine();
    //if (read != "S" && read != "s")
    //{
    //    publicSubscription = await huobiSocketClient.SpotPublicStreams.SubscribeToTradeUpdatesAsync("BTCUSDT", data =>
    //    {
    //        Console.WriteLine($"Stream:{data.Data.Stream} StreamSymbol:{data.Data.Symbol}");
    //        Console.WriteLine($"DataEventTime:{data.Data.Data.EventTime} DataEventType:{data.Data.Data.EventType} DataSymbol:{data.Data.Data.Symbol}");
    //        foreach (var item in data.Data.Data.Deals)
    //        {
    //            Console.WriteLine($"DealTradeType:{item.TradeType} DealPrice:{item.Price} DealTime:{item.DealTime} DealQuantity:{item.Quantity}");
    //        }
    //        Console.Write($"@ StreamEventTimeStamp:{data.Data.EventTimeStamp}\r\n");
    //    });

    //    if (!publicSubscription.Success)
    //    {
    //        Console.WriteLine("Failed to sub" + publicSubscription.Error);
    //        Console.ReadLine();
    //        return;
    //    }

    //    publicSubscription.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
    //    publicSubscription.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    //}

    ////订阅多交易代码逐笔交易
    //Console.WriteLine($"Press enter to subscribe symbols trade stream, Press [S] to skip current subscription");
    //read = Console.ReadLine();
    //if (read != "S" && read != "s")
    //{
    //    publicSubscription = await huobiSocketClient.SpotPublicStreams.SubscribeToTradeUpdatesAsync(new string[] { "ETHUSDT", "MXUSDT" }, data =>
    //    {
    //        Console.WriteLine($"Stream:{data.Data.Stream} StreamSymbol:{data.Data.Symbol}");
    //        Console.WriteLine($"DataEventTime:{data.Data.Data.EventTime} DataEventType:{data.Data.Data.EventType} DataSymbol:{data.Data.Data.Symbol}");
    //        foreach (var item in data.Data.Data.Deals)
    //        {
    //            Console.WriteLine($"DealTradeType:{item.TradeType} DealPrice:{item.Price} DealTime:{item.DealTime} DealQuantity:{item.Quantity}");
    //        }
    //        Console.Write($"@ StreamEventTimeStamp:{data.Data.EventTimeStamp}\r\n");
    //    });

    //    if (!publicSubscription.Success)
    //    {
    //        Console.WriteLine("Failed to sub" + publicSubscription.Error);
    //        Console.ReadLine();
    //        return;
    //    }

    //    publicSubscription.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
    //    publicSubscription.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    //}
    //#endregion

    //#region 订阅K线
    ////订阅一个交易代码，单时间区间
    //Console.WriteLine($"Press enter to subscribe one symbol candlestick stream, Press [S] to skip current subscription");
    //read = Console.ReadLine();
    //if (read != "S" && read != "s")
    //{
    //    publicSubscription = await huobiSocketClient.SpotPublicStreams.SubscribeToKlineUpdatesAsync("BTCUSDT", KlineInterval.FiveMinutes, data =>
    //    {
    //        Console.WriteLine($"Stream:{data.Data.Stream} StreamSymbol:{data.Data.Symbol}");
    //        Console.WriteLine($"DataEventTime:{data.Data.Data.EventTime} DataEventType:{data.Data.Data.EventType} DataSymbol:{data.Data.Data.Symbol}");
    //        Console.WriteLine($"DetailSymbol:{data.Data.Data.Details.Symbol} DetailOpenPrice:{data.Data.Data.Details.OpenPrice} DetailClosePrice:{data.Data.Data.Details.ClosePrice} DetailQuoteVolume:{data.Data.Data.Details.QuoteVolume}");
    //        Console.Write($"@ StreamEventTimeStamp:{data.Data.EventTimeStamp}\r\n");
    //    });

    //    if (!publicSubscription.Success)
    //    {
    //        Console.WriteLine("Failed to sub" + publicSubscription.Error);
    //        Console.ReadLine();
    //        return;
    //    }

    //    publicSubscription.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
    //    publicSubscription.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    //}

    ////订阅多交易代码，多时间区间（如果需要订阅的比较多，建议分多组申请，每组20个即可）
    //Console.WriteLine($"Press enter to subscribe symbols candlestick stream by string array, Press [S] to skip current subscription" +
    //    $"\r\nThe maximum number of combinations of symbol and Interval is 24, if it exceeds, no data will be received");
    //read = Console.ReadLine();
    //if (read != "S" && read != "s")
    //{
    //    publicSubscription = await huobiSocketClient.SpotPublicStreams.SubscribeToKlineUpdatesAsync(new string[] { "ETHUSDT", "MXUSDT" }, new KlineInterval[] { KlineInterval.OneMinute, KlineInterval.FiveMinutes }, data =>
    //    {
    //        Console.WriteLine($"Stream:{data.Data.Stream} StreamSymbol:{data.Data.Symbol}");
    //        Console.WriteLine($"DataEventTime:{data.Data.Data.EventTime} DataEventType:{data.Data.Data.EventType} DataSymbol:{data.Data.Data.Symbol}");
    //        Console.WriteLine($"DetailSymbol:{data.Data.Data.Details.Symbol} DetailOpenPrice:{data.Data.Data.Details.OpenPrice} DetailClosePrice:{data.Data.Data.Details.ClosePrice} DetailQuoteVolume:{data.Data.Data.Details.QuoteVolume}");
    //        Console.Write($"@ StreamEventTimeStamp:{data.Data.EventTimeStamp}\r\n");
    //    });

    //    if (!publicSubscription.Success)
    //    {
    //        Console.WriteLine("Failed to sub" + publicSubscription.Error);
    //        Console.ReadLine();
    //        return;
    //    }

    //    publicSubscription.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
    //    publicSubscription.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    //}

    ////订阅多交易代码，多时间区间（如果需要订阅的比较多，建议分多组申请，每组20个即可）
    //Console.WriteLine($"Press enter to subscribe symbols candlestick stream by IEnumerable<T>, Press [S] to skip current subscription" +
    //    $"\r\nThe maximum number of combinations of symbol and Interval is 24, if it exceeds, no data will be received!");
    //read = Console.ReadLine();
    //if (read != "S" && read != "s")
    //{
    //    List<string> symbolList = new List<string>();
    //    //symbolList.Add("AKITAUSDT");
    //    //symbolList.Add("API3USDT");
    //    symbolList.Add("ASSUSDT");
    //    symbolList.Add("BABYDOGEUSDT");
    //    symbolList.Add("BNBUSDT");
    //    symbolList.Add("BTCUSDT");
    //    symbolList.Add("BUNNYUSDT");
    //    symbolList.Add("CFXUSDT");
    //    symbolList.Add("COOKUSDT");
    //    symbolList.Add("DAIUSDT");
    //    symbolList.Add("DOGEUSDT");
    //    symbolList.Add("ETHUSDT");
    //    symbolList.Add("ETHWUSDT");
    //    symbolList.Add("FIL36USDT");
    //    symbolList.Add("FILUSDT");
    //    symbolList.Add("GLOUSDT");
    //    symbolList.Add("GNXUSDT");
    //    symbolList.Add("HOTUSDT");
    //    symbolList.Add("KISHUUSDT");
    //    symbolList.Add("LUNAUSDT");
    //    symbolList.Add("MXUSDT");
    //    symbolList.Add("PAINTUSDT");
    //    symbolList.Add("PETUSDT");
    //    symbolList.Add("PIGUSDT");
    //    symbolList.Add("RACAUSDT");
    //    symbolList.Add("SAFEMOONUSDT");
    //    IEnumerable<string> symbols = symbolList;

    //    List<KlineInterval> KlineIntervalList = new List<KlineInterval>();
    //    KlineIntervalList.Add(KlineInterval.OneMinute);
    //    //KlineIntervalList.Add(KlineInterval.FiveMinutes);
    //    IEnumerable<KlineInterval> KlineIntervals = KlineIntervalList;

    //    publicSubscription = await huobiSocketClient.SpotPublicStreams.SubscribeToKlineUpdatesAsync(symbols, KlineIntervals, data =>
    //    {
    //        Console.WriteLine($"Stream:{data.Data.Stream} StreamSymbol:{data.Data.Symbol}");
    //        Console.WriteLine($"DataEventTime:{data.Data.Data.EventTime} DataEventType:{data.Data.Data.EventType} DataSymbol:{data.Data.Data.Symbol}");
    //        Console.WriteLine($"DetailSymbol:{data.Data.Data.Details.Symbol} DetailOpenPrice:{data.Data.Data.Details.OpenPrice} DetailClosePrice:{data.Data.Data.Details.ClosePrice} DetailQuoteVolume:{data.Data.Data.Details.QuoteVolume}");
    //        Console.Write($"@ StreamEventTimeStamp:{data.Data.EventTimeStamp}\r\n");
    //    });

    //    if (!publicSubscription.Success)
    //    {
    //        Console.WriteLine("Failed to sub" + publicSubscription.Error);
    //        Console.ReadLine();
    //        return;
    //    }

    //    publicSubscription.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
    //    publicSubscription.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    //}
    //#endregion

    //#region 订阅增量深度信息
    ////订阅一个交易代码增量深度信息
    //Console.WriteLine($"Press enter to subscribe \"BTCUSDT\" diff depth stream, Press [S] to skip current subscription");
    //read = Console.ReadLine();
    //if (read != "S" && read != "s")
    //{
    //    publicSubscription = await huobiSocketClient.SpotPublicStreams.SubscribeToDiffDepthUpdatesAsync("BTCUSDT", data =>
    //    {
    //        Console.WriteLine($"Stream:{data.Data.Stream} StreamSymbol:{data.Data.Symbol}");
    //        Console.WriteLine($"DataEventTime:{data.Data.Data.EventTime} DataEventType:{data.Data.Data.EventType} DataSymbol:{data.Data.Data.Symbol}");
    //        if (!Object.Equals(data.Data.Data.Asks, null))
    //        {
    //            foreach (var item in data.Data.Data.Asks)
    //            {
    //                Console.WriteLine($"SellPrice:{item.Price} SellQuantity:{item.Quantity}");
    //            }
    //        }
    //        if (!Object.Equals(data.Data.Data.Bids, null))
    //        {
    //            foreach (var item in data.Data.Data.Bids)
    //            {
    //                Console.WriteLine($"BuyPrice:{item.Price} BuyQuantity:{item.Quantity}");
    //            }
    //        }
    //        Console.Write($"@ StreamEventTimeStamp:{data.Data.EventTimeStamp}\r\n");
    //    });

    //    if (!publicSubscription.Success)
    //    {
    //        Console.WriteLine("Failed to sub" + publicSubscription.Error);
    //        Console.ReadLine();
    //        return;
    //    }

    //    publicSubscription.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
    //    publicSubscription.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    //}

    ////订阅多个交易代码增量深度信息
    //Console.WriteLine($"Press enter to subscribe \"ETHUSDT\" and \"MXUSDT\" diff depth stream, Press [S] to skip current subscription");
    //read = Console.ReadLine();
    //if (read != "S" && read != "s")
    //{
    //    publicSubscription = await huobiSocketClient.SpotPublicStreams.SubscribeToDiffDepthUpdatesAsync(new string[] { "ETHUSDT", "MXUSDT" }, data =>
    //    {
    //        Console.WriteLine($"Stream:{data.Data.Stream} StreamSymbol:{data.Data.Symbol}");
    //        Console.WriteLine($"DataEventTime:{data.Data.Data.EventTime} DataEventType:{data.Data.Data.EventType} DataSymbol:{data.Data.Data.Symbol}");
    //        if (!Object.Equals(data.Data.Data.Asks, null))
    //        {
    //            foreach (var item in data.Data.Data.Asks)
    //            {
    //                Console.WriteLine($"SellPrice:{item.Price} SellQuantity:{item.Quantity}");
    //            }
    //        }
    //        if (!Object.Equals(data.Data.Data.Bids, null))
    //        {
    //            foreach (var item in data.Data.Data.Bids)
    //            {
    //                Console.WriteLine($"BuyPrice:{item.Price} BuyQuantity:{item.Quantity}");
    //            }
    //        }
    //        Console.Write($"@ StreamEventTimeStamp:{data.Data.EventTimeStamp}\r\n");
    //    });

    //    if (!publicSubscription.Success)
    //    {
    //        Console.WriteLine("Failed to sub" + publicSubscription.Error);
    //        Console.ReadLine();
    //        return;
    //    }

    //    publicSubscription.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
    //    publicSubscription.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    //}
    //#endregion
}
else if (read == "U" || read == "u")
{
    //#region 生成订阅私有数据的监听密钥（Listen Key)
    //using (var client = new HuobiClient())
    //{
    //    #region 创建现货WebSocket 连接 Listen Key
    //    Console.WriteLine("创建现货WebSocket 连接 Listen Key");
    //    WebCallResult<string>? result = await client.SpotApi.WebsocketAccount.StartUserStreamAsync();
    //    listenKey = result.Data;
    //    Console.WriteLine($"New Listen Key :{(listenKey != null ? listenKey : "Create failed!")}");
    //    #endregion
    //}
    //if (string.IsNullOrWhiteSpace(listenKey))
    //{
    //    Console.WriteLine($"Please check \"AccessKey\" and \"SecretKey\" are valid");
    //    return;
    //}
    //#endregion

    //#region 构造可以获取私有账户数据的连接客户端
    //HuobiSocketClientOptions privateOptions = new HuobiSocketClientOptions();
    //privateOptions.SpotStreamsOptions.BaseAddress = HuobiApiAddresses.Default.SocketClientPrivateAddress;
    //privateOptions.SpotStreamsOptions.ApiCredentials = new ApiCredentials(accessKey, secretKey);
    //HuobiSocketClient? huobiSocketClient = new HuobiSocketClient(privateOptions);
    //CallResult<UpdateSubscription>? privateSubV3scription = null;
    //#endregion

    //#region 订阅账户成交（实时）
    //Console.WriteLine($"Press enter to subscribe account deals stream");
    //read = Console.ReadLine();
    //if (read != "S" && read != "s")
    //{
    //    #region 方法一(功能已实现，保留请勿删除）：
    //    //privateSubV3scription = await mexcV3SocketClient.SpotPrivateStreams.SubscribeToPrivateDealsUpdatesAsync(listenKey, data =>
    //    //{
    //    //    Console.WriteLine($"Stream:{data.Data.Stream} StreamSymbol:{data.Data.Symbol}");
    //    //    Console.WriteLine($"AccountDealsClientOrderId:{data.Data.Data.ClientOrderId} AccountDealsOrderId:{data.Data.Data.OrderId} AccountDealsPrice:{data.Data.Data.Price} AccountDealsQuantity:{data.Data.Data.Quantity} AccountDealsMakerOrTaker:{data.Data.Data.MakerOrTaker}");
    //    //    Console.Write($"@ StreamEventTimeStamp:{data.Data.EventTimeStamp}\r\n");
    //    //});
    //    #endregion

    //    #region 方法二：
    //    privateSubV3scription = await huobiSocketClient.SpotPrivateStreams.SubscribeToPrivateDealsUpdatesAsync(listenKey, data =>
    //    {
    //        MexcV3StreamPrivateDeals? result = data.Data;
    //        if (result != null)
    //        {
    //            result.ListenKey = listenKey;
    //            Console.WriteLine(
    //                $"Stream:{result.Stream}\t" +
    //                $"ListenKey:{result.ListenKey}\r\n" +
    //                $"Symbol:{result.Symbol} " +
    //                $"TradeType {result.Data.TradeType} " +
    //                $"Price {result.Data.Price} " +
    //                $"Quantity {result.Data.Quantity} " +
    //                $"ClientOrderId {result.Data.ClientOrderId} " +
    //                $"OrderId {result.Data.OrderId} " +
    //                $"TradeId {result.Data.TradeId} " +
    //                $"@ {result.Data.DealTime}");
    //        }
    //    });
    //    #endregion

    //    if (!privateSubV3scription.Success)
    //    {
    //        Console.WriteLine("Failed to sub" + privateSubV3scription.Error);
    //        Console.WriteLine($"Please check if \"AccessKey\", \"SecretKey\" and \"WebSocket Api Server Host\" are valid");
    //        Console.ReadLine();
    //        return;
    //    }

    //    privateSubV3scription.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
    //    privateSubV3scription.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    //}
    //#endregion

    //#region 订阅账户订单（实时）
    //Console.WriteLine($"Press enter to subscribe account orders stream, Press [S] to skip current subscription");
    //read = Console.ReadLine();
    //if (read != "S" && read != "s")
    //{
    //    #region 方法一(功能已实现，保留请勿删除）：
    //    //privateSubV3scription = await mexcV3SocketClient.SpotPrivateStreams.SubscribeToPrivateOrdersUpdatesAsync(listenKey, data =>
    //    //{
    //    //    Console.WriteLine($"Stream:{data.Data.Stream} StreamSymbol:{data.Data.Symbol}");
    //    //    Console.WriteLine($"AccountOrdersClientOrderId:{data.Data.Data.ClientOrderId} AccountOrdersOrderId:{data.Data.Data.OrderId} AccountOrdersPrice:{data.Data.Data.Price} AccountDealsQuantity:{data.Data.Data.Quantity} AccountDealsMakerOrTaker:{data.Data.Data.MakerOrTaker}");
    //    //    Console.Write($"@ StreamEventTimeStamp:{data.Data.EventTimeStamp}\r\n");
    //    //});
    //    #endregion

    //    #region 方法二：
    //    privateSubV3scription = await huobiSocketClient.SpotPrivateStreams.SubscribeToPrivateOrdersUpdatesAsync(listenKey, data =>
    //    {
    //        MexcV3StreamPrivateOrders? result = data.Data;
    //        if (result != null)
    //        {
    //            result.ListenKey = listenKey;
    //            Console.WriteLine(
    //                $"Stream:{result.Stream}\t" +
    //                $"ListenKey:{result.ListenKey}\r\n" +
    //                $"Symbol:{result.Symbol} " +
    //                $"ClientOrderId {result.Data.ClientOrderId} " +
    //                $"OrderId {result.Data.OrderId} " +
    //                $"Price {result.Data.Price} " +
    //                $"Quantity {result.Data.Quantity} " +
    //                $"@ {result.Data.CreateTime}"
    //                );
    //        }
    //    });
    //    #endregion

    //    if (!privateSubV3scription.Success)
    //    {
    //        Console.WriteLine("Failed to sub" + privateSubV3scription.Error);
    //        Console.WriteLine($"Please check if \"AccessKey\", \"SecretKey\" and \"WebSocket Api Server Host\" are valid");
    //        Console.ReadLine();
    //        return;
    //    }

    //    privateSubV3scription.Data.ConnectionLost += () => Console.WriteLine("Connection lost, trying to reconnect..");
    //    privateSubV3scription.Data.ConnectionRestored += (t) => Console.WriteLine("Connection restored");
    //}
    //#endregion
}

//交易所现货数据接口测试-已完成
static async Task TestSpotApiExchangeDataEndpoints()
{
    using (var huobiSpotRestClient = new HuobiSpotClient())
    {
        #region 获取当前市场最新状态
        {
            await HandleRequest("Market Status", () => huobiSpotRestClient.SpotApi.ExchangeData.GetMarketStatusAsync(),
                result => $"{result.Status.ToString()}"
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
            //    Console.WriteLine($"火币现货服务器："+ "获取当前市场最新状态" + "异常\r\n" +
            //        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
            //        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
            //        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    $"现货市场服务器当前时间:{result.Data.ToString()}\r\n" +
                    $"本地时间:{result.Data.ToLocalTime()}\r\n" +
                    $"时差:{(result.Data.ToLocalTime() - result.Data).TotalHours}小时"
                    );
            }
            else
            {
                Console.WriteLine($"火币现货服务器：" + "获取服务器当前时间" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "获取币链参考信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "获取现货币种信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "获取现货K线数据" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "获取现货交易代码" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "获取所有现货交易代码最新行情" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "获取指定现货杠杆代码ETP实时净值" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "获取指定现货交易代码市场行情深度" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "获取指定现货交易代码24小时行情汇总" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "获取指定现货交易代码24小时行情汇总以及最佳买入卖出价格" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "获取指定现货交易代码最近成交记录" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器："+ "获取指定现货交易代码近期的所有交易记录" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }            
        }
        #endregion
    }
}

//交易所现货账户接口测试-已完成
static async Task TestSpotApiAccountEndpoints()
{
    decimal loanAmount = 0.0M;

    using (var huobiSpotRestClient = new HuobiSpotClient())
    {
        #region 对HuobiClient客户端的新实例使用新的设置(这里不设置则使用之前的默认设置）
        //使用accessKey, secretKey生成一个新的API凭证
        ApiCredentials apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
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
                    Console.WriteLine($"火币现货服务器：" + "查询母用户API Key信息" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "查询子用户API Key信息" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "通过用户账户Id查询账户流水(母用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "通过用户账户Id查询账户流水(子母用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "通过用户账户Id查询财务流水(母用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "通过用户账户Id查询财务流水(子用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "查询当前用户的所有账户ID(母用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "查询当前用户的所有账户ID(子用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "现货账户" + "获取平台资产总估值(母用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.Margin);
                if (result.Success)
                {
                    Console.WriteLine($"杠杆账户：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币现货服务器：" + "杠杆账户" + "获取平台资产总估值(母用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.Point);
                if (result.Success)
                {
                    Console.WriteLine($"点卡账户{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币现货服务器：" + "点卡账户" + "获取平台资产总估值(母用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.CrossMargin);
                if (result.Success)
                {
                    Console.WriteLine($"全仓杠杆账户{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币现货服务器：" + "全仓杠杆账户" + "获取平台资产总估值(母用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.Minepool);
                if (result.Success)
                {
                    Console.WriteLine($"矿池账户{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币现货服务器：" + "矿池账户" + "获取平台资产总估值(母用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.Etf);
                if (result.Success)
                {
                    Console.WriteLine($"ETF账户{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币现货服务器：" + "ETF账户" + "获取平台资产总估值(母用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.CryptoLoans);
                if (result.Success)
                {
                    Console.WriteLine($"抵押借贷{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币现货服务器：" + "抵押借贷" + "获取平台资产总估值(母用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.Otc);
                if (result.Success)
                {
                    Console.WriteLine($"OTC账户{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币现货服务器：" + "OTC账户" + "获取平台资产总估值(母用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "现货账户" + "获取平台资产总估值(子用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
                result = await huobiSpotRestClient.SpotApi.Account.GetAssetValuationAsync(AccountType.Margin);
                if (result.Success)
                {
                    Console.WriteLine($"杠杆账户{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币现货服务器：" + "杠杆账户" + "获取平台资产总估值(子用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "现货账户" + "获取账户余额(母用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "U本位合约账户" + "获取账户余额(母用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "现货账户" + "获取账户余额(子用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "U本位合约账户" + "获取账户余额(子用户示范）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "【全仓】查询借币币息率及额度" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "【全仓】查询借币账户详情" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "【全仓】查询借币订单" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "查询指定币种充币地址" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }

            result = await huobiSpotRestClient.SpotApi.Account.GetDepositAddressesAsync(testQuoteCurrency);
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result)}");
            }
            else
            {
                Console.WriteLine($"火币现货服务器：" + "查询指定币种充币地址" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "【逐仓】查询借币币息率及额度" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "【逐仓】查询借币账户详情" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "【逐仓】查询借币订单" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "查询还币交易记录" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "主用户查询子用户余额" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "主用户获取子用户列表" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "主用户获取指定子用户的账户列表" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "主用户获取自身Uid" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "查询充值记录" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "查询提币记录" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "执行解冻子用户" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                        Console.WriteLine($"火币现货服务器：" + "执行冻结子用户" + "异常\r\n" +
                            $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                            $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                            $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "【全仓】归还借币" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "【逐仓】归还借币" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "【通用】归还借币" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "【全仓】申请借币" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "【逐仓】申请借币" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "子用户创建API Key" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "子用户删除API Key" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "子用户修改API Key" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 创建子用户
        {
            Console.WriteLine("创建子用户");

            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);

            HuobiCreateSubUserAccountRequestInfo huobiCreateSubUserAccountRequestInfo = new HuobiCreateSubUserAccountRequestInfo()
            {
                UserName = "DaiPaxUsdt",
                Note = "NoteText"
            };
            IEnumerable<HuobiCreateSubUserAccountRequestInfo> UserList = new HuobiCreateSubUserAccountRequestInfo[] { huobiCreateSubUserAccountRequestInfo };
            HuobiCreateSubUserAccountRequest huobiCreateSubUserAccountRequest = new HuobiCreateSubUserAccountRequest(UserList);
            var result = await huobiSpotRestClient.SpotApi.Account.SubUserCreationAsync(huobiCreateSubUserAccountRequest);
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                Console.WriteLine($"火币现货服务器：" + "创建子用户" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "母子用户币种互转" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "全仓杠杆账户对现货账户进行资产划转" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "逐仓杠杆账户对现货账户进行资产划转" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "现货账户对全仓杠杆账户进行资产划转" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "现货账户对逐仓杠杆账户进行资产划转" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "母子用户间资产划转" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                Console.WriteLine($"火币现货服务器：" + "主用户现货账户数字币提取到区块链地址" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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

    using (var huobiSpotRestClient = new HuobiSpotClient())
    {
        #region 对HuobiClient客户端的新实例使用新的设置(这里不设置则使用之前的默认设置）
        //使用accessKey, secretKey生成一个新的API凭证
        ApiCredentials apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
        //当前客户端使用新生成的API凭证
        huobiSpotRestClient.SetApiCredentials(apiCredentials);
        #endregion

        #region 现货普通订单下单
        {
            Console.WriteLine("现货普通订单下单");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString()}";

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
                stopPrice: (decimal)0,
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
                Console.WriteLine($"火币现货服务器：" + "现货普通订单下单" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                    Console.WriteLine($"火币现货服务器：" + "通过订单编号撤销一条订单" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
            }
            else
            {
                Console.WriteLine($"火币现货服务器：" + "撤销订单无效" + "异常");
            }

            //await HandleRequest("Cancel an active order", () => huobiSpotRestClient.SpotApi.Trading.CancelOrderAsync(
            //    orderId: long.Parse(testCancelOrderId)                              //被撤销订单编号
            //    ),
            //    result => $"{result}"
            //    );
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
                    Console.WriteLine($"火币现货服务器：" + "通过用户自定义单号撤销一条订单" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                List<long> orderIdList = new List<long>();
                if(!string.IsNullOrWhiteSpace(testCancelOrderId))
                    orderIdList.Add(long.Parse(testCancelOrderId));
                IEnumerable<long> orderIds = orderIdList;

                if (orderIds.Count() > 0)
                {
                    var result = await huobiSpotRestClient.SpotApi.Trading.CancelOrdersAsync(
                    orderIds: orderIds);
                    if (result.Success)
                    {
                        Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                    }
                    else
                    {
                        Console.WriteLine($"火币现货服务器：" + "通过订单编号列表撤销订单" + "异常\r\n" +
                            $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                            $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                            $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                    }
                }
                else
                {
                    Console.WriteLine($"火币现货服务器：" + "撤销订单订单编号列表" + "异常");
                }
            }
            {
                Console.WriteLine("通过用户自定义单号列表撤销多条订单");

                List<string> clientOrderIdList = new List<string>();
                if (!string.IsNullOrWhiteSpace(testCancelClientOrderId))
                    clientOrderIdList.Add(testCancelClientOrderId);
                IEnumerable<string> clientOrderIds = clientOrderIdList;

                if (clientOrderIds.Count() > 0)
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
                        Console.WriteLine($"火币现货服务器：" + "通过用户自定义单号列表撤销订单" + "异常\r\n" +
                            $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                            $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                            $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                List<long> orderIdList = new List<long>();
                orderIdList.Add(long.Parse(testOrderId));
                IEnumerable<long> orderIds = orderIdList;

                List<string> symbolList = new List<string>();
                symbolList.Add("TestSymbol");
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
                    Console.WriteLine($"火币现货服务器：" + "批量撤销所有订单（可限制账户Id/交易代码/交易方向/撤销数量）" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
            }
        }
        #endregion

        #region 现货策略委托下单
        {
            Console.WriteLine("现货策略委托下单");
            string testPlaceConditionalClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString()}";

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
                Console.WriteLine($"火币现货服务器：" + "现货策略委托下单" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 通过用户自定义单号列表撤销未触发策略委托订单
        {
            Console.WriteLine("通过用户自定义单号列表撤销未触发策略委托订单");

            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);

            List<string> clientOrderIdList = new List<string>();
            if (!string.IsNullOrWhiteSpace(testCancelConditionalClientOrderId))
                clientOrderIdList.Add(testCancelConditionalClientOrderId);
            IEnumerable<string> clientOrderIds = clientOrderIdList;

            if (clientOrderIds.Count() > 0)
            {
                var result = await huobiSpotRestClient.SpotApi.Trading.CancelConditionalOrdersAsync(
                clientOrderIds: clientOrderIds);
                if (result.Success)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币现货服务器：" + "通过用户自定义单号列表撤销未触发策略委托订单" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
            }
            else
            {
                Console.WriteLine($"火币现货服务器：" + "撤销未触发策略委托用户自定义单号列表" + "异常");
            }


            //await HandleRequest("Cancel an active order", () => huobiSpotRestClient.SpotApi.Trading.CancelConditionalOrdersAsync(
            //    clientOrderIds: clientOrderIds                              //被撤销的用户自定义订单编号列表
            //    ),
            //    result => $"{result}"
            //    );
        }
        #endregion

        #region 通过订单编号获取指定订单详情
        {
            Console.WriteLine("通过订单编号获取指定订单详情");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString()}";

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
                Console.WriteLine($"火币现货服务器：" + "通过订单编号获取指定订单详情" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 通过用户自定义单号获取指定订单详情
        {
            Console.WriteLine("通过用户自定义单号获取指定订单详情");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString()}";

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
                Console.WriteLine($"火币现货服务器：" + "通过用户自定义单号获取指定订单详情" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 查询当前未成交订单
        {
            Console.WriteLine("查询当前未成交订单");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString()}";

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
                Console.WriteLine($"火币现货服务器：" + "查询当前未成交订单" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 查询当前未触发策略委托订单
        {
            Console.WriteLine("查询当前未触发策略委托订单");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString()}";

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
                Console.WriteLine($"火币现货服务器：" + "查询当前未触发策略委托订单" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 查询指定交易代码的历史订单
        {
            Console.WriteLine("查询指定交易代码的历史订单");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString()}";

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
                Console.WriteLine($"火币现货服务器：" + "查询指定交易代码的历史订单" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 查询指定交易代码的策略委托历史
        {
            Console.WriteLine("查询指定交易代码的策略委托历史");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString()}";

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
                Console.WriteLine($"火币现货服务器：" + "查询指定交易代码的策略委托历史" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 查询最近48小时内历史订单
        {
            Console.WriteLine("查询最近48小时内历史订单");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString()}";

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
                Console.WriteLine($"火币现货服务器：" + "查询最近48小时内历史订单" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 通过用户自定义单号查询策略委托订单
        {
            Console.WriteLine("通过用户自定义单号查询策略委托订单");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString()}";

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
                Console.WriteLine($"火币现货服务器：" + "通过用户自定义单号查询策略委托订单" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 查询指定订单编号的成交明细
        {
            Console.WriteLine("查询指定订单编号的成交明细");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString()}";

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
                Console.WriteLine($"火币现货服务器：" + "查询指定订单编号的成交明细" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 查询当前和历史成交记录
        {
            Console.WriteLine("查询当前和历史成交记录");
            string testPlaceClientOrderId = $"NewOrder{DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString()}";

            apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            huobiSpotRestClient.SetApiCredentials(apiCredentials);

            var result = await huobiSpotRestClient.SpotApi.Trading.GetUserTradesAsync();
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                Console.WriteLine($"火币现货服务器：" + "查询当前和历史成交记录" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion
    }
}

//交易所U本位合约基础信息接口测试-已完成
static async Task TestUsdtMarginedApiReferenceDataEndpoints()
{
    using (var huobiUsdtMarginedClient = new HuobiUsdtMarginedClient())
    {
        #region 对huobiUsdtMarginedClient客户端的新实例使用新的设置(这里不设置则使用之前的默认设置）
        //使用accessKey, secretKey生成一个新的API凭证
        ApiCredentials apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
        //当前客户端使用新生成的API凭证
        huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
        #endregion

        #region 获取账户类型
        {
            await HandleRequest("Swap Unified  Account Type \r\n", () => huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapUnifiedAccountTypeAsync(),
                result => $"当前账户类型：{(result.AccountType == 1 ? "非统一账户（全仓逐仓账户）" : (result.AccountType == 2 ? "统一账户" : "未知账户类型"))}" 
                );

            #region 方法二 不需要等待输入回车后才执行
            //Console.WriteLine("获取账户类型");
            //var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapUnifiedAccountTypeAsync();
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
            //    Console.WriteLine($"火币合约API服务器：" + "获取账户类型" + "异常\r\n" +
            //        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
            //        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
            //        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            //}
            #endregion
        }
        #endregion

        #region 账户类型更改接口
        {
            Console.WriteLine("账户类型更改接口");
            int accountType = 1;                    //账户类型	1:非统一账户（全仓逐仓账户）2:统一账户
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.LinearSwapSwitchAccountTypeAsync(accountType);
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
                Console.WriteLine($"火币合约API服务器：" + "账户类型更改接口" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】获取合约资金费率
        {
            Console.WriteLine("【通用】获取合约资金费率");
            string contractCode = "BTC-USDT";
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapFundingRateAsync(contractCode);
            if (result.Success)
            {
                Console.WriteLine($"合约代码{contractCode}资金费率：{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取合约资金费率" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】批量获取合约资金费率
        {
            Console.WriteLine("【通用】批量获取合约资金费率");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapBatchFundingRateAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码{item.ContractCode}资金费率：\r\n{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【通用】批量获取合约资金费率" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】获取合约的历史资金费率
        {
            string contractCode = "DOGE-USDT";

            Console.WriteLine("【通用】获取合约的历史资金费率");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapHistoricalFundingRateAsync(contractCode);
            if (result.Success)
            {
                foreach (var item in result.Data.Data)
                {
                    Console.WriteLine($"合约代码{item.ContractCode}资金费率：\r\n{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取合约的历史资金费率" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】获取强平订单(新)
        {
            int trade_type = 0;
            string contractCode = "DOGE-USDT";

            Console.WriteLine("【通用】获取强平订单(新)");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapLiquidationOrdersAsync(trade_type, contractCode);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 业务类型：{item.BusinessType} 强平方向：{(item.Direction == "sell" ? "多单" : "空单")}  强平数量:{item.Amount} 强平金额（计价币种）：{item.TradeTurnover}");
                }
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取强平订单(新)" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】查询平台历史结算记录
        {
            string contractCode = "DOGE-USDT";

            Console.WriteLine("【通用】查询平台历史结算记录");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapSettlementRecordsAsync(contractCode);
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】查询平台历史结算记录" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】精英账户多空持仓对比-账户数
        {
            string contractCode = "DOGE-USDT";
            string period = "15min";

            Console.WriteLine("【通用】精英账户多空持仓对比-账户数");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapEliteAccountRatioAsync(contractCode, period);
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】精英账户多空持仓对比-账户数" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】精英账户多空持仓对比-持仓量
        {
            string contractCode = "DOGE-USDT";
            string period = "15min";

            Console.WriteLine("【通用】精英账户多空持仓对比-持仓量");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapElitePositionRatioAsync(contractCode, period);
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】精英账户多空持仓对比-持仓量" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【逐仓】查询系统状态
        {
            Console.WriteLine("【逐仓】查询系统状态");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapApiStateAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】查询系统状态" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【全仓】获取平台阶梯保证金
        {
            string contractCode = "DOGE-USDT";

            Console.WriteLine("【全仓】获取平台阶梯保证金");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapCrossLadderMarginAsync(contractCode);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】获取平台阶梯保证金" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【逐仓】获取平台阶梯保证金
        {
            string contractCode = "DOGE-USDT";

            Console.WriteLine("【逐仓】获取平台阶梯保证金");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapLadderMarginAsync(contractCode);
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】获取平台阶梯保证金" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapEstimatedSettlementPriceAsync(contractCode, pair, contractType, businessType);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 合约类型：{item.ContractCode} 业务类型:{item.BusinessType} 本期预估结算价：{item.EstimatedSettlementPrice}");
                }
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取预估结算价" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【逐仓】查询平台阶梯调整系数
        {
            string contractCode = "DOGE-USDT";

            Console.WriteLine("【逐仓】查询平台阶梯调整系数");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapAdjustfactorAsync(contractCode);
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】查询平台阶梯调整系数" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【全仓】查询平台阶梯调整系数
        {
            string contractCode = "DOGE-USDT";

            Console.WriteLine("【全仓】查询平台阶梯调整系数");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapCrossAdjustfactorAsync(contractCode);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】查询平台阶梯调整系数" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】查询合约风险准备金余额历史数据
        {
            string contractCode = "DOGE-USDT";

            Console.WriteLine("【通用】查询合约风险准备金余额历史数据");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapInsuranceFundAsync(contractCode);
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】查询合约风险准备金余额历史数据" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】查询合约风险准备金余额和预估分摊比例
        {
            string contractCode = "DOGE-USDT";
            string businessType = "swap";

            Console.WriteLine("【通用】查询合约风险准备金余额和预估分摊比例");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapRiskInfoAsync(contractCode, businessType);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode} 合约类型：{item.ContractCode} 业务类型:{item.BusinessType} 风险准备金余额：{item.InsuranceFund} 预估分摊比例：{item.EstimatedClawback}");
                }
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【通用】查询合约风险准备金余额和预估分摊比例" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapPriceLimitAsync(contractCode, pair, contractType, businessType);
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取合约最高限价和最低限价" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】获取当前合约总持仓量
        {
            Console.WriteLine("【通用】获取当前合约总持仓量");
            string contractCode = "BTC-USDT";
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapOpenInterestAsync(contractCode);
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取当前合约总持仓量" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】获取合约信息
        {
            Console.WriteLine("【通用】获取合约信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapContractInfoAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码：{item.ContractCode}\r\n{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取合约信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】获取合约指数信息
        {
            Console.WriteLine("【通用】获取合约指数信息");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapIndexAsync();
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约指数：{item.ContractCode}\r\n{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取合约指数信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【合约服务器】获取当前系统时间戳
        {
            //await HandleRequest("Market Status", () => huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapServerTimestampAsync(),
            //    result => $"{result.ToString()}"
            //    );

            Console.WriteLine("【合约服务器】获取当前系统时间戳");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapServerTimestampAsync();
            if (result.Success)
            {
                Console.WriteLine($"当前系统时间戳:{result.Data.ToString()}\r\n");
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【合约服务器】获取当前系统时间戳" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【合约服务器】获取当前系统时间
        {
            Console.WriteLine("【合约服务器】获取当前系统时间");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapServerDateTimeAsync();
            if (result.Success)
            {
                Console.WriteLine(
                    $"合约服务器时间:{result.Data.ToString()}\r\n" +
                    $"本地时间:{result.Data.ToLocalTime()}\r\n" +
                    $"时差:{(result.Data.ToLocalTime() - result.Data).TotalHours}小时"
                    );
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【合约服务器】获取当前系统时间" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【U本位合约服务器】查询系统是否可用
        {
            //await HandleRequest("Market Status \r\n", () => huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapHeartbeatAsync(),
            //    result =>
            //    $"交割合约市场：{(result.Heartbeat == 1 ? "正常" : "停服维护")}\r\n" +
            //    $"币本位永续合约市场：{(result.SwapHeartbeat == 1 ? "正常" : "停服维护")}\r\n" +
            //    $"U本位合约市场：{(result.LinearSwapHeartbeat == 1 ? "正常" : "停服维护")}\r\n"
            //    );

            #region 方法二 不需要等待输入回车后才执行
            Console.WriteLine("【U本位合约服务器】查询系统是否可用");
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapHeartbeatAsync();
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
                Console.WriteLine($"火币合约API服务器：" + "获取合约服务器可用状态" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.ReferenceData.GetLinearSwapSummaryAsync();
                if (result.Success)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币合约API服务器：" + "【U本位合约服务器】获取当前系统状态" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
static async Task TestUsdtMarginedApiMarketDataEndpoints()
{
    using (var huobiUsdtMarginedClient = new HuobiUsdtMarginedClient())
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

            await HandleRequest("Linear Swap Ex Market History Kline \r\n", () => huobiUsdtMarginedClient.UsdtMarginedApi.MarketData.GetLinearSwapHistoryKlineAsync(contractCode, period, from, to),
                result => $"{contractCode}" + string.Join($" ", result.Select(s => $"\r\n开盘价：{s.Open}\t收盘价:{s.Close}\t最低价:{s.Low}\t最高价:{s.High}").Take(100)) + "\r\n......");

            #region 方法二 不需要等待输入回车后才执行，测试通过保留勿删！！！
            //Console.WriteLine("【通用】获取合约K线数据");
            //var result = await huobiUsdtMarginedClient.UsdtMarginedApi.MarketData.GetLinearSwapHistoryKlineAsync(contractCode, period, from, to);
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
            //    Console.WriteLine($"火币合约API服务器：" + "【通用】获取合约K线数据" + "异常\r\n" +
            //        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
            //        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
            //        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            //}
            #endregion
        }
        #endregion

        #region 【通用】获取合约行情深度数据
        {
            string contractCode = "BTC-USDT";
            string depthType = "step6";

            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.MarketData.GetLinearSwapDepthAsync(contractCode, depthType);
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取合约行情深度数据" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】获取合约市场最优挂单
        {
            {
                //string contractCode = "BTC-USDT";
                //string businessType = "swap";
                //var result = await huobiUsdtMarginedClient.UsdtMarginedApi.MarketData.GetLinearSwapBboAsync(contractCode, businessType);
            }

            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.MarketData.GetLinearSwapBboAsync(null, "All");
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取合约市场最优挂单" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】获取标记价格的K线数据
        {
            string contractCode = "BTC-USDT";
            string period = "1day";
            int size = 30;

            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.MarketData.GetLinearSwapMarkPriceKlineAsync(contractCode, period, size);
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取标记价格的K线数据" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】获取聚合行情
        {
            string contractCode = "BTC-USDT";

            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.MarketData.GetLinearSwapMergedAsync(contractCode);
            if (result.Success)
            {
                Console.WriteLine($"{contractCode}聚合行情");
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取聚合行情" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】批量获取聚合行情（V2)
        {
            //string contractCode = "BTC-USDT";
            //string businessType = "swap";
            //var result = await huobiUsdtMarginedClient.UsdtMarginedApi.MarketData.GetLinearSwapBatchMergedV2Async(contractCode, businessType);

            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.MarketData.GetLinearSwapBatchMergedV2Async();
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】批量获取聚合行情（V2)" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】获取市场最近成交记录
        {
            string contractCode = "BTC-USDT";
            string businessType = "swap";

            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.MarketData.GetLinearSwapMarketTradeAsync(contractCode, businessType);
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取市场最近成交记录" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】批量获取市场最近成交记录
        {
            string contractCode = "BTC-USDT";
            int size = 10;

            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.MarketData.GetLinearSwapMarketHistoryTradeAsync(contractCode, size);
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】批量获取市场最近成交记录" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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

            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.MarketData.GetLinearSwapHisOpenInterestAsync(period, amountType, contractCode, pair, contractType, size);
            if (result.Success)
            {
                Console.WriteLine($"{result.Data.ContractCode}平台历史持仓量查询");
                foreach (var item in result.Data.Ticks)
                {
                    Console.WriteLine($"持仓量：{item.Volume.ToString("N3")} {(item.AmountType == 1 ? "张" : "币")}\t总持仓额：{item.Value.ToString("N3")}\t统计时间{(DateTimeConverter.ConvertFromMilliseconds(item.Timestamp))}");
                }
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【通用】平台历史持仓量查询" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】获取合约的溢价指数K线
        {
            string contractCode = "BTC-USDT";
            string period = "1day";
            int size = 10;

            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.MarketData.GetLinearSwapPremiumIndexKlineAsync(contractCode, period, size);
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取合约的溢价指数K线" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】获取合约实时预测资金费率的K线数据
        {
            string contractCode = "BTC-USDT";
            string period = "1day";
            int size = 10;

            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.MarketData.GetLinearSwapEstimatedRateKlineAsync(contractCode, period, size);
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取合约实时预测资金费率的K线数据" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion

        #region 【通用】获取合约基差数据
        {
            string contractCode = "BTC-USDT";
            string period = "1day";
            int size = 20;
            string basisPriceType = "open";

            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.MarketData.GetLinearSwapBasisAsync(contractCode, period, size, basisPriceType);
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取合约基差数据" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion
    }
}

//交易所U本位合约账户接口测试-已完成
static async Task TestUsdtMarginedApiAccountEndpoints()
{
    using (var huobiUsdtMarginedClient = new HuobiUsdtMarginedClient())
    {
        #region 对huobiUsdtMarginedClient客户端的新实例使用新的设置(这里不设置则使用之前的默认设置）
        //使用accessKey, secretKey生成一个新的API凭证
        ApiCredentials apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
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

            await HandleRequest("Linear Swap Api Main Account Balance Valuation \r\n", () => huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapBalanceValuationAsync(valuationAsset),
            result => $"" + string.Join($" ", result.Select(s => $"\r\n合约总资产估值：{s.Balance} {s.ValuationAsset}").Take(100)) + "\r\n......");

            #region 子用户客户端
            apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
            huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            #endregion

            await HandleRequest("Linear Swap Api Sub Account Balance Valuation \r\n", () => huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapBalanceValuationAsync(valuationAsset),
            result => $"" + string.Join($" ", result.Select(s => $"\r\n合约总资产估值：{s.Balance} {s.ValuationAsset}").Take(100)) + "\r\n......");

            #region 方法二 不需要等待输入回车后才执行，测试通过保留勿删！！！
            //#region 主用户客户端
            //apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
            //huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
            //#endregion

            //Console.WriteLine("【通用】获取账户合约总资产估值");
            //var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapBalanceValuation(valuationAsset);
            //if (result.Success)
            //{
            //    foreach (var item in result.Data)
            //    {
            //        Console.WriteLine($"账户合约总资产估值：{item.Balance} {item.ValuationAsset}");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine($"火币合约API服务器：" + "【通用】获取账户合约总资产估值" + "异常\r\n" +
            //        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
            //        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
            //        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapAccountInfoAsync(contractCode);
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】获取用户的合约账户信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapCrossAccountInfoAsync(marginAccount);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】获取用户的合约账户信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapPositionInfoAsync(contractCode);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】获取用户的合约持仓信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapCrossPositionInfoAsync(contractCode);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【全仓】获取用户的合约持仓信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapAccountPositionInfoAsync(contractCode);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                }
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】查询用户账户和持仓信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapCrossAccountPositionInfoAsync(marginAccount);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】查询用户账户和持仓信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            List<string> subUidList = new List<string>();
            subUidList.Add(subUid1);
            subUidList.Add(subUid2);
            IEnumerable<string> subUids = subUidList;
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.SetLinearSwapSubAuthAsync(subUids, subAuth);
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】批量设置子账户交易权限" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapSubAccountListAsync(contractCode);
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】查询母账户下所有子账户资产信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapCrossSubAccountListAsync(marginAccount);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】查询母账户下所有子账户资产信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapSubAccountInfoListAsync(contractCode, pageIndex, pageSize);
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】批量获取子账户资产信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapCrossSubAccountInfoListAsync(marginAccount, pageIndex, pageSize);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】批量获取子账户资产信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapSubAccountInfoAsync(long.Parse(subUid), contractCode);
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】查询单个子账户资产信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapCrossSubAccountInfoAsync(long.Parse(subUid), marginAccount);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】查询单个子账户资产信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapSubPositionInfoAsync(long.Parse(subUid), contractCode);
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】查询单个子账户持仓信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapCrossSubPositionInfoAsync(long.Parse(subUid), contractCode, pair, contractType);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码{item.ContractCode} 持仓方向：{item.Direction} 持仓均价：{item.CostHold} 开仓均价：{item.CostOpen} 业务类型：{item.businessType}");
                }
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【全仓】查询单个子账户持仓信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapFinancialRecordAsync(marginAccount);
                if (result.Success)
                {
                    Console.WriteLine($"主用户财务记录：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币合约API服务器：" + "【通用】查询用户财务记录(新)" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
            }
            {
                #region 子用户客户端
                apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion

                Console.WriteLine("【通用】查询用户财务记录(新)");
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapFinancialRecordAsync(marginAccount);
                if (result.Success)
                {
                    Console.WriteLine($"子用户财务记录：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币合约API服务器：" + "【通用】查询用户财务记录(新)" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapFinancialRecordExactAsync(marginAccount);
                if (result.Success)
                {
                    Console.WriteLine($"主用户组合查询财务记录：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币合约API服务器：" + "【通用】查询用户财务记录(新)(PrivateData)" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
            }
            {
                #region 子用户客户端
                apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion

                Console.WriteLine("【通用】组合查询用户财务记录(新)");
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapFinancialRecordExactAsync(marginAccount);
                if (result.Success)
                {
                    Console.WriteLine($"子用户组合查询财务记录：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币合约API服务器：" + "【通用】组合查询用户财务记录(新)" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapUserSettlementRecordsAsync(contractCode);
                if (result.Success)
                {
                    Console.WriteLine($"主用户查询结算记录：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币合约API服务器：" + "【逐仓】查询用户结算记录" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
            }
            {
                #region 子用户客户端
                apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion

                Console.WriteLine("【逐仓】查询用户结算记录");
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapUserSettlementRecordsAsync(contractCode);
                if (result.Success)
                {
                    Console.WriteLine($"子用户查询结算记录：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币合约API服务器：" + "【逐仓】查询用户结算记录" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapCrossUserSettlementRecordsAsync(marginAccount);
                if (result.Success)
                {
                    Console.WriteLine($"主用户查询结算记录：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币合约API服务器：" + "【全仓】查询用户结算记录" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
            }
            {
                #region 子用户客户端
                apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion

                Console.WriteLine("【全仓】查询用户结算记录");
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapCrossUserSettlementRecordsAsync(marginAccount);
                if (result.Success)
                {
                    Console.WriteLine($"子用户查询结算记录：{JsonConvert.SerializeObject(result.Data)}");
                }
                else
                {
                    Console.WriteLine($"火币合约API服务器：" + "【全仓】查询用户结算记录" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapAvailableLevelRateAsync(contractCode);
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
                    Console.WriteLine($"火币合约API服务器：" + "【逐仓】查询用户可用杠杆倍数" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
            }
            {
                #region 子用户客户端
                apiCredentials = new ApiCredentials(subAccessKey, subSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion

                Console.WriteLine("【逐仓】查询用户可用杠杆倍数");
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapAvailableLevelRateAsync(contractCode);
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
                    Console.WriteLine($"火币合约API服务器：" + "【逐仓】查询用户可用杠杆倍数" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapCrossAvailableLevelRateAsync(contractCode);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】查询用户可用杠杆倍数" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapOrderLimitAsync(orderPriceType);
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】查询用户当前的下单量限制" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapFeeAsync(contractCode);
            if (result.Success)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine($"合约代码{item.ContractCode} 业务类型：{item.BusinessType} 开仓吃单手续费率：{item.OpenTakerFee} 开仓挂单手续费率：{item.OpenMakerFee}");
                }
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【通用】查询用户当前的手续费费率" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapTransferLimitAsync(contractCode);
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】查询用户当前的划转限制" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapCrossTransferLimitAsync(marginAccount);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】查询用户当前的划转限制" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapPositionLimitAsync(contractCode);
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】用户持仓量限制的查询" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapCrossPositionLimitAsync(contractCode);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】用户持仓量限制的查询" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapLeverPositionLimitAsync();
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】查询用户所有杠杆持仓量限制" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapCrossLeverPositionLimitAsync();
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】查询用户所有杠杆持仓量限制" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.LinearSwapMasterSubTransferAsync(
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】母子账户划转" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapMasterSubTransferRecordAsync(
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取母账户下的所有母子账户划转记录" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.LinearSwapTransferInnerAsync(
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
                Console.WriteLine($"火币合约API服务器：" + "【通用】同账号不同保证金账户的划转" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Account.GetLinearSwapApiTradingStatusAsync();
            if (result.Success)
            {
                Console.WriteLine($"{JsonConvert.SerializeObject(result.Data)}");                
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【通用】获取用户的API指标禁用信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
            }
        }
        #endregion
    }
}


//交易所U本位合约交易接口测试-已完成
static async Task TestUsdtMarginedApiTradeEndpoints()
{
    using (var huobiUsdtMarginedClient = new HuobiUsdtMarginedClient())
    {
        List<long> cancelIsolatedOrderIdList = new List<long>();
        List<long> cancelIsolatedClientOrderIdList = new List<long>();

        List<long> cancelCrossOrderIdList = new List<long>();
        List<long> cancelCrossClientOrderIdList = new List<long>();

        #region 对huobiUsdtMarginedClient客户端的新实例使用新的设置(这里不设置则使用之前的默认设置）
        //使用accessKey, secretKey生成一个新的API凭证
        ApiCredentials apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.GetLinearSwapCrossTradeStateAsync();
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】查询系统交易权限" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.LinearSwapSwitchPositionModeAsync(marginAccount, positionMode);
            if (result.Success)
            {
                Console.WriteLine($"保证金账户：{result.Data.MarginAccount} 持仓模式：{result.Data.PositionMode}");
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】切换持仓模式" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.LinearSwapCrossSwitchPositionModeAsync(marginAccount, positionMode);
            if (result.Success)
            {
                Console.WriteLine($"保证金账户：{result.Data.MarginAccount} 持仓模式：{result.Data.PositionMode}");
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【全仓】切换持仓模式" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                UmOrderPriceType orderPriceType = UmOrderPriceType.limit;
                decimal? tpTriggerPrice = null;
                decimal? tpOrderPrice = null;
                UmTpOrderPriceType? tpOrderPriceType = null;
                decimal? slTriggerPrice = null;
                decimal? slOrderPrice = null;
                UmSlOrderPriceType? slOrderPriceType = null;
                int? reduceOnly = null;
                long? clientOrderId = DateTimeConverter.ConvertToMicroseconds(DateTime.Now);

                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion

                Console.WriteLine("【逐仓】合约下单");
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.LinearSwapOrderAsync(
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
                    Console.WriteLine($"火币合约API服务器：" + "【逐仓】合约下单" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                UmOrderPriceType orderPriceType = UmOrderPriceType.limit;
                decimal? tpTriggerPrice = null;
                decimal? tpOrderPrice = null;
                UmTpOrderPriceType? tpOrderPriceType = null;
                decimal? slTriggerPrice = null;
                decimal? slOrderPrice = null;
                UmSlOrderPriceType? slOrderPriceType = null;
                string? pair = "DOGE-USDT";
                string? contractType = "swap";
                int? reduceOnly = null;
                long? clientOrderId = DateTimeConverter.ConvertToMicroseconds(DateTime.Now);

                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion

                Console.WriteLine("【全仓】合约下单");
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.LinearSwapCrossOrderAsync(
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
                    Console.WriteLine($"火币合约API服务器：" + "【全仓】合约下单" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
            }
            #endregion

            #region 【逐仓】合约批量下单(PrivateData)
            {
                HuobiUsdtMarginedIsolatedOrder isolatedOrder = new HuobiUsdtMarginedIsolatedOrder()
                {
                    ContractCode = "DOGE-USDT",
                    Direction = UmDirection.buy.ToString(),
                    Offset = UmOffset.open.ToString(),
                    Price = 0.04M,
                    LeverRate = UmLeverRate.LeverRate_20.GetHashCode(),
                    Volume = 1,
                    OrderPriceType = UmOrderPriceType.limit.ToString(),
                    TpTriggerPrice = null,
                    TpOrderPrice = null,
                    TpOrderPriceType = null,
                    SlTriggerPrice = null,
                    SlOrderPrice = null,
                    SlOrderPriceType = null,
                    ReduceOnly = null,
                    ClientOrderId = DateTimeConverter.ConvertToMicroseconds(DateTime.Now)
                };
                List<HuobiUsdtMarginedIsolatedOrder> isolatedOrderList = new List<HuobiUsdtMarginedIsolatedOrder>();
                isolatedOrderList.Add(isolatedOrder);
                IEnumerable<HuobiUsdtMarginedIsolatedOrder> isolatedOrders = isolatedOrderList;

                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion

                Console.WriteLine("【逐仓】合约批量下单");
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.LinearSwapBatchorderAsync(isolatedOrders);
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
                    Console.WriteLine($"火币合约API服务器：" + "【逐仓】合约批量下单" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
            }
            #endregion

            #region 【全仓】合约批量下单(PrivateData)
            {
                HuobiUsdtMarginedCrossOrder crossOrder = new HuobiUsdtMarginedCrossOrder()
                {
                    ContractCode = "DOGE-USDT",
                    Direction = UmDirection.buy.ToString(),
                    Offset = UmOffset.open.ToString(),
                    Price = 0.04M,
                    LeverRate = UmLeverRate.LeverRate_20.GetHashCode(),
                    Volume = 1,
                    OrderPriceType = UmOrderPriceType.limit.ToString(),
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
                List<HuobiUsdtMarginedCrossOrder> crossOrderList = new List<HuobiUsdtMarginedCrossOrder>();
                crossOrderList.Add(crossOrder);
                IEnumerable<HuobiUsdtMarginedCrossOrder> crossOrders = crossOrderList;

                #region 母用户客户端
                apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
                huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
                #endregion

                Console.WriteLine("【全仓】合约批量下单");
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.LinearSwapCrossBatchorderAsync(crossOrders);
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
                    Console.WriteLine($"火币合约API服务器：" + "【全仓】合约批量下单" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                if ((cancelIsolatedOrderIds == null || cancelIsolatedOrderIds.Count() <= 0) && (cancelIsolatedClientOrderIds == null || cancelIsolatedClientOrderIds.Count() <= 0))
                {
                    Console.WriteLine("【逐仓】撤销合约订单：未提供需要撤销的订单编号或者用户自定义单号");
                }
                else
                {
                    var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.LinearSwapCancelAsync(contractCode, cancelIsolatedOrderIds, cancelIsolatedClientOrderIds);
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
                        Console.WriteLine($"火币合约API服务器：" + "【逐仓】撤销合约订单" + "异常\r\n" +
                            $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                            $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                            $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                if ((cancelCrossOrderIds == null || cancelCrossOrderIds.Count() <= 0) && (cancelCrossClientOrderIds == null || cancelCrossClientOrderIds.Count() <= 0))
                {
                    Console.WriteLine("【全仓】撤销合约订单：未提供需要撤销的订单编号或者用户自定义单号");
                }
                else
                {
                    var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.LinearSwapCrossCancelAsync(contractCode, cancelCrossOrderIds, cancelCrossClientOrderIds, pair, contractType);
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
                        Console.WriteLine($"火币合约API服务器：" + "【全仓】撤销合约订单" + "异常\r\n" +
                            $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                            $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                            $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.LinearSwapCancelAllAsync(contractCode, direction, offset);
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
                    Console.WriteLine($"火币合约API服务器：" + "【逐仓】撤销全部合约单" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.LinearSwapCrossCancelAllAsync(contractCode, pair, contractType, direction, offset);
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
                    Console.WriteLine($"火币合约API服务器：" + "【全仓】撤销全部合约单" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.LinearSwapSwitchLeverRateAsync(contractCode, leverRate);
            if (result.Success)
            {
                Console.WriteLine($"合约代码：{result.Data.ContractCode}  保证金模式：{result.Data.MarginMode} 当前杠杆倍数：{result.Data.LeverRate}");
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】切换杠杆" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.LinearSwapCrossSwitchLeverRateAsync(contractCode, leverRate, pair, contractType);
            if (result.Success)
            {
                Console.WriteLine($"合约代码：{result.Data.ContractCode}  保证金模式：{result.Data.MarginMode} 当前杠杆倍数：{result.Data.LeverRate}\r\n" +
                    $"合约类型：{result.Data.ContractType} 交易对：{result.Data.Pair} 业务类型：{result.Data.BusinessType}");               
            }
            else
            {
                Console.WriteLine($"火币合约API服务器：" + "【全仓】切换杠杆" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.GetLinearSwapOrderInfoAsync(contractCode, isolatedOrderIds, isolatedClientOrderIds);
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】获取用户的合约订单信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.GetLinearSwapCrossOrderInfoAsync(contractCode, isolatedOrderIds, isolatedClientOrderIds, pair);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】获取用户的合约订单信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.GetLinearSwapOrderDetailAsync(orderId, contractCode, createdTimestamp, orderType, pageIndex, pageSize);
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】获取用户的合约订单明细信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.GetLinearSwapCrossOrderDetailAsync(orderId, contractCode, pair, createdTimestamp, orderType, pageIndex, pageSize);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】获取用户的合约订单明细信息" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.GetLinearSwapOpenordersAsync(contractCode, pageIndex, pageSize, sortBy, tradeType);
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】获取用户的合约当前未成交委托" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.GetLinearSwapCrossOpenordersAsync(contractCode, pair, pageIndex, pageSize, sortBy, tradeType);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】获取用户的合约当前未成交委托" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.GetLinearSwapHisordersAsync(tradeType, type, status, contractCode, startTime, endTime, direct, fromId);
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】获取用户的合约历史委托" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.GetLinearSwapCrossHisordersAsync(tradeType, type, status, contractCode, pair, startTime, endTime, direct, fromId);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】获取用户的合约历史委托" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.GetLinearSwapHisordersExactAsync(tradeType, type, status, contractCode, pair, startTime, endTime, direct, fromId, priceType);
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】组合查询合约历史委托" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.GetLinearSwapCrossHisordersExactAsync(tradeType, type, status, contractCode, pair, startTime, endTime, direct, fromId, priceType);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】组合查询合约历史委托" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.GetLinearSwapMatchresultsAsync(tradeType, contractCode, pair, startTime, endTime, direct, fromId);
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】获取用户的合约历史成交记录" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.GetLinearSwapCrossMatchresultsAsync(tradeType, contractCode, pair, startTime, endTime, direct, fromId);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】获取用户的合约历史成交记录" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.GetLinearSwapMatchresultsExactAsync(contractCode, tradeType, startTime, endTime, direct, fromId);
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
                Console.WriteLine($"火币合约API服务器：" + "【逐仓】组合查询用户历史成交记录" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
            var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.GetLinearSwapCrossMatchresultsExactAsync(tradeType, contractCode, pair, startTime, endTime, direct, fromId);
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
                Console.WriteLine($"火币合约API服务器：" + "【全仓】组合查询用户历史成交记录" + "异常\r\n" +
                    $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                    $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                    $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.LinearSwapLightningClosePositionAsync(contractCode, volume, direction, clientOrderId, OrderPriceType);
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
                    Console.WriteLine($"火币合约API服务器：" + "【逐仓】合约闪电平仓下单" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
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
                var result = await huobiUsdtMarginedClient.UsdtMarginedApi.Trade.LinearSwapCrossLightningClosePositionAsync(volume, direction, contractCode, pair, contract_type, clientOrderId, OrderPriceType);
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
                    Console.WriteLine($"火币合约API服务器：" + "【全仓】合约闪电平仓下单" + "异常\r\n" +
                        $"错误信息：{(result.Error == null ? "null" : result.Error)}\r\n" +
                        $"错误代码：{(result.Error == null ? "null" : result.Error.Code)}\r\n" +
                        $"错误提示：{(result.Error == null ? "null" : result.Error.Data)}");
                }
            }
            #endregion
        }
    }
}

//交易所U本位合约策略订单接口测试-开发中...
static async Task TestUsdtMarginedApiStrategyOrderEndpoints()
{
    using (var huobiUsdtMarginedClient = new HuobiUsdtMarginedClient())
    {
        #region 对huobiUsdtMarginedClient客户端的新实例使用新的设置(这里不设置则使用之前的默认设置）
        //使用accessKey, secretKey生成一个新的API凭证
        ApiCredentials apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
        //当前客户端使用新生成的API凭证
        huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
        #endregion

        await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapTriggerOrder();
        await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapTriggerCancel();
        await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapTriggerCancelAll();

        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapTrackOrder();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapTrackCancel();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapTrackCancelAll();
        
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.GetLinearSwapCrossRelationTpslOrder();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.GetLinearSwapCrossTpslHisorders();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.GetLinearSwapCrossTpslOpenorders();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.GetLinearSwapCrossTrackHisorders();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.GetLinearSwapCrossTrackOpenorders();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.GetLinearSwapCrossTriggerHisorders();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.GetLinearSwapCrossTriggerOpenorders();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.GetLinearSwapRelationTpslOrder();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.GetLinearSwapTpslHisorders();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.GetLinearSwapTpslOpenorders();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.GetLinearSwapTrackHisorders();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.GetLinearSwapTrackOpenOrders();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.GetLinearSwapTriggerHisorders();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.GetLinearSwapTriggerOpenorders();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapCrossTpslCancel();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapCrossTpslCancelAll();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapCrossTpslOrder();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapCrossTrackCancel();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapCrossTrackCancelAll();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapCrossTrackOrder();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapCrossTriggerCancel();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapCrossTriggerCancelAll();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapCrossTriggerOrder();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapTpslCancel();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapTpslCancelAll();
        //await huobiUsdtMarginedClient.UsdtMarginedApi.Strategy.LinearSwapTpslOrder();
    }
}

//交易所U本位合约划转接口测试-开发中...
static async Task TestUsdtMarginedApiTransferringEndpoints()
{
    using (var huobiUsdtMarginedClient = new HuobiUsdtMarginedClient())
    {
        #region 对huobiUsdtMarginedClient客户端的新实例使用新的设置(这里不设置则使用之前的默认设置）
        //使用accessKey, secretKey生成一个新的API凭证
        ApiCredentials apiCredentials = new ApiCredentials(mainAccessKey, mainSecretKey);
        //当前客户端使用新生成的API凭证
        huobiUsdtMarginedClient.SetApiCredentials(apiCredentials);
        #endregion

        await huobiUsdtMarginedClient.UsdtMarginedApi.Transferring.GetLinearSwapCrossTransferState();
        await huobiUsdtMarginedClient.UsdtMarginedApi.Transferring.LinearSwapAccountTransfer();
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
