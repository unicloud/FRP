#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/11 17:02:41
// 文件名：ContractEngineQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Domain.PaymentBC.Aggregates.ContractEngineAgg;
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PaymentBC.Query.ContractEngineQueries
{
    /// <summary>
    /// 合同发动机查询实现
    /// </summary>
    public class ContractEngineQuery : IContractEngineQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public ContractEngineQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///    所有合同发动机查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>合同发动机DTO集合。</returns>
        public IQueryable<ContractEngineDTO> ContractEnginesQuery(
            QueryBuilder<ContractEngine> query)
        {
            var dbSupplier = _unitOfWork.CreateSet<Supplier>();
            return
                query.ApplyTo(_unitOfWork.CreateSet<ContractEngine>())
                     .Select(p => new ContractEngineDTO
                     {
                         ContractName = p.ContractName,
                         ContractNumber = p.ContractNumber,
                         RankNumber = p.RankNumber,
                         SerialNumber = p.SerialNumber,
                         IsValid = p.IsValid,
                         ImportType = p.ImportCategory.ActionType + "-" + p.ImportCategory.ActionName,
                         SupplierName = dbSupplier.FirstOrDefault(c => c.Id == p.SupplierId).Name,
                         SupplierId = p.SupplierId,

                     });
        }

    }
}
