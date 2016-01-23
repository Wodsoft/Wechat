using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 微信打开网页菜单。
    /// </summary>
    public class MenuUrlItem : MenuItem
    {
        /// <summary>
        /// 获取菜单类型。
        /// </summary>
        public override string Type
        {
            get { return "view"; }
        }

        /// <summary>
        /// 获取或设置跳转地址。
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
