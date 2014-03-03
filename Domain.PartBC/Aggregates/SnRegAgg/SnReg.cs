#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 9:23:21

// 文件名：SnReg
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
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.SnRegAgg
{
    /// <summary>
    ///     SnReg聚合根。
    /// </summary>
    public class SnReg : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<LifeMonitor> _lifeMonitors;
        private HashSet<SnHistory> _snHistories;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal SnReg()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     序号
        /// </summary>
        public string Sn { get; internal set; }

        /// <summary>
        ///     TSN，自装机以来使用小时数
        /// </summary>
        public decimal TSN { get; internal set; }

        /// <summary>
        ///     TSR，自上一次修理以来使用小时数
        /// </summary>
        public decimal TSR { get; internal set; }

        /// <summary>
        ///     CSN，自装机以来使用循环
        /// </summary>
        public decimal CSN { get; internal set; }

        /// <summary>
        ///     CSR，自上一次修理以来使用循环
        /// </summary>
        public decimal CSR { get; internal set; }

        /// <summary>
        ///     初始安装日期
        /// </summary>
        public DateTime InstallDate { get; internal set; }

        /// <summary>
        ///     件号
        /// </summary>
        public string Pn { get; private set; }

        /// <summary>
        ///     是否停用
        /// </summary>
        public bool IsStop { get; private set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     最近一次更新日期
        /// </summary>
        public DateTime UpdateDate { get; internal set; }

        /// <summary>
        ///     当前装机机号
        /// </summary>
        public string RegNumber { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     附件Id
        /// </summary>
        public int PnRegId { get; private set; }

        /// <summary>
        ///     当前飞机Id
        /// </summary>
        public Guid? AircraftId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     到寿监控集合
        /// </summary>
        public virtual ICollection<LifeMonitor> LifeMonitors
        {
            get { return _lifeMonitors ?? (_lifeMonitors = new HashSet<LifeMonitor>()); }
            set { _lifeMonitors = new HashSet<LifeMonitor>(value); }
        }

        /// <summary>
        ///     装机历史
        /// </summary>
        public virtual ICollection<SnHistory> SnHistories
        {
            get { return _snHistories ?? (_snHistories = new HashSet<SnHistory>()); }
            set { _snHistories = new HashSet<SnHistory>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置是否停用
        /// </summary>
        /// <param name="isStop">是否停用</param>
        public void SetIsStop(bool isStop)
        {
            IsStop = isStop;
        }

        /// <summary>
        ///     设置附件
        /// </summary>
        /// <param name="pnReg">附件</param>
        public void SetPnReg(PnReg pnReg)
        {
            if (pnReg == null || pnReg.IsTransient())
            {
                throw new ArgumentException("附件参数为空！");
            }

            Pn = pnReg.Pn;
            PnRegId = pnReg.Id;
        }

        /// <summary>
        ///     设置当前飞机
        /// </summary>
        /// <param name="aircraft">当前飞机</param>
        public void SetAircraft(Aircraft aircraft)
        {
            if (aircraft == null || aircraft.IsTransient())
            {
                throw new ArgumentException("飞机参数为空！");
            }

            AircraftId = aircraft.Id;
            RegNumber = aircraft.RegNumber;
        }

        /// <summary>
        ///     新增到寿监控
        /// </summary>
        /// <param name="maintainWork">维修工作</param>
        /// <param name="start">到寿日期开始</param>
        /// <param name="end">到寿日期结束</param>
        /// <returns>到寿监控</returns>
        public LifeMonitor AddNewLifeMonitor(MaintainWork maintainWork, DateTime start, DateTime end)
        {
            var lifeMonitor = new LifeMonitor
            {
                SnRegId = Id,
            };
            lifeMonitor.GenerateNewIdentity();
            lifeMonitor.SetMaintainWork(maintainWork);
            lifeMonitor.SetMointorPeriod(start, end);
            LifeMonitors.Add(lifeMonitor);

            return lifeMonitor;
        }

        /// <summary>
        ///     新增装机历史
        /// </summary>
        /// <returns></returns>
        public SnHistory AddNewSnHistory()
        {
            var snHistory = new SnHistory
            {
                SnRegId = Id,
                Sn = Sn,
            };
            snHistory.GenerateNewIdentity();
            SnHistories.Add(snHistory);

            return snHistory;
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