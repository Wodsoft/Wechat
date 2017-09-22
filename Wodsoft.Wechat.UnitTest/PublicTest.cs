using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Wodsoft.Wechat.MPublic;

namespace Wodsoft.Wechat.UnitTest
{
    [TestClass]
    public class PublicTest
    {
        [TestMethod]
        public async Task ServiceTokenTest()
        {
            var service = WechatConfiguration.GetPublicService();
            await service.CheckServiceToken();
        }

        [TestMethod]
        public async Task Test()
        {
            var service = WechatConfiguration.GetPublicService();
            await service.SetMenu(new Menu[]
            {
                new MenuCollection
                {
                    Name = "²Ëµ¥1",
                    Items = new MenuItem[]{
                        new MenuClickItem
                        {
                            Name = "µã»÷1",
                            Key = "Click1"
                        }
                    }
                }
            });
        }
    }
}
