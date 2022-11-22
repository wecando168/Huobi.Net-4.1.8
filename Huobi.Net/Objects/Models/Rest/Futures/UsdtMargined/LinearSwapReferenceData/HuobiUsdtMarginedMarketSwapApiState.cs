using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMargined.LinearSwapReferenceData
{
    /// <summary>
    /// 【逐仓】查询系统状态
    /// </summary>
    public class HuobiUsdtMarginedMarketSwapApiState
    {
        /// <summary>
        /// 品种代码	"BTC","ETH"...
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// 合约代码	"BTC-USDT"...
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金模式
        /// </summary>
        [JsonProperty("margin_mode", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// 保证金账户
        /// </summary>
        [JsonProperty("margin_account", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginAccount { get; set; } = string.Empty;

        /// <summary>
        /// 开仓下单权限："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("open", NullValueHandling = NullValueHandling.Ignore)]
        public int Open { get; set; } = default;

        /// <summary>
        /// 平仓下单权限："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("close", NullValueHandling = NullValueHandling.Ignore)]
        public int Close { get; set; } = default;

        /// <summary>
        /// 撤单权限："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("cancel", NullValueHandling = NullValueHandling.Ignore)]
        public int Cancel { get; set; } = default;

        /// <summary>
        /// 从币币转入的权限："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("transfer_in", NullValueHandling = NullValueHandling.Ignore)]
        public int TransferIn { get; set; } = default;

        /// <summary>
        /// 转出至币币的权限："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("transfer_out", NullValueHandling = NullValueHandling.Ignore)]
        public int TransferOut { get; set; } = default;

        /// <summary>
        /// 从母账号划转到子账号的权限："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("master_transfer_sub", NullValueHandling = NullValueHandling.Ignore)]
        public int MasterTransferSub { get; set; } = default;

        /// <summary>
        /// 从子账号划转到母账号的权限："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("sub_transfer_master", NullValueHandling = NullValueHandling.Ignore)]
        public int SubTransferMaster { get; set; } = default;

        /// <summary>
        /// 母账号划转到子账号的转入权限-跨账户："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("master_transfer_sub_inner_in", NullValueHandling = NullValueHandling.Ignore)]
        public int MasterTransferSubInnerIn { get; set; } = default;

        /// <summary>
        /// 母账号划转到子账号的转出权限-跨账户："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("master_transfer_sub_inner_out", NullValueHandling = NullValueHandling.Ignore)]
        public int MasterTransferSubInnerOut { get; set; } = default;

        /// <summary>
        /// 子账号划转到母账号的转入权限-跨账户："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("sub_transfer_master_inner_in", NullValueHandling = NullValueHandling.Ignore)]
        public int SubTransferMasterInnerIn { get; set; } = default;

        /// <summary>
        /// 子账号划转到母账号的转出权限-跨账户："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("sub_transfer_master_inner_out", NullValueHandling = NullValueHandling.Ignore)]
        public int SubTransferMasterInnerOut { get; set; } = default;

        /// <summary>
        /// 同账号不同保证金账户划转的转入权限："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("transfer_inner_in", NullValueHandling = NullValueHandling.Ignore)]
        public int TransferInnerIn { get; set; } = default;

        /// <summary>
        /// 同账号不同保证金账户划转的转出权限："1"表示可用，“0”表示不可用
        /// </summary>
        [JsonProperty("transfer_inner_out", NullValueHandling = NullValueHandling.Ignore)]
        public int TransferInnerOut { get; set; } = default;

        /// <summary>
        /// 成交分区 如 USDT
        /// </summary>
        [JsonProperty("trade_partition", NullValueHandling = NullValueHandling.Ignore)]
        public string TradePartition { get; set; } = string.Empty;
    }
}