#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 14:51:28
// 文件名：CaacProgrammingLine
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.CaacProgrammingAgg
{
    /// <summary>
    ///     民航局五年规划明细
    /// </summary>
    public class CaacProgrammingLine : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal CaacProgrammingLine()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     数量
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        ///     年份
        /// </summary>
        public int Year { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     飞机类别（座机）
        /// </summary>
        public Guid AircraftCategoryId { get; private set; }

        /// <summary>
        ///     民航局下发规划
        /// </summary>
        public Guid CaacProgrammingId { get; internal set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     飞机类别（座机）
        /// </summary>
        public virtual AircraftCategory AircraftCategory { get; private set; }
       
        #endregion

        #region 操作

        /// <summary>
        /// 设置规划内容
        /// </summary>
        /// <param name="year"></param>
        /// <param name="number"></param>
        public void SetCaacProgramming(int year, int number)
        {
            if (number < 0)
            {
                throw new ArgumentException("数量不能为负数！");
            }
            Year = year;
            Number = number;
        }
        
        /// <summary>
        ///     设置飞机类别（座级范围）
        /// </summary>
        /// <param name="aircraftCategory">座级范围</param>
        public void SetAircraftCategory(AircraftCategory aircraftCategory)
        {
            if (aircraftCategory == null || aircraftCategory.IsTransient())
            {
                throw new ArgumentException("座级参数为空！");
            }

            AircraftCategory = aircraftCategory;
            AircraftCategoryId = aircraftCategory.Id;
        }

        #endregion
    }
}
