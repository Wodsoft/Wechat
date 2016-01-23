using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 微信菜单集合。
    /// </summary>
    public class MenuCollection : Menu
    {
        /// <summary>
        /// 获取或设置子菜单。
        /// </summary>
        [JsonProperty("sub_button")]
        public MenuItem[] Items { get; set; }
    }
}
