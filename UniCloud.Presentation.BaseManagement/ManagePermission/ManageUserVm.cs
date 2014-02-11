#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/11 14:27:53
// 文件名：ManageUserVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/11 14:27:53
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    [Export(typeof(ManageUserVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManageUserVm
    {

    }
}
