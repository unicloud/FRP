#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/6 15:38:11
// 文件名：EnginePlanStatus
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

#endregion

namespace UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums
{
    /// <summary>
    ///     计划的处理状态
    /// </summary>
    public enum EnginePlanStatus
    {
        草稿 = 0,
        待审核 = 1,
        已审核 = 2,
    }
}
