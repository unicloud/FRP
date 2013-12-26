#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 14:13:08
// 文件名：PlanEngine
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

namespace UniCloud.Domain.FleetPlanBC.Aggregates.PlanEngineAgg
{
    /// <summary>
    ///     计划发动机聚合根
    /// </summary>
    public class PlanEngine : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal PlanEngine()
        {
        }

        #endregion

        #region 属性
        
        #endregion

        #region 外键属性

        /// <summary>
        ///     实际发动机ID
        /// </summary>
        public Guid? EngineID { get; private set; }

        /// <summary>
        ///     发动机型号外键
        /// </summary>
        public Guid EngineTypeID { get; private set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesID { get; private set; }

        #endregion

        #region 导航属性


        #endregion

        #region 操作



        #endregion
    }
}
