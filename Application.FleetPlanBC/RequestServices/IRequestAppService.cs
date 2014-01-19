#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/29，17:12
// 文件名：IRequestAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion


#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;

#endregion

namespace UniCloud.Application.FleetPlanBC.RequestServices
{
    public interface IRequestAppService
    {

        /// <summary>
        ///     获取所有申请
        /// </summary>
        /// <returns></returns>
        IQueryable<RequestDTO> GetRequests();

        /// <summary>
        ///     获取批文的申请
        /// </summary>
        /// <returns></returns>
        IQueryable<ApprovalRequestDTO> GetApprovalRequests();

        /// <summary>
        /// 新增申请
        /// </summary>
        /// <param name="request">申请</param>
        void InsertRequest(RequestDTO request);

        /// <summary>
        /// 修改申请
        /// </summary>
        /// <param name="request"></param>
        void ModifyRequest(RequestDTO request);

        /// <summary>
        /// 删除申请
        /// </summary>
        /// <param name="request"></param>
        void DeleteRequest(RequestDTO request);
    }
}
