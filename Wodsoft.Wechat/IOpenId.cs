using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    /// <summary>
    /// 用户OpenId。
    /// </summary>
    public interface IOpenId
    {
        /// <summary>
        /// 获取用户OpenId。
        /// </summary>
        string OpenId { get; }
    }
}
