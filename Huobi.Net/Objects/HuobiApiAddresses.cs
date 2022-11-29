namespace Huobi.Net.Objects
{
    /// <summary>
    /// Api addresses usable for the Huobi clients
    /// </summary>
    public class HuobiApiAddresses
    {
        /// <summary>
        /// The address used by the HuobiClient for the rest spot API
        /// HuobiSpotClient 用于现货的REST API 的地址
        /// </summary>
        public string RestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the HuobiSocketClient for the public socket spot API
        /// HuobiSocketClient 用于现货公共套接字 API 的地址
        /// </summary>
        public string SocketClientPublicAddress { get; set; } = "";
        /// <summary>
        /// The address used by the HuobiSocketClient for the private socket spot API
        /// HuobiSocketClient 用于现货私有套接字 API 的地址
        /// </summary>
        public string SocketClientPrivateAddress { get; set; } = "";
        /// <summary>
        /// The address used by the HuobiSocketClient for the incremental order book socket spot API
        /// HuobiSocketClient用于增量订单簿socket API的地址
        /// </summary>
        public string SocketClientIncrementalOrderBookAddress { get; set; } = "";

        /// <summary>
        /// The address used by the HuobiClient for the rest usdt margin swaps API
        /// U本位合约服务器Rest API地址
        /// 备用：api.btcgateway.pro
        /// AWS专用：api.hbdm.vn
        /// </summary>
        public string UsdtMarginSwapRestClientAddress { get; set; } = "";

        /// <summary>
        /// The address used by the HuobiSocketClient for the public socket usdt margin swaps API
        /// U本位合约站行情请求以及订阅地址
        /// 备用：wss://api.btcgateway.pro/linear-swap-ws
        /// </summary>
        public string SocketClientUsdtMarginSwapPublicAddress { get; set; } = "";

        /// <summary>
        /// The address used by the HuobiSocketClient for the index/basis socket usdt margin swaps API
        /// U本位合约站指数K线及基差数据订阅地址
        /// 备用：wss://api.btcgateway.pro/ws_index
        /// </summary>
        public string SocketClientUsdtMarginSwapIndexAddress { get; set; } = "";

        /// <summary>
        /// The address used by the HuobiSocketClient for the private user socket usdt margin swaps API
        /// U本位合约站订单推送订阅地址
        /// 备用：wss://api.btcgateway.pro/linear-swap-notification
        /// </summary>
        public string SocketClientUsdtMarginSwapPrivateAddress { get; set; } = "";

        /// <summary>
        /// U本位合约站系统状态更新订阅地址
        /// 备用：wss://api.btcgateway.pro/center-notification
        /// </summary>
        public string SocketClientCenterNotificationAddress { get; set; } = "";

        /// <summary>
        /// U本位合约服务器状态验证地址
        /// </summary>
        public string UsdtMarginedStatusAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the Huobi.com API
        /// </summary>
        public static HuobiApiAddresses Default = new HuobiApiAddresses
        {
            RestClientAddress = "https://api.huobi.pro",
            SocketClientPublicAddress = "wss://api.huobi.pro/ws",
            SocketClientPrivateAddress = "wss://api.huobi.pro/ws/v2",
            SocketClientIncrementalOrderBookAddress = "wss://api.huobi.pro/feed",
            UsdtMarginSwapRestClientAddress = "https://api.hbdm.com",
            SocketClientUsdtMarginSwapPublicAddress = "wss://api.hbdm.com/linear-swap-ws",
            SocketClientUsdtMarginSwapIndexAddress = "wss://api.hbdm.com/ws_index",
            SocketClientUsdtMarginSwapPrivateAddress = "wss://api.hbdm.com/linear-swap-notification",
            UsdtMarginedStatusAddress = "https://status-linear-swap.huobigroup.com",
            SocketClientCenterNotificationAddress = "wss://api.hbdm.com/center-notification"
        };
    }
}
