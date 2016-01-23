using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class MenuInfo
    {
        [JsonProperty("button")]
        public MenuGeneralItem[] Items { get; set; }

        [JsonProperty("menuid")]
        public int Id { get; set; }
    }
}
