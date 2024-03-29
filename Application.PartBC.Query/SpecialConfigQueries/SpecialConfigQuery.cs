#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SpecialConfigQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Domain.PartBC.Aggregates.SpecialConfigAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PartBC.Query.SpecialConfigQueries
{
    /// <summary>
    ///     SpecialConfig查询
    /// </summary>
    public class SpecialConfigQuery : ISpecialConfigQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public SpecialConfigQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     SpecialConfig查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>SpecialConfigDTO集合</returns>
        public IQueryable<SpecialConfigDTO> SpecialConfigDTOQuery(QueryBuilder<SpecialConfig> query)
        {
            var items = _unitOfWork.CreateSet<Item>();

            return query.ApplyTo(_unitOfWork.CreateSet<SpecialConfig>()).Select(p => new SpecialConfigDTO
            {
                Id = p.Id,
                StartDate = p.StartDate,
                Description = p.Description,
                ContractAircraftId = p.ContractAircraftId,
                EndDate = p.EndDate,
                FiNumber = items.FirstOrDefault(l => l.Id == p.ItemId).FiNumber,
                ItemNo = items.FirstOrDefault(l => l.Id == p.ItemId).ItemNo,
                ItemName = items.FirstOrDefault(l => l.Id == p.ItemId).Name,
                IsValid = p.IsValid,
                ItemId = p.ItemId,
                ParentId = p.ParentId,
                RootId = p.RootId,
                Position = (int)p.Position,
            });
        }
    }
}