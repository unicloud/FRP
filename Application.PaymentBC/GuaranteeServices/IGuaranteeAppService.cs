#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/25，16:12
// 文件名：IGuaranteeAppService.cs
// 程序集：UniCloud.Application.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion


#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.DTO.GuaranteeDTO;

#endregion

namespace UniCloud.Application.PaymentBC.GuaranteeServices
{
    /// <summary>
    /// 查询保函服务接口
    /// </summary>
    public interface IGuaranteeAppService
    {
        #region 租赁保证金
        /// <summary>
        ///     获取租赁保证金保函
        /// </summary>
        /// <returns></returns>
        IQueryable<LeaseGuaranteeDTO> GetLeaseGuarantees();

        /// <summary>
        /// 新增租赁保证金
        /// </summary>
        /// <param name="guarantee">保证金</param>
        void InsertLeaseGuarantee(LeaseGuaranteeDTO guarantee);

        /// <summary>
        /// 修改租赁保证金
        /// </summary>
        /// <param name="guarantee">保证金</param>
        void ModifyLeaseGuarantee(LeaseGuaranteeDTO guarantee);

        /// <summary>
        /// 删除租赁保证金
        /// </summary>
        /// <param name="guarantee">保证金</param>
        void DeleteLeaseGuarantee(LeaseGuaranteeDTO guarantee);

        #endregion

        #region 大修保证金

        /// <summary>
        ///     获取大修保证金DTO
        /// </summary>
        /// <returns></returns>
        IQueryable<MaintainGuaranteeDTO> GetMaintainGuarantee();

        /// <summary>
        /// 新增大修保证金
        /// </summary>
        /// <param name="guarantee">保证金</param>
        void InsertMaintainGuarantee(MaintainGuaranteeDTO guarantee);

        /// <summary>
        /// 修改大修保证金
        /// </summary>
        /// <param name="guarantee">保证金</param>
        void ModifyMaintainGuarantee(MaintainGuaranteeDTO guarantee);

        /// <summary>
        /// 删除大修保证金
        /// </summary>
        /// <param name="guarantee">保证金</param>
        void DeleteMaintainGuarantee(MaintainGuaranteeDTO guarantee);
        #endregion
      
    }
}
