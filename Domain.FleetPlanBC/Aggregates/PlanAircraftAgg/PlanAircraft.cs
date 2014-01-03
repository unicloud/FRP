#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:09:09
// 文件名：PlanAircraft
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg
{
    /// <summary>
    ///     计划飞机聚合根
    /// </summary>
    public class PlanAircraft : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal PlanAircraft()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     是否锁定，确定计划时锁定相关飞机。一旦锁定，对应的计划明细不能修改机型。
        /// </summary>
        public bool IsLock { get; private set; }

        /// <summary>
        ///     是否自有，用以区分PlanAircraft，民航局均为False。
        /// </summary>
        public bool IsOwn { get; private set; }

        /// <summary>
        ///     管理状态
        /// </summary>
        public ManageStatus Status { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     实际飞机外键
        /// </summary>
        public Guid? AircraftId { get; private set; }

        /// <summary>
        ///     机型外键
        /// </summary>
        public Guid AircraftTypeId { get; private set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     实际飞机
        /// </summary>
        public virtual Aircraft Aircraft { get; private set; }

        /// <summary>
        ///     机型
        /// </summary>
        public virtual AircraftType AircraftType { get; private set; }

        /// <summary>
        ///     航空公司
        /// </summary>
        public virtual Airlines Airlines { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置管理状态
        /// </summary>
        /// <param name="status">管理状态</param>
        public void SetManageStatus(ManageStatus status)
        {
            switch (status)
            {
                case ManageStatus.草稿:
                    Status = ManageStatus.草稿;
                    break;
                case ManageStatus.计划:
                    Status = ManageStatus.计划;
                    break;
                case ManageStatus.申请:
                    Status = ManageStatus.申请;
                    break;
                case ManageStatus.批文:
                    Status = ManageStatus.批文;
                    break;
                case ManageStatus.签约:
                    Status = ManageStatus.签约;
                    break;
                case ManageStatus.技术接收:
                    Status = ManageStatus.技术接收;
                    break;
                case ManageStatus.接收:
                    Status = ManageStatus.接收;
                    break;
                case ManageStatus.运营:
                    Status = ManageStatus.运营;
                    break;
                case ManageStatus.停场待退:
                    Status = ManageStatus.停场待退;
                    break;
                case ManageStatus.技术交付:
                    Status = ManageStatus.技术交付;
                    break;
                case ManageStatus.退役:
                    Status = ManageStatus.退役;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }

        /// <summary>
        ///     锁定
        /// </summary>
        public void SetLock()
        {
            IsLock = true;
        }

        /// <summary>
        ///     设置为自有
        /// </summary>
        public void SetOwn()
        {
            IsOwn = true;
        }

        /// <summary>
        ///     设置实际飞机
        /// </summary>
        /// <param name="aircraft">实际飞机</param>
        public void SetAircraft(Aircraft aircraft)
        {
            Aircraft = aircraft;
            AircraftId = aircraft.Id;
        }

        /// <summary>
        ///     设置机型
        /// </summary>
        /// <param name="aircraftType">机型</param>
        public void SetAircraftType(AircraftType aircraftType)
        {
            if (aircraftType == null || aircraftType.IsTransient())
            {
                throw new ArgumentException("机型参数为空！");
            }

            AircraftType = aircraftType;
            AircraftTypeId = aircraftType.Id;
        }

        /// <summary>
        ///     设置航空公司
        /// </summary>
        /// <param name="airlines">航空公司</param>
        public void SetAirlines(Airlines airlines)
        {
            if (airlines == null || airlines.IsTransient())
            {
                throw new ArgumentException("航空公司参数为空！");
            }

            Airlines = airlines;
            AirlinesId = airlines.Id;
        }

        #endregion
    }
}