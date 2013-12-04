#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/3 10:51:58
// 文件名：AircraftTypeQuery
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
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.AircraftTypeQueries
{
    /// <summary>
    /// 机型查询
    /// </summary>
    public class AircraftTypeQuery : IAircraftTypeQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public AircraftTypeQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     机型查询。
        /// </summary>
        /// <param name="query">查询表达式</param>s
        /// <returns>AircraftTypeDTO集合</returns>
        public IQueryable<AircraftTypeDTO> AircraftTypeDTOQuery(QueryBuilder<AircraftType> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<AircraftType>()).Select(p => new AircraftTypeDTO
            {
                Id = p.Id,
                Name = p.Name
            });
        }
    }
}