using Newtonsoft.Json;

namespace Huobi.Net.Objects.Internal
{
    internal class HuobiApiResponseV3<T>
    {
        /// <summary>
        /// API接口返回码
        /// </summary>
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public int Code { get; set; } = default(int);

        /// <summary>
        /// 结果描述
        /// </summary>
        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 接口返回数据主体
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
#pragma warning disable 8618
        public T Data { get; set; }
#pragma warning restore 8618
    }
}
