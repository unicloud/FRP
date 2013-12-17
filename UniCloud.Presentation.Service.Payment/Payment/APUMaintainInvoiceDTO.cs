#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/16 14:50:56
// 文件名：APUMaintainInvoiceDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/16 14:50:56
// 修改说明：
// ========================================================================*/
#endregion

using System.Collections.Generic;

namespace UniCloud.Presentation.Service.Payment.Payment
{
    public partial class APUMaintainInvoiceDTO
    {
        public List<SupplierDTO> Suppliers
        {
            get
            {
                return null;/*GlobalServiceHelper.Suppliers.Where(p => p.MaintainSupplier).ToList();*/
            }
        }
    }
}
