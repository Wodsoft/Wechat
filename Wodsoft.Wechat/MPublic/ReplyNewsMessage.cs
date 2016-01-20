using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 回复新闻消息。
    /// </summary>
    public class ReplyNewsMessage : ReplyMessage
    {
        public ReplyNewsMessage()
        {
            Type = "news";
        }

        /// <summary>
        /// 获取或设置新闻项。
        /// </summary>
        public ReplyNewsItem[] Items { get; set; }

        protected override XElement[] GetResponseNode()
        {
            if (Items == null)
                throw new NotSupportedException("新闻项不能为空。");
            var items = new XElement("Articles");
            foreach (var item in Items)
            {
                var xItem = new XElement("item");
                if (item.Title != null)
                    xItem.Add(new XElement("Title", item.Title));
                if (item.Description != null)
                    xItem.Add(new XElement("Description", item.Description));
                if (item.ImageUrl != null)
                    xItem.Add(new XElement("PicUrl", item.ImageUrl));
                if (item.Link != null)
                    xItem.Add(new XElement("Url", item.Link));
                items.Add(xItem);
            }
            return new XElement[] { new XElement("ArticleCount", Items.Length), items };
        }
    }
}
