#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/13 14:42:20
// 文件名：PaymentService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/13 14:42:20
// 修改说明：
// ========================================================================*/
#endregion

using System.Data.Services.Client;

namespace UniCloud.Presentation.Service.Payment
{
    public class PaymentService: ServiceBase, IPaymentService
    {
        public PaymentService(DataServiceContext context)
            : base(context)
        {
        }
    }
}
