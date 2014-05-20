#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/18，10:11
// 文件名：ForwarderQuery.cs
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
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.MaterialQueries
{
    /// <summary>
    ///     实现物料接口。
    /// </summary>
    public class MaterialQuery : IMaterialQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public MaterialQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     飞机物料查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>飞机物料DTO集合</returns>
        public IQueryable<AircraftMaterialDTO> AircraftMaterialsQuery(
            QueryBuilder<Material> query)
        {
            return
                query.ApplyTo(_unitOfWork.CreateSet<Material>())
                    .OfType<AircraftMaterial>()
                    .Select(p => new AircraftMaterialDTO
                    {
                        AcMaterialId = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        AircraftTypeId = p.AircraftTypeId,
                    }
                    );
        }

        /// <summary>
        ///     发动机物料查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>发动机DTO集合</returns>
        public IQueryable<EngineMaterialDTO> EngineMaterialsQuery(
            QueryBuilder<Material> query)
        {
            return
                query.ApplyTo(_unitOfWork.CreateSet<Material>())
                    .OfType<EngineMaterial>()
                    .Select(p => new EngineMaterialDTO
                    {
                        EngineMaterialId = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        PartId = p.PartID,
                        ListPrice = p.ListPrice,
                    }
                    );
        }

        /// <summary>
        ///     BFE物料查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>BFE物料DTO集合</returns>
        public IQueryable<BFEMaterialDTO> BFEMaterialsQuery(
            QueryBuilder<Material> query)
        {
            return
                query.ApplyTo(_unitOfWork.CreateSet<Material>())
                    .OfType<BFEMaterial>()
                    .Select(p => new BFEMaterialDTO
                    {
                        BFEMaterialId= p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        PartId = p.PartID
                    }
                    );
        }
    }
}