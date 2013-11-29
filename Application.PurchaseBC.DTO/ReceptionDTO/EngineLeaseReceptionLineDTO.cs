#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/11 13:45:23
// 文件名：EngineLeaseReceptionLineDTO
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
    ///  租赁发动机接收项目
    /// 接收行
    /// </summary>
    [DataServiceKey("EngineLeaseReceptionLineId")]
    public partial class EngineLeaseReceptionLineDTO : ReceptionLineDTO
    {
        /// <summary>
        /// 租赁发动机接收行主键
        /// </summary>
        public int EngineLeaseReceptionLineId { get; set; }

    }
}
