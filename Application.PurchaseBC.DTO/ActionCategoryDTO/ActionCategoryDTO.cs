#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/3 10:39:04
// 文件名：ActionCategoryDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Services.Common;


#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    /// 活动类型（引进\退出）
    /// </summary>
    [DataServiceKey("Id")]
    public class ActionCategoryDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     活动类型
        /// </summary>
        public string ActionType { get; set; }

        /// <summary>
        ///     活动类型名称
        /// </summary>
        public string ActionName { get; set; }

        #endregion
    }
}
