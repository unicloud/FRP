#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/03，09:04
// 文件名：IItemQuery.cs
// 程序集：UniCloud.Application.PartBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;

#endregion

namespace UniCloud.Application.PartBC.Query.ItemQueries
{
    public interface IItemQuery
    {
        /// <summary>
        ///     附件项查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>附件项DTO集合</returns>
        IQueryable<ItemDTO> ItemDTOQuery(
            QueryBuilder<Item> query);

        /// <summary>
        /// 获取机型的项
        /// </summary>
        /// <param name="aircraftTypeId"></param>
        /// <returns></returns>
        List<ItemDTO> GetItemsByAircraftType(Guid aircraftTypeId);
    }
}