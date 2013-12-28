#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:07:28
// 文件名：EnginePlan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Domain.FleetPlanBC.Enums;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg
{
    /// <summary>
    ///     备发计划聚合根
    /// </summary>
    public class EnginePlan : EntityGuid
    {
        #region 私有字段

        private HashSet<EnginePlanHistory> _enginePlanHistories;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal EnginePlan()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     计划标题
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        ///     计划文号
        /// </summary>
        public string DocNumber { get; private set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        ///     版本号
        /// </summary>
        public int VersionNumber { get; internal set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsFinished { get; private set; }

        /// <summary>
        ///     计划编辑处理状态
        /// </summary>
        public EnginePlanStatus Status { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesId { get; private set; }

        /// <summary>
        /// 年度外键
        /// </summary>
        public Guid AnnualId { get; private set; }

        /// <summary>
        ///     文档Id
        /// </summary>
        public Guid? DocumentId { get; private set; }

        #endregion

        #region 导航属性
        /// <summary>
        ///   航空公司
        /// </summary>
        public virtual Airlines Airlines { get; set; }

        /// <summary>
        /// 年度
        /// </summary>
        public virtual Annual Annual { get; set; }

        /// <summary>
        ///     备发计划明细
        /// </summary>
        public virtual ICollection<EnginePlanHistory> EnginePlanHistories
        {
            get { return _enginePlanHistories ?? (_enginePlanHistories = new HashSet<EnginePlanHistory>()); }
            set { _enginePlanHistories = new HashSet<EnginePlanHistory>(value); }
        }
        #endregion

        #region 操作
        /// <summary>
        ///     设置计划编辑处理状态
        /// </summary>
        /// <param name="status">计划编辑处理状态</param>
        public void SetEnginePlanStatus(EnginePlanStatus status)
        {
            switch (status)
            {
                case EnginePlanStatus.草稿:
                    Status = EnginePlanStatus.草稿;
                    break;
                case EnginePlanStatus.待审核:
                    Status = EnginePlanStatus.待审核;
                    break;
                case EnginePlanStatus.已审核:
                    Status = EnginePlanStatus.已审核;
                    IsValid = true;
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
            if (string.IsNullOrWhiteSpace(docNumber))
            {
                throw new ArgumentException("计划文号参数为空！");
            }

            DocNumber = docNumber;
        }

        /// <summary>
        ///     设置备注
        /// </summary>
        /// <param name="note">备注</param>
        public void SetNote(string note)
        {
            Note = note;
        }

        /// <summary>
        ///     设置航空公司
        /// </summary>
        /// <param name="airlinesId">航空公司</param>
        public void SetAirlines(Guid airlinesId)
        {
            if (airlinesId == null)
            {
                throw new ArgumentException("航空公司Id参数为空！");
            }

            AirlinesId = airlinesId;
        }

        /// <summary>
        ///     设置计划年度
        /// </summary>
        /// <param name="annualId">计划年度</param>
        public void SetAnnual(Guid annualId)
        {
            if (annualId == null)
            {
                throw new ArgumentException("计划年度Id参数为空！");
            }

            AnnualId = annualId;
        }

        /// <summary>
        ///     设置计划文档
        /// </summary>
        /// <param name="documentId">计划文档</param>
        public void SetDocument(Guid? documentId)
        {
            //if (documentId == null)
            //{
            //    throw new ArgumentException("计划文档Id参数为空！");
            //}

            DocumentId = documentId;
        }

        /// <summary>
        /// 新增备发计划明细
        /// </summary>
        /// <returns></returns>
        public EnginePlanHistory AddNewEnginePlanHistory()
        {
            var enginePlanHistory = new EnginePlanHistory
            {
                EnginePlanId = Id,
            };

            return enginePlanHistory;
        }

        #endregion
    }
}
