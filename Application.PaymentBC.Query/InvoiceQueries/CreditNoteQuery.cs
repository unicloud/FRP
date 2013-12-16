#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/15 10:38:26
// 文件名：CreditNoteQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PaymentBC.Query.InvoiceQueries
{
    /// <summary>
    /// 贷项单查询实现
    /// </summary>
    public class CreditNoteQuery : ICreditNoteQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public CreditNoteQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///    贷项单查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>贷项单DTO集合。</returns>
        //public IQueryable<CreditNoteDTO> ContractEngineDTOQuery(
        //    QueryBuilder<CreditNote> query)
        //{

        //    return
        //        query.ApplyTo(_unitOfWork.CreateSet<CreditNote>())
        //             .Select(p => new CreditNoteDTO
        //             {
        //             });
        //}

    }

}
