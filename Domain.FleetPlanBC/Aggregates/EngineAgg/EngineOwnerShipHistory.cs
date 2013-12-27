#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 13:46:21
// 文件名：EngineOwnerShipHistory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg
{
    /// <summary>
    ///     发动机所有权历史
    /// </summary>
    public class EngineOwnerShipHistory : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal EngineOwnerShipHistory()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     开始日期
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        ///     结束日期
        /// </summary>
        public DateTime? EndDate { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     实际发动机外键
        /// </summary>
        public Guid EngineId { get; internal set; }

        /// <summary>
        ///     所有权人
        /// </summary>
        public Guid SupplierId { get; private set; }

        #endregion

        #region 导航属性



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
        ///     设置所有权人
        /// </summary>
        /// <param name="supplierId">所有权人</param>
        public void SetSupplier(Guid supplierId)
        {
            if (supplierId == null)
            {
                throw new ArgumentException("所有权人Id参数为空！");
            }

            SupplierId = supplierId;
        }
        #endregion
    }
}
