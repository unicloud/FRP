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
        ///     结束日期
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        ///     开始日期
        /// </summary>
        public DateTime? StartDate { get; private set; }

        /// <summary>
        ///     最大推力
        /// </summary>
        public decimal MAXThrust { get; private set; }
        
        #endregion

        #region 外键属性

        /// <summary>
        ///     实际发动机外键
        /// </summary>
        public Guid EngineID { get; private set; }

        /// <summary>
        ///     发动机型号外键
        /// </summary>
        public Guid EngineTypeID { get; private set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public Guid ImportCategoryID { get; private set; }

        #endregion

        #region 导航属性



        #endregion

        #region 操作



        #endregion
    }
}
