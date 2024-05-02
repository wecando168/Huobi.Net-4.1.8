namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Huobi sub-user account info
    /// </summary>
    public class WWTHuobiSubUserCreation
    {
        /// <summary>
        /// Sub user name
        /// </summary>
        [JsonProperty("userName")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Sub user note
        /// </summary>
        [JsonProperty("note", NullValueHandling = NullValueHandling.Ignore)]
        public string? Note { get; set; } = string.Empty;

        /// <summary>
        /// Sub user id
        /// </summary>
        [JsonProperty("uid", NullValueHandling = NullValueHandling.Ignore)]
        public long? SubUserID { get; set; }

        /// <summary>
        /// Error code
        /// </summary>
        [JsonProperty("errCode", NullValueHandling = NullValueHandling.Ignore)]
        public string? ErrCode { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        [JsonProperty("errMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string? ErrMessage { get; set; }
    }
}