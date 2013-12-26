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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     计划文号
        /// </summary>
        public string DocNumber { get; private set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; private set; }

        /// <summary>
        ///     计划标题
        /// </summary>
        public string Title { get; private set; }

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
        public PlanStatus Status { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesID { get; private set; }

        /// <summary>
        /// 年度外键
        /// </summary>
        public Guid AnnualID { get; private set; }

        /// <summary>
        ///     文档Id
        /// </summary>
        public Guid? DocumentID { get; private set; }

        #endregion

        #region 导航属性

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



        #endregion
    }
}
