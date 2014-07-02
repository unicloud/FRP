#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：ISupplierQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.SupplierQueries
{
    public interface ISupplierQuery
    {
        /// <summary>
        ///     所有权人（供应商）查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>所有权人（供应商）DTO集合</returns>
        IQueryable<SupplierDTO> SupplierDTOQuery(
            QueryBuilder<Supplier> query);


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