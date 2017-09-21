using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Wodsoft.Wechat.MPublic;

namespace Wodsoft.Wechat.Payment
{
    /// <summary>
    /// 微信支付服务。
    /// </summary>
    public class PaymentService : ServiceBase
    {
        private HttpHelper HttpHelper;

        /// <summary>
        /// 实例化微信支付服务。
        /// </summary>
        /// <param name="serviceToken">微信服务令牌。</param>
        /// <param name="appId">公众号Id。</param>
        /// <param name="appKey">公众号密钥。</param>
        /// <param name="shopId">商户号。</param>
        /// <param name="shopKey">商户密钥。</param>
        public PaymentService(IServiceToken serviceToken, string appId, string appKey, string shopId, string shopKey)
            : base(serviceToken, appId, appKey)
        {
            if (shopId == null)
                throw new ArgumentNullException("shopId");
            if (shopKey == null)
                throw new ArgumentNullException("shopKey");
            ShopId = shopId;
            ShopKey = shopKey;
            HttpHelper = new HttpHelper();
            HttpHelper.Client.Timeout = TimeSpan.FromSeconds(5);
            HttpHelper.Handler.AllowAutoRedirect = true;
        }

        /// <summary>
        /// 实例化微信支付服务。
        /// </summary>
        /// <param name="serviceToken">微信服务令牌。</param>
        /// <param name="appId">公众号Id。</param>
        /// <param name="appKey">公众号密钥。</param>
        /// <param name="shopId">商户号。</param>
        /// <param name="shopKey">商户密钥。</param>
        /// <param name="cert">客户端证书。</param>
        public PaymentService(IServiceToken serviceToken, string appId, string appKey, string shopId, string shopKey, X509Certificate2 cert)
            : this(serviceToken, appId, appKey, shopId, shopKey)
        {
            if (cert == null)
                throw new ArgumentNullException("cert");
            Certificate = cert;
            IsCertificateEnabled = true;
            HttpHelper.Handler.ClientCertificates.Add(cert);
        }

        /// <summary>
        /// 实例化微信支付服务。
        /// </summary>
        /// <param name="serviceToken">微信服务令牌。</param>
        /// <param name="appId">公众号Id。</param>
        /// <param name="appKey">公众号密钥。</param>
        /// <param name="shopId">商户号。</param>
        /// <param name="shopKey">商户密钥。</param>
        /// <param name="certPath">客户端证书路径。</param>
        /// <param name="certPassword">客户端证书密码。</param>
        public PaymentService(IServiceToken serviceToken, string appId, string appKey, string shopId, string shopKey, string certPath, string certPassword)
            : this(serviceToken, appId, appKey, shopId, shopKey, new X509Certificate2(certPath, certPassword))
        { }

        /// <summary>
        /// 获取商户号。
        /// </summary>
        public string ShopId { get; private set; }

        /// <summary>
        /// 获取商户密钥。
        /// </summary>
        public string ShopKey { get; private set; }

        /// <summary>
        /// 获取客户端证书。
        /// </summary>
        public X509Certificate2 Certificate { get; private set; }

        /// <summary>
        /// 获取是否启用客户端证书。
        /// </summary>
        public bool IsCertificateEnabled { get; private set; }

        /// <summary>
        /// 创建支付接口地址。
        /// </summary>
        public static string CreatePayUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";

        /// <summary>
        /// 查询支付接口地址。
        /// </summary>
        public static string QueryPayUrl = "https://api.mch.weixin.qq.com/pay/orderquery";

        /// <summary>
        /// 关闭支付接口地址。
        /// </summary>
        public static string ClosePayUrl = "https://api.mch.weixin.qq.com/pay/closeorder";

        /// <summary>
        /// 退款支付接口地址。
        /// </summary>
        public static string RefundPayUrl = "https://api.mch.weixin.qq.com/secapi/pay/refund";

        /// <summary>
        /// 退款查询接口地址。
        /// </summary>
        public static string RefundQueryUrl = "https://api.mch.weixin.qq.com/pay/refundquery";

        /// <summary>
        /// 下载对账单接口地址。
        /// </summary>
        public static string DownloadBillUrl = "https://api.mch.weixin.qq.com/pay/downloadbill";

