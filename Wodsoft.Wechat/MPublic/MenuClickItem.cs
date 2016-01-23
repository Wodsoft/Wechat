using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 微信发送用户点击事件菜单。
    /// </summary>
    public class MenuClickItem : MenuKeyItem
    {
        /// <summary>
        /// 获取菜单类型。
        /// </summary>
        public override string Type
        {
            get { return "click"; }
        }
    }
}
