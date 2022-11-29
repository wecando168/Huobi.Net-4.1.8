using System;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Internal
{
    internal abstract class HuobiApiResponse
    {
        /// <summary>
        /// API接口返回状态
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        internal string? Status { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("err-msg", NullValueHandling = NullValueHandling.Ignore)]
        internal string? ErrorMessage { get; set; }

        /// <summary>
        /// 错误信息2
        /// </summary>
        [JsonProperty("err_msg", NullValueHandling = NullValueHandling.Ignore)]
        internal string? ErrorMessage2 { set => ErrorMessage = value; get => ErrorMessage; }

        /// <summary>
        /// 错误编码
        /// </summary>
        [JsonProperty("err-code", NullValueHandling = NullValueHandling.Ignore)]
        internal string? ErrorCode { get; set; }

        /// <summary>
        /// 错误编码2
        /// </summary>
        [JsonProperty("err_code", NullValueHandling = NullValueHandling.Ignore)]
        internal string? ErrorCode2 { set => ErrorCode = value; get => ErrorCode; }
    }

    internal class HuobiBasicResponse<T> : HuobiApiResponse
    {
        /// <summary>
        /// 接口返回数据主体2
        /// </summary>
        [JsonProperty("tick", NullValueHandling = NullValueHandling.Ignore)]
        private T Tick { set => Data = value; get => Data; }

        /// <summary>
        /// 接口返回数据主体2
        /// </summary>
        [JsonProperty("ticks", NullValueHandling = NullValueHandling.Ignore)]
        private T Ticks { set => Data = value; get => Data; }

        /// <summary>
        /// 接口返回的UTC时间的时间戳，单位毫秒
        /// </summary>
        [JsonProperty("ts", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 接口数据对应的数据流。部分接口没有对应数据流因此不返回此字段
        /// </summary>
        [JsonProperty("ch", NullValueHandling = NullValueHandling.Ignore)]
        public string Channel { get; set; } = string.Empty;

        /// <summary>
        /// 下一查询起始时间（当请求字段”direct”为”prev”时有效）, 下一查询结束时间（当请求字段”direct”为”next”时有效）。注：仅在检索出的总条目数量超出size字段限定时，此返回字段存在。
        /// </summary>
        [JsonProperty("next-time", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(DateTimeConverter))]
        private DateTime NextTime { get => Timestamp; set => Timestamp = value; }

        /// <summary>
        /// 接口返回数据主体1
        /// </summary>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)] 
        public T Data { get; set; } = default!;
    }
}
