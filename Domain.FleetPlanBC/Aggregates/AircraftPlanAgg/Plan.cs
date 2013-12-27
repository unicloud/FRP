#region 版本信息
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.FleetPlanBC.Enums;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg
{
    /// <summary>
    ///     飞机计划聚合根
    /// </summary>
    public class Plan : EntityGuid
    {
        #region 私有字段

        private HashSet<PlanHistory> _planHistories;

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
        ///     是否有效版本
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        ///     版本号
        /// </summary>
        public int VersionNumber { get; private set; }

        /// <summary>
        ///     是否当前版本
        /// </summary>
        public bool IsCurrentVersion { get;internal set; }

        /// <summary>
        ///     提交日期
        /// </summary>
        public DateTime? SubmitDate { get; internal set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     文档Id
        /// </summary>
        public Guid? DocumentID { get; private set; }

        /// <summary>
        ///     计划文号
        /// </summary>
        public string DocNumber { get; private set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsFinished { get; private set; }

        /// <summary>
        ///     客机评审标记
        /// </summary>
        public bool? ManageFlagPnr { get; private set; }

        /// <summary>
        ///     货机评审标记
        /// </summary>
        public bool? ManageFlagCargo { get; private set; }

        /// <summary>
        ///     评审备注
        /// </summary>
        public string ManageNote { get;private set; }

        /// <summary>
        ///     计划编辑处理状态
        /// </summary>
        public PlanStatus Status { get; private set; }

        /// <summary>
        ///     发布计划处理状态
        /// </summary>
        public PlanPublishStatus PublishStatus { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesID { get; private set; }

        /// <summary>
        ///     计划年度外键
        /// </summary>
        public Guid AnnualID { get; private set; }


        #endregion

        #region 导航属性

        /// <summary>
        ///     飞机计划明细
        /// </summary>
        public virtual ICollection<PlanHistory> EnginePlanHistories
        {
            get { return _planHistories ?? (_planHistories = new HashSet<PlanHistory>()); }
            set { _planHistories = new HashSet<PlanHistory>(value); }
        }


        #endregion

        #region 操作



        #endregion
    }
}
