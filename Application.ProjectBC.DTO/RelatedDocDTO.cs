#region 版本信息

// ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/08，22:01
// 方案：FRP
// 项目：Application.ProjectBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.ProjectBC.DTO
{
    /// <summary>
    ///     关联文档
    /// </summary>
    [DataServiceKey("Id")]
    public class RelatedDocDTO
    {
        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     业务外键
        /// </summary>
        public Guid SourceId { get; set; }

        /// <summary>
        ///     文档外键
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        ///     文档名称
        /// </summary>
        public string DocumentName { get; set; }

        #endregion
    }
}