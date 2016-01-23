using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 微信文章菜单。
    /// </summary>
    public class MenuArticleItem : MenuItem
    {
        /// <summary>
        /// 获取菜单类型。
        /// </summary>
        public override string Type
        {
            get { return "view_limited"; }
        }

        /// <summary>
        /// 获取或设置图文素材Id。
        /// </summary>
        [JsonProperty("media_id")]
        public string Media { get; set; }
    }
}
