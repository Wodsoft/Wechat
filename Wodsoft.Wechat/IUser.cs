using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    /// <summary>
    /// 用户。
    /// </summary>
    public interface IUser : IOpenId, IUnionId
    {
        /// <summary>
        /// 获取类型。
        /// </summary>
        string Scope { get; }
    }
}
