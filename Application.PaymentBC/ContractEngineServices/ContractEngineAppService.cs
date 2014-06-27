#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/11 17:46:04
// 文件名：ContractEngineAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.Query.ContractEngineQueries;
using UniCloud.Domain.PaymentBC.Aggregates.ContractEngineAgg;

#endregion

namespace UniCloud.Application.PaymentBC.ContractEngineServices
{
    [LogAOP]
    public class ContractEngineAppService : ContextBoundObject, IContractEngineAppService
    {
        private readonly IContractEngineQuery _contractEngineQuery;

        public ContractEngineAppService(IContractEngineQuery contractEngineQuery)
        {
            _contractEngineQuery = contractEngineQuery;
        }

        #region ContractEngineDTO

        /// <summary>
        ///     获取所有合同发动机
        /// </summary>
        /// <returns></returns>
        public IQueryable<ContractEngineDTO> GetContractEngines()
        {
            var query =
                new QueryBuilder<ContractEngine>();
            return _contractEngineQuery.ContractEnginesQuery(query);
        }

        #endregion
    }
}