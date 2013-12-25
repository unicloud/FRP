#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/25，16:12
// 文件名：MaintainInvoiceQuery.cs
// 程序集：UniCloud.Application.PaymentBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO.GuaranteeDTO;
using UniCloud.Domain.PaymentBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PaymentBC.Aggregates.GuaranteeAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PaymentBC.Query.GuaranteeQueries
{
    public class GuaranteeQuery : IGuaranteeQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public GuaranteeQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<LeaseGuaranteeDTO> LeaseGuaranteeQuery(QueryBuilder<Guarantee> query)
        {
            var dbCurrency = _unitOfWork.CreateSet<Currency>();
            return
                query.ApplyTo(_unitOfWork.CreateSet<Guarantee>()).OfType<LeaseGuarantee>()
                    .Select(p => new LeaseGuaranteeDTO
                    {
                        GuaranteeId = p.Id,
                        Amount = p.Amount,
                        CreateDate = p.CreateDate,
                        CurrencyId = p.CurrencyId,
                        EndDate = p.EndDate,
                        OperatorName = p.OperatorName,
                        OrderId = p.OrderId,
                        ReviewDate = p.ReviewDate,
                        Reviewer = p.Reviewer,
                        StartDate = p.StartDate,
                        SupplierId = p.SupplierId,
                        SupplierName = p.SupplierName,
                        CurrencyName = dbCurrency.FirstOrDefault(c => c.Id == p.CurrencyId).CnName,
                    });
        }

        public IQueryable<MaintainGuaranteeDTO> MaintainGuaranteeQuery(QueryBuilder<Guarantee> query)
        {
            var dbCurrency = _unitOfWork.CreateSet<Currency>();
            return
                query.ApplyTo(_unitOfWork.CreateSet<Guarantee>()).OfType<MaintainGuarantee>()
                    .Select(p => new MaintainGuaranteeDTO
                    {
                        GuaranteeId = p.Id,
                        Amount = p.Amount,
                        CreateDate = p.CreateDate,
                        CurrencyId = p.CurrencyId,
                        EndDate = p.EndDate,
                        OperatorName = p.OperatorName,
                        MaintainContractId = p.MaintainContractId,
                        ReviewDate = p.ReviewDate,
                        Reviewer = p.Reviewer,
                        StartDate = p.StartDate,
                        SupplierId = p.SupplierId,
                        SupplierName = p.SupplierName,
                        CurrencyName = dbCurrency.FirstOrDefault(c => c.Id == p.CurrencyId).CnName,
                    });
        }
    }
}