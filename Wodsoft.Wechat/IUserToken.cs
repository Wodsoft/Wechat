using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    public interface IUserToken : IUser, IToken
    {
        string RefreshToken { get; }

        int ExpiredTime { get; }
    }
}
