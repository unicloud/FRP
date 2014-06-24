#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/13 17:33:44
// 文件名：FunctionItemQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/13 17:33:44
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Domain.BaseManagementBC.Aggregates.FunctionItemAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleFunctionAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.BaseManagementBC.Query.FunctionItemQueries
{
    /// <summary>
    ///     FunctionItem查询
    /// </summary>
    public class FunctionItemQuery : IFunctionItemQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public FunctionItemQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     FunctionItem查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>FunctionItemDTO集合</returns>
        public IQueryable<FunctionItemDTO> FunctionItemsQuery(QueryBuilder<FunctionItem> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<FunctionItem>()).Select(p => new FunctionItemDTO
            {
                Id = p.Id,
                CreateDate = p.CreateDate,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                IsButton = p.IsButton,
                IsLeaf = p.IsLeaf,
                IsValid = p.IsValid,
                Name = p.Name,
                NaviUrl = p.NaviUrl,
                ParentItemId = p.ParentItemId,
                Sort = p.Sort,
            });
        }

        /// <summary>
        ///     获取FunctionItemWithHierarchy集合。
        ///     如果userName为空，则返回所有。
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>FunctionItemWithHierarchy集合。</returns>
        public IEnumerable<FunctionItemDTO> GetFunctionItemsWithHierarchy(string userName = null)
        {
            var tempFunctionItems = new List<FunctionItemDTO>();
            List<FunctionItem> applications;
            if (userName == null)
            {
                applications = _unitOfWork.CreateSet<FunctionItem>().Where(p => p.ParentItemId == null).ToList();
            }
            else
            {
                var roleIds =
                    _unitOfWork.CreateSet<User>()
                        .Where(u => u.UserName == userName)
                        .SelectMany(u => u.UserRoles)
                        .Select(ur => ur.RoleId)
                        .ToList();
                applications =
                    _unitOfWork.CreateSet<RoleFunction>()
                        .Where(rf => roleIds.Contains(rf.RoleId))
                        .Select(rf => rf.FunctionItem)
                        .Where(f => f.ParentItemId == null)
                        .ToList();
            }
            applications.ForEach(p =>
            {
                var temp = GenerateFunctionItem(p);
                tempFunctionItems.Add(temp);
            });

            return tempFunctionItems;
        }

        /// <summary>
        ///     <see cref="UniCloud.Application.BaseManagementBC.Query.FunctionItemQueries.IFunctionItemQuery" />
        /// </summary>
        /// <param name="userName">
        ///     <see cref="UniCloud.Application.BaseManagementBC.Query.FunctionItemQueries.IFunctionItemQuery" />
        /// </param>
        /// <returns>
        ///     <see cref="UniCloud.Application.BaseManagementBC.Query.FunctionItemQueries.IFunctionItemQuery" />
        /// </returns>
        public IEnumerable<FunctionItemDTO> GetFunctionItemsByUser(string userName)
        {
            if (userName == null) throw new ArgumentNullException("userName");
            var tempFunctionItems = new List<FunctionItemDTO>();
            var roleIds =
                _unitOfWork.CreateSet<User>()
                    .Where(u => u.UserName == userName)
                    .SelectMany(u => u.UserRoles)
                    .Select(ur => ur.RoleId)
                    .ToList();
            var applications = _unitOfWork.CreateSet<RoleFunction>()
                .Where(rf => roleIds.Contains(rf.RoleId))
                .Select(rf => rf.FunctionItem)
                .ToList();
            applications.ForEach(functionItem => tempFunctionItems.Add(new FunctionItemDTO
            {
                Id = functionItem.Id,
                CreateDate = functionItem.CreateDate,
                Description = functionItem.Description,
                ImageUrl = functionItem.ImageUrl,
                IsButton = functionItem.IsButton,
                IsLeaf = functionItem.IsLeaf,
                IsValid = functionItem.IsValid,
                Name = functionItem.Name,
                NaviUrl = functionItem.NaviUrl,
                ParentItemId = functionItem.ParentItemId,
                Sort = functionItem.Sort,
            }));

            return tempFunctionItems;
        }

        private FunctionItemDTO GenerateFunctionItem(FunctionItem functionItem)
        {
            var functionItemDataObject = new FunctionItemDTO
            {
                Id = functionItem.Id,
                CreateDate = functionItem.CreateDate,
                Description = functionItem.Description,
                ImageUrl = functionItem.ImageUrl,
                IsButton = functionItem.IsButton,
                IsLeaf = functionItem.IsLeaf,
                IsValid = functionItem.IsValid,
                Name = functionItem.Name,
                NaviUrl = functionItem.NaviUrl,
                ParentItemId = functionItem.ParentItemId,
                Sort = functionItem.Sort,
                SubFunctionItems = new List<FunctionItemDTO>(),
            };
            var temp =
                _unitOfWork.CreateSet<FunctionItem>()
                    .Where(p => p.ParentItemId == functionItem.Id)
                    .ToList()
                    .OrderBy(p => p.Sort);
            foreach (var subItem in temp)
            {
                functionItemDataObject.SubFunctionItems.Add(GenerateFunctionItem(subItem));
            }
            return functionItemDataObject;
        }
    }
}