namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Huobi sub user API key deletion info
    /// </summary>
    public class WWTHuobiSubUserAPIKeyDeletion
    {
        /// <summary>
        /// Status code
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        /// Deletion data(null)
        /// </summary>
        [JsonProperty("data")]
        public object? Data { get; set; } = null;
    }
}
