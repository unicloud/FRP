#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：MailAddressQuery.cs
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
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.MailAddressQueries
{
    public class MailAddressQuery : IMailAddressQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public MailAddressQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     邮件账号查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>邮件账号DTO集合。</returns>
        public IQueryable<MailAddressDTO> MailAddressDTOQuery(
            QueryBuilder<MailAddress> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<MailAddress>()).Select(p => new MailAddressDTO
            {
                Id = p.Id,
                Address = p.Address,
                DisplayName = p.DisplayName,
                LoginPassword = p.LoginPassword,
                LoginUser = p.LoginUser,
                Pop3Host = p.Pop3Host,
                ReceivePort = p.ReceivePort,
                ReceiveSSL = p.ReceiveSSL,
                SendPort = p.SendPort,
                SendSSL = p.SendSSL,
                ServerType = p.ServerType,
                SmtpHost = p.SmtpHost,
                StartTLS = p.StartTLS,
            });
        }
    }
}