﻿#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/4 16:48:40
// 文件名：APUMaintainContractDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System.Collections.Generic;

namespace UniCloud.Presentation.Service.Purchase.Purchase
{
    public partial class APUMaintainContractDTO
    {
        public List<SupplierDTO> Suppliers
        {
            get { return GlobalServiceHelper.Suppliers; }
        }
    }
}
