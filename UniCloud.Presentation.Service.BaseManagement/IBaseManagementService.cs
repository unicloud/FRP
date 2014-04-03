#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/13 16:37:56
// 文件名：IBaseManagementService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/13 16:37:56
// 修改说明：
// ========================================================================*/
#endregion

using UniCloud.Presentation.Service.BaseManagement.BaseManagement;

namespace UniCloud.Presentation.Service.BaseManagement
{
    public interface IBaseManagementService : IService
    {
        /// <summary>
        ///     数据服务上下文
        /// </summary>
        BaseManagementData Context { get; }
    }
}
