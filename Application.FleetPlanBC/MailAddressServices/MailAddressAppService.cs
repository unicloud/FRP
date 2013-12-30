﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：MailAddressAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.MailAddressQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.MailAddressAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.MailAddressServices
{
    /// <summary>
    ///     实现邮箱账号服务接口。
    ///     用于处理邮箱账号相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class MailAddressAppService : IMailAddressAppService
    {
        private readonly IMailAddressQuery _mailAddressQuery;

        public MailAddressAppService(IMailAddressQuery mailAddressQuery)
        {
            _mailAddressQuery = mailAddressQuery;
        }

        #region MailAddressDTO

        /// <summary>
        ///     获取所有邮箱账号
        /// </summary>
        /// <returns></returns>
        public IQueryable<MailAddressDTO> GetMailAddresses()
        {
            var queryBuilder =
                new QueryBuilder<MailAddress>();
            return _mailAddressQuery.MailAddressDTOQuery(queryBuilder);
        }

        #endregion
    }
}
