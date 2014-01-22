#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:20:56
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

namespace UniCloud.Application.AircraftConfigBC.DTO
{
    /// <summary>
    /// 活动类型(飞发引进\退出方式)
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

        /// <summary>
        ///     需要审批
        /// </summary>
        public bool NeedRequest { get; set; }

        #endregion
    }
}
