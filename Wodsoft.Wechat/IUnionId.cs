using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    /// <summary>
    /// 用户通用Id。
    /// </summary>
    public interface IUnionId
    {
        /// <summary>
        /// 获取通用Id。
        /// </summary>
        string UnionId { get; }
    }
}
