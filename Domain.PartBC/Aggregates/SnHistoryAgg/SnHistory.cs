#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/2 15:50:55
// 文件名：SnHistory
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
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRemInstRecordAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.SnHistoryAgg
{
    /// <summary>
    ///     SnHistory聚合根。
    ///     序号件装机历史
    /// </summary>
    public class SnHistory : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal SnHistory()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     装机序号
        /// </summary>
        public string Sn { get; private set; }

        /// <summary>
        ///     装机件号
        /// </summary>
        public string Pn { get; private set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     操作时间
        /// </summary>
        public DateTime ActionDate { get; private set; }

        /// <summary>
        ///     位置信息
        /// </summary>
        public Position Position { get; private set; }

        /// <summary>
        ///     操作类型 拆下/装上/非拆换
        /// </summary>
        public ActionType ActionType { get; private set; }

        /// <summary>
        ///     CSN，自装机以来使用循环,操作时间节点的值
        /// </summary>
        public int CSN { get; private set; }

        /// <summary>
        ///     TSN，自装机以来使用小时数，操作时间节点的值
        /// </summary>
        public decimal TSN { get; private set; }

        /// <summary>
        ///     CSN的基础上再累加在库时间折算的使用循环数
        /// </summary>
        public int CSN2 { get; private set; }

        /// <summary>
        ///     TSN的基础上再累加在库时间折算的使用小时数
        /// </summary>
        public decimal TSN2 { get; private set; }

        /// <summary>
        ///     序号件在历史节点上的状态
        /// </summary>
        public SnStatus Status { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     飞机ID
        /// </summary>
        public Guid AircraftId { get; private set; }

        /// <summary>
        ///     Sn外键
        /// </summary>
        public int SnRegId { get; private set; }

        /// <summary>
        ///     Pn外键
        /// </summary>
        public int PnRegId { get; private set; }

        /// <summary>
        ///     拆换记录外键（如果针对序号做操作时，不涉及拆换的情况，拆换记录Id可以为空）
        /// </summary>
        public int? RemInstRecordId { get; private set; }

        #endregion

        #region 导航属性

        public SnRemInstRecord RemInstRecord { get; private set; }

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
                case SnStatus.装机:
                    Status = SnStatus.装机;
                    break;
                case SnStatus.在库:
                    Status = SnStatus.在库;
                    break;
                case SnStatus.在修:
                    Status = SnStatus.在修;
                    break;
                case SnStatus.出租:
                    Status = SnStatus.出租;
                    break;
                case SnStatus.出售:
                    Status = SnStatus.出售;
                    break;
                case SnStatus.报废:
                    Status = SnStatus.报废;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }

        /// <summary>
        ///     设置拆换类型
        /// </summary>
        /// <param name="actionType">拆换类型</param>
        public void SetActionType(ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.拆下:
                    ActionType = ActionType.拆下;
                    break;
                case ActionType.装上:
                    ActionType = ActionType.装上;
                    break;
                case ActionType.拆换:
                    ActionType = ActionType.拆换;
                    break;
                case ActionType.非拆换:
                    ActionType = ActionType.非拆换;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("actionType");
            }
        }

        /// <summary>
        ///     设置位置信息
        /// </summary>
        /// <param name="position">位置信息</param>
        public void SetPosition(Position position)
        {
            Position = position;
        }

        /// <summary>
        ///     设置装机序号
        /// </summary>
        /// <param name="snReg">装机序号</param>
        public void SetSn(SnReg snReg)
        {
            if (snReg == null || snReg.IsTransient())
            {
                throw new ArgumentException("装机序号参数为空！");
            }

            Sn = snReg.Sn;
            SnRegId = snReg.Id;
        }

        /// <summary>
        ///     设置装机件号
        /// </summary>
        /// <param name="pnReg">装机件号</param>
        public void SetPn(PnReg pnReg)
        {
            if (pnReg == null || pnReg.IsTransient())
            {
                throw new ArgumentException("装机件号参数为空！");
            }

            Pn = pnReg.Pn;
            PnRegId = pnReg.Id;
        }

        /// <summary>
        ///     设置操作时间
        /// </summary>
        /// <param name="date">操作时间</param>
        public void SetActionDate(DateTime date)
        {
            ActionDate = date;
        }

        /// <summary>
        ///     设置CSN
        /// </summary>
        /// <param name="csn">CSN</param>
        public void SetCSN(int csn)
        {
            if (csn < 0)
            {
                throw new ArgumentException("CSN参数不能为负数！");
            }

            CSN = csn;
        }


        /// <summary>
        ///     设置TSN
        /// </summary>
        /// <param name="tsn">TSN</param>
        public void SetTSN(decimal tsn)
        {
            if (tsn < 0)
            {
                throw new ArgumentException("TSN参数不能为负数！");
            }

            TSN = tsn;
        }

        /// <summary>
        ///     设置CSN2
        /// </summary>
        /// <param name="csn">CSN</param>
        public void SetCSN2(int csn)
        {
            if (csn < 0)
            {
                throw new ArgumentException("CSN2参数不能为负数！");
            }

            CSN2 = csn;
        }


        /// <summary>
        ///     设置TSN2
        /// </summary>
        /// <param name="tsn">TSN</param>
        public void SetTSN2(decimal tsn)
        {
            if (tsn < 0)
            {
                throw new ArgumentException("TSN2参数不能为负数！");
            }

            TSN2 = tsn;
        }

        /// <summary>
        ///     设置飞机
        /// </summary>
        /// <param name="aircraft">飞机</param>
        public void SetAircraft(Aircraft aircraft)
        {
            if (aircraft == null || aircraft.IsTransient())
            {
                throw new ArgumentException("飞机参数为空！");
            }

            AircraftId = aircraft.Id;
        }

        /// <summary>
        ///     设置拆换记录
        /// </summary>
        /// <param name="remInstRecord">拆换记录</param>
        public void SetRemInstRecord(SnRemInstRecord remInstRecord)
        {
            if (remInstRecord != null)
            {
                RemInstRecordId = remInstRecord.Id;
                RemInstRecord = remInstRecord;
            }
            else
            {
                if (ActionType != ActionType.非拆换)
                {
                    throw new ArgumentException("拆换记录外键不能为空！");
                }
                RemInstRecordId = null;
                RemInstRecord = null;
            }
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