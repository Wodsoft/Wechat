using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 微信关键字菜单项。
    /// </summary>
    public abstract class MenuKeyItem : MenuItem
    {
        /// <summary>
        /// 获取或设置关键字。
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }
    }
}
