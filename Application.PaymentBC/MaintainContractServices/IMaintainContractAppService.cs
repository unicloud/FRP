#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/27，13:12
// 文件名：IMaintainContractAppService.cs
// 程序集：UniCloud.Application.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;

namespace UniCloud.Application.PaymentBC.MaintainContractServices
{
    /// <summary>
    ///     表示用于维修合同相关信息服务
    /// </summary>
    public interface IMaintainContractAppService
    {
        /// <summary>
        ///     获取所有维修合同
        /// </summary>
        /// <returns>所有维修合同</returns>
        IQueryable<MaintainContractDTO> GetMaintainContracts();
     
    }
}
