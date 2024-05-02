using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Huobi sub user API key creation info
    /// </summary>
    public class WWTHuobiSubUserAPIKeyCreation
    {
        /// <summary>
        /// The access key of the account
        /// 访问密钥
        /// </summary>
        [JsonProperty("accessKey")]
        public string AccessKey { get; set; } = string.Empty;

        /// <summary>
        /// The secret key of the account
        /// 执行密钥
        /// </summary>
        [JsonProperty("secretKey")]
        public string SecretKey { get; set; } = string.Empty;

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
    }
}