#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：BasicConfigGroupQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;
using UniCloud.Infrastructure.Data;
#endregion

namespace UniCloud.Application.PartBC.Query.BasicConfigGroupQueries
{
    /// <summary>
    /// BasicConfigGroup查询
    /// </summary>
    public class BasicConfigGroupQuery : IBasicConfigGroupQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public BasicConfigGroupQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// BasicConfigGroup查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>BasicConfigGroupDTO集合</returns>
        public IQueryable<BasicConfigGroupDTO> BasicConfigGroupDTOQuery(QueryBuilder<BasicConfigGroup> query)
        {
            var basicConfigs = _unitOfWork.CreateSet<BasicConfig>();

            return query.ApplyTo(_unitOfWork.CreateSet<BasicConfigGroup>()).Select(p => new BasicConfigGroupDTO
            {
                Id = p.Id,
                Description = p.Description,
                GroupNo = p.GroupNo,
                StartDate = p.StartDate,
                AircraftTypeId = p.AircraftTypeId,
                AircraftTypeName = p.AircraftType.Name,
            });
        }
    }
}
