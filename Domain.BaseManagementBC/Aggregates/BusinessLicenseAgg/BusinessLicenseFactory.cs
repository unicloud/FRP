#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 9:35:52
// 文件名：BusinessLicenseFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 9:35:52
// 修改说明：
// ========================================================================*/
#endregion

using System;
using UniCloud.Domain.Common.Enums;

namespace UniCloud.Domain.BaseManagementBC.Aggregates.BusinessLicenseAgg
{
    /// <summary>
    ///     经营证照工厂
    /// </summary>
    public static class BusinessLicenseFactory
    {
        /// <summary>
        /// 新建经营证照
        /// </summary>
        /// <returns></returns>
        public static BusinessLicense CreateBusinessLicense()
        {
            var businessLicense = new BusinessLicense();
            businessLicense.GenerateNewIdentity();
            return businessLicense;
        }

        /// <summary>
        /// 设置经营证照属性
        /// </summary>
        /// <param name="businessLicense">当前经营证照</param>
        /// <param name="name">名字</param>
        /// <param name="description">描述</param>
        /// <param name="issuedUnit">发证单位</param>
        /// <param name="issuedDate">发证日期</param>
        /// <param name="validMonths">有效期</param>
        /// <param name="expireDate">到证日期</param>
        /// <param name="state">状态</param>
        /// <param name="fileName">扫描件名字</param>
        /// <param name="fileContent">证照扫描件</param>
        public static void SetBusinessLicense(BusinessLicense businessLicense, string name, string description, string issuedUnit,
            DateTime issuedDate, int validMonths, DateTime expireDate, int state, string fileName, byte[] fileContent)
        {
            businessLicense.Name = name;
            businessLicense.Description = description;
            businessLicense.IssuedUnit = issuedUnit;
            businessLicense.IssuedDate = issuedDate;
            businessLicense.ValidMonths = validMonths;
            businessLicense.ExpireDate = expireDate;
            businessLicense.State = (LicenseStatus)state;
            businessLicense.FileName = fileName;
            businessLicense.FileContent = fileContent;
        }
    }
}
