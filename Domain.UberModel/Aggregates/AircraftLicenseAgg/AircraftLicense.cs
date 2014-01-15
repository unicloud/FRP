﻿#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 9:35:40
// 文件名：AircraftLicense
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 9:35:40
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.UberModel.Aggregates.AircraftAgg;
using UniCloud.Domain.UberModel.Aggregates.LicenseTypeAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AircraftLicenseAgg
{
    /// <summary>
    ///     飞机证照聚合根
    /// </summary>
    public class AircraftLicense : EntityInt
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftLicense()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// 发证单位
        /// </summary>
        public string IssuedUnit { get; internal set; }

        /// <summary>
        /// 发证日期
        /// </summary>
        public DateTime IssuedDate { get; internal set; }

        /// <summary>
        /// 有效期（月）
        /// </summary>
        public int ValidMonths { get; internal set; }

        /// <summary>
        /// 证照到期日
        /// </summary>
        public DateTime ExpireDate { get; internal set; }

        /// <summary>
        /// 状态
        /// </summary>
        public LicenseStatus State { get; internal set; }

        /// <summary>
        /// 证照扫描件
        /// </summary>
        public byte[] LicenseFile { get; internal set; }

        #endregion

        #region 外键属性
        /// <summary>
        /// 飞机
        /// </summary>
        public Guid AircraftId { get; internal set; }

        /// <summary>
        /// 证照类型
        /// </summary>
        public int LicenseTypeId { get; internal set; }
        #endregion

        #region 导航属性
        /// <summary>
        /// 飞机
        /// </summary>
        public virtual Aircraft Aircraft { get; internal set; }

        /// <summary>
        /// 证照类型
        /// </summary>
        public virtual LicenseType LicenseType { get; internal set; }
        #endregion
    }
}
