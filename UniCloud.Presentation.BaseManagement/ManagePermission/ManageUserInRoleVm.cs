#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/11 14:52:18
// 文件名：ManageUserInRoleVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/11 14:52:18
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    [Export(typeof(ManageUserInRoleVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManageUserInRoleVm
    {

    }
}
