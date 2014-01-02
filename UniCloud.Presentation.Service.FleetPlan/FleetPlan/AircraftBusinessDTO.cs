#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/2 11:30:45
// 文件名：AircraftBusinessDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/2 11:30:45
// 修改说明：
// ========================================================================*/
#endregion
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

namespace UniCloud.Presentation.Service.FleetPlan.FleetPlan
{
    public partial class AircraftBusinessDTO
    {
        private string _aircraftTypeName;
        public string AircraftTypeName { get { return _aircraftTypeName; } }

        private string _regional;
        public string Regional { get { return _regional; } }

        partial void OnAircraftTypeIdChanging(Guid value)
        {
        }
    }
}
