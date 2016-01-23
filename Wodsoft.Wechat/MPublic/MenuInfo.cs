using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 微信菜单信息。
    /// </summary>
    public class MenuInfo
    {
        /// <summary>
        /// 获取或设置菜单项。
        /// </summary>
        [JsonProperty("button")]
        public MenuGeneralItem[] Items { get; set; }

        /// <summary>
        /// 获取或设置匹配规则。
        /// </summary>
        [JsonProperty("matchrule")]
        public MenuRule Rule { get; set; }

        /// <summary>
        /// 获取或设置菜单Id。
        /// </summary>
        [JsonProperty("menuid")]
        public int Id { get; set; }
    }
}
