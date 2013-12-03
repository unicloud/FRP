#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/3 11:18:50
// 文件名：IActionCategoryAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;

#endregion

namespace UniCloud.Application.PurchaseBC.ActionCategoryServices
{
    /// <summary>
    ///     活动类型服务接口。
    /// </summary>
    public interface IActionCategoryAppService
    {
        /// <summary>
        ///     获取所有活动类型
        /// </summary>
        /// <returns></returns>
        IQueryable<ActionCategoryDTO> GetActionCategories();
    }
}
