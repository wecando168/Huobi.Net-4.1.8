namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Huobi sub user API key modification info
    /// </summary>
    public class WWTHuobiSubUserAPIKeyModification
    {
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
