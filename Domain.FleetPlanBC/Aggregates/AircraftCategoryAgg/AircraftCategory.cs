#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:01:12
// 文件名：AircraftCategory
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


#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg
{
    /// <summary>
    ///     飞机座级聚合根
    /// </summary>
    public class AircraftCategory : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftCategory()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     类型
        /// </summary>
        public string Category { get; protected set; }

        /// <summary>
        ///     座级
        /// </summary>
        public string Regional { get; protected set; }

        #endregion

        #region 外键属性



        #endregion

        #region 导航属性



        #endregion

        #region 操作



        #endregion
    }
}
