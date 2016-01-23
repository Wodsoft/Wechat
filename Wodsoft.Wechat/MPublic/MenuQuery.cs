using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.MPublic
{
    public class MenuQuery
    {
        [JsonProperty("menu")]
        public MenuInfo Menu { get; set; }
    }
}
