#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：SupplierAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.SupplierQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.SupplierServices
{
    /// <summary>
    ///     实现供应商服务接口。
    ///     用于处理供应商相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class SupplierAppService : ContextBoundObject, ISupplierAppService
    {
        private readonly ISupplierQuery _supplierQuery;

        public SupplierAppService(ISupplierQuery supplierQuery)
        {
            _supplierQuery = supplierQuery;
        }

        #region SupplierDTO

        /// <summary>
        ///     获取所有供应商
        /// </summary>
        /// <returns></returns>
        public IQueryable<SupplierDTO> GetSuppliers()
        {
            var queryBuilder =
                new QueryBuilder<Supplier>();
            return _supplierQuery.SupplierDTOQuery(queryBuilder);
        }

        /// <summary>
        /// 获取所有的飞机供应商（飞机采购和租赁供应商）
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetAircraftSuppliers()
        {
            return _supplierQuery.GetAircraftSuppliers();
        }

        /// <summary>
        /// 获取所有的发动机供应商（发动机采购和租赁供应商）
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetEngineSuppliers()
        {
            return _supplierQuery.GetEngineSuppliers();
        }
        #endregion
    }
}