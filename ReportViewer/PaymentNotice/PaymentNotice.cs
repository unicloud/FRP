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
using ReportViewer.Payment;

#endregion

namespace ReportViewer.PaymentNotice
{
    public class PaymentNotice
    {
        static Uri serviceRoot = new Uri("http://localhost:20109/PaymentDataService.svc");
        PaymentData serviceContext = new PaymentData(serviceRoot);

        public IEnumerable<PaymentNoticeDTO> GetNoticeNumber()
        {
            var notices = from notice in serviceContext.PaymentNotices
                          select notice;
            //var numbers = new List<string>();
            //notices.ToList().ForEach(p=>numbers.Add(p.NoticeNumber));
            return notices.AsEnumerable();
        }
        public IEnumerable<PaymentNoticeDTO> GetPaymentNotice(string noticeNumber)
        {
            var productQuery = from product in serviceContext.PaymentNotices
                               where product.NoticeNumber.StartsWith(noticeNumber)
                               select product;

            return productQuery.ToArray();
        }

        public IEnumerable<PaymentNoticeLineDTO> GetPaymentNoticeLines(string noticeNumber)
        {
            var productQuery = from product in serviceContext.PaymentNotices
                               where product.NoticeNumber.StartsWith(noticeNumber)
                               select product;
            return productQuery.FirstOrDefault().PaymentNoticeLines;
        }
    }
}
