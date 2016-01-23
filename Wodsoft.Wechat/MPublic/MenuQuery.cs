using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 微信菜单查询。
    /// </summary>
    public class MenuQuery
    {
        /// <summary>
        /// 获取或设置普通菜单。
        /// </summary>
        [JsonProperty("menu")]
        public MenuInfo Menu { get; set; }


        /// <summary>
        /// 获取或设置匹配菜单。
        /// </summary>
        [JsonProperty("conditionalmenu")]
        public MenuInfo ConditionalMenu { get; set; }
    }
}
