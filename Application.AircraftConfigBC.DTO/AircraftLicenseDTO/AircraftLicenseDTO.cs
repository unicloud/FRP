#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/16 14:21:05
// 文件名：AircraftLicenseDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/16 14:21:05
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.AircraftConfigBC.DTO
{
    /// <summary>
    /// 飞机证照
    /// </summary>
    [DataServiceKey("AircraftLicenseId")]
    public class AircraftLicenseDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int AircraftLicenseId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 发证单位
        /// </summary>
        public string IssuedUnit { get; set; }

        /// <summary>
        /// 发证日期
        /// </summary>
        public DateTime IssuedDate { get; set; }

        /// <summary>
        /// 有效期（月）
        /// </summary>
        public int ValidMonths { get; set; }

        /// <summary>
        /// 证照到期日
        /// </summary>
        public DateTime ExpireDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 证照扫描件
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        /// 扫描件名字
        /// </summary>
        public string FileName { get; set; }
        #endregion

        #region 外键属性
        /// <summary>
        /// 飞机
        /// </summary>
        public Guid AircraftId { get; set; }

        /// <summary>
        /// 证照类型
        /// </summary>
        public int LicenseTypeId { get; set; }
        #endregion
    }
}
