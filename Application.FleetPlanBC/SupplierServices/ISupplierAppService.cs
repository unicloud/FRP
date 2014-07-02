#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：ISupplierAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;

#endregion

namespace UniCloud.Application.FleetPlanBC.SupplierServices
{
    /// <summary>
    ///     供应商服务接口。
    /// </summary>
    public interface ISupplierAppService
    {
        /// <summary>
        ///     获取所有供应商
        /// </summary>
        /// <returns></returns>
        IQueryable<SupplierDTO> GetSuppliers();

        /// <summary>
        /// 获取所有的飞机供应商（飞机采购和租赁供应商）
        /// </summary>
        /// <returns></returns>
        List<SupplierDTO> GetAircraftSuppliers();

        /// <summary>
        /// 获取所有的发动机供应商（发动机采购和租赁供应商）
        /// </summary>
        /// <returns></returns>
        List<SupplierDTO> GetEngineSuppliers();
    }
}