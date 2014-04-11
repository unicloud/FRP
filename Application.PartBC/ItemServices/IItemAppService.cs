#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/03，10:04
// 文件名：IItemAppService.cs
// 程序集：UniCloud.Application.PartBC
// 版本：V1.0.0
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

#endregion

namespace UniCloud.Application.PartBC.ItemServices
{
    /// <summary>
    ///     附件项服务接口。
    /// </summary>
    public interface IItemAppService
    {
        /// <summary>
        ///     获取所有附件项
        /// </summary>
        /// <returns></returns>
        IQueryable<ItemDTO> GetItems();

        /// <summary>
        /// 获取机型对应的项的集合
        /// </summary>
        /// <param name="aircraftTypeId"></param>
        /// <returns></returns>
        List<ItemDTO> GetItemsByAircraftType(Guid aircraftTypeId);
    }
}