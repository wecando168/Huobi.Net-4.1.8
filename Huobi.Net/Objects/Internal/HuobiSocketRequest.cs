using Newtonsoft.Json;

namespace Huobi.Net.Objects.Internal
{
    internal class HuobiRequest
    {
        [JsonIgnore]
        public string? Id { get; set; }
    }

    internal class HuobiSocketRequest: HuobiRequest
    {
        [JsonProperty("req")]
        public string Request { get; set; }

        [JsonProperty("id")]
        public new string Id { get; set; }

        /// <summary>
        /// From timestamp in second
        /// </summary>
        [JsonProperty("from",NullValueHandling = NullValueHandling.Ignore)]
        public long From { get; set; }

        /// <summary>
        /// To timestamp in second
        /// </summary>
        [JsonProperty("to", NullValueHandling = NullValueHandling.Ignore)]
        public long To { get; set; }


        public HuobiSocketRequest(string id, string topic, long? from = null, long? to = null)
        {
            Id = id;
            Request = topic;
            if (from != null)
            {
                From = (long)from;
            }
            if (to != null)
            {
                To = (long)to;
            }
        }
    }

    internal class HuobiAuthenticatedSubscribeRequest
    {
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("ch")]
        public string Channel { get; set; }

        public HuobiAuthenticatedSubscribeRequest(string channel, string action = "sub")
        {
            Action = action;
            Channel = channel;
        }
    }

    internal class HuobiSubscribeRequest: HuobiRequest
    {
        [JsonProperty("sub")]
        public string Topic { get; set; }
        [JsonProperty("id")]
        public new string Id { get; set; }

        public HuobiSubscribeRequest(string id, string topic)
        {
            Id = id;
            Topic = topic;
        }
    }

    //internal class HuobiSocketReqFromTo : HuobiRequest
    //{
    //    [JsonProperty("req")]
    //    public string req { get; set; }

    //    [JsonProperty("id")]
    //    public string id { get; set; } = string.Empty;

    //    /// <summary>
    //    /// From timestamp in second
    //    /// </summary>
    //    [JsonProperty("from")]
    //    public long from { get; set; }

    //    /// <summary>
    //    /// To timestamp in second
    //    /// </summary>
    //    [JsonProperty("to")]
    //    public long to { get; set; }

    //    /// <summary>
    //    /// get huobi socket request from timestamp to timestamp
    //    /// </summary>
    //    /// <param name="socketClientId">web socket client id</param>
    //    /// <param name="topic">request topic</param>
    //    /// <param name="from">From timestamp in second</param>
    //    /// <param name="to">To timestamp in second</param>
    //    public HuobiSocketReqFromTo(string topic, string socketClientId, long from, long to)
    //    {
    //        this.req = topic;
    //        this.id = socketClientId;
    //        this.from = from;
    //        this.to = to;
    //    }
    //}
}
