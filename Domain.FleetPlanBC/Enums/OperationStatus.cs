#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:52:56
// 文件名：OperationStatus
// 版本：V1.0.0
//
// 修改者： 时间： 
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

namespace UniCloud.Domain.FleetPlanBC.Enums
{
    /// <summary>
    ///处理状态
    /// </summary>
    public enum OperationStatus
    {
        草稿 = 0,
        待审核 = 1,
        已审核 = 2,
        已提交 = 3,
    }
}
