using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapAccount
{
    /// <summary>
    /// 【通用】查询用户财务记录(新)(PrivateData)
    /// </summary>
    public class WWTHuobiUsdtMarginedFinancialRecord
    {
        /// <summary>
        /// 下次查询ID
        /// </summary>
        [JsonProperty("query_id", NullValueHandling = NullValueHandling.Ignore)]
        public long QueryId { get; set; } = default(long);

        /// <summary>
        /// 唯一标识
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; } = default(long);

        /// <summary>
        /// 交易类型	3:平多; 4:平空; 5:开仓手续费-吃单; 6:开仓手续费-挂单; 7:平仓手续费-吃单; 8:平仓手续费-挂单; 9:交割平多; 10:交割平空; 11:交割手续费; 12:强制平多; 13:强制平空; 14:从币币转入; 15:转出至币币; 16:结算未实现盈亏-多仓; 17:结算未实现盈亏-空仓; 19:穿仓分摊; 26:系统; 28:活动奖励; 29:返利; 30:资金费-收入; 31:资金费-支出; 34:转出到子账号合约账户; 35:从子账号合约账户转入; 36:转出到母账号合约账户; 37:从母账号合约账户转入;38:从其他保证金账户转入 ;39:转出到其他保证金账户 ;
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public int Type { get; set; } = default;

        /// <summary>
        /// 金额（计价货币）
        /// </summary>
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public decimal Amount { get; set; } = default;

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty("ts", NullValueHandling = NullValueHandling.Ignore)]
        public long Timestamp { get; set; } = default(long);

        /// <summary>
        /// 合约代码	"BTC-USDT"...
        /// </summary>
        [JsonProperty("contract_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>
        /// 币种	"USDT"...
        /// </summary>
        [JsonProperty("asset", NullValueHandling = NullValueHandling.Ignore)]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// 保证金账户	"BTC-USDT","USDT"...
        /// </summary>
        [JsonProperty("margin_account", NullValueHandling = NullValueHandling.Ignore)]
        public string MarginAccount { get; set; } = string.Empty;

        /// <summary>
        /// 对手方保证金账户，仅在type交易类型为34、35、36、37、38、39时有值，其他类型为空字符串	"BTC-USDT"...
        /// </summary>
        [JsonProperty("face_margin_account", NullValueHandling = NullValueHandling.Ignore)]
        public string FaceMarginAccount { get; set; } = string.Empty;
    }

}