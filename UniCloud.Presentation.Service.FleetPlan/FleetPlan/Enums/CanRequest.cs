#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/14 15:41:05
// 文件名：CanRequest
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
    /// 能否提出申请
    /// 1、可申请
    /// 2、未报计划
    /// 3、已申请
    /// 4、无需申请
    /// </summary>
    public enum CanRequest
    {
        [Description("1、可申请")]
        可申请 = 0,

        [Description("2、未报计划")]
        未报计划 = 1,

        [Description("3、已申请")]
        已申请 = 2,

        [Description("4、无需申请")]
        无需申请 = 3,

    }
}
