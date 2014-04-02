#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/2 14:51:39
// 文件名：QueryContractVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/2 14:51:39
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Contract.QueryContracts
{
    [Export(typeof(QueryContractVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class QueryContractVm
    {

    }
}
