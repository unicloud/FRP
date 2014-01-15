#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 14:44:07
// 文件名：AirProgrammingLine
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftSeriesAgg;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AirProgrammingAgg
{
    /// <summary>
    ///     航空公司五年规划行
    /// </summary>
    public class AirProgrammingLine : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AirProgrammingLine()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     年份
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        ///     购买数量
        /// </summary>
        public int BuyNum { get; private set; }

        /// <summary>
        ///     退出数量
        /// </summary>
        public int ExportNum { get; private set; }

        /// <summary>
        ///     租赁数量
        /// </summary>
        public int LeaseNum { get; private set; }


        #endregion

        #region 外键属性

        /// <summary>
        ///     飞机系列
        /// </summary>
        public Guid AircraftSeriesId { get; private set; }

        /// <summary>
        ///     飞机座级
        /// </summary>
        public Guid AircraftCategoryId { get; private set; }

        /// <summary>
        ///     航空公司规划
        /// </summary>
        public Guid AirProgrammingId { get; internal set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 飞机系列
        /// </summary>
        public virtual AircraftSeries AircraftSeries { get; private set; }

        /// <summary>
        /// 飞机座级
        /// </summary>
        public virtual AircraftCategory AircraftCategory { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        /// 设置规划内容
        /// </summary>
        /// <param name="year"></param>
        /// <param name="buyNum"></param>
        /// <param name="leaseNum"></param>
        /// <param name="exportNum"></param>
        public void SetAirProgramming(int year, int buyNum, int leaseNum, int exportNum)
        {
            if (buyNum < 0)
            {
                throw new ArgumentException("采购数量不能为负数！");
            }
            if (leaseNum < 0)
            {
                throw new ArgumentException("租赁数量不能为负数！");
            }
            if (exportNum < 0)
            {
                throw new ArgumentException("退出数量不能为负数！");
            }
            Year = year;
            BuyNum = buyNum;
            LeaseNum = leaseNum;
            ExportNum = exportNum;
        }

        /// <summary>
        ///     设置飞机系列
        /// </summary>
        /// <param name="aircraftSeries">飞机系列</param>
        public void SetAircraftSeries(AircraftSeries aircraftSeries)
        {
            if (aircraftSeries == null)
            {
                throw new ArgumentException("飞机系列参数为空！");
            }

            AircraftSeries = aircraftSeries;
            AircraftSeriesId = aircraftSeries.Id;
        }

        /// <summary>
        ///     设置飞机座级
        /// </summary>
        /// <param name="aircraftCategory">飞机座级</param>
        public void SetAircraftCategory(AircraftCategory aircraftCategory)
        {
            if (aircraftCategory == null)
            {
                throw new ArgumentException("飞机座级参数为空！");
            }

            AircraftCategory = aircraftCategory;
            AircraftCategoryId = aircraftCategory.Id;
        }

        #endregion
    }
}
