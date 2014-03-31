#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：SupplierQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.SupplierQueries
{
    public class SupplierQuery : ISupplierQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public SupplierQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     所有权人（供应商）查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>所有权人（供应商）DTO集合。</returns>
        public IQueryable<SupplierDTO> SupplierDTOQuery(
            QueryBuilder<Supplier> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Supplier>()).Select(p => new SupplierDTO
            {
                Id = p.Id,
                AirlineGuid = p.AirlineGuid,
                CnName = p.CnName,
                CnShortName = p.CnShortName,
                EnName = p.EnName,
                EnShortName = p.EnShortName,
                Code = p.Code,
                IsValid = p.IsValid,
                Note = p.Note,
                SupplierType = (int)p.SupplierType,
            });
        }
    }
}