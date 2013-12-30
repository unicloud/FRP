#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/28 11:30:01
// 文件名：EngineImportTypeVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/28 11:30:01
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.FleetPlan.QueryAnalyse
{
    [Export(typeof(EngineImportTypeVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EngineImportTypeVm
    {

    }
}
