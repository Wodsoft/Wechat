using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 回复新闻项。
    /// </summary>
    public class ReplyNewsItem
    {
        /// <summary>
        /// 获取或设置新闻标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置新闻简介。
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置图片Url。
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 获取或设置点击跳转地址。
        /// </summary>
        public string Link { get; set; }
    }
}
