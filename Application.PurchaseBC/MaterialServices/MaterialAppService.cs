#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/22，10:11
// 文件名：MaterialAppService.cs
// 程序集：UniCloud.Application.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.MaterialQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.MaterialServices
{
    /// <summary>
    ///     实现部件接口。
    ///     用于处理部件相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class MaterialAppService : ContextBoundObject, IMaterialAppService
    {
        private readonly IMaterialQuery _materialQuery;

        public MaterialAppService(IMaterialQuery materialQuery)
        {
            _materialQuery = materialQuery;
        }

        #region 飞机

        public IQueryable<AircraftMaterialDTO> GetAircraftMaterials()
        {
            var queryBuilder =
                new QueryBuilder<Material>();
            return _materialQuery.AircraftMaterialsQuery(queryBuilder);
        }

        #endregion

        #region BFE

        public IQueryable<BFEMaterialDTO> GetBFEMaterials()
        {
            var queryBuilder =
                new QueryBuilder<Material>();
            return _materialQuery.BFEMaterialsQuery(queryBuilder);
        }

        #endregion

        #region 发动机

        public IQueryable<EngineMaterialDTO> GetEngineMaterials()
        {
            var queryBuilder =
                new QueryBuilder<Material>();
            return _materialQuery.EngineMaterialsQuery(queryBuilder);
        }
        #endregion

    }
}