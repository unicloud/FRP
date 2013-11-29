#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/15 17:26:01
// 文件名：MaintainContractQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.AcTypeQueries
{
    public class AcTypeQuery : IAcTypeQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public AcTypeQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<AcTypeDTO> AcTypesQuery(QueryBuilder<AircraftType> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<AircraftType>()).Select(p => new AcTypeDTO
            {
                AcTypeId = p.Id,
                Name = p.Name
            });
        }
    }
}