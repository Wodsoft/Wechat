using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    /// <summary>
    /// 用户信息。
    /// </summary>
    public interface IUserInfo : IOpenId, IUnionId
    {
        /// <summary>
        /// 获取用户昵称。
        /// </summary>
        string NickName { get; }

        /// <summary>
        /// 获取用户的性别。值为1时是男性，值为2时是女性，值为0时是未知。
        /// </summary>
        string Gender { get; }

        /// <summary>
        /// 获取用户个人资料填写的省份。
        /// </summary>
        string Province { get; }

        /// <summary>
        /// 获取普通用户个人资料填写的城市。
        /// </summary>
        string City { get; }

        /// <summary>
        /// 获取国家。如中国为CN。
        /// </summary>
        string Country { get; }

        /// <summary>
        /// 获取用户头像。最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。
        /// </summary>
        string Avatar { get; }

        /// <summary>
        /// 获取用户特权信息。如微信沃卡用户为“chinaunicom”。
        /// </summary>
        string[] Privilege { get; }
    }
}