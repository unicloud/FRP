#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/23 16:00:39
// 文件名：FlightLog
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg
{
    /// <summary>
    ///     飞行日志聚合根
    /// </summary>
    public class FlightLog : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal FlightLog()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 航班号
        /// </summary>
        public string FlightNum
        {
            get;
            set;
        }

        ///日志号
        public string LogNo
        {
            get;
            set;
        }

        ///航段号
        public string LegNo
        {
            get;
            set;
        }

        /// <summary>
        /// 飞机注册号
        /// </summary>
        public string AcReg
        {
            get;
            set;
        }

        /// <summary>
        /// 飞机序列号
        /// </summary>
        public string MSN
        {
            get;
            set;
        }

        /// <summary>
        /// 飞行类别
        /// </summary>
        public string FlightType
        {
            get;
            set;
        }

        /// <summary>
        /// 航班日期
        /// </summary>
        public DateTime FlightDate
        {
            get;
            set;
        }

        /// <summary>
        /// 开车时间
        /// </summary>
        public string BlockOn
        {
            get;
            set;
        }

        /// <summary>
        /// 实际起飞时间
        /// </summary>
        public string TakeOff
        {
            get;
            set;
        }

        /// <summary>
        /// 着陆时间
        /// </summary>
        public string Landing
        {
            get;
            set;
        }

        /// <summary>
        /// 关车时间
        /// </summary>
        public string BlockStop
        {
            get;
            set;
        }

        /// <summary>
        /// 累计飞行时间
        /// </summary>
        public decimal TotalFH
        {
            get;
            set;
        }

        /// <summary>
        /// 累计开车时间
        /// </summary>
        public decimal TotalBH
        {
            get;
            set;
        }

        /// <summary>
        /// 飞行时间
        /// </summary>
        public decimal FlightHours
        {
            get;
            set;
        }

        /// <summary>
        /// 开车时长
        /// </summary>
        public decimal BlockHours
        {
            get;
            set;
        }

        /// <summary>
        /// 累计循环数
        /// </summary>
        public int TotalCycles
        {
            get;
            set;
        }

        /// <summary>
        /// 飞行循环
        /// </summary>
        public int Cycle
        {
            get;
            set;
        }

        /// <summary>
        /// 起始机场
        /// </summary>
        public string DepartureAirport
        {
            get;
            set;
        }

        /// <summary>
        /// 目的机场
        /// </summary>
        public string ArrivalAirport
        {
            get;
            set;
        }

        /// <summary>
        /// 训练循环数
        /// </summary>
        public int ToGoNumber
        {
            get;
            set;
        }

        /// <summary>
        /// APU累计使用次数
        /// </summary>
        public int ApuCycle
        {
            get;
            set;
        }

        /// <summary>
        /// APU累计使用分钟数
        /// </summary>
        public int ApuMM
        {
            get;
            set;
        }

        /// <summary>
        /// 航前发动机1滑油添加量
        /// </summary>
        public decimal ENG1OilDep
        {
            get;
            set;
        }

        /// <summary>
        /// 航后发动机1滑油添加量
        /// </summary>
        public decimal ENG1OilArr
        {
            get;
            set;
        }

        /// <summary>
        /// 航前发动机2滑油添加量
        /// </summary>
        public decimal ENG2OilDep
        {
            get;
            set;
        }

        /// <summary>
        /// 航后发动机2滑油添加量
        /// </summary>
        public decimal ENG2OilArr
        {
            get;
            set;
        }

        /// <summary>
        /// 航前发动机3滑油添加量
        /// </summary>
        public decimal ENG3OilDep
        {
            get;
            set;
        }

        /// <summary>
        /// 航后发动机3滑油添加量
        /// </summary>
        public decimal ENG3OilArr
        {
            get;
            set;
        }

        /// <summary>
        /// 航前发动机4滑油添加量
        /// </summary>
        public decimal ENG4OilDep
        {
            get;
            set;
        }

        /// <summary>
        /// 航后发动机4滑油添加量
        /// </summary>
        public decimal ENG4OilArr
        {
            get;
            set;
        }

        /// <summary>
        /// 航前APU滑油添加量
        /// </summary>
        public decimal ApuOilDep
        {
            get;
            set;
        }

        /// <summary>
        /// 航后APU滑油添加量
        /// </summary>
        public decimal ApuOilArr
        {
            get;
            set;
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate
        {
            get;
            set;
        }

        /// <summary>
        /// 最近一次更新日期
        /// </summary>
        public DateTime? UpdateDate
        {
            get;
            set;
        }
        #endregion

        #region 外键属性



        #endregion

        #region 导航属性



        #endregion

        #region 操作



        #endregion

        #region IValidatableObject 成员

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            #region 验证逻辑

            #endregion

            return validationResults;
        }

        #endregion
    }
}
