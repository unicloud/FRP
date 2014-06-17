#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/3 12:03:47
// 文件名：SnStatus
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
    ///     序号件状态
    /// </summary>
    public enum SnStatus
    {
        装机 = 0,
        在库 = 1,
        在修 = 2,
        出租 = 3,
        出售 = 4,
        报废 = 5,
        其它 = 6,
    }
}
