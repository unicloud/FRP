#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/13 17:40:06
// 文件名：ManaApprovalHistory
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/13 17:40:06
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO.DataTransfer
{
    public class ManaApprovalHistory
    {
        public Guid ApprovalHistoryID { get; set; }
        public bool IsApproved { get; set; } // 是否批准

        public virtual ApprovalHistory ApprovalHistory { get; set; }
    }

}
