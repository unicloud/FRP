#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:29:23
// 文件名：EnginePlanDTO
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

namespace UniCloud.Application.FleetPlanBC.DTO.EnginePlanDTO
{
    /// <summary>
    /// 备发计划
    /// </summary>
    [DataServiceKey("Id")]
    public class EnginePlanDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     计划标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     计划文号
        /// </summary>
        public string DocNumber { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        ///     版本号
        /// </summary>
        public int VersionNumber { get; set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsFinished { get; set; }

        /// <summary>
        ///     计划编辑处理状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesId { get; set; }

        /// <summary>
        /// 年度外键
        /// </summary>
        public Guid AnnualId { get; set; }

        /// <summary>
        ///     文档Id
        /// </summary>
        public Guid? DocumentId { get; set; }

        #endregion
    }
}
