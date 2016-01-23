using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 微信菜单基类。
    /// </summary>
    public abstract class Menu
    {
        /// <summary>
        /// 获取或设置菜单名称。
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
