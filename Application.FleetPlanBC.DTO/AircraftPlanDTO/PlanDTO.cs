#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:23:28
// 文件名：PlanDTO
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

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    ///     运力增减计划
    /// </summary>
    [DataServiceKey("Id")]
    public class PlanDTO
    {
        #region 私有字段

        //private List<PlanHistoryDTO> _planHistories;

        #endregion

        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     计划标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     是否有效版本
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        ///     版本号
        /// </summary>
        public int VersionNumber { get; set; }

        /// <summary>
        ///     提交日期
        /// </summary>
        public DateTime? SubmitDate { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     计划文号
        /// </summary>
        public string DocNumber { get; set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsFinished { get; set; }

        /// <summary>
        ///     计划编辑处理状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     发布计划处理状态
        /// </summary>
        public int PublishStatus { get; set; }

        /// <summary>
        ///     文档名称
        /// </summary>
        public string DocName { get; set; }

        /// <summary>
        ///     航空公司名称
        /// </summary>
        public string AirlinesName { get; set; }

        /// <summary>
        ///     年度
        /// </summary>
        public int Year { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesId { get; set; }

        /// <summary>
        ///     计划年度外键
        /// </summary>
        public Guid AnnualId { get; set; }

        /// <summary>
        ///     文档Id
        /// </summary>
        public Guid? DocumentId { get; set; }

        #endregion

        #region 导航属性

        ///// <summary>
        /////     飞机计划明细
        ///// </summary>
        //public virtual List<PlanHistoryDTO> PlanHistories
        //{
        //    get { return _planHistories ?? (_planHistories = new List<PlanHistoryDTO>()); }
        //    set { _planHistories = value ; }
        //}

        #endregion
    }
}