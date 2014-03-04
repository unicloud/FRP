#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/4 9:53:09
// 文件名：CorrectAcDailyUtilizationVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/4 9:53:09
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.MaintainAcDailyUtilization
{
    [Export(typeof(CorrectAcDailyUtilizationVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CorrectAcDailyUtilizationVm
    {

    }
}
