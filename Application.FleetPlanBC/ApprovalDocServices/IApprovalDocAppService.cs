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
using UniCloud.Application.FleetPlanBC.DTO.ApporvalDocDTO;

#endregion

namespace UniCloud.Application.FleetPlanBC.ApprovalDocServices
{
    public interface IApprovalDocAppService
    {

        /// <summary>
        ///     获取所有批文
        /// </summary>
        /// <returns></returns>
        IQueryable<ApprovalDocDTO> GetApprovalDocs();

        /// <summary>
        /// 新增批文
        /// </summary>
        /// <param name="approvalDoc"></param>
        void InsertApprovalDoc(ApprovalDocDTO approvalDoc);

        /// <summary>
        /// 修改批文
        /// </summary>
        /// <param name="approvalDoc"></param>
        void ModifyApprovalDoc(ApprovalDocDTO approvalDoc);

        /// <summary>
        /// 删除批文
        /// </summary>
        /// <param name="approvalDoc"></param>
        void DeleteApprovalDoc(ApprovalDocDTO approvalDoc);
    
    }
}
