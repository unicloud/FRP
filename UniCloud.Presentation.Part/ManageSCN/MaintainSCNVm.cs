﻿#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/21 15:56:19
// 文件名：MaintainSCNVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/21 15:56:19
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Part.ManageSCN
{
    [Export(typeof(MaintainSCNVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MaintainSCNVm
    {

    }
}
