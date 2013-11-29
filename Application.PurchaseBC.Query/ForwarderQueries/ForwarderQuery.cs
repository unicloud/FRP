#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/18，10:11
// 文件名：ForwarderQuery.cs
// 程序集：UniCloud.Application.PurchaseBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.ForwarderAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.ForwarderQueries
{
    /// <summary>
    ///     实现承运人接口。
    /// </summary>
    public class ForwarderQuery : IForwarderQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public ForwarderQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     承运人查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>承运人DTO集合。</returns>
        public IQueryable<ForwarderDTO> ForwardersQuery(
            QueryBuilder<Forwarder> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Forwarder>()).Select(a => new ForwarderDTO
            {
                ForwarderId = a.Id,
                Addr = a.Address.AddressLine1,
                Attn = a.Attn,
                Fax = a.Fax,
                Email = a.Email,
                Name = a.CnName,
                Tel = a.Tel
            });
        }
    }
}