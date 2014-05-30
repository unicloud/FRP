#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/16 0:51:52
// 文件名：ActionType
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

namespace UniCloud.Presentation.Service.Part.Part.Enums
{
    /// <summary>
    ///     SCN类型
    /// </summary>
    public enum ActionType
    {
        装上 = 0,
        拆下 = 1,
        拆换 = 2,
        非拆换 = 3,
    }
}
