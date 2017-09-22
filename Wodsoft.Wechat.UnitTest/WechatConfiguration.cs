using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Wodsoft.Wechat.MPublic;

namespace Wodsoft.Wechat.UnitTest
{
    public static class WechatConfiguration
    {
        static WechatConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Config = builder.Build();
        }

        public static IConfigurationRoot Config { get; private set; }

        private static ServiceToken _ServiceToken;
        public static ServiceToken GetServiceToken(string appId, string appKey)
        {
            if (_ServiceToken == null)
                _ServiceToken = new ServiceToken(appId, appKey);
            return _ServiceToken;
        }

        private static PublicService _PublicService;
        public static PublicService GetPublicService()
        {
            if (_PublicService == null)
                _PublicService = new PublicService(GetServiceToken(Config["Wechat:AppId"], Config["Wechat:AppKey"]), Config["Wechat:AppId"], Config["Wechat:AppKey"]);
            return _PublicService;
        }
    }
}
