#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/4 10:41:05
// 文件名：ContractAircraftQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.ContractAircraftQueries
{
    public class ContractAircraftQuery : IContractAircraftQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public ContractAircraftQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///    所有合同飞机查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>合同飞机DTO集合。</returns>
        public IQueryable<ContractAircraftDTO> ContractAircraftDTOQuery(
            QueryBuilder<ContractAircraft> query)
        {
            
            return 
                query.ApplyTo(_unitOfWork.CreateSet<ContractAircraft>())
                     .Select(p => new ContractAircraftDTO
                     {
                         ContractName = p.ContractName,
                         ContractNumber = p.ContractNumber,
                         RankNumber = p.RankNumber,
                         CSCNumber = p.CSCNumber,
                         SerialNumber = p.SerialNumber,
                         IsValid = p.IsValid,
                         ReceivedAmount = p.ReceivedAmount,
                         AcceptedAmount = p.AcceptedAmount,
                         AircraftTypeId = p.AircraftTypeId,
                         AircraftTypeName = p.AircraftType.Name,
                         PlanAircraftID = p.PlanAircraftID,
                         PlanAircraft = p.PlanAircraft.RegNumber,
                         ImportCategoryId = p.ImportCategoryId,
                         ImportType = p.ImportCategory.ActionType,
                         ImportActionName = p.ImportCategory.ActionName,
                     });
        }

    }
}
