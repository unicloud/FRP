#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/5 9:36:11
// 文件名：AcDailyUtilizationDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/5 9:36:11
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

namespace UniCloud.Presentation.Service.Part.Part
{
    public partial class AcDailyUtilizationDTO
    {
        public DateTime Date
        {
            get { return new DateTime(Year, Month, 1); }
        }
    }
}
