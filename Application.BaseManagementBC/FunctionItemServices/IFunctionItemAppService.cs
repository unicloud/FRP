#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/13 17:38:35
// 文件名：IFunctionItemAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/13 17:38:35
// 修改说明：
// ========================================================================*/
#endregion

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;

namespace UniCloud.Application.BaseManagementBC.FunctionItemServices
{
    /// <summary>
    /// FunctionItem的服务接口。
    /// </summary>
    public interface IFunctionItemAppService
    {
        /// <summary>
        /// 获取所有FunctionItem。
        /// </summary>
        IQueryable<FunctionItemDTO> GetFunctionItems();

        /// <summary>
        /// 获取所有FunctionItemWithHierarchy
        /// </summary>
        /// <returns>所有的FunctionItemWithHierarchy。</returns>
        IEnumerable<FunctionItemDTO> GetFunctionItemsWithHierarchy();
    }
}
