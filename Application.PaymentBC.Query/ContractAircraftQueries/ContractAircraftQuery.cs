#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/12，16:12
// 文件名：ContractAircraftQuery.cs
// 程序集：UniCloud.Application.PaymentBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion


#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Domain.PaymentBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PaymentBC.Query.ContractAircraftQueries
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
        public IQueryable<ContractAircraftDTO> ContractAircraftsQuery(
            QueryBuilder<ContractAircraft> query)
        {
            var dbSupplier = _unitOfWork.CreateSet<Supplier>();
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
                         AircraftTypeName = p.AircraftType.Name,
                         ImportType = p.ImportCategory.ActionType + "-" + p.ImportCategory.ActionName,
                         ContractAircrafId = p.Id,
                         SupplierName = dbSupplier.FirstOrDefault(c=>c.Id==p.SupplierId).Name,
                         SupplierId = p.SupplierId,
                     });
        }

    }
}
