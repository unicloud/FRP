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

namespace UniCloud.Domain.UberModel.Aggregates.LicenseTypeAgg
{
    /// <summary>
    ///   证照类型工厂
    /// </summary>
    public static class LicenseTypeFactory
    {
        /// <summary>
        ///     创建证照类型
        /// </summary>
        /// <param name="id">证照类型ID</param>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        /// <param name="hasFile">是否有附件</param>
        /// <returns></returns>
        public static LicenseType CreateLicenseType(int id, string name, string description, bool hasFile)
        {
            var licenseType = new LicenseType
                                 {
                                     Description = description,
                                     HasFile = hasFile,
                                 };
            licenseType.SetType(name);
            licenseType.ChangeCurrentIdentity(id);

            return licenseType;
        }
    }
}
