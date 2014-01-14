#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/14 16:17:48
// 文件名：CompleteStatus
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.ComponentModel;
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
    /// 计划完成状态
    /// 0：草稿
    /// 1：审核
    /// 2：已审核
    /// 3：已提交
    /// -1：无状态
    /// </summary>
    public enum CompleteStatus
    {
        [Description("0、草稿")]
        草稿 = 0,
        [Description("1、审核")]
        审核 = 1,
        [Description("2、已审核")]
        已审核 = 2,
        [Description("3、已提交")]
        已提交 = 3,
        [Description("-1、无状态")]
        无状态 = -1,


    }
}
