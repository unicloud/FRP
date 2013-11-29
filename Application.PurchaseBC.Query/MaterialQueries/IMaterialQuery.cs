#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/22，10:11
// 文件名：IMaterialQuery.cs
// 程序集：UniCloud.Application.PurchaseBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.MaterialQueries
{
    /// <summary>
    ///     物料查询接口。
    /// </summary>
    public interface IMaterialQuery
    {
        /// <summary>
        ///     飞机物料查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>飞机物料DTO集合</returns>
        IQueryable<AircraftMaterialDTO> AircraftMaterialsQuery(
            QueryBuilder<Material> query);

        /// <summary>
        ///     BFE物料查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>BFE物料DTO集合</returns>
        IQueryable<BFEMaterialDTO> BFEMaterialsQuery(
            QueryBuilder<Material> query);

        /// <summary>
        ///     发动机物料查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>发动机DTO集合</returns>
        IQueryable<EngineMaterialDTO> EngineMaterialsQuery(
            QueryBuilder<Material> query);
    }
}