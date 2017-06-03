using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// 微信支付服务扩展。
    /// </summary>
    public static class PaymentServiceExtensions
    {
        /// <summary>
        /// 获取交易信息通知。
        /// </summary>
        /// <param name="controller">Mvc控制器。</param>
        /// <param name="service">微信支付服务。</param>
        /// <returns>返回交易信息。</returns>
        public static async Task<IPaymentInfo> GetNotifyPaymentInfo(this  PaymentService service, Controller controller)
        {
            if (controller == null)
                throw new ArgumentNullException("controller");
            if (service == null)
                throw new ArgumentNullException("service");
            return await service.NotifyPayment(controller.Request.Body);
        }
    }
}
