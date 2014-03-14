#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/14 15:09:54
// 文件名：RoleQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/14 15:09:54
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg;
using UniCloud.Infrastructure.Data;

namespace UniCloud.Application.BaseManagementBC.Query.RoleQueries
{
    /// <summary>
    /// Role查询
    /// </summary>
    public class RoleQuery : IRoleQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public RoleQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Role查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>RoleDTO集合</returns>
        public IQueryable<RoleDTO> RolesQuery(QueryBuilder<Role> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Role>()).Select(p => new RoleDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                RoleFunctions = p.RoleFunctions.Select(q => new RoleFunctionDTO
                                                          {
                                                              Id = q.Id,
                                                              RoleId = q.RoleId,
                                                              FunctionItemId = q.FunctionItemId
                                                          }).ToList()
            });
        }
    }
}
