namespace Huobi.Net.Objects
{
    /// <summary>
    /// Api addresses usable for the Huobi clients
    /// </summary>
    public class HuobiApiAddresses
    {
        /// <summary>
        /// The address used by the HuobiSpotClient for the spot rest API
        /// HuobiSpotClient 用于现货的REST API 的地址
        /// </summary>
        public string RestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the HuobiSocketClient for the spot public socket API
        /// HuobiSocketClient 用于现货公共套接字 API 的地址
        /// </summary>
        public string SocketClientPublicAddress { get; set; } = "";
        /// <summary>
        /// The address used by the HuobiSocketClient for the spot private socket API
        /// HuobiSocketClient 用于现货私有套接字 API 的地址
        /// </summary>
        public string SocketClientPrivateAddress { get; set; } = "";
        /// <summary>
        /// The address used by the HuobiSocketClient for the spot incremental order book socket API
        /// HuobiSocketClient用于增量订单簿socket API的地址
        /// </summary>
        public string SocketClientIncrementalOrderBookAddress { get; set; } = "";

        /// <summary>
        /// U本位合约服务器状态验证地址
        /// </summary>
        public string UsdtMarginedStatusAddress { get; set; } = "";

        /// <summary>
        /// The address used by the HuobiSpotClient for the usdt margined rest API
        /// U本位合约服务器Rest API地址
        /// 备用：api.btcgateway.pro
        /// AWS专用：api.hbdm.vn
        /// </summary>
        public string UsdtMarginedRestClientAddress { get; set; } = "";

        /// <summary>
        /// The address used by the HuobiSocketClient for the usdt margined public socket API
        /// U本位合约站行情请求以及订阅地址
        /// 备用：wss://api.btcgateway.pro/linear-swap-ws
        /// </summary>
        public string SocketClientLinearSwapWsAddress { get; set; } = "";

        /// <summary>
        /// The address used by the HuobiSocketClient for the usdt margined private socket API
        /// U本位合约站订单推送订阅地址
        /// 备用：wss://api.btcgateway.pro/linear-swap-notification
        /// </summary>
        public string SocketClientLinearSwapNotificationAddress { get; set; } = "";

        /// <summary>
        /// The address used by the HuobiSocketClient for the spot incremental order book socket API
        /// U本位合约站指数K线及基差数据订阅地址
        /// 备用：wss://api.btcgateway.pro/ws_index
        /// </summary>
        public string SocketClientWsIndexAddress { get; set; } = "";

        /// <summary>
        /// U本位合约站系统状态更新订阅地址
        /// 备用：wss://api.btcgateway.pro/center-notification
        /// </summary>
        public string SocketClientCenterNotificationAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the Huobi.com API
        /// </summary>
        public static HuobiApiAddresses Default = new HuobiApiAddresses
        {
            RestClientAddress = "https://api.huobi.pro",
            SocketClientPublicAddress = "wss://api.huobi.pro/ws",
            SocketClientPrivateAddress = "wss://api.huobi.pro/ws/v2",
            SocketClientIncrementalOrderBookAddress = "wss://api.huobi.pro/feed",
            UsdtMarginedStatusAddress = "https://status-linear-swap.huobigroup.com",
            UsdtMarginedRestClientAddress = "https://api.hbdm.com",
            SocketClientLinearSwapWsAddress = "wss://api.hbdm.com/linear-swap-ws",
            SocketClientLinearSwapNotificationAddress = "wss://api.hbdm.com/linear-swap-notification",
            SocketClientWsIndexAddress = "wss://api.hbdm.com/ws_index",
            SocketClientCenterNotificationAddress = "wss://api.hbdm.com/center-notification"
        };
    }
}
