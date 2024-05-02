using System;
using System.Collections.Generic;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Huobi create sub-user account request
    /// </summary>
    public class WWTHuobiCreateSubUserAccountRequest
    {
        /// <summary>
        /// Request huobi create sub user account info list
        /// </summary>
        [JsonProperty("userList")]
        private IEnumerable<HuobiCreateSubUserAccountRequestInfo> UserList = default!;

        /// <summary>
        /// set user list
        /// </summary>
        /// <param name="userList"></param>
        public WWTHuobiCreateSubUserAccountRequest(IEnumerable<HuobiCreateSubUserAccountRequestInfo> userList)
        {
            this.UserList = userList;
        }

        /// <summary>
        /// 转换为Json字符串
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class HuobiCreateSubUserAccountRequestInfo
    {
        /// <summary>
        /// Sub user name, an important identifier of the sub user's identity, requires unique within the huobi platform
        /// 子用户名，子用户身份的重要标识，要求火币平台内唯一
        /// 6至20位字母和数字的组合，可为纯字母；若字母和数字的组合，需以字母开头；字母不区分大小写；
        /// </summary>
        [JsonProperty("userName")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Sub user note, no unique requirements
        /// 子用户备注，无唯一性要求
        /// 最多20位字符，字符类型不限
        /// </summary>
        [JsonProperty("note", NullValueHandling = NullValueHandling.Ignore)]
        public string Note { get; set; } = string.Empty;
    }
}