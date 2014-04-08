#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/8 11:52:36
// 文件名：BusinessLicenseDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/8 11:52:36
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.BaseManagementBC.DTO
{
    /// <summary>
    ///     BusinessLicense
    /// </summary>
    [DataServiceKey("Id")]
    public class BusinessLicenseDTO
    {
        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get;  set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get;  set; }

        /// <summary>
        /// 发证单位
        /// </summary>
        public string IssuedUnit { get;  set; }

        /// <summary>
        /// 发证日期
        /// </summary>
        public DateTime IssuedDate { get;  set; }

        /// <summary>
        /// 有效期（月）
        /// </summary>
        public int ValidMonths { get;  set; }

        /// <summary>
        /// 证照到期日
        /// </summary>
        public DateTime ExpireDate { get;  set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get;  set; }

        /// <summary>
        /// 证照扫描件
        /// </summary>
        public byte[] FileContent { get;  set; }

        /// <summary>
        /// 扫描件名字
        /// </summary>
        public string FileName { get;  set; }
        #endregion
    }
}
