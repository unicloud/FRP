#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：chency 时间：2014/2/21 14:58:58

// 文件名：UserQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Domain.BaseManagementBC.Aggregates.OrganizationAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.BaseManagementBC.Query.UserQueries
{
    /// <summary>
    ///     User查询
    /// </summary>
    public class UserQuery : IUserQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public UserQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     User查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>UserDTO集合</returns>
        public IQueryable<UserDTO> UsersQuery(QueryBuilder<User> query)
        {
            var dbOrganization = _unitOfWork.CreateSet<Organization>().OrderByDescending(p => p.Code);
            return query.ApplyTo(_unitOfWork.CreateSet<User>()).Select(p => new UserDTO
            {
                Id = p.Id,
                DisplayName = p.DisplayName,
                CreateDate = p.CreateDate,
                Description = p.Comment,
                Email = p.Email,
                EmployeeCode = p.UserName,
                OrganizationNo = p.OrganizationNo,
                Mobile = p.Mobile,
                Password = p.Password,
                OrganizationName = dbOrganization.FirstOrDefault(t => p.OrganizationNo.Equals(t.Code)).Name,
                UserRoles = p.UserRoles.Select(q => new UserRoleDTO
                {
                    Id = q.Id,
                    RoleId = q.RoleId,
                    UserId = q.UserId
                }).ToList()
            });
        }
    }
}