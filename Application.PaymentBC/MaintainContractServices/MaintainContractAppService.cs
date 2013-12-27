#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/27，13:12
// 文件名：MaintainContractAppService.cs
// 程序集：UniCloud.Application.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion


#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.Query.MaintainContractQueries;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainContractAgg;

#endregion

namespace UniCloud.Application.PaymentBC.MaintainContractServices
{
    /// <summary>
    ///     实现发动机维修合同接口。
    ///     用于处于维修合同相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class MaintainContractAppService : IMaintainContractAppService
    {
        private readonly IMaintainContractQuery _maintainContractQuery;


        public MaintainContractAppService(IMaintainContractQuery maintainContractQuery)
        {
            _maintainContractQuery = maintainContractQuery;
        }
        /// <summary>
        ///     获取所有维修合同。
        /// </summary>
        /// <returns>所有维修合同。</returns>
        public IQueryable<MaintainContractDTO> GetMaintainContracts()
        {
            var queryBuilder = new QueryBuilder<MaintainContract>();
            return _maintainContractQuery.MaintainContractsQuery(queryBuilder);
        }

   
    }
}
