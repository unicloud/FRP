#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/13 9:43:24
// 文件名：ManageOperationLicenseVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/13 9:43:24
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManageOperationQualification
{
    [Export(typeof(ManageOperationLicenseVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManageOperationLicenseVm
    {

    }
}
