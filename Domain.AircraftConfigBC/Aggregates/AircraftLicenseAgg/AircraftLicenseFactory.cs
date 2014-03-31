#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 9:35:52
// 文件名：AircraftLicenseFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 9:35:52
// 修改说明：
// ========================================================================*/
#endregion

using System;
using UniCloud.Domain.Common.Enums;

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftLicenseAgg
{
    /// <summary>
    ///     飞机证照工厂
    /// </summary>
    public static class AircraftLicenseFactory
    {
        /// <summary>
        /// 新建飞机证照
        /// </summary>
        /// <returns></returns>
        public static AircraftLicense CreateAircraftLicense()
        {
            var aircraftLicense = new AircraftLicense();
            aircraftLicense.GenerateNewIdentity();
            return aircraftLicense;
        }

        /// <summary>
        /// 设置飞机证照属性
        /// </summary>
        /// <param name="aircraftLicense">当前飞机证照</param>
        /// <param name="name">名字</param>
        /// <param name="licenseTypeId">证照种类</param>
        /// <param name="description">描述</param>
        /// <param name="issuedUnit">发证单位</param>
        /// <param name="issuedDate">发证日期</param>
        /// <param name="validMonths">有效期</param>
        /// <param name="expireDate">到证日期</param>
        /// <param name="state">状态</param>
        /// <param name="fileName">扫描件名字</param>
        /// <param name="fileContent">证照扫描件</param>
        public static void SetAircraftLicense(AircraftLicense aircraftLicense, string name,int licenseTypeId, string description, string issuedUnit,
            DateTime issuedDate, int validMonths, DateTime expireDate, int state, string fileName, byte[] fileContent)
        {
            aircraftLicense.Name = name;
            aircraftLicense.LicenseTypeId = licenseTypeId;
            aircraftLicense.Description = description;
            aircraftLicense.IssuedUnit = issuedUnit;
            aircraftLicense.IssuedDate = issuedDate;
            aircraftLicense.ValidMonths = validMonths;
            aircraftLicense.ExpireDate = expireDate;
            aircraftLicense.State = (LicenseStatus)state;
            aircraftLicense.FileName = fileName;
            aircraftLicense.FileContent = fileContent;
        }
    }
}
