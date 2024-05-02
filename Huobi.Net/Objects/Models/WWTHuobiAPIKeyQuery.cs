using CryptoExchange.Net.Converters;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Huobi sub user API key query info
    /// </summary>
    public class WWTHuobiAPIKeyQuery
    {
        /// <summary>
        /// The access key of the account
        /// 访问密钥
        /// </summary>
        [JsonProperty("accessKey")]
        public string AccessKey { get; set; } = string.Empty;

        /// <summary>
        /// The secret key status of the account
        /// API key当前状态	normal(正常)，expired(已过期)
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// The access key note of the account
        /// API key备注	最多255位字符，字符类型不限
        /// </summary>
        [JsonProperty("note")]
        public string AccessKeyNote { get; set; } = string.Empty;

        /// <summary>
        /// The access key permission of the account
        /// API key权限:取值范围：readOnly、trade，其中readOnly必传，trade选传，两个间用半角逗号分隔。
        /// </summary>
        [JsonProperty("permission")]
        public string Permission { get; set; } = string.Empty;

        /// <summary>
        /// The access key ip addresses of the account
        /// API key绑定的IP地址	
        /// </summary>
        [JsonProperty("ipAddresses")]
        public string IpAddresses { get; set; } = string.Empty;

        /// <summary>
        /// The access key expire in (days) of the account
        /// API key剩余有效天数	若为-1，则表示永久有效	
        /// </summary>
        [JsonProperty("validDays")]
        public int ValidDays { get; set; }

        /// <summary>
        /// The access key creation time of the account
        /// API key创建时间	
        /// </summary>
        [JsonProperty("createTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime createTime { get; set; }

        /// <summary>
        /// The access key  last modified time of the account
        /// API key最近一次修改时间
        /// </summary>
        [JsonProperty("updateTime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime UpdateTime { get; set; }
    }
}