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

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Domain.BaseManagementBC.Aggregates.FunctionItemAgg;
using UniCloud.Infrastructure.Data;

namespace UniCloud.Application.BaseManagementBC.Query.FunctionItemQueries
{
    /// <summary>
    /// FunctionItem查询
    /// </summary>
    public class FunctionItemQuery : IFunctionItemQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public FunctionItemQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// FunctionItem查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>FunctionItemDTO集合</returns>
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
        /// 获取所有FunctionItemWithHierarchy
        /// </summary>
        /// <returns>所有的FunctionItemWithHierarchy。</returns>
        public IEnumerable<FunctionItemDTO> GetFunctionItemsWithHierarchy()
        {
            var applications = _unitOfWork.CreateSet<FunctionItem>().Where(p => p.ParentItemId == null).ToList();
            var tempFunctionItems = new List<FunctionItemDTO>();
            applications.ForEach(p =>
                                 {
                                     var temp = GenerateFunctionItem(p);
                                     tempFunctionItems.Add(temp);
                                 });

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
            var temp = _unitOfWork.CreateSet<FunctionItem>().Where(p => p.ParentItemId == functionItem.Id).ToList().OrderBy(p => p.Sort);
            foreach (var subItem in temp)
            {
                functionItemDataObject.SubFunctionItems.Add(GenerateFunctionItem(subItem));
            }
            return functionItemDataObject;
        }
    }
}
