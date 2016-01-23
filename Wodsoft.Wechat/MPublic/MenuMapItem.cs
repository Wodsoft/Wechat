using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 微信地图选择菜单。
    /// </summary>
    public class MenuMapItem : MenuKeyItem
    {
        /// <summary>
        /// 获取菜单类型。
        /// </summary>
        public override string Type
        {
            get { return "location_select"; }
        }
    }
}
