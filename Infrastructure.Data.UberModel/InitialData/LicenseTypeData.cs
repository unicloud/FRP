#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/17 11:16:21
// 文件名：LicenseTypeData
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/17 11:16:21
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.UberModel.Aggregates.LicenseTypeAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class LicenseTypeData : InitialDataBase
    {
        public LicenseTypeData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        /// <summary>
        ///     初始化飞机系列相关信息。
        /// </summary>
        /// <returns></returns>
        public override void InitialData()
        {
            var licenseType = new List<LicenseType>
                                 {
                                     LicenseTypeFactory.CreateLicenseType(1, "飞机国籍登记证", "", true),
                                     LicenseTypeFactory.CreateLicenseType(2, "初始适航证", "", true),
                                     LicenseTypeFactory.CreateLicenseType(3, "无线电", "", true),
                                     LicenseTypeFactory.CreateLicenseType(4, "机电许可证", "", true),
                                     LicenseTypeFactory.CreateLicenseType(5, "飞机所有权证", "", true),
                                     LicenseTypeFactory.CreateLicenseType(6, "抵押权证", "", true),
                                     LicenseTypeFactory.CreateLicenseType(7, "占有权证", "", true),
                                     LicenseTypeFactory.CreateLicenseType(8, "IR登记（国际利益登记）", "", true),
                                     LicenseTypeFactory.CreateLicenseType(9, "IDERA登记", "", true),
                                 };

            licenseType.ForEach(p => Context.LicenseTypes.Add(p));
        }
    }
}
