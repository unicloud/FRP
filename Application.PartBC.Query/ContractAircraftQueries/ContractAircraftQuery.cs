#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ContractAircraftQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigHistoryAgg;
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.SpecialConfigAgg;
using UniCloud.Infrastructure.Data;
#endregion

namespace UniCloud.Application.PartBC.Query.ContractAircraftQueries
{
    /// <summary>
    /// ContractAircraft查询
    /// </summary>
    public class ContractAircraftQuery : IContractAircraftQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public ContractAircraftQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// ContractAircraft查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>ContractAircraftDTO集合</returns>
        public IQueryable<ContractAircraftDTO> ContractAircraftDTOQuery(QueryBuilder<ContractAircraft> query)
        {
            var basicConfigHistories = _unitOfWork.CreateSet<BasicConfigHistory>();
            var specialConfigs = _unitOfWork.CreateSet<SpecialConfig>();

            return query.ApplyTo(_unitOfWork.CreateSet<ContractAircraft>()).Select(p => new ContractAircraftDTO
            {
                Id = p.Id,
                ContractName = p.ContractName,
                ContractNumber = p.ContractNumber,
                CSCNumber = p.CSCNumber,
                IsValid = p.IsValid,
                RankNumber = p.RankNumber,
                SerialNumber = p.SerialNumber,
                BasicConfigHistories = basicConfigHistories.Where(q=>q.ContractAircraftId==p.Id && q.EndDate==null).Select(r=>new BasicConfigHistoryDTO
                {
                    Id = r.Id,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    BasicConfigGroupId = r.Id,
                    ContractAircraftId = r.ContractAircraftId,
                }).ToList(),
                SpecialConfigs = specialConfigs.Where(q=>q.ContractAircraftId==p.Id && q.EndDate==null).Select(r=>new SpecialConfigDTO
                {
                    Id = r.Id,
                    ContractAircraftId = r.ContractAircraftId,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    IsValid = r.IsValid,
                    FiNumber = r.FiNumber,
                    ItemId = r.ItemId,
                    ItemNo = r.ItemNo,
                    ParentId = r.ParentId,
                    ParentItemNo = r.ParentItemNo,
                    Position = r.Position,
                    RootId = r.RootId,
                    Description = r.Description,
                }).ToList(),
            });
        }
    }
}