        /// <summary>
        /// 微信内置JsAPI支付。
        /// </summary>
        /// <param name="order">订单信息。</param>
        /// <param name="openId">用户OpenId。</param>
        /// <param name="notifyUrl">回调通知地址。</param>
        /// <returns></returns>
        public virtual async Task<IJsPayment> CreatePayment(IPaymentOrder order, IOpenId openId, string notifyUrl)
        {
            if (order == null)
                throw new ArgumentNullException("order");
            if (openId == null)
                throw new ArgumentNullException("openId");
            if (notifyUrl == null)
                throw new ArgumentNullException("notifyUrl");
            ValidateOrderInfo(order);
            var payData = OrderInfoToDictionary(order);
            payData.Add("trade_type", "JSAPI");
            payData.Add("openid", openId.OpenId);
            payData.Add("appid", AppId);//公众账号ID
            payData.Add("mch_id", ShopId);//商户号
            payData.Add("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            payData.Add("notify_url", notifyUrl);
            payData.Add("sign", GetSignature(payData, ShopKey));

            string backData = await HttpHelper.PostHttp(new Uri(CreatePayUrl), Encoding.UTF8.GetBytes(GetXml(payData)), "text/xml", Encoding.UTF8);
            XElement root = XDocument.Parse(backData).Element("xml");
            if (root.Element("return_code").Value == "FAIL")
            {
                string errMsg = root.Element("return_msg").Value;
                throw new WechatException(errMsg);
            }
            if (root.Element("result_code").Value == "FAIL")
            {
                string errMsg = root.Element("err_code").Value;
                throw new WechatException(errMsg);
            }
            JsPayment payment = new JsPayment();
            payment.PrepayId = root.Element("prepay_id").Value;
            payment.Nonce = Guid.NewGuid().ToString().Replace("-", "");
            payment.TradeType = root.Element("trade_type").Value;
            payment.TimeStamp = GetTimestamp();
            payment.Signature = GetSignature(new
            {
                appId = AppId,
                timeStamp = payment.TimeStamp,
                nonceStr = payment.Nonce,
                package = "prepay_id=" + payment.PrepayId,
                signType = "MD5"
            }, ShopKey);

            return payment;
        }

        /// <summary>
        /// 微信二维码扫码支付。
        /// </summary>
        /// <param name="order">订单信息。</param>
        /// <param name="productId">商品Id。</param>
        /// <param name="notifyUrl">回调通知地址。</param>
        /// <returns></returns>
        public virtual async Task<IQrPayment> CreatePayment(IPaymentOrder order, string productId, string notifyUrl)
        {
            if (order == null)
                throw new ArgumentNullException("order");
            if (productId == null)
                throw new ArgumentNullException("productId");
            if (productId.Length > 32)
                throw new ArgumentOutOfRangeException("productId长度不能大于32。");
            ValidateOrderInfo(order);
            var payData = OrderInfoToDictionary(order);
            payData.Add("trade_type", "NATIVE");
            payData.Add("product_id", productId);
            payData.Add("appid", AppId);//公众账号ID
            payData.Add("mch_id", ShopId);//商户号
            payData.Add("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            payData.Add("notify_url", notifyUrl);
            payData.Add("sign", GetSignature(payData, ShopKey));

            string backData = await HttpHelper.PostHttp(new Uri(CreatePayUrl), Encoding.UTF8.GetBytes(GetXml(payData)), "text/xml", Encoding.UTF8);
            XElement root = XDocument.Parse(backData).Element("xml");
            if (root.Element("return_code").Value == "FAIL")
            {
                string errMsg = root.Element("return_msg").Value;
                throw new WechatException(errMsg);
            }
            if (root.Element("result_code").Value == "FAIL")
            {
                string errMsg = root.Element("err_code").Value;
                throw new WechatException(errMsg);
            }
            QrPayment payment = new QrPayment();
            payment.PrepayId = root.Element("prepay_id").Value;
            payment.TradeType = root.Element("trade_type").Value;
            payment.QrUrl = root.Element("code_url").Value;
            return payment;
        }

        /// <summary>
        /// 微信APP支付。
        /// </summary>
        /// <param name="order">订单信息。</param>
        /// <param name="notifyUrl">回调通知地址。</param>
        /// <returns></returns>
        public virtual async Task<IAppPayment> CreatePayment(IPaymentOrder order, string notifyUrl)
        {
            if (order == null)
                throw new ArgumentNullException("order");
            ValidateOrderInfo(order);
            var payData = OrderInfoToDictionary(order);
            payData.Add("trade_type", "APP");
            payData.Add("appid", AppId);//公众账号ID
            payData.Add("mch_id", ShopId);//商户号
            payData.Add("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            payData.Add("notify_url", notifyUrl);
            payData.Add("sign", GetSignature(payData, ShopKey));

            string backData = await HttpHelper.PostHttp(new Uri(CreatePayUrl), Encoding.UTF8.GetBytes(GetXml(payData)), "text/xml", Encoding.UTF8);
            XElement root = XDocument.Parse(backData).Element("xml");
            if (root.Element("return_code").Value == "FAIL")
            {
                string errMsg = root.Element("return_msg").Value;
                throw new WechatException(errMsg);
            }
            if (root.Element("result_code").Value == "FAIL")
            {
                string errMsg = root.Element("err_code").Value;
                throw new WechatException(errMsg);
            }
            AppPayment payment = new AppPayment();
            payment.PrepayId = root.Element("prepay_id").Value;
            payment.TradeType = root.Element("trade_type").Value;
            payment.TimeStamp = GetTimestamp();
            payment.Nonce = Guid.NewGuid().ToString().Replace("-", "");
            payment.Signature = GetSignature(new
            {
                appId = AppId,
                timeStamp = payment.TimeStamp,
                nonceStr = payment.Nonce,
                partnerid = ShopId,
                prepayid = payment.PrepayId,
                package = "Sign=WXPay"
            }, ShopKey);
            return payment;
        }

        /// <summary>
        /// 获取微信交易信息。
        /// </summary>
        /// <param name="tradeNo">商户订单号。</param>
        /// <returns></returns>
        public virtual async Task<IPaymentInfo> GetPaymentInfo(ITradeNumber tradeNo)
        {
            if (tradeNo == null)
                throw new ArgumentNullException("tradeNo");
            var payData = new Dictionary<string, string>();
            payData.Add("appid", AppId);
            payData.Add("mch_id", ShopId);
            payData.Add("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            payData.Add("out_trade_no", tradeNo.TradeNo);
            payData.Add("sign", GetSignature(payData, ShopKey));

            string backData = await HttpHelper.PostHttp(new Uri(QueryPayUrl), Encoding.UTF8.GetBytes(GetXml(payData)), "text/xml", Encoding.UTF8);
            XElement root = XDocument.Parse(backData).Element("xml");
            if (root.Element("return_code").Value == "FAIL")
            {
                string errMsg = root.Element("return_msg").Value;
                throw new WechatException(errMsg);
            }
            if (root.Element("result_code").Value == "FAIL")
            {
                string errMsg = root.Element("err_code").Value;
                throw new WechatException(errMsg);
            }
            IPaymentInfo info = GetPaymentInfo(root);
            return info;
        }

        /// <summary>
        /// 获取微信交易信息。
        /// </summary>
        /// <param name="transactionId">微信订单号。</param>
        /// <returns></returns>
        public virtual async Task<IPaymentInfo> GetPaymentInfo(ITransactionId transactionId)
        {
            if (transactionId == null)
                throw new ArgumentNullException("transactionId");
            var payData = new Dictionary<string, string>();
            payData.Add("appid", AppId);
            payData.Add("mch_id", ShopId);
            payData.Add("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            payData.Add("transactionId", transactionId.TransactionId);
            payData.Add("sign", GetSignature(payData, ShopKey));

            //await CheckRemoteToken();

            string backData = await HttpHelper.PostHttp(new Uri(QueryPayUrl), Encoding.UTF8.GetBytes(GetXml(payData)), "text/xml", Encoding.UTF8);
            XElement root = XDocument.Parse(backData).Element("xml");
            if (root.Element("return_code").Value == "FAIL")
            {
                string errMsg = root.Element("return_msg").Value;
                throw new WechatException(errMsg);
            }
            if (root.Element("result_code").Value == "FAIL")
            {
                string errMsg = root.Element("err_code").Value;
                throw new WechatException(errMsg);
            }
            IPaymentInfo info = GetPaymentInfo(root);
            return info;
        }

        /// <summary>
        /// 关闭微信交易。
        /// </summary>
        /// <param name="tradeNo">商户订单号。</param>
        /// <returns></returns>
        public virtual async Task<bool> ClosePayment(ITradeNumber tradeNo)
        {
            if (tradeNo == null)
                throw new ArgumentNullException("tradeNo");
            var payData = new Dictionary<string, string>();
            payData.Add("appid", AppId);
            payData.Add("mch_id", ShopId);
            payData.Add("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            payData.Add("out_trade_no", tradeNo.TradeNo);
            payData.Add("sign", GetSignature(payData, ShopKey));

            string backData = await HttpHelper.PostHttp(new Uri(ClosePayUrl), Encoding.UTF8.GetBytes(GetXml(payData)), "text/xml", Encoding.UTF8);
            XElement root = XDocument.Parse(backData).Element("xml");
            if (root.Element("return_code").Value == "FAIL")
            {
                string errMsg = root.Element("return_msg").Value;
                throw new WechatException(errMsg);
            }
            if (root.Element("result_code").Value == "FAIL")
            {
                string errMsg = root.Element("err_code").Value;
                throw new WechatException(errMsg);
            }
            return true;
        }

        /// <summary>
        /// 微信交易退款。
        /// </summary>
        /// <param name="refundInfo">退款信息。</param>
        /// <returns></returns>
        public virtual async Task<IRefundResult> RefundPayment(IRefundOrder refundInfo)
        {
            if (refundInfo == null)
                throw new ArgumentNullException("refundInfo");
            if (!IsCertificateEnabled)
                throw new NotSupportedException("不支持没有证书的操作。");
            var payData = new Dictionary<string, string>();
            payData.Add("appid", AppId);
            payData.Add("mch_id", ShopId);
            payData.Add("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            if (refundInfo.TransactionId != null)
                payData.Add("transaction_id", refundInfo.TransactionId);
            else
                payData.Add("out_trade_no", refundInfo.TradeNo);
            payData.Add("total_fee", refundInfo.TotalFee.ToString());
            payData.Add("refund_fee", refundInfo.RefundFee.ToString());
            if (refundInfo.RefundCurrency != null)
                payData.Add("refund_fee_type", refundInfo.RefundCurrency);
            payData.Add("out_refund_no", refundInfo.RefundNo);
            payData.Add("op_user_id", refundInfo.Operator ?? ShopId);
            payData.Add("sign", GetSignature(payData, ShopKey));


            string backData = await HttpHelper.PostHttp(new Uri(RefundPayUrl), Encoding.UTF8.GetBytes(GetXml(payData)), "text/xml", Encoding.UTF8);
            XElement root = XDocument.Parse(backData).Element("xml");
            if (root.Element("return_code").Value == "FAIL")
            {
                string errMsg = root.Element("return_msg").Value;
                throw new WechatException(errMsg);
            }
            if (root.Element("result_code").Value == "FAIL")
            {
                string errMsg = root.Element("err_code").Value;
                throw new WechatException(errMsg);
            }

            RefundResult result = new RefundResult();
            result.RefundId = root.Element("refund_id").Value;
            result.RefundChannel = root.Element("refund_channel").Value;
            result.RefundFee = int.Parse(root.Element("refund_fee").Value);
            result.RefundNo = root.Element("out_refund_no").Value;
            result.Status = RefundStatus.PROCESSING;
            if (root.Element("cash_refund_fee") != null)
                result.RefundCash = int.Parse(root.Element("cash_fee").Value);
            if (root.Element("coupon_refund_fee") != null)
                result.Coupon = int.Parse(root.Element("coupon_refund_fee").Value);
            return result;
        }

        /// <summary>
        /// 查询退款信息。
        /// </summary>
        /// <param name="transactionId">微信交易号。</param>
        /// <returns>返回退款信息。</returns>
        public virtual async Task<IRefundInfo> GetRefundInfo(ITransactionId transactionId)
        {
            if (transactionId == null)
                throw new ArgumentNullException("transactionId");
            var payData = new Dictionary<string, string>();
            payData.Add("appid", AppId);
            payData.Add("mch_id", ShopId);
            payData.Add("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));
            payData.Add("transactionId", transactionId.TransactionId);
            payData.Add("sign", GetSignature(payData, ShopKey));

            string backData = await HttpHelper.PostHttp(new Uri(QueryPayUrl), Encoding.UTF8.GetBytes(GetXml(payData)), "text/xml", Encoding.UTF8);
            XElement root = XDocument.Parse(backData).Element("xml");
            if (root.Element("return_code").Value == "FAIL")
            {
                string errMsg = root.Element("return_msg").Value;
                throw new WechatException(errMsg);
            }
            if (root.Element("result_code").Value == "FAIL")
            {
                string errMsg = root.Element("err_code").Value;
                throw new WechatException(errMsg);
            }
            IRefundInfo info = GetRefundInfo(root);
            return info;
        }

        /// <summary>
        /// 查询退款信息。
        /// </summary>
        /// <param name="tradeNo">商户订单号。</param>
        /// <returns>返回退款信息。</returns>
        public virtual async Task<IRefundInfo> GetRefundInfo(ITradeNumber tradeNo)
        {
            if (tradeNo == null)
                throw new ArgumentNullException("tradeNo");
            var payData = new Dictionary<string, string>();
            payData.Add("appid", AppId);
            payData.Add("mch_id", ShopId);
            payData.Add("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));
            payData.Add("out_trade_no", tradeNo.TradeNo);
            payData.Add("sign", GetSignature(payData, ShopKey));

            string backData = await HttpHelper.PostHttp(new Uri(QueryPayUrl), Encoding.UTF8.GetBytes(GetXml(payData)), "text/xml", Encoding.UTF8);
            XElement root = XDocument.Parse(backData).Element("xml");
            if (root.Element("return_code").Value == "FAIL")
            {
                string errMsg = root.Element("return_msg").Value;
                throw new WechatException(errMsg);
            }
            if (root.Element("result_code").Value == "FAIL")
            {
                string errMsg = root.Element("err_code").Value;
                throw new WechatException(errMsg);
            }
            IRefundInfo info = GetRefundInfo(root);
            return info;
        }

        /// <summary>
        /// 查询退款信息。
        /// </summary>
        /// <param name="refundNo">商户退款号。</param>
        /// <returns>返回退款信息。</returns>
        public virtual async Task<IRefundInfo> GetRefundInfo(IRefundNumber refundNo)
        {
            if (refundNo == null)
                throw new ArgumentNullException("refundNo");
            var payData = new Dictionary<string, string>();
            payData.Add("appid", AppId);
            payData.Add("mch_id", ShopId);
            payData.Add("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));
            payData.Add("out_refund_no", refundNo.RefundNo);
            payData.Add("sign", GetSignature(payData, ShopKey));

            string backData = await HttpHelper.PostHttp(new Uri(QueryPayUrl), Encoding.UTF8.GetBytes(GetXml(payData)), "text/xml", Encoding.UTF8);
            XElement root = XDocument.Parse(backData).Element("xml");
            if (root.Element("return_code").Value == "FAIL")
            {
                string errMsg = root.Element("return_msg").Value;
                throw new WechatException(errMsg);
            }
            if (root.Element("result_code").Value == "FAIL")
            {
                string errMsg = root.Element("err_code").Value;
                throw new WechatException(errMsg);
            }
            IRefundInfo info = GetRefundInfo(root);
            return info;
        }

        /// <summary>
        /// 查询退款信息。
        /// </summary>
        /// <param name="refundId">微信退款号。</param>
        /// <returns>返回退款信息。</returns>
        public virtual async Task<IRefundInfo> GetRefundInfo(IRefundId refundId)
        {
            if (refundId == null)
                throw new ArgumentNullException("transactionId");
            var payData = new Dictionary<string, string>();
            payData.Add("appid", AppId);
            payData.Add("mch_id", ShopId);
            payData.Add("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));
            payData.Add("refund_id", refundId.RefundId);
            payData.Add("sign", GetSignature(payData, ShopKey));

            string backData = await HttpHelper.PostHttp(new Uri(QueryPayUrl), Encoding.UTF8.GetBytes(GetXml(payData)), "text/xml", Encoding.UTF8);
            XElement root = XDocument.Parse(backData).Element("xml");
            if (root.Element("return_code").Value == "FAIL")
            {
                string errMsg = root.Element("return_msg").Value;
                throw new WechatException(errMsg);
            }
            if (root.Element("result_code").Value == "FAIL")
            {
                string errMsg = root.Element("err_code").Value;
                throw new WechatException(errMsg);
            }
            IRefundInfo info = GetRefundInfo(root);
            return info;
        }

        /// <summary>
        /// 获取退款信息。
        /// </summary>
        /// <param name="root">退款XML Linq节点数据。</param>
        /// <returns>返回退款信息。</returns>
        protected virtual IRefundInfo GetRefundInfo(XElement root)
        {
            var dict = root.Elements().ToDictionary(t => t.Name.LocalName, t => t.Value);
            if (dict["sign"] != GetSignature(dict, ShopKey))
                return null;

            RefundInfo info = new RefundInfo();
            var count = int.Parse(root.Element("refund_count").Value);
            info.Items = new IRefundResult[count];
            for (int i = 0; i < count; i++)
            {
                RefundResult item = new RefundResult();
                item.RefundId = root.Element("refund_id_" + i).Value;
                item.RefundFee = int.Parse(root.Element("refund_fee_" + i).Value);
                item.Status = (RefundStatus)Enum.Parse(typeof(RefundStatus), root.Element("refund_status_" + i).Value);
                if (root.Element("refund_channel_" + i) != null)
                    item.RefundChannel = root.Element("refund_channel_" + i).Value;
                if (root.Element("coupon_refund_fee_" + i) != null)
                    item.Coupon = int.Parse(root.Element("coupon_refund_fee_" + i).Value);
                if (root.Element("refund_recv_accout_" + i) != null)
                    item.Account = root.Element("refund_recv_accout_" + i).Value;
                var ncount = int.Parse(root.Element("coupon_refund_count_" + i).Value);
                item.CouponItems = new Coupon[ncount];
                for (int n = 0; n < ncount; n++)
                {
                    Coupon coupon = new Coupon();
                    coupon.Id = root.Element("coupon_refund_id_" + i + "_" + n).Value;
                    coupon.Batch = root.Element("coupon_refund_batch_id_" + i + "_" + n).Value;
                    coupon.Fee = int.Parse(root.Element("coupon_refund_fee_" + i + "_" + n).Value);
                }
            }
            return info;
        }

        /// <summary>
        /// 下载对账单。
        /// </summary>
        /// <param name="date">对账日期。</param>
        /// <returns>返回对账单文本。</returns>
        public virtual async Task<string> DownloadBill(DateTime date)
        {
            var data = new Dictionary<string, string>();
            data.Add("appid", AppId);//公众账号ID
            data.Add("mch_id", ShopId);//商户号
            data.Add("nonce_str", Guid.NewGuid().ToString().Replace("-", ""));//随机字符串
            data.Add("bill_date", date.ToString("yyyyMMdd"));
            data.Add("bill_type", "ALL");
            data.Add("sign", GetSignature(data, ShopKey));

            string backData = await HttpHelper.PostHttp(new Uri(DownloadBillUrl), Encoding.UTF8.GetBytes(GetXml(data)), "application/x-www-form-urlencoded", Encoding.UTF8);
            if (backData.Contains("return_code"))
            {
                XElement root = XDocument.Parse(backData).Element("xml");
                string errMsg = root.Element("return_msg").Value;
                throw new WechatException(errMsg);
            }
            return backData;
        }

        /// <summary>
        /// 处理交易回调信息。
        /// </summary>
        /// <param name="stream">回调内容。</param>
        /// <returns></returns>
        public virtual async Task<IPaymentInfo> NotifyPayment(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string data = await reader.ReadToEndAsync();
            XElement root = XDocument.Parse(data).Element("xml");
            if (root.Element("return_code").Value == "FAIL")
            {
                string errMsg = root.Element("return_msg").Value;
                throw new WechatException(errMsg);
            }
            if (root.Element("result_code").Value == "FAIL")
            {
                string errMsg = root.Element("err_code").Value;
                throw new WechatException(errMsg);
            }
            return GetPaymentInfo(root);
        }

        private string _NotifySuccess;
        /// <summary>
        /// 获取回调成功返回信息。
        /// </summary>
        public virtual string NofitySuccess
        {
            get
            {
                if (_NotifySuccess == null)
                {
                    var doc = new System.Xml.XmlDocument();
                    var root = doc.CreateElement("xml");
                    doc.AppendChild(root);
                    var return_code = doc.CreateElement("return_code");
                    return_code.InnerText = "SUCCESS";
                    root.AppendChild(return_code);
                    var return_msg = doc.CreateElement("return_msg");
                    return_msg.InnerText = "";
                    root.AppendChild(return_msg);
                    _NotifySuccess = doc.OuterXml;
                }
                return _NotifySuccess;
            }
        }

        private string _NotifyFail;
        /// <summary>
        /// 获取回调失败返回信息。
        /// </summary>
        public virtual string NotifyFail
        {
            get
            {
                if (_NotifyFail == null)
                {
                    var doc = new System.Xml.XmlDocument();
                    var root = doc.CreateElement("xml");
                    doc.AppendChild(root);
                    var return_code = doc.CreateElement("return_code");
                    return_code.InnerText = "FAIL ";
                    root.AppendChild(return_code);
                    var return_msg = doc.CreateElement("return_msg");
                    return_msg.InnerText = "";
                    root.AppendChild(return_msg);
                    _NotifyFail = doc.OuterXml;
                }
                return _NotifyFail;
            }
        }

        /// <summary>
        /// 获取交易信息。
        /// </summary>
        /// <param name="root">XML Linq节点。</param>
        /// <returns>返回交易信息。</returns>
        protected virtual IPaymentInfo GetPaymentInfo(XElement root)
        {
            var dict = root.Elements().ToDictionary(t => t.Name.LocalName, t => t.Value);
            if (dict["sign"] != GetSignature(dict, ShopKey))
                return null;
            PaymentInfo info = new PaymentInfo();
            info.TradeNo = root.Element("out_trade_no").Value;
            if (root.Element("trade_state") != null)
                info.State = (TradeState)Enum.Parse(typeof(TradeState), root.Element("trade_state").Value);
            else
                info.State = TradeState.SUCCESS;
            if (root.Element("openid") != null)
                info.OpenId = root.Element("openid").Value;
            if (root.Element("trade_type") != null)
                info.Type = root.Element("trade_type").Value;
            if (root.Element("bank_type") != null)
                info.Bank = root.Element("bank_type").Value;
            if (root.Element("total_fee") != null)
                info.Fee = int.Parse(root.Element("total_fee").Value);
            if (root.Element("cash_fee") != null)
                info.Cash = int.Parse(root.Element("cash_fee").Value);
            if (root.Element("transaction_id") != null)
                info.TransactionId = root.Element("transaction_id").Value;
            if (root.Element("time_end") != null)
                info.CompletedDate = DateTime.Parse(root.Element("time_end").Value.Insert(12, ":").Insert(10, ":").Insert(8, " ").Insert(6, "-").Insert(4, "-"));
            if (root.Element("trade_state_desc") != null)
                info.Description = root.Element("trade_state_desc").Value;
            if (root.Element("device_info") != null)
                info.Device = root.Element("device_info").Value;
            if (root.Element("is_subscribe") != null)
                info.IsSubscribe = root.Element("is_subscribe").Value == "Y";
            if (root.Element("fee_type") != null)
                info.Currency = root.Element("fee_type").Value;
            if (root.Element("cash_fee_type") != null)
                info.CashCurrency = root.Element("cash_fee_type").Value;
            if (root.Element("coupon_fee") != null)
            {
                info.Coupon = int.Parse(root.Element("coupon_fee").Value);
                var count = int.Parse(root.Element("coupon_count").Value);
                info.CouponItems = new Coupon[count];
                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        Coupon item = new Coupon();
                        item.Batch = root.Element("coupon_batch_id_" + i)?.Value;
                        item.Id = root.Element("coupon_id_" + i)?.Value;
                        item.Fee = int.Parse(root.Element("coupon_fee_" + i)?.Value);
                        info.CouponItems[i] = item;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            if (root.Element("attach") != null)
                info.Comment = root.Element("attach").Value;
            return info;

        }

        /// <summary>
        /// 转换字典数据为XML数据。
        /// </summary>
        /// <param name="directory">字典数据。</param>
        /// <returns>返回XML文本。</returns>
        protected static string GetXml(IDictionary<string, string> directory)
        {
            XmlDocument doc = new XmlDocument();
            doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            var root = doc.CreateElement("xml");
            doc.AppendChild(root);
            foreach (var kv in directory)
            {
                var element = doc.CreateElement(kv.Key);
                element.InnerText = kv.Value;
                root.AppendChild(element);
            }
            return doc.OuterXml;
        }

        /// <summary>
        /// 验证交易信息正确性。
        /// </summary>
        public virtual void ValidateOrderInfo(IPaymentOrder orderInfo)
        {
            if (string.IsNullOrEmpty(orderInfo.Title))
                throw new Exception("Title不能为空。");
            if (orderInfo.Title.Length > 32)
                throw new Exception("Title长度不能大于32。");
            if (orderInfo.Detail != null && orderInfo.Detail.Length > 8192)
                throw new Exception("Detail长度不能大于8192。");
            if (orderInfo.Comment != null && orderInfo.Comment.Length > 127)
                throw new Exception("Comment长度不能大于8192。");
            if (string.IsNullOrEmpty(orderInfo.TradeNo))
                throw new Exception("TradeNo不能为空。");
            if (orderInfo.TradeNo.Length > 32)
                throw new Exception("TradeNo长度不能大于32。");
            if (orderInfo.Currency != null && orderInfo.Currency.Length > 16)
                throw new Exception("Currency长度不能大于16。");
            if (orderInfo.Fee < 1)
                throw new Exception("Fee不能小于1。");
            if (string.IsNullOrEmpty(orderInfo.UserIp))
                throw new Exception("UserIp不能为空。");
            if (orderInfo.UserIp.Length > 32)
                throw new Exception("UserIp长度不能大于16。");
            if (orderInfo.Tag != null && orderInfo.Tag.Length > 32)
                throw new Exception("Tag长度不能大于32。");
            if (orderInfo.Device != null && orderInfo.Device.Length > 32)
                throw new Exception("Device长度不能大于32。");
        }

        private IDictionary<string, string> OrderInfoToDictionary(IPaymentOrder orderInfo)
        {
            Dictionary<string, string> payData = new Dictionary<string, string>();
            payData.Add("body", orderInfo.Title);
            payData.Add("out_trade_no", orderInfo.TradeNo);
            payData.Add("total_fee", orderInfo.Fee.ToString());
            payData.Add("spbill_create_ip", orderInfo.UserIp);
            if (orderInfo.Detail != null)
                payData.Add("detail", orderInfo.Detail);
            if (orderInfo.Comment != null)
                payData.Add("attach", orderInfo.Comment);
            if (orderInfo.Currency != null)
                payData.Add("fee_type", orderInfo.Currency);
            if (orderInfo.StartDate != null)
                payData.Add("time_start", orderInfo.StartDate.Value.ToString("yyyyMMddHHmmss"));
            if (orderInfo.ExpiredDate != null)
                payData.Add("time_expire", orderInfo.ExpiredDate.Value.ToString("yyyyMMddHHmmss"));
            if (orderInfo.Tag != null)
                payData.Add("goods_tag", orderInfo.Tag);
            if (orderInfo.Device != null)
                payData.Add("device_info", orderInfo.Device);
            return payData;
        }
    }
}
