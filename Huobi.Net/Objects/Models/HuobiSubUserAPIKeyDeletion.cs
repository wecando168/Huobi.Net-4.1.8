using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Huobi sub user API key deletion info
    /// </summary>
    public class HuobiSubUserAPIKeyDeletion
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
