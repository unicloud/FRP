#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:14:33
// 文件名：SubOperationHistory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg
{
    /// <summary>
    ///     (分子公司)运营权历史
    /// </summary>
    public class SubOperationHistory : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal SubOperationHistory()
        {
        }

        #endregion

        #region 属性


        /// <summary>
        ///     运营日期
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        ///     退出日期
        /// </summary>
        public DateTime? EndDate { get; private set; }


        #endregion

        #region 外键属性

        /// <summary>
        ///     母公司运营历史外键
        /// </summary>
        public Guid OperationHistoryID { get; private set; }

        /// <summary>
        ///     分公司外键
        /// </summary>
        public Guid SubAirlinesID { get; private set; }


        #endregion

        #region 导航属性



        #endregion

        #region 操作



        #endregion
    }
}
