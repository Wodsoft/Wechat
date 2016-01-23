using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    /// <summary>
    /// 微信菜单项规则。
    /// </summary>
    public class MenuRule
    {
        /// <summary>
        /// 获取或设置匹配的用户组Id。
        /// </summary>
        [JsonProperty("group_id")]
        public int? Group { get; set; }

        /// <summary>
        /// 获取或设置匹配的性别。
        /// </summary>
        [JsonProperty("sex", ItemConverterType = null)]
        public bool? Gender { get; set; }

        /// <summary>
        /// 获取或设置匹配的客户端平台。
        /// </summary>
        [JsonProperty("client_platform_type", ItemConverterType = typeof(Int32Converter))]
        public MenuPlatform? Platform { get; set; }

        /// <summary>
        /// 获取或设置匹配的国家。
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// 获取或设置匹配的身份。
        /// </summary>
        [JsonProperty("province")]
        public string Province { get; set; }

        /// <summary>
        /// 获取或设置匹配的城市。
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// 获取或设置匹配的语言。
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// 性别转换器。
        /// </summary>
        public class GenderConverter : TypeConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {
                if (value is bool?)
                {
                    if (value.Equals(true))
                        return 1;
                    else if (value.Equals(false))
                        return 2;
                }
                return null;
            }
        }
    }

    /// <summary>
    /// 微信菜单客户端平台。
    /// </summary>
    public enum MenuPlatform
    {
        /// <summary>
        /// 苹果IOS平台。
        /// </summary>
        IOS = 1,
        /// <summary>
        /// 谷歌安卓平台。
        /// </summary>
        Android = 2,
        /// <summary>
        /// 其它平台。
        /// </summary>
        Others = 3
    }
}
