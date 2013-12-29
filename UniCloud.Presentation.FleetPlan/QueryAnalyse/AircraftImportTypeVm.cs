#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/28 11:25:03
// 文件名：AircraftImportTypeVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/28 11:25:03
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export(typeof(AircraftImportTypeVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AircraftImportTypeVm
    {

    }
}
