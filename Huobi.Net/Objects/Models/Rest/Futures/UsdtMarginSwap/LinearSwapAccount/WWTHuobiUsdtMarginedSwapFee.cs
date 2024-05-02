using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount.CommonBaseModels;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount
{
    /// <summary>
    /// 【通用】查询用户当前的手续费费率(PrivateData)
    /// </summary>
    public class WWTHuobiUsdtMarginedSwapFee
    {        
        /// <summary>
        /// 品种代码	"BTC","ETH"...
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)] 
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// 合约代码	"BTC-USDT" ...
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 开仓挂单的手续费费率，小数形式
        /// </summary>
        [JsonProperty("open_maker_fee", NullValueHandling = NullValueHandling.Ignore)]
        public string OpenMakerFee { get; set; } = string.Empty;

        /// <summary>
        /// 开仓吃单的手续费费率，小数形式
        /// </summary>
        [JsonProperty("open_taker_fee", NullValueHandling = NullValueHandling.Ignore)]
        public string OpenTakerFee { get; set; } = string.Empty;

        /// <summary>
        /// 平仓挂单的手续费费率，小数形式
        /// </summary>
        [JsonProperty("close_maker_fee", NullValueHandling = NullValueHandling.Ignore)]
        public string CloseMakerFee { get; set; } = string.Empty;

        /// <summary>
        /// 平仓吃单的手续费费率，小数形式
        /// </summary>
        [JsonProperty("close_taker_fee", NullValueHandling = NullValueHandling.Ignore)]
        public string CloseTakerFee { get; set; } = string.Empty;

        /// <summary>
        /// 手续费币种	"USDT"...
        /// </summary>
        [JsonProperty("fee_asset", NullValueHandling = NullValueHandling.Ignore)]
        public string FeeAsset { get; set; } = string.Empty;

        /// <summary>
        /// 合约类型	swap（永续）、this_week（当周）、next_week（次周）、quarter（当季）、next_quarter（次季）
        /// </summary>
        [JsonProperty("contract_type", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 交易对	如：“BTC-USDT”
        /// </summary>
        [JsonProperty("pair", NullValueHandling = NullValueHandling.Ignore)]
        public string Pair { get; set; } = string.Empty;

        /// <summary>
        /// 业务类型	futures：交割、swap：永续
        /// </summary>
        [JsonProperty("business_type", NullValueHandling = NullValueHandling.Ignore)]
        public string BusinessType { get; set; } = string.Empty;

        /// <summary>
        /// 交割的手续费费率，小数形式	
        /// </summary>
        [JsonProperty("delivery_fee", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliveryFee { get; set; } = string.Empty;

        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;
    }    
}