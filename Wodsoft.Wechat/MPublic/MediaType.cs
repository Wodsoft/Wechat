using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 素材类型。
    /// </summary>
    public enum MediaType
    {
        /// <summary>
        /// 图片素材。支持BMP、PNG、JPEG、JPG、GIF格式。
        /// </summary>
        Image,
        /// <summary>
        /// 语音素材。支持AMR、MP3格式。
        /// </summary>
        Voice,
        /// <summary>
        /// 视频素材。支持MP4格式。
        /// </summary>
        Video,
        /// <summary>
        /// 缩略图素材。支持JPG格式。
        /// </summary>
        Thumb
    }
}
