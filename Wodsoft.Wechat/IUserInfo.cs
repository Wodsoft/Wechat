using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat
{
    public interface IUserInfo : IOpenId, IUnionId
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        string NickName { get; }

        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        string Gender { get; }

        /// <summary>
        /// 用户个人资料填写的省份
        /// </summary>
        string Province { get; }

        /// <summary>
        /// 普通用户个人资料填写的城市
        /// </summary>
        string City { get; }

        /// <summary>
        /// 国家，如中国为CN
        /// </summary>
        string Country { get; }

        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
        /// </summary>
        string Avatar { get; }

        /// <summary>
        /// 用户特权信息，json 数组，如微信沃卡用户为（chinaunicom）
        /// </summary>
        string[] Privilege { get; }
    }
}