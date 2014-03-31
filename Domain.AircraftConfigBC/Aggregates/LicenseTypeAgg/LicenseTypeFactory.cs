#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 9:55:11
// 文件名：LicenseTypeFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 9:55:11
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.LicenseTypeAgg
{
    /// <summary>
    ///   证照类型工厂
    /// </summary>
    public static class LicenseTypeFactory
    {
        /// <summary>
        /// 新建证照类型
        /// </summary>
        /// <returns></returns>
        public static LicenseType CreateLicenseType()
        {
            var licenseType = new LicenseType();
            licenseType.GenerateNewIdentity();
            return licenseType;
        }

        /// <summary>
        /// 设置证照类型属性
        /// </summary>
        /// <param name="licenseType"> 当前证照类型</param>
        /// <param name="name">名字</param>
        /// <param name="hasFile">是否有附件</param>
        /// <param name="description">描述</param>
        public static void SetLicenseType(LicenseType licenseType, string name, bool hasFile, string description)
        {
            licenseType.Type = name;
            licenseType.HasFile = hasFile;
            licenseType.Description = description;
        }
    }
}
