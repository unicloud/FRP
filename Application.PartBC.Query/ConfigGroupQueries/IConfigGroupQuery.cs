#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/02/25，10:02
// 文件名：IConfigGroupQuery.cs
// 程序集：UniCloud.Application.PartBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.PartBC.DTO;

#endregion

namespace UniCloud.Application.PartBC.Query.ConfigGroupQueries
{
    public interface IConfigGroupQuery
    {
        /// <summary>
        ///     构型组查询
        /// </summary>
        /// <returns>构型组DTO集合</returns>
        List<ConfigGroupDTO> ConfigGroupDTOQuery();
    }
}