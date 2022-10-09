﻿using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Lock/Unlock Sub User
    /// </summary>
    public class HuobiLockOrUnlockSubUser
    {
        /// <summary>
        /// Sub user UID
        /// </summary>
        [JsonProperty("subUid")]
        public long SubUid { get; set; }
        /// <summary>
        /// The state of sub user(lock,normal)
        /// </summary>
        [JsonProperty("userState")]
        public string UserState { get; set; } = string.Empty;
    }
}
