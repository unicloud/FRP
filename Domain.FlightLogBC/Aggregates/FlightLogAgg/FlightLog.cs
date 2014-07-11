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
        ///     航班号
        /// </summary>
        public string FlightNum { get; internal set; }

        /// <summary>
        ///     日志号
        /// </summary>
        public string LogNo { get; internal set; }

        /// <summary>
        ///     航段号
        /// </summary>
        public string LegNo { get; internal set; }

        /// <summary>
        ///     飞机注册号
        /// </summary>
        public string AcReg { get; internal set; }

        /// <summary>
        ///     飞机序列号
        /// </summary>
        public string MSN { get; internal set; }

        /// <summary>
        ///     飞行类别
        /// </summary>
        public string FlightType { get; internal set; }

        /// <summary>
        ///     航班日期
        /// </summary>
        public DateTime FlightDate { get; internal set; }

        /// <summary>
        ///     开车时间
        /// </summary>
        public string BlockOn { get; internal set; }

        /// <summary>
        ///     实际起飞时间
        /// </summary>
        public string TakeOff { get; internal set; }

        /// <summary>
        ///     着陆时间
        /// </summary>
        public string Landing { get; internal set; }

        /// <summary>
        ///     关车时间
        /// </summary>
        public string BlockStop { get; internal set; }

        /// <summary>
        ///     累计飞行时间（以起飞落地计）
        /// </summary>
        public decimal TotalFH { get; internal set; }

        /// <summary>
        ///     累计飞行时间（以开关车计）
        /// </summary>
        public decimal TotalBH { get; internal set; }

        /// <summary>
        ///     飞行时间（以起飞落地计）
        /// </summary>
        public decimal FlightHours { get; internal set; }

        /// <summary>
        ///     飞行时间（以开关车计）
        /// </summary>
        public decimal BlockHours { get; internal set; }

        /// <summary>
        ///     累计循环数
        /// </summary>
        public int TotalCycles { get; internal set; }

        /// <summary>
        ///     飞行循环
        /// </summary>
        public int Cycle { get; internal set; }

        /// <summary>
        ///     出发机场
        /// </summary>
        public string DepartureAirport { get; internal set; }

        /// <summary>
        ///     目的机场
        /// </summary>
        public string ArrivalAirport { get; internal set; }

        /// <summary>
        ///     训练循环数
        /// </summary>
        public int ToGoNumber { get; internal set; }

        /// <summary>
        ///     APU累计使用次数
        /// </summary>
        public int ApuCycle { get; internal set; }

        /// <summary>
        ///     APU累计使用分钟数
        /// </summary>
        public int ApuMM { get; internal set; }

        /// <summary>
        ///     航前发动机1滑油添加量
        /// </summary>
        public decimal ENG1OilDep { get; private set; }

        /// <summary>
        ///     航后发动机1滑油添加量
        /// </summary>
        public decimal ENG1OilArr { get; private set; }

        /// <summary>
        ///     航前发动机2滑油添加量
        /// </summary>
        public decimal ENG2OilDep { get; private set; }

        /// <summary>
        ///     航后发动机2滑油添加量
        /// </summary>
        public decimal ENG2OilArr { get; private set; }

        /// <summary>
        ///     航前发动机3滑油添加量
        /// </summary>
        public decimal ENG3OilDep { get; private set; }

        /// <summary>
        ///     航后发动机3滑油添加量
        /// </summary>
        public decimal ENG3OilArr { get; private set; }

        /// <summary>
        ///     航前发动机4滑油添加量
        /// </summary>
        public decimal ENG4OilDep { get; private set; }

        /// <summary>
        ///     航后发动机4滑油添加量
        /// </summary>
        public decimal ENG4OilArr { get; private set; }

        /// <summary>
        ///     航前APU滑油添加量
        /// </summary>
        public decimal ApuOilDep { get; private set; }

        /// <summary>
        ///     航后APU滑油添加量
        /// </summary>
        public decimal ApuOilArr { get; private set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     最近一次更新日期
        /// </summary>
        public DateTime? UpdateDate { get; internal set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置飞行时刻
        /// </summary>
        /// <param name="blockOn">开车时刻</param>
        /// <param name="takeOff">起飞时刻</param>
        /// <param name="landing">落地时刻</param>
        /// <param name="blockStop">关车时刻</param>
        public void SetFlightTime(string blockOn, string takeOff, string landing, string blockStop)
        {
            BlockOn = blockOn;
            TakeOff = takeOff;
            Landing = landing;
            BlockStop = blockStop;
        }

        /// <summary>
        ///     设置飞行小时
        /// </summary>
        /// <param name="flightHour">飞行小时（以起飞落地计）</param>
        /// <param name="blockHour">飞行小时（以开关车计）</param>
        /// <param name="totalFH">累计飞行时间（以起飞落地计）</param>
        /// <param name="totalBH">累计飞行时间（以开关车计）</param>
        /// <param name="totalCycle">累计使用循环</param>
        /// <param name="cycle">使用循环</param>
        /// <param name="toGoNum">训练循环数</param>
        /// <param name="apuCycle">累计APU使用循环</param>
        /// <param name="apuMM">累计APU使用分钟</param>
        public void SetFlightConsume(decimal flightHour, decimal blockHour, decimal totalFH, decimal totalBH,
            int totalCycle, int cycle, int toGoNum, int apuCycle, int apuMM)
        {
            FlightHours = flightHour;
            BlockHours = blockHour;
            TotalFH = totalFH;
            TotalBH = totalBH;
            TotalCycles = totalCycle;
            Cycle = cycle;
            ToGoNumber = toGoNum;
            ApuCycle = apuCycle;
            ApuMM = apuMM;
        }

        /// <summary>
        ///     设置滑油添加量数据
        /// </summary>
        /// <param name="eng1OilDep">1发航前添加量</param>
        /// <param name="eng1OilArr">1发航后添加量</param>
        /// <param name="eng2OilDep">2发航前添加量</param>
        /// <param name="eng2OilArr">2发航后添加量</param>
        /// <param name="eng3OilDep">3发航前添加量</param>
        /// <param name="eng3OilArr">3发航后添加量</param>
        /// <param name="eng4OilDep">4发航前添加量</param>
        /// <param name="eng4OilArr">4发航后添加量</param>
        /// <param name="apuOilDep">航前添加量</param>
        /// <param name="apuOilArr">航后添加量</param>
        public void SetOil(decimal eng1OilDep, decimal eng1OilArr, decimal eng2OilDep, decimal eng2OilArr,
            decimal eng3OilDep, decimal eng3OilArr, decimal eng4OilDep, decimal eng4OilArr, decimal apuOilDep,
            decimal apuOilArr)
        {
            ENG1OilDep = eng1OilDep;
            ENG1OilArr = eng1OilArr;
            ENG2OilDep = eng2OilDep;
            ENG2OilArr = eng2OilArr;
            ENG3OilDep = eng3OilDep;
            ENG3OilArr = eng3OilArr;
            ENG4OilDep = eng4OilDep;
            ENG4OilArr = eng4OilArr;
            ApuOilDep = apuOilDep;
            ApuOilArr = apuOilArr;
        }

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