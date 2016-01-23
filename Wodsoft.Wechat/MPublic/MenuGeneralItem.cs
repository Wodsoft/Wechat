using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 微信通用菜单。
    /// </summary>
    public class MenuGeneralItem : Menu
    {
        /// <summary>
        /// 获取或设置菜单类型。
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 获取或设置关键字。
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }
        
        /// <summary>
        /// 获取或设置跳转地址。
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// 获取或设置图文素材Id。
        /// </summary>
        [JsonProperty("media_id")]
        public string Media { get; set; }
        
        /// <summary>
        /// 获取或设置子菜单。
        /// </summary>
        [JsonProperty("sub_button")]
        public MenuGeneralItem[] Items { get; set; }
    }
}
