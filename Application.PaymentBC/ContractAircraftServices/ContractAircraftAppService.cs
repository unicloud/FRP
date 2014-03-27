#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/12，16:12
// 文件名：ContractAircraftAppService.cs
// 程序集：UniCloud.Application.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.Query.ContractAircraftQueries;
using UniCloud.Domain.PaymentBC.Aggregates.ContractAircraftAgg;

#endregion

namespace UniCloud.Application.PaymentBC.ContractAircraftServices
{
    [LogAOP]
    public class ContractAircraftAppService : ContextBoundObject, IContractAircraftAppService
    {
        private readonly IContractAircraftQuery _contractAircraftQuery;

        public ContractAircraftAppService(IContractAircraftQuery contractAircraftQuery)
        {
            _contractAircraftQuery = contractAircraftQuery;
        }

        #region ContractAircraftDTO

        /// <summary>
        ///     获取所有合同飞机
        /// </summary>
        /// <returns></returns>
        public IQueryable<ContractAircraftDTO> GetContractAircrafts()
        {
            var query =
                new QueryBuilder<ContractAircraft>();
            return _contractAircraftQuery.ContractAircraftsQuery(query);
        }

        #endregion
    }
}