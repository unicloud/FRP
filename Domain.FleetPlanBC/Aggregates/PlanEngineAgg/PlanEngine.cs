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
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg;

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
        public Guid? EngineId { get; private set; }

        /// <summary>
        ///     发动机型号外键
        /// </summary>
        public Guid EngineTypeId { get; private set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 实际发动机
        /// </summary>
        public virtual Engine Engine { get; set; }

        /// <summary>
        /// 发动机型号
        /// </summary>
        public virtual EngineType EngineType { get; set; }

        /// <summary>
        /// 航空公司
        /// </summary>
        public virtual Airlines Airlines { get; set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置发动机
        /// </summary>
        /// <param name="engineID">实际发动机</param>
        public void SetEngine(Guid engineID)
        {
            if (engineID == null)
            {
                throw new ArgumentException("实际发动机Id参数为空！");
            }

            EngineId = engineID;
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
        ///     设置航空公司
        /// </summary>
        /// <param name="airlinesId">航空公司</param>
        public void SetAirlines(Guid airlinesId)
        {
            if (airlinesId == null)
            {
                throw new ArgumentException("航空公司Id参数为空！");
            }

            AirlinesId = airlinesId;
        }

        #endregion
    }
}
