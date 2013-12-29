#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 13:46:13
// 文件名：EngineBusinessHistory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg
{
    /// <summary>
    ///     发动机商业数据历史
    /// </summary>
    public class EngineBusinessHistory : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal EngineBusinessHistory()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     开始日期
        /// </summary>
        public DateTime? StartDate { get; private set; }

        /// <summary>
        ///     结束日期
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        ///     最大推力
        /// </summary>
        public decimal MaxThrust { get; private set; }
        
        #endregion

        #region 外键属性

        /// <summary>
        ///     实际发动机外键
        /// </summary>
        public Guid EngineId { get; internal set; }

        /// <summary>
        ///     发动机型号外键
        /// </summary>
        public Guid EngineTypeId { get; private set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public Guid ImportCategoryId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 发动机型号
        /// </summary>
        public virtual EngineType EngineType { get; set; }

        /// <summary>
        /// 引进方式
        /// </summary>
        public virtual ActionCategory ImportCategory { get; set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置开始日期
        /// </summary>
        /// <param name="date">开始日期</param>
        public void SetStartDate(DateTime date)
        {
            StartDate = date;
        }

        /// <summary>
        ///     设置结束日期
        /// </summary>
        /// <param name="date">结束日期</param>
        public void SetEndDate(DateTime? date)
        {
            EndDate = date;
        }

        /// <summary>
        ///     设置发动机最大推力
        /// </summary>
        /// <param name="maxThrust">最大推力</param>
        public void SetMaxThrust(decimal maxThrust)
        {
            MaxThrust = maxThrust;
        }


        /// <summary>
        ///     设置发动机型号
        /// </summary>
        /// <param name="engineTypeId">发动机型号</param>
        public void SetEngineType(Guid engineTypeId)
        {
            if (engineTypeId == null)
            {
                throw new ArgumentException("发动机型号Id参数为空！");
            }

            EngineTypeId = engineTypeId;
        }

        /// <summary>
        ///     设置引进方式
        /// </summary>
        /// <param name="importCategoryId">引进方式</param>
        public void SetImportCategory(Guid importCategoryId)
        {
            if (importCategoryId == null)
            {
                throw new ArgumentException("引进方式Id参数为空！");
            }

            ImportCategoryId = importCategoryId;
        }
        #endregion
    }
}
