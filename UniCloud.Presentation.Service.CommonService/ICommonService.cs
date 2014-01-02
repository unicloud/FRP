#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/06，11:12
// 文件名：IPurchaseService.cs
// 程序集：UniCloud.Presentation.Service.CommonService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using UniCloud.Presentation.Service.CommonService.Common;

#endregion

namespace UniCloud.Presentation.Service.CommonService
{
    /// <summary>
    ///     通用领域服务
    /// </summary>
    public interface ICommonService : IService
    {
        /// <summary>
        ///     数据服务上下文
        /// </summary>
        CommonServiceData Context { get; }
    }
}