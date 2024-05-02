namespace Huobi.Net.Objects.Internal
{
    internal class HuobiApiResponseV2<T>
    {
        /// <summary>
        /// API接口返回码
        /// </summary>
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public int Code { get; set; }

        /// <summary>
        /// 错误消息（如果有）
        /// </summary>
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 接口返回数据主体
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
#pragma warning disable 8618
        public T Data { get; set; }
#pragma warning restore 8618

        /// <summary>
        /// 接口返回数据主体
        /// </summary>
        [JsonProperty("ticks", NullValueHandling = NullValueHandling.Ignore)]
#pragma warning disable 8618
        public T Ticks { set => Data = value; get => Data; }
#pragma warning restore 8618
    }
}
