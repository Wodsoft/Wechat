using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Wodsoft.Wechat.Payment
{
    public static class PaymentServiceExtensions
    {
        public static async Task<IPaymentInfo> GetNotifyPaymentInfo(this Controller controller, PaymentService service)
        {
            if (controller == null)
                throw new ArgumentNullException("controller");
            if (service == null)
                throw new ArgumentNullException("service");
            return await service.NotifyPayment(controller.Request.InputStream);
        }
    }
}
