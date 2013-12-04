#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/3 10:52:24
// 文件名：PlanAircraftQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.PlanAircraftAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.PlanAircraftQueries
{
    /// <summary>
    /// 计划飞机查询
    /// </summary>
    public class PlanAircraftQuery : IPlanAircraftQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public PlanAircraftQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     计划飞机查询。
        /// </summary>
        /// <param name="query">查询表达式</param>s
        /// <returns>PlanAircraftDTO集合</returns>
        public IQueryable<PlanAircraftDTO> PlanAircraftDTOQuery(QueryBuilder<PlanAircraft> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<PlanAircraft>()).Select(p => new PlanAircraftDTO
            {
                Id = p.Id,
                RegNumber = p.RegNumber,
            });
        }
    }
}