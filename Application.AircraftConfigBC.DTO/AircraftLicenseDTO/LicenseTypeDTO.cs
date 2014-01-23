#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/16 14:18:12
// 文件名：LicenseTypeDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/16 14:18:12
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.AircraftConfigBC.DTO
{
    /// <summary>
    /// 证照类型
    /// </summary>
    [DataServiceKey("LicenseTypeId")]
    public class LicenseTypeDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public int LicenseTypeId { get; set; }

        /// <summary>
        ///   类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否有附件
        /// </summary>
        public bool HasFile { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        #endregion
    }
}
