#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:00:52
// 文件名：AircraftBusiness
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

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg
{
    /// <summary>
    ///     商业数据历史
    /// </summary>
    public class AircraftBusiness : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftBusiness()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     座位数
        /// </summary>
        public int SeatingCapacity { get; private set; }

        /// <summary>
        ///     商载量
        /// </summary>
        public decimal CarryingCapacity { get; private set; }

        /// <summary>
        ///     开始日期
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        ///     结束日期
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        ///     处理状态
        /// </summary>
        public OperationStatus Status { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     飞机外键
        /// </summary>
        public Guid AircraftID { get; private set; }

        /// <summary>
        ///     机型外键
        /// </summary>
        public Guid AircraftTypeID { get; private set; }

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
