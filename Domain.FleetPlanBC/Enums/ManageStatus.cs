#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:52:31
// 文件名：ManageStatus
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
    ///管理状态
    /// </summary>
    public enum ManageStatus
    {
        草稿 = 0,
        计划 = 1,
        申请 = 2,
        批文 = 3,
        签约 = 4,
        技术接收 = 5,
        接收 = 6,
        运营 = 7,
        停场待退 = 8,
        技术交付 = 9,
        退役 = 10,
    }
}
