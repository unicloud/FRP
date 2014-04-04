#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/13 11:38:33
// 文件名：AcConfigHistory
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
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftConfigurationAgg;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg
{
    /// <summary>
    ///     飞机配置历史
    /// </summary>
    public class AcConfigHistory : EntityInt
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AcConfigHistory()
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
        ///     飞机外键
        /// </summary>
        public Guid AircraftId { get; internal set; }


        /// <summary>
        ///     飞机配置外键
        /// </summary>
        public int AircraftConfigurationId { get; private set; }

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
        ///     设置飞机配置
        /// </summary>
        /// <param name="acConfiguration">配置</param>
        public void SetAircraftConfiguration(AircraftConfiguration acConfiguration)
        {
            if (acConfiguration == null || acConfiguration.IsTransient())
            {
                throw new ArgumentException("配置参数为空！");
            }

            AircraftConfigurationId = acConfiguration.Id;
        }

        #endregion
    }
}
