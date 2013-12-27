﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 18:17:32
// 文件名：EnginePlanDeliverStatus
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
    ///    备发计划明细状态
    /// </summary>
    public enum EnginePlanDeliverStatus
    {
        预备 = 0,
        签约 = 1,
        接收 = 2,
        运营 = 3
    }
}