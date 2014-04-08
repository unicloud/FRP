#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：IMailAddressQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.MailAddressAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.MailAddressQueries
{
    public interface IMailAddressQuery
    {
        /// <summary>
        ///     邮件账号查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>邮件账号DTO集合</returns>
        IQueryable<MailAddressDTO> MailAddressDTOQuery(
            QueryBuilder<MailAddress> query);
    }
}