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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;

namespace UniCloud.Domain.PartBC.Aggregates.SnRegAgg
{
    /// <summary>
    /// SnReg聚合根。
    /// </summary>
    public class SnReg : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<SnHistory> _snHistories;

        private HashSet<LifeMonitor> _lifeMonitors;
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
        /// 序号
        /// </summary>
        public string Sn
        {
            get;
            private set;
        }

        /// <summary>
        /// 初始安装日期
        /// </summary>
        public DateTime InstallDate
        {
            get;
            private set;
        }

        /// <summary>
        /// 件号
        /// </summary>
        public string Pn
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否停用
        /// </summary>
        public bool IsStop
        {
            get;
            private set;
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

        /// <summary>
        /// 当前装机机号
        /// </summary>
        public string RegNumber
        {
            get;
            private set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 附件Id
        /// </summary>
        public int PnRegId
        {
            get;
            private set;
        }

        /// <summary>
        /// 当前飞机Id
        /// </summary>
        public Guid? AircraftId
        {
            get;
            private set;
        }

        #endregion

        #region 导航属性

        /// <summary>
        /// 到寿监控集合
        /// </summary>
        public virtual ICollection<LifeMonitor> LifeMonitors
        {
            get { return _lifeMonitors ?? (_lifeMonitors = new HashSet<LifeMonitor>()); }
            set { _lifeMonitors = new HashSet<LifeMonitor>(value); }
        }

        /// <summary>
        /// 装机历史
        /// </summary>
        public virtual ICollection<SnHistory> SnHistories
        {
            get { return _snHistories ?? (_snHistories = new HashSet<SnHistory>()); }
            set { _snHistories = new HashSet<SnHistory>(value); }
        }
        #endregion

        #region 操作

        /// <summary>
        ///     设置序号
        /// </summary>
        /// <param name="sn">序号</param>
        public void SetSn(string sn)
        {
            if (string.IsNullOrWhiteSpace(sn))
            {
                throw new ArgumentException("序号参数为空！");
            }

            Sn = sn;
        }

        /// <summary>
        ///     设置初始安装日期
        /// </summary>
        /// <param name="date">初始安装日期</param>
        public void SetInstallDate(DateTime date)
        {
            InstallDate = date;
        }

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
            if (aircraft != null)
            {
                AircraftId = aircraft.Id;
                RegNumber = aircraft.RegNumber;
            }
        }

        /// <summary>
        /// 新增到寿监控
        /// </summary>
        /// <returns></returns>
        public LifeMonitor AddNewLifeMonitor()
        {
            var lifeMonitor = new LifeMonitor
            {
                SnRegId = Id,
            };

            lifeMonitor.GenerateNewIdentity();
            LifeMonitors.Add(lifeMonitor);

            return lifeMonitor;
        }

        /// <summary>
        /// 新增装机历史
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
