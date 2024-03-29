﻿#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:44:17
// 文件名：Plan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg
{
    /// <summary>
    ///     飞机计划聚合根
    /// </summary>
    public class Plan : EntityGuid
    {
        #region 私有字段

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Plan()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     计划标题
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        ///     是否有效版本，通过审核的计划均为有效
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        ///     版本号，同年度计划中版本号最高的为那一年度的“当前计划”
        /// </summary>
        public int VersionNumber { get; internal set; }

        /// <summary>
        ///     是否当前版本，民航局系统中设置
        /// </summary>
        public bool IsCurrentVersion { get; private set; }

        /// <summary>
        ///     提交日期
        /// </summary>
        public DateTime? SubmitDate { get; private set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     计划文号
        /// </summary>
        public string DocNumber { get; private set; }

        /// <summary>
        ///     是否完成，计划是否完成评审流程，计划发送后设为完成
        /// </summary>
        public bool IsFinished { get; private set; }

        /// <summary>
        ///     计划编辑处理状态
        /// </summary>
        public PlanStatus Status { get; private set; }

        /// <summary>
        ///     发布计划处理状态
        /// </summary>
        public PlanPublishStatus PublishStatus { get; private set; }

        /// <summary>
        ///     文档名称
        /// </summary>
        public string DocName { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesId { get; private set; }

        /// <summary>
        ///     计划年度外键
        /// </summary>
        public Guid AnnualId { get; private set; }

        /// <summary>
        ///     文档Id
        /// </summary>
        public Guid? DocumentId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     航空公司
        /// </summary>
        public virtual Airlines Airlines { get; private set; }

        /// <summary>
        ///     计划年度
        /// </summary>
        public virtual Annual Annual { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置计划编辑处理状态
        /// </summary>
        /// <param name="status">计划编辑处理状态</param>
        public void SetPlanStatus(PlanStatus status)
        {
            switch (status)
            {
                case PlanStatus.草稿:
                    Status = PlanStatus.草稿;
                    break;
                case PlanStatus.待审核:
                    Status = PlanStatus.待审核;
                    break;
                case PlanStatus.已审核:
                    Status = PlanStatus.已审核;
                    IsValid = true;
                    break;
                case PlanStatus.已提交:
                    Status = PlanStatus.已提交;
                    IsFinished = true;
                    SubmitDate = DateTime.Now;
                    break;
                case PlanStatus.退回:
                    Status = PlanStatus.退回;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }


        /// <summary>
        ///     设置发布计划处理状态
        /// </summary>
        /// <param name="status">发布计划处理状态</param>
        public void SetPlanPublishStatus(PlanPublishStatus status)
        {
            switch (status)
            {
                case PlanPublishStatus.待发布:
                    PublishStatus = PlanPublishStatus.待发布;
                    break;
                case PlanPublishStatus.待审核:
                    PublishStatus = PlanPublishStatus.待审核;
                    break;
                case PlanPublishStatus.已审核:
                    PublishStatus = PlanPublishStatus.已审核;
                    IsValid = true;
                    break;
                case PlanPublishStatus.已发布:
                    PublishStatus = PlanPublishStatus.已发布;
                    IsCurrentVersion = true;
                    IsFinished = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }

        /// <summary>
        ///     设置计划标题
        /// </summary>
        /// <param name="title">计划标题</param>
        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("计划标题参数为空！");
            }

            Title = title;
        }

        /// <summary>
        ///     设置计划文号
        /// </summary>
        /// <param name="docNumber">计划文号</param>
        public void SetDocNumber(string docNumber)
        {
            DocNumber = docNumber;
        }

        /// <summary>
        ///     设置航空公司
        /// </summary>
        /// <param name="airlines">航空公司</param>
        public void SetAirlines(Airlines airlines)
        {
            if (airlines == null || airlines.IsTransient())
            {
                throw new ArgumentException("航空公司Id参数为空！");
            }

            Airlines = airlines;
            AirlinesId = airlines.Id;
        }

        /// <summary>
        ///     设置计划年度
        /// </summary>
        /// <param name="annual">计划年度</param>
        public void SetAnnual(Annual annual)
        {
            if (annual == null || annual.IsTransient())
            {
                throw new ArgumentException("计划年度参数为空！");
            }

            Annual = annual;
            AnnualId = annual.Id;
        }

        /// <summary>
        ///     设置计划文档
        /// </summary>
        /// <param name="documentId">计划文档</param>
        /// <param name="docName">文档名称</param>
        public void SetDocument(Guid? documentId, string docName)
        {
            DocumentId = documentId;
            DocName = docName;
        }

        #endregion
    }
}