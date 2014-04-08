#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/4 16:07:02
// 文件名：OrganizationQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/4 16:07:02
// 修改说明：
// ========================================================================*/
#endregion

using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg;
using UniCloud.Infrastructure.Data;

namespace UniCloud.Application.BaseManagementBC.Query.OrganizationQueries
{
    /// <summary>
    /// Organization查询
    /// </summary>
    public class OrganizationQuery : IOrganizationQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public OrganizationQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Organization查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>OrganizationDTO集合</returns>
        public IQueryable<OrganizationDTO> OrganizationsQuery(QueryBuilder<Organization> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Organization>()).Select(p => new OrganizationDTO
            {
                Id = p.Id,
                Code = p.Code,
                Description = p.Description,
                IsValid = p.IsValid,
                CreateDate = p.CreateDate,
                LastUpdateTime = p.LastUpdateTime,
                Sort = p.Sort,
                Name = p.Name,
                OrganizationRoles = p.OrganizationRoles.Select(q => new OrganizationRoleDTO
                {
                    Id = q.Id,
                    RoleId = q.RoleId,
                    OrganizationId = q.OrganizationId
                }).ToList()
            });
        }
    }
}
