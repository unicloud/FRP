#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/13 9:44:59
// 文件名：EngineMaintainVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/13 9:44:59
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Payment.Invoice
{
    [Export(typeof(EngineMaintainVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EngineMaintainVm
    {

    }
}
