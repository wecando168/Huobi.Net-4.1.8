using System;
using System.Collections.Generic;
using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Rest.Futures.UsdtMarginSwap.LinearSwapReferenceData
{
    /// <summary>
    /// Status of the usdt margined system
    /// </summary>
    public class HuobiUsdtMarginedMarketStatus
    {
        /// <summary>
        /// 合约页面基本信息
        /// </summary>
        [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
        public StatusPage Page { get; set; } = default!;

        /// <summary>
        /// 系统组件及状态
        /// </summary>
        [JsonProperty("components", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Component> Components { get; set; } = Array.Empty<Component>();

        /// <summary>
        /// 系统故障事件及状态
        /// </summary>
        [JsonProperty("incidents", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Incident> Incidents { get; set; } = Array.Empty<Incident>();

        /// <summary>
        /// 系统计划维护事件及状态
        /// </summary>
        [JsonProperty("scheduled_maintenances", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Incident> ScheduledMaintenances { get; set; } = Array.Empty<Incident>();

        /// <summary>
        /// 系统整体状态
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public SystemStatus Status { get; set; } = default!;
    }

    /// <summary>
    /// 合约页面基本信息
    /// </summary>
    public class StatusPage
    {
        /// <summary>
        /// 页面id
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string PageId { get; set; } = string.Empty;

        /// <summary>
        /// 页面名称
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string PageName { get; set; } = string.Empty;

        /// <summary>
        /// 页面地址
        /// </summary>
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string PageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 时区
        /// </summary>
        [JsonProperty("time_zone", NullValueHandling = NullValueHandling.Ignore)]
        public string TimeZone { get; set; } = string.Empty;

        /// <summary>
        /// 页面最新一次更新时间
        /// </summary>
        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(DateTimeConverter))]
        public DateTime PageUpdatedTime { get; set; }
    }

    /// <summary>
    /// 系统组件及状态
    /// </summary>
    public class Component
    {
        /// <summary>
        /// 组件id
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string ComponentId { get; set; } = string.Empty;

        /// <summary>
        /// 组件名称
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string ComponentName { get; set; } = string.Empty;

        /// <summary>
        /// 组件状态
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string ComponentStatus { get; set; } = string.Empty;

        /// <summary>
        /// 组件创建时间
        /// </summary>
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(DateTimeConverter))]
        public DateTime ComponentCreatedTime { get; set; }

        /// <summary>
        /// 组件更新时间
        /// </summary>
        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(DateTimeConverter))]
        public DateTime ComponentUpdatedTime { get; set; }

        /// <summary>
        /// 组件位置
        /// </summary>
        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        public int ComponentPosition { get; set; } = default!;

        /// <summary>
        /// 组件描述
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string ComponentDescription { get; set; } = string.Empty;

        /// <summary>
        /// 组件展示状态
        /// </summary>
        [JsonProperty("showcase", NullValueHandling = NullValueHandling.Ignore)]
        public bool ComponentShowcase { get; set; } = default!;

        /// <summary>
        /// 组件分组编号
        /// </summary>
        [JsonProperty("group_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ComponentGroupId { get; set; } = string.Empty;

        /// <summary>
        /// 组件页面编号
        /// </summary>
        [JsonProperty("page_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ComponentPageId { get; set; } = string.Empty;

        /// <summary>
        /// 组件分组状态
        /// </summary>
        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public bool Group { get; set; } = default!;

        /// <summary>
        /// 组件是否仅显示是否降级
        /// </summary>
        [JsonProperty("only_show_if_degraded", NullValueHandling = NullValueHandling.Ignore)]
        public bool OnlyShowIfDegraded { get; set; } = default!;
    }

    /// <summary>
    /// 事件及状态
    /// </summary>
    public class Incident
    {
        /// <summary>
        /// 事件编号
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string IncidentId { get; set; } = string.Empty;

        /// <summary>
        /// 事件名称
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string IncidentName { get; set; } = string.Empty;

        /// <summary>
        /// 事件状态
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string IncidentStatus { get; set; } = string.Empty;

        /// <summary>
        /// 事件创建时间
        /// </summary>
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(DateTimeConverter))]
        public DateTime IncidentCreatedTime { get; set; }

        /// <summary>
        /// 事件更新时间
        /// </summary>
        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(DateTimeConverter))]
        public DateTime IncidentUpdatedTime { get; set; }

        /// <summary>
        /// 事件监控时间
        /// </summary>
        [JsonProperty("monitoring_at", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(DateTimeConverter))]
        public DateTime IncidentMonitoringTime { get; set; }

        /// <summary>
        /// 事件解决时间
        /// </summary>
        [JsonProperty("resolved_at", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(DateTimeConverter))]
        public DateTime IncidentResolvedTime { get; set; }

        /// <summary>
        /// 事件影响
        /// </summary>
        [JsonProperty("impact", NullValueHandling = NullValueHandling.Ignore)]
        public string Impact { get; set; } = string.Empty;

        /// <summary>
        /// 事件短链接
        /// </summary>
        [JsonProperty("shortlink", NullValueHandling = NullValueHandling.Ignore)]
        public string IncidentShortlink { get; set; } = string.Empty;

        /// <summary>
        /// 事件开始时间
        /// </summary>
        [JsonProperty("started_at", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(DateTimeConverter))]
        public DateTime IncidentStartedTime { get; set; }

        /// <summary>
        /// 事件页面编号
        /// </summary>
        [JsonProperty("page_id", NullValueHandling = NullValueHandling.Ignore)]
        public string IncidentPageId { get; set; } = string.Empty;

        /// <summary>
        /// 事件更新
        /// </summary>
        [JsonProperty("incident_updates", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<IncidentUpdates> IncidentUpdates { get; set; } = Array.Empty<IncidentUpdates>();

        /// <summary>
        /// 事件构成
        /// </summary>
        [JsonProperty("components", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Component> Components { get; set; } = Array.Empty<Component>();

        /// <summary>
        /// 预计开始时间
        /// </summary>
        [JsonProperty("scheduled_for", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ScheduledMaintenanceScheduledForTime { get; set; }

        /// <summary>
        /// 预计持续时间
        /// </summary>
        [JsonProperty("scheduled_until", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ScheduledMaintenanceScheduledUntilTime { get; set; }
    }

    /// <summary>
    /// 事件更新
    /// </summary>
    public class IncidentUpdates
    {
        /// <summary>
        /// 事件更新编号
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string IncidentUpdatesId { get; set; } = string.Empty;

        /// <summary>
        /// 事件更新状态
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string IncidentUpdatesStatus { get; set; } = string.Empty;

        /// <summary>
        /// 事件更新主体
        /// </summary>
        [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
        public string IncidentUpdatesBody { get; set; } = string.Empty;

        /// <summary>
        /// 事件更新对应事件编号
        /// </summary>
        [JsonProperty("incident_id", NullValueHandling = NullValueHandling.Ignore)]
        public string IncidentUpdatesIncidentId { get; set; } = string.Empty;

        /// <summary>
        /// 事件更新创建时间
        /// </summary>
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(DateTimeConverter))]
        public DateTime IncidentUpdatesCreatedTime { get; set; }

        /// <summary>
        /// 事件更新更新时间
        /// </summary>
        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(DateTimeConverter))]
        public DateTime IncidentUpdatesUpdatedTime { get; set; }

        /// <summary>
        /// 事件更新显示事件
        /// </summary>
        [JsonProperty("display_at", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(DateTimeConverter))]
        public DateTime IncidentUpdatesDisplayTime { get; set; }

        /// <summary>
        /// 受到影响的组件
        /// </summary>
        [JsonProperty("affected_components", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<AffectedComponent> AffectedComponents { get; set; } = Array.Empty<AffectedComponent>();

        /// <summary>
        /// 是否发送通知
        /// </summary>
        [JsonProperty("deliver_notifications", NullValueHandling = NullValueHandling.Ignore)]
        public bool DeliverNotifications { get; set; }

        /// <summary>
        /// 自定义推文
        /// </summary>
        [JsonProperty("custom_tweet", NullValueHandling = NullValueHandling.Ignore)]
        public string CustomTweet { get; set; } = string.Empty;

        /// <summary>
        /// 推文编号
        /// </summary>
        [JsonProperty("tweet_id", NullValueHandling = NullValueHandling.Ignore)]
        public string TweetId { get; set; } = string.Empty;
    }

    /// <summary>
    /// 受到影响的组件
    /// </summary>
    public class AffectedComponent
    {
        /// <summary>
        /// 受到影响组件代码
        /// </summary>
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string AffectedComponentCode { get; set; } = string.Empty;

        /// <summary>
        /// 受到影响组件名称
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string AffectedComponentName { get; set; } = string.Empty;

        /// <summary>
        /// 受到影响组件之前状态
        /// </summary>
        [JsonProperty("old_status", NullValueHandling = NullValueHandling.Ignore)]
        public string AffectedComponentOldStatus { get; set; } = string.Empty;

        /// <summary>
        /// 受到影响组件新状态
        /// </summary>
        [JsonProperty("new_status", NullValueHandling = NullValueHandling.Ignore)]
        public string AffectedComponentNewStatus { get; set; } = string.Empty;
    }

    /// <summary>
    /// 系统整体状态
    /// </summary>
    public class SystemStatus
    {
        /// <summary>
        /// 系统状态指标
        /// </summary>
        [JsonProperty("indicator", NullValueHandling = NullValueHandling.Ignore)]
        public string Indicator { get; set; } = string.Empty;

        /// <summary>
        /// 系统状态描述
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; } = string.Empty;
    }

}
