#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/15 10:38:26
// 文件名：CreditMemoQuery
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


#endregion

namespace UniCloud.Application.PaymentBC.Query.InvoiceQueries
{
    /// <summary>
    /// 合同发动机查询实现
    /// </summary>
    public class CreditMemoQuery : ICreditMemoQuery
    {
    //            private readonly IQueryableUnitOfWork _unitOfWork;
    //    public CreditMemoQuery(IQueryableUnitOfWork unitOfWork)
    //    {
    //        _unitOfWork = unitOfWork;
    //    }

    //    /// <summary>
    //    ///    所有合同发动机查询
    //    /// </summary>
    //    /// <param name="query">查询表达式。</param>
    //    /// <returns>合同发动机DTO集合。</returns>
    //    public IQueryable<CreditMemoDTO> ContractEngineDTOQuery(
    //        QueryBuilder<CreditMemo> query)
    //    {
            
    //        return
    //            query.ApplyTo(_unitOfWork.CreateSet<ContractEngine>())
    //                 .Select(p => new ContractEngineDTO
    //                 {
    //                     ContractName = p.ContractName,
    //                     ContractNumber = p.ContractNumber,
    //                     RankNumber = p.RankNumber,
    //                     SerialNumber = p.SerialNumber,
    //                     IsValid = p.IsValid,
    //                     ReceivedAmount = p.ReceivedAmount,
    //                     AcceptedAmount = p.AcceptedAmount,
    //                     ImportCategoryId = p.ImportCategoryId,
    //                     ImportType = p.ImportCategory.ActionType,
    //                     ImportActionName = p.ImportCategory.ActionName,
    //                 });
    //    }

    //}

    }
}
