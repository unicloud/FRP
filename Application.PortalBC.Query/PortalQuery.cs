#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：10:45
// 方案：FRP
// 项目：Application.PortalBC.Query
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PortalBC.DTO;
using UniCloud.Domain.PortalBC.Aggregates.AircraftSeriesAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PortalBC.Query
{
    public class PortalQuery : IPortalQuery
    {
        #region IPortalQuery 成员

        private readonly IQueryableUnitOfWork _unitOfWork;

        public PortalQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<AircraftSeriesDTO> AircraftSeriesDTOQuery(QueryBuilder<AircraftSeries> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<AircraftSeries>()).Select(p => new AircraftSeriesDTO
            {
                Id = p.Id,
                Name = p.Name,
            });
        }

        #endregion
    }
}