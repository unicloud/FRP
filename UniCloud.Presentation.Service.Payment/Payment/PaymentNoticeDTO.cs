#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/10 13:41:14
// 文件名：PaymentNoticeDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/10 13:41:14
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.Service.Payment.Payment
{
    public partial class PaymentNoticeDTO
    {
        static readonly PaymentService PaymentService = new PaymentService();

        //private IEnumerable<BankAccountDTO> _bankAccounts;

        //public IEnumerable<BankAccountDTO> BankAccounts
        //{
        //    get { return new List<BankAccountDTO>(); }
        //}
        partial void OnSupplierIdChanged()
        {
            var supplier = PaymentService.GetSupplier(null).FirstOrDefault(p => p.SupplierId == SupplierId);
            if (supplier != null)
            {
                SupplierName = supplier.Name;
                //_bankAccounts = supplier.BankAccounts;
                BankAccountId = supplier.BankAccounts.FirstOrDefault().BankAccountId;
            }
        }

        partial void OnStatusChanged()
        {
            StatusString = ((PaymentNoticeStatus) Status).ToString();
        }
    }
}
