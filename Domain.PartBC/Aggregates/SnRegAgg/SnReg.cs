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
using UniCloud.Domain.Common.Enums;
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
        ///     当前序号
        /// </summary>
        public string Sn { get; internal set; }

        /// <summary>
        ///     所有曾经使用过的序号名称
        /// </summary>
        public string AllSnName { get; set; }

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
        ///     当前件号
        /// </summary>
        public string Pn { get; private set; }

        /// <summary>
        ///     所有曾经使用过件号名称
        /// </summary>
        public string AllPnName { get; set; }

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

        /// <summary>
        ///     序号件状态
        /// </summary>
        public SnStatus Status { get; private set; }

        /// <summary>
        ///     是否寿控件
        /// </summary>
        public bool IsLife { get; private set; }

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

        #endregion

        #region 操作

        /// <summary>
        ///     设置序号件状态
        /// </summary>
        /// <param name="status">序号件状态</param>
        public void SetSnStatus(SnStatus status)
        {
            switch (status)
            {
                case SnStatus.NotAgree:
                    Status = SnStatus.NotAgree;
                    break;
                case SnStatus.Stored:
                    Status = SnStatus.Stored;
                    break;
                case SnStatus.InTransfer:
                    Status = SnStatus.InTransfer;
                    break;
                case SnStatus.FlightKit:
                    Status = SnStatus.FlightKit;
                    break;
                case SnStatus.Lent:
                    Status = SnStatus.Lent;
                    break;
                case SnStatus.InLocalRepair:
                    Status = SnStatus.InLocalRepair;
                    break;
                case SnStatus.InOutsideRepair:
                    Status = SnStatus.InOutsideRepair;
                    break;
                case SnStatus.InRepairOnOtherSite:
                    Status = SnStatus.InRepairOnOtherSite;
                    break;
                case SnStatus.ToolsWithPersonalInventoryDocument:
                    Status = SnStatus.ToolsWithPersonalInventoryDocument;
                    break;
                case SnStatus.WorkshopOk:
                    Status = SnStatus.WorkshopOk;
                    break;
                case SnStatus.InStoreUorS:
                    Status = SnStatus.InStoreUorS;
                    break;
                case SnStatus.WorkshopUorS:
                    Status = SnStatus.WorkshopUorS;
                    break;
                case SnStatus.Unknown:
                    Status = SnStatus.Unknown;
                    break;
                case SnStatus.TemporaryScrap:
                    Status = SnStatus.TemporaryScrap;
                    break;
                case SnStatus.Scrapped:
                    Status = SnStatus.Scrapped;
                    break;
                case SnStatus.NewReference:
                    Status = SnStatus.NewReference;
                    break;
                case SnStatus.OnAircraft:
                    Status = SnStatus.OnAircraft;
                    break;
                case SnStatus.NotFollowed:
                    Status = SnStatus.NotFollowed;
                    break;
                case SnStatus.ToBeRepaired:
                    Status = SnStatus.ToBeRepaired;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
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
        ///     设置是否寿控件
        /// </summary>
        /// <param name="isLife">是否寿控件</param>
        public void SetIsLife(bool isLife)
        {
            IsLife = isLife;
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
            //当件号发生改变的时候，将原来的件号记录起来
            if (Pn != pnReg.Pn)
            {
                AllPnName += (Pn + ";");
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
            else
            {
                AircraftId = null;
                RegNumber = null;
            }
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