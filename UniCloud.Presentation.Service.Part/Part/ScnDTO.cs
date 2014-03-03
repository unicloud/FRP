#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/25 14:52:17
// 文件名：ScnDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/25 14:52:17
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Linq;
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
    public partial class ScnDTO
    {
        partial void OnCostChanged()
        {
            if (ApplicableAircrafts != null && ApplicableAircrafts.Count > 0)
            {
                if (ScnType == 0)
                {
                    var average = Cost / ApplicableAircrafts.Count;
                    ApplicableAircrafts.ToList().ForEach(p => p.Cost = average);
                }
                else
                {
                    var first = ApplicableAircrafts.First();
                    first.Cost = Cost;
                    ApplicableAircrafts.ToList().ForEach(p => { if (p.Id != first.Id) p.Cost = 0; });
                }
            }
        }
    }
}
