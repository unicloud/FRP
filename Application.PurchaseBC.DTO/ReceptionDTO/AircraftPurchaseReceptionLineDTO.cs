#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/11 13:44:14
// 文件名：AircraftPurchaseReceptionLineDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///  购买飞机接收项目
    /// 接收行
    /// </summary>
    [DataServiceKey("AircraftPurchaseReceptionLineId")]
    public partial class AircraftPurchaseReceptionLineDTO : ReceptionLineDTO
    {
        /// <summary>
        /// 购买飞机接收行主键
        /// </summary>
        public int AircraftPurchaseReceptionLineId { get; set; }

    }
}
