#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/08，21:22
// 方案：FRP
// 项目：Application.ProjectBC.DTO
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.ProjectBC.DTO
{
    /// <summary>
    ///     任务案例
    /// </summary>
    [DataServiceKey("Id")]
    public class TaskCaseDTO
    {
        /// <summary>
        ///     任务案例ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     任务标准ID
        /// </summary>
        public int TaskStandardId { get; set; }

        /// <summary>
        ///     关联业务ID
        ///     <remarks>
        ///         用于查找相关实体
        ///         订单任务需要关联，但文档类任务无需关联
        ///     </remarks>
        /// </summary>
        public int RelatedId { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }
    }
}