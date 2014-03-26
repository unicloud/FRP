#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/02/25，10:02
// 文件名：ConfigGroupAppService.cs
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
using UniCloud.Application.AOP.Log;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.ConfigGroupQueries;

#endregion

namespace UniCloud.Application.PartBC.ConfigGroupServices
{
    /// <summary>
    ///     实现构型组服务接口。
    ///     用于处理typeName相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class ConfigGroupAppService : ContextBoundObject, IConfigGroupAppService
    {
        private readonly IConfigGroupQuery _actionCategoryQuery;

        public ConfigGroupAppService(IConfigGroupQuery actionCategoryQuery)
        {
            _actionCategoryQuery = actionCategoryQuery;
        }

        #region ConfigGroupDTO

        /// <summary>
        ///     获取所有构型组
        /// </summary>
        /// <returns></returns>
        public List<ConfigGroupDTO> GetConfigGroups()
        {
            return _actionCategoryQuery.ConfigGroupDTOQuery();
        }
        #endregion
    }
}
