using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 微信菜单项。
    /// </summary>
    public abstract class MenuItem : Menu
    {
        /// <summary>
        /// 获取菜单类型。
        /// </summary>
        [JsonProperty("type")]
        public abstract string Type { get; }
    }
}
