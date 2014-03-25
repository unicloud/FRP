#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/24 17:06:17
// 文件名：PaymentNotice
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/24 17:06:17
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using ReportViewer.Payment;

#endregion

namespace ReportViewer.PaymentNotice
{
    public class PaymentNotice
    {
        private static readonly Uri ServiceRoot = null;// new Uri(Application.Current.Resources["PaymentDataService"].ToString());
        readonly PaymentData _serviceContext = new PaymentData(ServiceRoot);

        public IEnumerable<PaymentNoticeDTO> GetNoticeNumber()
        {
            var notices = from notice in _serviceContext.PaymentNotices
                          select notice;
            //var numbers = new List<string>();
            //notices.ToList().ForEach(p=>numbers.Add(p.NoticeNumber));
            return notices.AsEnumerable();
        }
        public IEnumerable<PaymentNoticeDTO> GetPaymentNotice(string noticeNumber)
        {
            var productQuery = from product in _serviceContext.PaymentNotices
                               where product.NoticeNumber.StartsWith(noticeNumber)
                               select product;

            return productQuery.ToArray();
        }

        public IEnumerable<PaymentNoticeLineDTO> GetPaymentNoticeLines(string noticeNumber)
        {
            var productQuery = from product in _serviceContext.PaymentNotices
                               where product.NoticeNumber.StartsWith(noticeNumber)
                               select product;
            return productQuery.FirstOrDefault().PaymentNoticeLines;
        }
    }
}
